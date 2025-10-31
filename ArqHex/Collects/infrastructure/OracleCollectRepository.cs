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

            const string sql = @"
        INSERT INTO ADMINCAFEPAY.COLLECT (COLLECT_ID, COLLECTOR_ID, COLLECT_DATE, COLLECTED_KILOS, HARVEST_ID, PAYMENT_ID, STATUS, PAID)
        VALUES (:p_collect_id, :p_collector_id, :p_collect_date, :p_collected_kilos, :p_harvest_id, :p_payment_id, :p_status_id, :p_paid)";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("p_collect_id", OracleDbType.Int64).Value = collect.id.collectId;
                cmd.Parameters.Add("p_collector_id", OracleDbType.Int64).Value = collect.collectorId.collectIdCollector;
                cmd.Parameters.Add("p_collect_date", OracleDbType.Date).Value = collect.date.collectDate;
                cmd.Parameters.Add("p_collected_kilos", OracleDbType.Decimal).Value = collect.kilos.collectedKilos;
                cmd.Parameters.Add("p_harvest_id", OracleDbType.Int64).Value = collect.tHarvestId.collectIdHarvest;
                cmd.Parameters.Add("p_payment_id", OracleDbType.Int64).Value = collect.paymentId.collectIdPayment;
                cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value = collect.status.collectStatus;
                cmd.Parameters.Add("p_paid", OracleDbType.Int64).Value = collect.paid.collectPaid;

                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex) when (ex.Number == 1) // ORA-00001 unique constraint violated
                {
                    throw new InvalidOperationException("Ya existe una recolección con ese ID.", ex);
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
       PAID = :p_paid,
       COLLECT_ID = :p_new_collect_id
 WHERE COLLECT_ID = :p_old_collect_id";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("p_collector_id", OracleDbType.Int64).Value = collect.collectorId.collectIdCollector;
                cmd.Parameters.Add("p_collect_date", OracleDbType.Date).Value = collect.date.collectDate;
                cmd.Parameters.Add("p_collected_kilos", OracleDbType.Decimal).Value = collect.kilos.collectedKilos;
                cmd.Parameters.Add("p_harvest_id", OracleDbType.Int64).Value = collect.tHarvestId.collectIdHarvest;
                cmd.Parameters.Add("p_payment_id", OracleDbType.Int64).Value = collect.paymentId.collectIdPayment;
                cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value = collect.status.collectStatus;
                cmd.Parameters.Add("p_paid", OracleDbType.Int64).Value = collect.paid.collectPaid;
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
                        var collectCollectorId = new CollectIdCollector(reader.GetInt64(1));
                        var collectDate = new CollectDate(reader.GetDateTime(2));
                        var collectedKilos = new CollectedKilos(reader.GetDecimal(3));
                        var collectIdHarvest = new CollectIdHarvest(reader.GetInt64(4));
                        var collectIdPayment = new CollectIdPayment(reader.GetInt64(5));
                        var collectStatus = new CollectStatus(reader.GetInt32(6));
                        var collectPaid = new CollectPaid(reader.GetInt64(7));

                        var collect = new Collect(collectId, collectCollectorId, collectIdPayment,
                                                  collectIdHarvest, collectDate, collectedKilos,
                                                  collectStatus, collectPaid);
                        collects.Add(collect);
                    }
                }
            }

            return collects;
        }
    }
}