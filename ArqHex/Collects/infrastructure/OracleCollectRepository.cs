using CAFEPAY.ArqHex.Collects.domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.infrastructure
{
    public class OracleCollectRepository : CollectRepository
    {
        private readonly string connectionString;

        public OracleCollectRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public void save(Collect collect)
        {
            if (collect == null) throw new ArgumentNullException(nameof(collect));

            const string sql = @"
        INSERT INTO COLLECT (
            WORKER_CODE, 
            IDPLOT, 
            IDHARVEST, 
            IDCOLLECT, 
            COLLECTDATE, 
            KILOS, 
            AMOUNT_TO_PAY, 
            STATUS_ID, 
            IS_COUNTABLE
        )
        VALUES (
            :p_workerCode, 
            :p_idPlot, 
            :p_idHarvest, 
            :p_idCollect, 
            :p_collectDate, 
            :p_Kilos, 
            :p_amountToPaid, 
            :p_statusId, 
            :p_isCountable
        )";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("p_workerCode", OracleDbType.Varchar2).Value =
                    collect.collectorWorkerCode.collectorWorkerCode;
                cmd.Parameters.Add("p_idPlot", OracleDbType.Int64).Value =
                    collect.plotId.collectIdPlot;
                cmd.Parameters.Add("p_idHarvest", OracleDbType.Int64).Value =
                    collect.harvestId.collectIdHarvest;
                cmd.Parameters.Add("p_idCollect", OracleDbType.Int64).Value =
                    collect.id?.collectId ?? (object)DBNull.Value;
                cmd.Parameters.Add("p_collectDate", OracleDbType.Date).Value =
                    collect.date.collectDate;
                cmd.Parameters.Add("p_Kilos", OracleDbType.Decimal).Value =
                    collect.kilos.collectedKilos;
                cmd.Parameters.Add("p_amountToPaid", OracleDbType.Decimal).Value =
                    collect.amountToPaid.collectAmountToPaidValue ?? (object)DBNull.Value;
                cmd.Parameters.Add("p_statusId", OracleDbType.Int32).Value =
                    collect.status.collectStatus;
                cmd.Parameters.Add("p_isCountable", OracleDbType.Int32).Value =
                    collect.isCountable.isCountableValue;

                connection.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    // Mapear errores específicos de Oracle
                    switch (ex.Number)
                    {
                        case 1: // ORA-00001: unique constraint violated
                            throw new InvalidOperationException(
                                $"Ya existe un registro ZERO para esta asociación. " +
                                $"WORKER_CODE={collect.collectorWorkerCode.collectorWorkerCode}, " +
                                $"IDPLOT={collect.plotId.collectIdPlot}, " +
                                $"IDHARVEST={collect.harvestId.collectIdHarvest}",
                                ex);

                        case 2290: // ORA-02290: check constraint violated
                                   // Determinar qué constraint falló
                            if (ex.Message.Contains("COLLECT_CHK_ZERO_KILOS"))
                                throw new InvalidOperationException(
                                    "Error: Los registros ZERO deben tener KILOS = 0", ex);
                            else if (ex.Message.Contains("COLLECT_CHK_ZERO_AMOUNT"))
                                throw new InvalidOperationException(
                                    "Error: Los registros ZERO deben tener AMOUNT_TO_PAY = 0", ex);
                            else if (ex.Message.Contains("COLLECT_CHK_ZERO_COUNTABLE"))
                                throw new InvalidOperationException(
                                    "Error: Los registros ZERO deben tener IS_COUNTABLE = 0", ex);
                            else if (ex.Message.Contains("COLLECT_CHK_KILOS"))
                                throw new InvalidOperationException(
                                    "Error: Los kilos no pueden ser negativos", ex);
                            else if (ex.Message.Contains("COLLECT_CHK_AMOUNT"))
                                throw new InvalidOperationException(
                                    "Error: El monto no puede ser negativo", ex);
                            else if (ex.Message.Contains("COLLECT_CHK_STATUS"))
                                throw new InvalidOperationException(
                                    "Error: El estado debe ser 0 (ZERO), 1 (REGISTRADO) o 2 (PAGADO)", ex);
                            else
                                throw new InvalidOperationException(
                                    $"Error de validación: {ex.Message}", ex);

                        case 2291: // ORA-02291: integrity constraint (parent key not found)
                            if (ex.Message.Contains("COLLECT_FK_COLLECTOR"))
                                throw new InvalidOperationException(
                                    $"El recolector '{collect.collectorWorkerCode.collectorWorkerCode}' no existe", ex);
                            else if (ex.Message.Contains("COLLECT_FK_HARVEST"))
                                throw new InvalidOperationException(
                                    $"La cosecha (IDPLOT={collect.plotId.collectIdPlot}, " +
                                    $"IDHARVEST={collect.harvestId.collectIdHarvest}) no existe", ex);
                            else
                                throw new InvalidOperationException(
                                    "Error: No se encontró un registro relacionado requerido", ex);

                        case 20052: // RAISE_APPLICATION_ERROR personalizado (validación ZERO)
                            throw new InvalidOperationException(
                                $"Ya existe un registro ZERO para esta asociación. " +
                                $"WORKER_CODE={collect.collectorWorkerCode.collectorWorkerCode}, " +
                                $"IDPLOT={collect.plotId.collectIdPlot}, " +
                                $"IDHARVEST={collect.harvestId.collectIdHarvest}",
                                ex);

                        case 20053: // Cosecha no encontrada
                            throw new InvalidOperationException(
                                $"No se encontró información de la cosecha " +
                                $"(IDPLOT={collect.plotId.collectIdPlot}, " +
                                $"IDHARVEST={collect.harvestId.collectIdHarvest})",
                                ex);

                        case 20054: // No se puede marcar como PAGADO sin detalle
                            throw new InvalidOperationException(
                                "No se puede marcar como PAGADO sin detalle de pago asociado",
                                ex);

                        case 20055: // No se puede cambiar a ZERO con pago asociado
                            throw new InvalidOperationException(
                                "No se puede cambiar a ZERO una recolecta con pago asociado",
                                ex);

                        case 20056: // Recolecta no encontrada en PaymentsDetails
                            throw new InvalidOperationException(
                                $"No se encontró la recolecta especificada",
                                ex);
                        case 20072: // Recolecta ya registrada
                            throw new InvalidOperationException(
                                $"El recolector ya ha registrado una recolecta para esta cosecha", ex);

                        default:
                            throw new InvalidOperationException(
                                $"Error al guardar la recolección. Oracle Error {ex.Number}: {ex.Message}",
                                ex);
                    }
                }
            }
        }
        public void update(Collect collect, long oldId)
        {
            if (collect == null) throw new ArgumentNullException(nameof(collect));
            if (string.IsNullOrWhiteSpace(oldId.ToString())) throw new ArgumentException("oldId es requerido", nameof(oldId));

            const string sql = @"
UPDATE COLLECT
   SET COLLECTOR_ID = :p_collector_id,
       COLLECT_DATE = :p_collect_date,
       COLLECTED_KILOS = :p_collected_kilos,
       HARVEST_ID = :p_harvest_id,
       STATUS = :p_status_id,
       PAID = :p_amountToPaid,
       COLLECT_ID = :p_new_collect_id
 WHERE COLLECT_ID = :p_old_collect_id";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("p_collector_id", OracleDbType.Int64).Value = collect.collectorWorkerCode.collectorWorkerCode;
                cmd.Parameters.Add("p_collect_date", OracleDbType.Date).Value = collect.date.collectDate;
                cmd.Parameters.Add("p_collected_kilos", OracleDbType.Decimal).Value = collect.kilos.collectedKilos;
                cmd.Parameters.Add("p_harvest_id", OracleDbType.Int64).Value = collect.harvestId.collectIdHarvest;
                cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value = collect.status.collectStatus;
                cmd.Parameters.Add("p_amountToPaid", OracleDbType.Int64).Value = collect.amountToPaid.collectAmountToPaidValue;
                cmd.Parameters.Add("p_new_collect_id", OracleDbType.Int64).Value = collect.id.collectId;
                cmd.Parameters.Add("p_old_collect_id", OracleDbType.Int64).Value = oldId;

                connection.Open();
                try
                {
                    var rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new KeyNotFoundException("No existe una recolección con ese ID.");
                    }
                }
                catch (OracleException ex) when (ex.Number == 1) // ORA-00001 unique constraint violated
                {
                    throw new InvalidOperationException("Ya existe una recolección con ese ID.", ex);
                }
            }
        }

        public List<Collect> queryAll()
        {
            var collects = new List<Collect>();

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                const string query = "SELECT COLLECT_ID, WORKER_CODE, COLLECTDATE, KILOS, IDHARVEST, STATUS_ID, AMOUNT_TO_PAY, IS_COUNTABLE, IDPLOT FROM COLLECT ";

                using (var command = new OracleCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var collectId = new CollectId(reader.GetInt64(0));
                        var collectWorkerCode = new CollectWorkerCode(reader.GetString(0));
                        var collectDate = new CollectDate(reader.GetDateTime(2));
                        var collectedKilos = new CollectedKilos(reader.GetDecimal(3));
                        var collectIdHarvest = new CollectIdHarvest(reader.GetInt64(4));
                        var collectStatus = new CollectStatus(reader.GetInt32(6));
                        var collectAmountToPaidValue = new CollectAmountToPaid(reader.GetDecimal(7));
                        var collectIscountable = new CollectIsCountable(reader.GetInt32(8));
                        var collectIdPlot = new CollectIdPlot(reader.GetInt64(9));

                        var collect = new Collect(collectId, collectWorkerCode,
                                                  collectIdHarvest, collectDate, collectedKilos,
                                                  collectStatus, collectAmountToPaidValue, collectIdPlot, collectIscountable);
                        collects.Add(collect);
                    }
                }
            }

            return collects;
        }
        public List<Collect> queryByStatus(int isCountable, int status, long idPlot, long idHarvest)
        {
            var collects = new List<Collect>();

            const string sql = @"
        SELECT 
            WORKER_CODE, 
            IDPLOT, 
            IDHARVEST, 
            IDCOLLECT, 
            COLLECTDATE, 
            KILOS, 
            AMOUNT_TO_PAY, 
            STATUS_ID, 
            IS_COUNTABLE
        FROM COLLECT
        WHERE IS_COUNTABLE = :p_isCountable
          AND STATUS_ID = :p_status
          AND IDPLOT = :p_idPlot
          AND IDHARVEST = :p_idHarvest";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("p_isCountable", OracleDbType.Int32).Value = isCountable;
                cmd.Parameters.Add("p_status", OracleDbType.Int32).Value = status;
                cmd.Parameters.Add("p_idPlot", OracleDbType.Int64).Value = idPlot;
                cmd.Parameters.Add("p_idHarvest", OracleDbType.Int64).Value = idHarvest;

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
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

                        var collect = new Collect(
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

                        collects.Add(collect);
                    }
                }
            }

            return collects;
        }

        public List<Collect> queryByWorkerCode(int isCountable, string workerCode, long idPlot, long? idHarvest)
        {
            var collects = new List<Collect>();

            const string sql = @"
        SELECT 
            WORKER_CODE, 
            IDPLOT, 
            IDHARVEST, 
            IDCOLLECT, 
            COLLECTDATE, 
            KILOS, 
            AMOUNT_TO_PAY, 
            STATUS_ID, 
            IS_COUNTABLE
        FROM COLLECT
        WHERE IS_COUNTABLE = :p_isCountable
          AND WORKER_CODE = :p_workerCode
          AND IDPLOT = :p_idPlot
          AND IDHARVEST = :p_idHarvest
        ORDER BY COLLECTDATE";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("p_isCountable", OracleDbType.Int32).Value = isCountable;
                cmd.Parameters.Add("p_workerCode", OracleDbType.Varchar2).Value = workerCode;
                cmd.Parameters.Add("p_idPlot", OracleDbType.Int64).Value = idPlot;
                cmd.Parameters.Add("p_idHarvest", OracleDbType.Int64).Value = idHarvest;

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var collectCollectorWorkerCode = new CollectWorkerCode(reader.GetString(0));
                        var collectIdPlot = new CollectIdPlot(reader.GetInt64(1));
                        var collectIdHarvest = new CollectIdHarvest(reader.GetInt64(2));

                        var collectId = reader.IsDBNull(3)
                            ? null
                            : new CollectId(reader.GetInt64(3));

                        var collectDate = new CollectDate(reader.GetDateTime(4));
                        var collectedKilos = new CollectedKilos(reader.GetDecimal(5));
                        var collectAmountToPaidValue = new CollectAmountToPaid(reader.GetDecimal(6));
                        var collectStatus = new CollectStatus(reader.GetInt32(7));
                        var collectIsCountable = new CollectIsCountable(reader.GetInt32(8));

                        var collect = new Collect(
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

                        collects.Add(collect);
                    }
                }
            }

            return collects;
        }

        public List<Collect> queryByStatusAndWorkerCode(int isCountable, string workerCode, int status, long idPlot, long? idHarvest)
        {
            var collects = new List<Collect>();

            const string sql = @"
        SELECT 
            WORKER_CODE, 
            IDPLOT, 
            IDHARVEST, 
            IDCOLLECT, 
            COLLECTDATE, 
            KILOS, 
            AMOUNT_TO_PAY, 
            STATUS_ID, 
            IS_COUNTABLE
        FROM COLLECT
        WHERE IS_COUNTABLE = :p_isCountable
          AND WORKER_CODE = :p_workerCode
          AND STATUS_ID = :p_status
          AND IDPLOT = :p_idPlot
          AND IDHARVEST = :p_idHarvest
        ORDER BY COLLECTDATE";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("p_isCountable", OracleDbType.Int32).Value = isCountable;
                cmd.Parameters.Add("p_workerCode", OracleDbType.Varchar2).Value = workerCode;
                cmd.Parameters.Add("p_status", OracleDbType.Int32).Value = status;
                cmd.Parameters.Add("p_idPlot", OracleDbType.Int64).Value = idPlot;
                cmd.Parameters.Add("p_idHarvest", OracleDbType.Int64).Value = idHarvest;

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
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

                        var collect = new Collect(
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

                        collects.Add(collect);
                    }
                }
            }

            return collects;
        }
    }
}