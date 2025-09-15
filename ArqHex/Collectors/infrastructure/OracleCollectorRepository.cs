using CAFEPAY.ArqHex.Collectors.domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.infrastructure
{
    public class OracleCollectorRepository : CollectorRepository
    {
        private readonly String connectionString;
        public OracleCollectorRepository(String _connectionString)
        {
            this.connectionString = _connectionString;
        }
        public void save(Collector collector)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    MERGE INTO COLLECTOR c
                    USIGN (SELECT :Id AS Id FROM DUAL) src
                    ON (src.Id = c.Id)
                    WHEN MATCHED THEN
                        UPDATE SET c.Name = :Name, Phone = :Phone, Status = :Status
                    WHEN NOT MATCHED THEN
                        INSERT (Id, Name, Phone, Status)
                        VALUES (:Id, :Name, :Phone, :Status)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", collector.Id.getValue()));
                    command.Parameters.Add(new OracleParameter("Name", collector.Name.getValue()));
                    command.Parameters.Add(new OracleParameter("Phone", collector.Phone.getValue()));
                    command.Parameters.Add(new OracleParameter("Status", collector.Status.getValue()));
                    command.ExecuteNonQuery();
                }
            }
        }
        public Dictionary<CollectorId, Collector> queryAll()
        {
            var collectors = new Dictionary<CollectorId, Collector>();
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Phone, Status FROM COLLECTOR";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CollectorId id = new CollectorId(reader.GetDecimal(0));
                            CollectorName name = new CollectorName(reader.GetString(1));
                            CollectorPhone phone = new CollectorPhone(reader.GetDecimal(2));
                            CollectorStatus status = new CollectorStatus(reader.GetBoolean(3));
                            Collector collector = new Collector(id, name, phone, status);
                            collectors.Add(id, collector);
                        }
                    }
                }
            }
            return collectors;
        }
    }
}
