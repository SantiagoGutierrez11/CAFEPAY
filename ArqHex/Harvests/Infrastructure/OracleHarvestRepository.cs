using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Harvests.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
            VALUES (:p_idplot, :p_startdate, :p_price)
            RETURNING IDHARVEST INTO :p_new_id";

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value = harvest.idPlot.idPlotValue;
                cmd.Parameters.Add("p_startdate", OracleDbType.Date).Value = harvest.startDate.startDateValue;
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
<<<<<<< HEAD
=======
                catch (OracleException ex)
                {
                    string cleanMessage = CleanOracleErrorMessage(ex.Message);
                    throw new HarvestOperationException(
                        "Error al consultar las cosechas: " + cleanMessage, ex);
                }
            }

            return harvests;
        }

        public List<Harvest> queryByStatus(int status)
        {
            var harvests = new List<Harvest>();

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_HARVEST_MANAGEMENT.query_harvests_by_status", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetro de entrada
                cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value = status;

                // Parámetro de salida (cursor)
                var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outCursor);

                conn.Open();

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            harvests.Add(MapReaderToHarvest(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    if (ex.Number == 20204)
                    {
                        throw new InvalidOperationException(
                            "Estado inválido. Use 1 para cosechas en proceso o 2 para finalizadas", ex);
                    }
                    string cleanMessage = CleanOracleErrorMessage(ex.Message);
                    throw new HarvestOperationException(
                        "Error al consultar las cosechas: " + cleanMessage, ex);
                }
>>>>>>> Santiago
            }

            return harvests;
        }

        public void update(Harvest harvest)
        {
            if (harvest == null) throw new ArgumentNullException(nameof(harvest));
            const string sql = @"
            UPDATE HARVEST
            SET STARTDATE      = :p_startdate,
            ENDDATE  = :p_enddate,
            PRICEPERKILO         = :p_priceperkilo,
            WHERE IDPLOT = :p_idplot
            AND IDHARVEST          = :p_id";
            ;
            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add("p_id", OracleDbType.Int64).Value = harvest.id.idValue;
                cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value = harvest.idPlot.idPlotValue;
                cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value = harvest.endDate.endDateValue;
                cmd.Parameters.Add("p_enddate", OracleDbType.Date).Value = harvest.endDate.endDateValue;
                cmd.Parameters.Add("p_priceperkilo", OracleDbType.Decimal).Value = harvest.pricePerKilo.pricePerKiloValue;

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
<<<<<<< HEAD
    }
}
=======

        //METODOS AUXILIARES

        // Método para limpiar mensajes de error de Oracle
        private string CleanOracleErrorMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return message;

            // Remover código ORA-XXXXX: del inicio
            string cleaned = Regex.Replace(
                message,
                @"^ORA-\d+:\s*",
                "",
                RegexOptions.IgnoreCase
            );

            // Remover saltos de línea y texto adicional después del primer salto
            int newLineIndex = cleaned.IndexOf('\n');
            if (newLineIndex > 0)
            {
                cleaned = cleaned.Substring(0, newLineIndex);
            }

            return cleaned.Trim();
        }

        private Exception MapUpdateOracleException(OracleException ex, long plotId, long harvestId)
        {
            // Limpiar mensaje de Oracle
            string cleanMessage = CleanOracleErrorMessage(ex.Message);

            switch (ex.Number)
            {
                case 20203:
                    return new HarvestNotFoundException(plotId, harvestId, ex);

                case 20061:
                    return new InvalidHarvestDurationException(cleanMessage, ex);

                case 20062:
                    return new HarvestHasPendingCollectsException(plotId, harvestId, ex);

                case 20051:
                    return new HarvestActiveExistsException(plotId, ex);

                case 20213:
                    return new HarvestOperationException(
                        string.IsNullOrWhiteSpace(cleanMessage) ? "Estado inválido. Use 1 (EN_PROCESO) o 2 (FINALIZADO)." : cleanMessage,
                        ex);

                case 20214:
                    return new HarvestOperationException(
                        string.IsNullOrWhiteSpace(cleanMessage) ? "La fecha de finalización es requerida." : cleanMessage,
                        ex);

                case 20215:
                    return new HarvestOperationException(
                        string.IsNullOrWhiteSpace(cleanMessage) ? "No se pudo actualizar la cosecha. Verifique los datos." : cleanMessage,
                        ex);

                case 20216:
                    return new HarvestOperationException(
                        string.IsNullOrWhiteSpace(cleanMessage) ? "Transición de estado no permitida." : cleanMessage,
                        ex);

                case 1: // ORA-00001 unique constraint
                    return new HarvestOperationException("Ya existe un registro con estos datos.", ex);

                default:
                    return new HarvestOperationException($"Error al actualizar la cosecha: {cleanMessage}", ex);
            }
        }

        private Harvest MapReaderToHarvest(IDataReader reader)
        {
            HarvestId id = new HarvestId(reader.GetInt64(0));
            HarvestIdPlot idPlot = new HarvestIdPlot(reader.GetInt64(1));
            HarvestStartDate startDate = new HarvestStartDate(reader.GetDateTime(2));
            HarvestPricePerKilo price = new HarvestPricePerKilo(reader.GetDecimal(3));
            HarvestStatus status = new HarvestStatus(reader.GetInt32(4));

            HarvestEndDate endDate = reader.IsDBNull(5)
                ? null
                : new HarvestEndDate(reader.GetDateTime(5));

            return new Harvest(id, idPlot, startDate, price, status, endDate);
        }

        private Exception MapOracleException(OracleException ex, long plotId)
        {
            // Limpiar mensaje de Oracle
            string cleanMessage = CleanOracleErrorMessage(ex.Message);

            switch (ex.Number)
            {
                case 20051:
                    return new HarvestActiveExistsException(plotId, ex);

                case 20073:
                case 20074:
                    return new InvalidPriceRangeException(cleanMessage, ex);

                case 20200:
                    return new PlotNotFoundException(plotId, ex);

                case 20201:
                    return new InvalidOperationException(
                        string.IsNullOrWhiteSpace(cleanMessage) ? "La fecha de inicio es requerida" : cleanMessage,
                        ex);

                case 20202:
                    return new InvalidOperationException(
                        string.IsNullOrWhiteSpace(cleanMessage) ? "Ya existe una cosecha con ese ID" : cleanMessage,
                        ex);

                case 1: // ORA-00001 unique constraint
                    if (ex.Message.IndexOf("UNQ_HARVEST_ONE_ACTIVE", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return new HarvestActiveExistsException(plotId, ex);
                    }
                    return new InvalidOperationException("Ya existe un registro con estos datos", ex);

                default:
                    return new HarvestOperationException("Error al guardar la cosecha: " + cleanMessage, ex);
            }
        }
    }

    // EXCEPCIONES PERSONALIZADAS
    public class InvalidHarvestDurationException : HarvestOperationException
    {
        public InvalidHarvestDurationException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class HarvestHasPendingCollectsException : HarvestOperationException
    {
        public long PlotId { get; }
        public long HarvestId { get; }

        public HarvestHasPendingCollectsException(long plotId, long harvestId, Exception inner = null)
            : base($"La cosecha {harvestId} del lote {plotId} tiene recolecciones pendientes. " +
                   "Debe completar o eliminar todas las recolecciones antes de finalizar la cosecha.", inner)
        {
            PlotId = plotId;
            HarvestId = harvestId;
        }
    }

    public class HarvestOperationException : Exception
    {
        public HarvestOperationException(string message) : base(message) { }
        public HarvestOperationException(string message, Exception inner) : base(message, inner) { }
    }

    public class InvalidPriceRangeException : HarvestOperationException
    {
        public InvalidPriceRangeException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class PlotNotFoundException : HarvestOperationException
    {
        public long PlotId { get; }

        public PlotNotFoundException(long plotId, Exception inner = null)
            : base($"El lote con ID {plotId} no existe", inner)
        {
            PlotId = plotId;
        }
    }

    public class HarvestNotFoundException : HarvestOperationException
    {
        public long PlotId { get; }
        public long HarvestId { get; }

        public HarvestNotFoundException(long plotId, long harvestId, Exception inner = null)
            : base($"No se encontró la cosecha {harvestId} del lote {plotId}", inner)
        {
            PlotId = plotId;
            HarvestId = harvestId;
        }
    }
}
>>>>>>> Santiago
