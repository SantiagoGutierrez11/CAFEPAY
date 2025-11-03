using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;

namespace CAFEPAY.ArqHex.Harvests.Infrastructure
{
    public class OracleHarvestRepository : HarvestRepository
    {
        private readonly string connectionString;

        public OracleHarvestRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public long save(Harvest harvest)
        {
            if (harvest == null) throw new ArgumentNullException(nameof(harvest));
            // Asumimos: harvest.PlotId.Value (long), harvest.HarvestId?.Value (long? nullable)
            const string sql = @"
            INSERT INTO HARVEST (IDPLOT, STARTDATE, PRICEPERKILO)
            VALUES (:p_idPlot, :p_startDate, :p_price)
            RETURNING IDHARVEST INTO :p_new_id";

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("p_idPlot", OracleDbType.Int64).Value = harvest.idPlot.idPlotValue;
                cmd.Parameters.Add("p_startDate", OracleDbType.Date).Value = harvest.startDate.startDateValue;
                cmd.Parameters.Add("p_price", OracleDbType.Decimal).Value = harvest.pricePerKilo.pricePerKiloValue;

                // Parámetro OUT para recuperar el ID asignado por el trigger (si el trigger asignó uno)
                var outParam = new OracleParameter("p_new_id", OracleDbType.Int64)
                {
                    Direction = ParameterDirection.ReturnValue // Para RETURNING INTO se usa ReturnValue o Output - depende de versión; si falla usa Output.
                };
                // En Oracle.ManagedDataAccess mejor usar ParameterDirection.Output
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex) when (ex.Number == 1) // ORA-00001 unique constraint
                {
                    // duplicidad de pk/unique (depende de tus constraints)
                    throw new InvalidOperationException("Clave duplicada al insertar harvest.", ex);
                }
                catch (OracleException ex) when (ex.Number == 20051)
                {
                    // RAISE_APPLICATION_ERROR del trigger: una activa por lote
                    throw new HarvestActiveExistsException(harvest.idPlot.idPlotValue, ex);
                }
                catch (OracleException ex) when (ex.Number == 1 &&
                                                 ex.Message.IndexOf("UNQ_HARVEST_ONE_ACTIVE", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Violación del índice único condicional
                    throw new HarvestActiveExistsException(harvest.idPlot.idPlotValue, ex);
                }
                catch (OracleException ex) when (ex.Number == 1)
                {
                    // Otras únicas (PK, etc.)
                    throw new InvalidOperationException("Clave duplicada.", ex);
                }
                long assignedId = -1;
                // Si se devolvió algo en p_new_id, asignarlo
                if (outParam.Value != null && outParam.Value != DBNull.Value)
                {
                    assignedId = long.Parse(outParam.Value.ToString());
                    // Crear y devolver una nueva instancia o setear el ID según tu modelo
                    // Asumo que Harvest tiene método WithHarvestId o un setter interno — ajusta según tu dominio.

                }

                return assignedId;
            }
        }

        public List<Harvest> queryAll()
        {
            var harvests = new List<Harvest>();

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT IDHARVEST, IDPLOT, STARTDATE, PRICEPERKILO, STATUS_ID, ENDDATE FROM HARVEST";

                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HarvestId id = new HarvestId(reader.GetInt64(0));
                            HarvestIdPlot idPlot = new HarvestIdPlot(reader.GetInt64(1));
                            HarvestStartDate startDate = new HarvestStartDate(reader.GetDateTime(2));
                            HarvestPricePerKilo price = new HarvestPricePerKilo(reader.GetDecimal(3));
                            HarvestStatus status = new HarvestStatus(reader.GetInt32(4));
                            HarvestEndDate endDate = null;
                            if (reader.IsDBNull(5))
                            {
                                endDate = null;
                            }
                            else
                            {
                                endDate = new HarvestEndDate(reader.GetDateTime(5));
                            }

                            Harvest harvest = new Harvest(id, idPlot, startDate, price, status, endDate);
                            harvests.Add(harvest);
                        }
                    }
                }
            }

            return harvests;
        }

        public void update(Harvest harvest)
        {
            if (harvest == null) throw new ArgumentNullException(nameof(harvest));
            const string sql = @"
            UPDATE HARVEST
            SET ENDDATE  = :p_endDate,
            STATUS_ID         = :p_status
            WHERE IDPLOT = :p_idPlot
            AND IDHARVEST          = :p_id";
            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = harvest.id.idValue;
                cmd.Parameters.Add("p_idPlot", OracleDbType.Int64).Value = harvest.idPlot.idPlotValue;
                cmd.Parameters.Add("p_endDate", OracleDbType.Date).Value = harvest.endDate.endDateValue;
                cmd.Parameters.Add("p_status", OracleDbType.Int32).Value = harvest.status.statusValue;

                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex) when (ex.Number == 1) // ORA-00001 unique constraint
                {
                    // duplicidad de pk/unique (depende de tus constraints)
                    throw new InvalidOperationException("Clave duplicada al insertar harvest.", ex);
                }
            }
        }

        public long associateCollector(long idHarvest, long idCollector)
        {
            const string sql = @"
            INSERT INTO HARVEST_COLLECTOR (IDHARVEST, IDCOLLECTOR)
            VALUES (:p_idHarvest, :p_idCollector)
            RETURNING ID INTO :p_new_id";

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("p_idHarvest", OracleDbType.Int64).Value = idHarvest;
                cmd.Parameters.Add("p_idCollector", OracleDbType.Int64).Value = idCollector;

                var outParam = new OracleParameter("p_new_id", OracleDbType.Int64)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outParam);

                conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex) when (ex.Number == 1) // ORA-00001 unique constraint
                {
                    throw new InvalidOperationException("El recolector ya está asociado a esta cosecha.", ex);
                }
                catch (OracleException ex) when (ex.Number == 2291) // ORA-02291 FK violation
                {
                    throw new InvalidOperationException("La cosecha o el recolector no existen.", ex);
                }
                catch (OracleException ex) when (ex.Number == 20052) // RAISE_APPLICATION_ERROR personalizado
                {
                    throw new InvalidOperationException("No se puede asociar un recolector a una cosecha finalizada.", ex);
                }
                catch (OracleException ex) when (ex.Number == 20053) // RAISE_APPLICATION_ERROR personalizado
                {
                    throw new InvalidOperationException("La cosecha no está activa.", ex);
                }

                long assignedId = -1;
                if (outParam.Value != null && outParam.Value != DBNull.Value)
                {
                    assignedId = long.Parse(outParam.Value.ToString());
                }

                return assignedId;
            }
        }
    }
}