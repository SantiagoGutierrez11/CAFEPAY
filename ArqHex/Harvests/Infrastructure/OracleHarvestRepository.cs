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
            if (harvest == null)
                throw new ArgumentNullException(nameof(harvest));

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_HARVEST_MANAGEMENT.save_harvest", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetros de entrada
                cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value =
                    harvest.id?.idValue ?? (object)DBNull.Value;
                cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value =
                    harvest.idPlot.idPlotValue;
                cmd.Parameters.Add("p_startdate", OracleDbType.Date).Value =
                    harvest.startDate.startDateValue;
                cmd.Parameters.Add("p_priceperkilo", OracleDbType.Decimal).Value =
                    harvest.pricePerKilo.pricePerKiloValue;

                // Parámetros de salida
                var outIdParam = new OracleParameter("p_idharvest_out", OracleDbType.Int64)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outIdParam);

                var outResultParam = new OracleParameter("p_result", OracleDbType.Varchar2, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outResultParam);

                conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();

                    // Verificar el resultado
                    string result = outResultParam.Value?.ToString() ?? "";

                    if (result.StartsWith("ERROR:"))
                    {
                        // Extraer el mensaje de error
                        string errorMessage = result.Substring(7).Trim();
                        throw new HarvestOperationException(errorMessage);
                    }

                    // Obtener el ID asignado
                    if (outIdParam.Value != null && outIdParam.Value != DBNull.Value)
                    {
                        return Convert.ToInt64(outIdParam.Value.ToString());
                    }

                    throw new HarvestOperationException("No se pudo obtener el ID de la cosecha creada");
                }
                catch (OracleException ex)
                {
                    // Mapear códigos de error de Oracle a excepciones específicas
                    throw MapOracleException(ex, harvest.idPlot.idPlotValue);
                }
            }
        }

        public List<Harvest> queryAll()
        {
            var harvests = new List<Harvest>();

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_HARVEST_MANAGEMENT.query_all_harvests", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

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
                    throw new HarvestOperationException(
                        "Error al consultar las cosechas: " + ex.Message, ex);
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
                    throw new HarvestOperationException(
                        "Error al consultar las cosechas: " + ex.Message, ex);
                }
            }

            return harvests;
        }
        public void update(Harvest harvest)
        {
            if (harvest == null)
            {
                throw new ArgumentNullException(nameof(harvest), "El objeto harvest no puede ser nulo");
            }

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_HARVEST_MANAGEMENT.update_harvest", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Validar datos requeridos
                    if (harvest.id?.idValue == null || harvest.id.idValue <= 0)
                    {
                        throw new ArgumentException("El ID de la cosecha es requerido");
                    }

                    if (harvest.idPlot?.idPlotValue == null || harvest.idPlot.idPlotValue <= 0)
                    {
                        throw new ArgumentException("El ID del lote es requerido");
                    }

                    if (harvest.status?.statusValue == null)
                    {
                        throw new ArgumentException("El estado es requerido");
                    }

                    // Parámetros de entrada
                    cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value =
                        harvest.idPlot.idPlotValue;

                    cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value =
                        harvest.id.idValue;

                    // Si está finalizando, debe tener fecha de fin
                    if (harvest.status.statusValue == 2) // FINALIZADO
                    {
                        if (harvest.endDate?.endDateValue == null)
                        {
                            throw new ArgumentException("La fecha de finalización es requerida para finalizar la cosecha");
                        }
                        cmd.Parameters.Add("p_enddate", OracleDbType.Date).Value =
                            harvest.endDate.endDateValue;
                    }
                    else
                    {
                        cmd.Parameters.Add("p_enddate", OracleDbType.Date).Value = DBNull.Value;
                    }

                    cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value =
                        harvest.status.statusValue;

                    // Parámetro de salida
                    var outResultParam = new OracleParameter("p_result", OracleDbType.Varchar2, 500)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outResultParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Verificar resultado del procedimiento
                    string result = outResultParam.Value?.ToString() ?? "";

                    if (result.StartsWith("ERROR:"))
                    {
                        string errorMessage = result.Substring(7).Trim();
                        throw new HarvestOperationException(errorMessage);
                    }

                    // Resultado exitoso (puede ser SUCCESS o INFO)
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (HarvestOperationException)
                {
                    throw;
                }
                catch (OracleException ex)
                {
                    throw MapUpdateOracleException(ex, harvest.idPlot?.idPlotValue ?? 0, harvest.id?.idValue ?? 0);
                }
                catch (Exception ex)
                {
                    throw new HarvestOperationException($"Error inesperado al actualizar la cosecha: {ex.Message}", ex);
                }
            }
        }
        //METODOS AUXILIARES
        private Exception MapUpdateOracleException(OracleException ex, long plotId, long harvestId)
        {
            switch (ex.Number)
            {
                case 20203:
                    return new HarvestNotFoundException(plotId, harvestId, ex);

                case 20061:
                    return new InvalidHarvestDurationException(ex.Message, ex);

                case 20062:
                    return new HarvestHasPendingCollectsException(plotId, harvestId, ex);

                case 20051:
                    return new HarvestActiveExistsException(plotId, ex);

                case 20213:
                    return new HarvestOperationException("Estado inválido. Use 1 (EN_PROCESO) o 2 (FINALIZADO).", ex);

                case 20214:
                    return new HarvestOperationException("La fecha de finalización es requerida.", ex);

                case 20215:
                    return new HarvestOperationException("No se pudo actualizar la cosecha. Verifique los datos.", ex);

                case 20216:
                    return new HarvestOperationException("Transición de estado no permitida.", ex);

                case 1: // ORA-00001 unique constraint
                    return new HarvestOperationException("Ya existe un registro con estos datos.", ex);

                default:
                    return new HarvestOperationException($"Error al actualizar la cosecha: {ex.Message}", ex);
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
            switch (ex.Number)
            {
                case 20051:
                    return new HarvestActiveExistsException(plotId, ex);

                case 20073:
                case 20074:
                    return new InvalidPriceRangeException(ex.Message, ex);

                case 20200:
                    return new PlotNotFoundException(plotId, ex);

                case 20201:
                    return new InvalidOperationException("La fecha de inicio es requerida", ex);

                case 20202:
                    return new InvalidOperationException("Ya existe una cosecha con ese ID", ex);

                case 1: // ORA-00001 unique constraint
                    if (ex.Message.IndexOf("UNQ_HARVEST_ONE_ACTIVE", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return new HarvestActiveExistsException(plotId, ex);
                    }
                    return new InvalidOperationException("Ya existe un registro con estos datos", ex);

                default:
                    return new HarvestOperationException(
                        "Error al guardar la cosecha: " + ex.Message, ex);
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

    public class HarvestActiveExistsException : HarvestOperationException
    {
        public long PlotId { get; }

        public HarvestActiveExistsException(long plotId, Exception inner = null)
            : base($"Ya existe una cosecha activa para el lote {plotId}. " +
                   "Debe finalizar la cosecha actual antes de crear una nueva.", inner)
        {
            PlotId = plotId;
        }
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
