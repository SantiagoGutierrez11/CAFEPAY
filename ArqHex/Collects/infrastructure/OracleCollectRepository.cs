using CAFEPAY.ArqHex.Collects.domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace CAFEPAY.ArqHex.Collects.infrastructure
{
    public class OracleCollectRepository : CollectRepository
    {
        private readonly string connectionString;

        public OracleCollectRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }
        // metodo para guardar una recolecta
        public void save(Collect collect)
        {
            if (collect == null)
                throw new ArgumentNullException(nameof(collect));

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECT_MANAGEMENT.save_collect", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Parámetros de entrada
                    cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2).Value =
                        collect.collectorWorkerCode.collectorWorkerCode;

                    cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value =
                        collect.plotId.collectIdPlot;

                    cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value =
                        collect.harvestId.collectIdHarvest;

                    cmd.Parameters.Add("p_idcollect", OracleDbType.Int64).Value =
                        collect.id?.collectId ?? (object)DBNull.Value;

                    cmd.Parameters.Add("p_collectdate", OracleDbType.Date).Value =
                        collect.date.collectDate;

                    cmd.Parameters.Add("p_kilos", OracleDbType.Decimal).Value =
                        collect.kilos.collectedKilos;

                    cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value =
                        collect.status.collectStatus;

                    cmd.Parameters.Add("p_is_countable", OracleDbType.Int32).Value =
                        collect.isCountable.isCountableValue;

                    cmd.Parameters.Add("p_amount_to_pay", OracleDbType.Decimal).Value =
                        collect.amountToPaid.collectAmountToPaidValue ?? (object)DBNull.Value;

                    // Parámetros de salida
                    var outIdParam = new OracleParameter("p_idcollect_out", OracleDbType.Int64)
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
                    cmd.ExecuteNonQuery();

                    // Verificar resultado
                    string result = outResultParam.Value?.ToString() ?? "";

                    if (result.StartsWith("ERROR:"))
                    {
                        string errorMessage = result.Substring(7).Trim();
                        throw new CollectOperationException(errorMessage);
                    }

                    // Si llegamos aquí, la operación fue exitosa
                    // Actualizar el ID del collect si era NULL
                    if (collect.id == null && outIdParam.Value != null &&
                        outIdParam.Value != DBNull.Value)
                    {
                        long newId = Convert.ToInt64(outIdParam.Value.ToString());
                        // Aquí podrías actualizar el ID en el objeto si tu dominio lo permite
                    }
                }
                catch (CollectOperationException)
                {
                    throw;
                }
                catch (OracleException ex)
                {
                    throw MapOracleException(ex, collect);
                }
                catch (Exception ex)
                {
                    throw new CollectOperationException(
                        $"Error inesperado al guardar la recolecta: {ex.Message}", ex);
                }
            }
        }

        // metodo para actualizar una recolecta
        public void update(Collect collect, long oldId)
        {
            if (collect == null)
                throw new ArgumentNullException(nameof(collect));

            if (oldId <= 0)
                throw new ArgumentException("El ID anterior debe ser mayor a cero", nameof(oldId));

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECT_MANAGEMENT.update_collect", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Parámetros de entrada
                    cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2).Value =
                        collect.collectorWorkerCode.collectorWorkerCode;

                    cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value =
                        collect.plotId.collectIdPlot;

                    cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value =
                        collect.harvestId.collectIdHarvest;

                    cmd.Parameters.Add("p_old_idcollect", OracleDbType.Int64).Value = oldId;

                    cmd.Parameters.Add("p_new_idcollect", OracleDbType.Int64).Value =
                        collect.id.collectId;

                    cmd.Parameters.Add("p_collectdate", OracleDbType.Date).Value =
                        collect.date.collectDate;

                    cmd.Parameters.Add("p_kilos", OracleDbType.Decimal).Value =
                        collect.kilos.collectedKilos;

                    cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value =
                        collect.status.collectStatus;

                    // Parámetro de salida
                    var outResultParam = new OracleParameter("p_result", OracleDbType.Varchar2, 500)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outResultParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Verificar resultado
                    string result = outResultParam.Value?.ToString() ?? "";

                    if (result.StartsWith("ERROR:"))
                    {
                        string errorMessage = result.Substring(7).Trim();
                        throw new CollectOperationException(errorMessage);
                    }
                }
                catch (CollectOperationException)
                {
                    throw;
                }
                catch (OracleException ex)
                {
                    throw MapUpdateOracleException(ex, oldId);
                }
                catch (Exception ex)
                {
                    throw new CollectOperationException(
                        $"Error inesperado al actualizar la recolecta: {ex.Message}", ex);
                }
            }
        }
        // metodo para consultar todas las recolectas
        public List<Collect> queryAll()
        {
            var collects = new List<Collect>();

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECT_MANAGEMENT.query_all_collects", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Parámetro de salida (cursor)
                    var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outCursor);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            collects.Add(MapReaderToCollect(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    throw new CollectOperationException(
                        "Error al consultar las recolectas: " + ex.Message, ex);
                }
            }

            return collects;
        }

        // metodo para consultar recolectas por estado
        public List<Collect> queryByStatus(int isCountable, int status, long idPlot, long idHarvest)
        {
            var collects = new List<Collect>();

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECT_MANAGEMENT.query_collects_by_status", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Parámetros de entrada
                    cmd.Parameters.Add("p_is_countable", OracleDbType.Int32).Value = isCountable;
                    cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value = status;
                    cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value = idPlot;
                    cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value = idHarvest;

                    // Parámetro de salida (cursor)
                    var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outCursor);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            collects.Add(MapReaderToCollect(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    // Mapear errores específicos
                    if (ex.Number == 20310)
                    {
                        throw new InvalidOperationException(
                            "Estado inválido. Use 0 (Asociación), 1 (Registrado) o 2 (Pagado)", ex);
                    }
                    else if (ex.Number == 20311)
                    {
                        throw new InvalidOperationException(
                            "Valor inválido. Debe ser 0 (No contable) o 1 (Contable)", ex);
                    }

                    throw new CollectOperationException(
                        "Error al consultar las recolectas: " + ex.Message, ex);
                }
            }

            return collects;
        }
        // metodo para consultar recolectas por código de trabajador
        public List<Collect> queryByWorkerCode(int isCountable, string workerCode,
            long idPlot, long? idHarvest)
        {
            var collects = new List<Collect>();

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECT_MANAGEMENT.query_collects_by_worker", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Parámetros de entrada
                    cmd.Parameters.Add("p_is_countable", OracleDbType.Int32).Value = isCountable;
                    cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2).Value = workerCode;
                    cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value = idPlot;
                    cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value = idHarvest ?? 0;

                    // Parámetro de salida (cursor)
                    var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outCursor);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            collects.Add(MapReaderToCollect(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    if (ex.Number == 20312)
                    {
                        throw new CollectorNotFoundException(workerCode, ex);
                    }
                    else if (ex.Number == 20311)
                    {
                        throw new InvalidOperationException(
                            "Valor inválido. Debe ser 0 (No contable) o 1 (Contable)", ex);
                    }

                    throw new CollectOperationException(
                        "Error al consultar las recolectas: " + ex.Message, ex);
                }
            }

            return collects;
        }
        // metodo para consultar recolectas por estado y código de trabajador
        public List<Collect> queryByStatusAndWorkerCode(int isCountable, string workerCode,
            int status, long idPlot, long? idHarvest)
        {
            var collects = new List<Collect>();

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECT_MANAGEMENT.query_collects_by_status_worker", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Parámetros de entrada
                    cmd.Parameters.Add("p_is_countable", OracleDbType.Int32).Value = isCountable;
                    cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2).Value = workerCode;
                    cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value = status;
                    cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value = idPlot;
                    cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value = idHarvest ?? 0;

                    // Parámetro de salida (cursor)
                    var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outCursor);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            collects.Add(MapReaderToCollect(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    if (ex.Number == 20310)
                    {
                        throw new InvalidOperationException(
                            "Estado inválido. Use 0 (Asociación), 1 (Registrado) o 2 (Pagado)", ex);
                    }
                    else if (ex.Number == 20311)
                    {
                        throw new InvalidOperationException(
                            "Valor inválido. Debe ser 0 (No contable) o 1 (Contable)", ex);
                    }
                    else if (ex.Number == 20312)
                    {
                        throw new CollectorNotFoundException(workerCode, ex);
                    }

                    throw new CollectOperationException(
                        "Error al consultar las recolectas: " + ex.Message, ex);
                }
            }

            return collects;
        }

        // metodo para mapear un IDataReader a un objeto Collect
        private Collect MapReaderToCollect(IDataReader reader)
        {
            var collectCollectorWorkerCode = new CollectWorkerCode(reader.GetString(0));
            var collectIdPlot = new CollectIdPlot(reader.GetInt64(1));
            var collectIdHarvest = new CollectIdHarvest(reader.GetInt64(2));

            // IDCOLLECT puede ser NULL para registros ZERO
            var collectId = reader.IsDBNull(3)
                ? null
                : new CollectId(reader.GetInt64(3));

            var collectDate = new CollectDate(reader.GetDateTime(4));
            var collectedKilos = new CollectedKilos(reader.GetDecimal(5));
            var collectAmountToPaidValue = new CollectAmountToPaid(reader.GetDecimal(6));
            var collectStatus = new CollectStatus(reader.GetInt32(7));
            var collectIsCountable = new CollectIsCountable(reader.GetInt32(8));

            return new Collect(
                collectId,
                collectCollectorWorkerCode,
                collectIdHarvest,
                collectDate,
                collectedKilos,
                collectStatus,
                collectAmountToPaidValue,
                collectIdPlot,
                collectIsCountable
            );
        }
        // metodo para mapear excepciones de Oracle a excepciones personalizadas
        private Exception MapOracleException(OracleException ex, Collect collect)
        {
            switch (ex.Number)
            {
                case 20301:
                case 20302:
                case 20303:
                    return new CollectOperationException(ex.Message.Replace("ORA-20301:", "")
                        .Replace("ORA-20302:", "").Replace("ORA-20303:", "").Trim(), ex);

                case 20304:
                    return new CollectorNotFoundException(
                        collect.collectorWorkerCode.collectorWorkerCode, ex);

                case 20305:
                    return new HarvestNotFoundException(
                        collect.plotId.collectIdPlot,
                        collect.harvestId.collectIdHarvest, ex);

                case 20052:
                    return new CollectAlreadyExistsException(
                        collect.collectorWorkerCode.collectorWorkerCode,
                        collect.plotId.collectIdPlot,
                        collect.harvestId.collectIdHarvest, ex);

                case 20063:
                    return new InactiveCollectorException(
                        collect.collectorWorkerCode.collectorWorkerCode, ex);

                case 20064:
                case 20065:
                    return new FinishedHarvestException(
                        collect.plotId.collectIdPlot,
                        collect.harvestId.collectIdHarvest, ex);

                case 20066:
                case 20067:
                    return new InvalidCollectDateException(ex.Message, ex);

                case 20068:
                    return new InvalidKilosException(ex.Message, ex);

                case 20072:
                    return new DuplicateCollectDateException(
                        collect.collectorWorkerCode.collectorWorkerCode,
                        collect.date.collectDate, ex);

                case 1: // ORA-00001 unique constraint
                    if (ex.Message.IndexOf("UNQ_COLLECT_ONE_ZERO",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return new CollectAlreadyExistsException(
                            collect.collectorWorkerCode.collectorWorkerCode,
                            collect.plotId.collectIdPlot,
                            collect.harvestId.collectIdHarvest, ex);
                    }
                    return new InvalidOperationException(
                        "Ya existe un registro con estos datos", ex);

                default:
                    return new CollectOperationException(
                        $"Error al guardar la recolecta: {ex.Message}", ex);
            }
        }
        // metodo para mapear excepciones de Oracle al actualizar una recolecta
        private Exception MapUpdateOracleException(OracleException ex, long oldId)
        {
            switch (ex.Number)
            {
                case 20306:
                    return new CollectNotFoundException(oldId, ex);

                case 20307:
                    return new CollectAlreadyPaidException(oldId, ex);

                case 20308:
                    return new InvalidOperationException(
                        "Ya existe una recolecta con ese ID", ex);

                case 20309:
                    return new CollectOperationException(
                        "No se pudo actualizar la recolecta", ex);

                case 20068:
                    return new InvalidKilosException(
                        "La cantidad de kilos debe ser mayor a cero", ex);

                case 1: // ORA-00001 unique constraint
                    return new InvalidOperationException(
                        "Ya existe un registro con estos datos", ex);

                default:
                    return new CollectOperationException(
                        $"Error al actualizar la recolecta: {ex.Message}", ex);
            }
        }
    }
    // Excepciones personalizadas para operaciones de recolección

    public class CollectOperationException : Exception
    {
        public CollectOperationException(string message) : base(message) { }
        public CollectOperationException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class CollectorNotFoundException : CollectOperationException
    {
        public string WorkerCode { get; }

        public CollectorNotFoundException(string workerCode, Exception inner = null)
            : base($"El recolector '{workerCode}' no existe en el sistema", inner)
        {
            WorkerCode = workerCode;
        }
    }

    public class HarvestNotFoundException : CollectOperationException
    {
        public long PlotId { get; }
        public long? HarvestId { get; }

        public HarvestNotFoundException(long plotId, long? harvestId, Exception inner = null)
            : base($"La cosecha {harvestId} del lote {plotId} no existe", inner)
        {
            PlotId = plotId;
            HarvestId = harvestId;
        }
    }

    public class CollectAlreadyExistsException : CollectOperationException
    {
        public string WorkerCode { get; }
        public long PlotId { get; }
        public long? HarvestId { get; }

        public CollectAlreadyExistsException(string workerCode, long plotId,
            long? harvestId, Exception inner = null)
            : base($"El recolector '{workerCode}' ya está asociado a la cosecha " +
                   $"{harvestId} del lote {plotId}", inner)
        {
            WorkerCode = workerCode;
            PlotId = plotId;
            HarvestId = harvestId;
        }
    }

    public class InactiveCollectorException : CollectOperationException
    {
        public string WorkerCode { get; }

        public InactiveCollectorException(string workerCode, Exception inner = null)
            : base($"El recolector '{workerCode}' está inactivo y no puede " +
                   "ser asignado a cosechas activas", inner)
        {
            WorkerCode = workerCode;
        }
    }

    public class FinishedHarvestException : CollectOperationException
    {
        public long PlotId { get; }
        public long? HarvestId { get; }

        public FinishedHarvestException(long plotId, long? harvestId, Exception inner = null)
            : base($"La cosecha {harvestId} del lote {plotId} ya está finalizada. " +
                   "No se pueden registrar más recolecciones", inner)
        {
            PlotId = plotId;
            HarvestId = harvestId;
        }
    }

    public class InvalidCollectDateException : CollectOperationException
    {
        public InvalidCollectDateException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class InvalidKilosException : CollectOperationException
    {
        public InvalidKilosException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class DuplicateCollectDateException : CollectOperationException
    {
        public string WorkerCode { get; }
        public DateTime CollectDate { get; }

        public DuplicateCollectDateException(string workerCode, DateTime collectDate,
            Exception inner = null)
            : base($"El recolector '{workerCode}' ya tiene una recolección registrada " +
                   $"para la fecha {collectDate:dd/MM/yyyy}", inner)
        {
            WorkerCode = workerCode;
            CollectDate = collectDate;
        }
    }

    public class CollectNotFoundException : CollectOperationException
    {
        public long CollectId { get; }

        public CollectNotFoundException(long collectId, Exception inner = null)
            : base($"La recolecta con ID {collectId} no existe", inner)
        {
            CollectId = collectId;
        }
    }

    public class CollectAlreadyPaidException : CollectOperationException
    {
        public long CollectId { get; }

        public CollectAlreadyPaidException(long collectId, Exception inner = null)
            : base($"La recolecta con ID {collectId} ya fue pagada y no puede modificarse",
                inner)
        {
            CollectId = collectId;
        }
    }
}