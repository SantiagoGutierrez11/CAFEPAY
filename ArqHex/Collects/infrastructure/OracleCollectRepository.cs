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

        public OracleCollectRepository(string _connectionstring)
        {
            this.connectionString = _connectionstring;
        }

        public void save(Collect collect)
        {
            if (collect == null) throw new ArgumentNullException(nameof(collect));

            // ✅ SQL corregido: Ahora incluye todos los parámetros
            const string sql = @"
        INSERT INTO COLLECT (
            WORKER_CODE, 
            IDPLOT, 
            IDHARVEST, 
            IDCOLLECT, 
            COLLECTDATE, 
            KILOS, 
            AMOUNT_TO_PAY, 
            IDPAYMENT, 
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
            :p_idPayment, 
            :p_statusId, 
            :p_isCountable
        )";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                // WORKER_CODE - VARCHAR2
                cmd.Parameters.Add("p_workerCode", OracleDbType.Varchar2).Value =
                    collect.collectorWorkerCode.collectorWorkerCode;

                // IDPLOT - NUMBER
                cmd.Parameters.Add("p_idPlot", OracleDbType.Int64).Value =
                    collect.plotId.collectIdPlot;

                // IDHARVEST - NUMBER
                cmd.Parameters.Add("p_idHarvest", OracleDbType.Int64).Value =
                    collect.harvestId.collectIdHarvest;

                // IDCOLLECT - NUMBER (nullable, se autogenera)
                cmd.Parameters.Add("p_idCollect", OracleDbType.Int64).Value =
                    collect.id?.collectId ?? (object)DBNull.Value;

                // COLLECTDATE - DATE
                cmd.Parameters.Add("p_collectDate", OracleDbType.Date).Value =
                    collect.date.collectDate;

                // KILOS - NUMBER(10,2)
                cmd.Parameters.Add("p_Kilos", OracleDbType.Decimal).Value =
                    collect.kilos.collectedKilos;

                // AMOUNT_TO_PAY - NUMBER(12,2)
                cmd.Parameters.Add("p_amountToPaid", OracleDbType.Decimal).Value =
                    collect.amountToPaid.collectAmountToPaidValue;

                // ✅ IDPAYMENT - NUMBER (nullable) - ESTE FALTABA
                cmd.Parameters.Add("p_idPayment", OracleDbType.Int64).Value =
                    collect.paymentId?.collectIdPayment ?? (object)DBNull.Value;

                // STATUS_ID - NUMBER
                cmd.Parameters.Add("p_statusId", OracleDbType.Int32).Value =
                    collect.status.collectStatus;

                // IS_COUNTABLE - NUMBER(1,0)
                // Extraer el valor int del Value Object
                cmd.Parameters.Add("p_isCountable", OracleDbType.Int32).Value =
                    collect.isCountable.isCountableValue;

                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex) when (ex.Number == 1) // ORA-00001 unique constraint violated
                {
                    throw new InvalidOperationException(
                        $"Ya existe un registro ZERO para esta asociación. " +
                        $"WORKER_CODE={collect.collectorWorkerCode.collectorWorkerCode}, " +
                        $"IDPLOT={collect.plotId.collectIdPlot}, " +
                        $"IDHARVEST={collect.harvestId.collectIdHarvest}",
                        ex);
                }
                catch (OracleException ex)
                {
                    // Capturar otros errores de Oracle con más contexto
                    throw new InvalidOperationException(
                        $"Error al guardar la recolección. Oracle Error {ex.Number}: {ex.Message}",
                        ex);
                }
            }
        }

        public void update(Collect collect, long oldId)
        {
            if (collect == null) throw new ArgumentNullException(nameof(collect));
            if (string.IsNullOrWhiteSpace(oldId.ToString())) throw new ArgumentException("oldId es requerido", nameof(oldId));

            const string sql = @"
UPDATE ADMINCAFEPAY.COLLECT
   SET COLLECTOR_ID = :p_collector_id,
       COLLECT_DATE = :p_collect_date,
       COLLECTED_KILOS = :p_collected_kilos,
       HARVEST_ID = :p_harvest_id,
       PAYMENT_ID = :p_payment_id,
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
                cmd.Parameters.Add("p_payment_id", OracleDbType.Int64).Value = collect.paymentId.collectIdPayment;
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
                const string query = "SELECT COLLECT_ID, COLLECTOR_ID, COLLECT_DATE, COLLECTED_KILOS, HARVEST_ID, PAYMENT_ID, STATUS, PAID FROM ADMINCAFEPAY.COLLECT ORDER BY COLLECT_ID";

                using (var command = new OracleCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var collectId = new CollectId(reader.GetInt64(0));
                        var collectCollectorId = new CollectWorkerCode(reader.GetString(0));
                        var collectDate = new CollectDate(reader.GetDateTime(2));
                        var collectedKilos = new CollectedKilos(reader.GetDecimal(3));
                        var collectIdHarvest = new CollectIdHarvest(reader.GetInt64(4));
                        var collectIdPayment = new CollectIdPayment(reader.GetInt64(5));
                        var collectStatus = new CollectStatus(reader.GetInt32(6));
                        var collectAmountToPaidValue = new CollectorAmountToPaid(reader.GetInt64(7));
                        var collectIscountable = new CollectIsCountable(reader.GetInt32(8));
                        var collectIdPlot = new CollectIdPlot(reader.GetInt64(9));

                        var collect = new Collect(collectId, collectCollectorId, collectIdPayment,
                                                  collectIdHarvest, collectDate, collectedKilos,
                                                  collectStatus, collectAmountToPaidValue, collectIdPlot,collectIscountable );
                        collects.Add(collect);
                    }
                }
            }

            return collects;
        }
    }
}