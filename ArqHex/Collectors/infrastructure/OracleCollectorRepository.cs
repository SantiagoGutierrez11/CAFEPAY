using CAFEPAY.ArqHex.Collectors.domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace CAFEPAY.ArqHex.Collectors.infrastructure
{
    public class OracleCollectorRepository : CollectorRepository
    {
        private readonly string connectionString;

        public OracleCollectorRepository(string _connectionstring)
        {
            this.connectionString = _connectionstring;
        }

        public void save(Collector collector)
        {
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            const string sql = @"
        INSERT INTO ADMINCAFEPAY.COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
        VALUES (:p_worker_code, :p_id, :p_first_name, :p_last_name, :p_phone, :p_status_id)";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add(new OracleParameter("p_worker_code", OracleDbType.Varchar2, 30,
                    collector.workerCode.collectorWorkerCode, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_id", OracleDbType.Int64, 30,
                    collector.id.collectorId, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_first_name", OracleDbType.Varchar2, 50,
                    collector.firstName.collectorFirstName, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_last_name", OracleDbType.Varchar2, 50,
                    collector.lastName.collectorLastName, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_phone", OracleDbType.Varchar2, 20,
                    collector.phone.collectorPhone, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_status_id", OracleDbType.Int32,
                    collector.status.collectorStatus, ParameterDirection.Input));

                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex) when (ex.Number == 1) // ORA-00001 unique constraint violated
                {
                    // WORKER_CODE (PK) o ID (UNIQUE) duplicados
                    throw new InvalidOperationException("Ya existe un recolector con ese WorkerCode o ID.", ex);
                }
            }
        }
        public void update(Collector collector, long oldId)
        {
            if (collector == null) throw new ArgumentNullException(nameof(collector));
            if (string.IsNullOrWhiteSpace(oldId.ToString())) throw new ArgumentException("oldId es requerido", nameof(oldId));
            const string sql = @"
UPDATE ADMINCAFEPAY.COLLECTOR
   SET FIRST_NAME = :p_first_name,
       LAST_NAME  = :p_last_name,
       PHONE      = :p_phone,
       STATUS_ID  = :p_status_id,
       ID         = :p_new_id
 WHERE WORKER_CODE = :p_worker_code
   AND ID          = :p_old_id";
            ;

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add(new OracleParameter("p_first_name", OracleDbType.Varchar2, 50,
                    collector.firstName.collectorFirstName, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_last_name", OracleDbType.Varchar2, 50,
                    collector.lastName.collectorLastName, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_phone", OracleDbType.Varchar2, 20,
                    collector.phone.collectorPhone, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_status_id", OracleDbType.Int32,
                    collector.status.collectorStatus, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_new_id", OracleDbType.Varchar2, 30,
                    collector.id.collectorId, ParameterDirection.Input));
                // Clave para ubicar el registro
                cmd.Parameters.Add(new OracleParameter("p_worker_code", OracleDbType.Varchar2, 30,
                    collector.workerCode.collectorWorkerCode, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_old_id", OracleDbType.Varchar2, 30, oldId, ParameterDirection.Input));
                connection.Open();
                try
                {
                    var rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        // No se encontró el registro con ese par (WORKER_CODE, ID)
                        throw new KeyNotFoundException("No existe un recolector con ese WorkerCode e ID.");
                    }
                }
                catch (OracleException ex) when (ex.Number == 1) // ORA-00001 unique constraint violated
                {
                    // ID (UNIQUE) duplicados
                    throw new InvalidOperationException("Ya existe un recolector con ese ID.", ex);
                }
            }
        }


        public List<Collector> queryAll()
        {
            var collectors = new List<Collector>();

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                const string query = "SELECT WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID FROM ADMINCAFEPAY.COLLECTOR ORDER BY WORKER_CODE";

                using (var command = new OracleCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var workerCode = new CollectorWorkerCode(reader.GetString(0));
                        var id = new CollectorId(reader.GetInt64(1));
                        var firstName = new CollectorFirstName(reader.GetString(2));
                        var lastName = new CollectorLastName(reader.GetString(3));
                        var phone = new CollectorPhone(reader.GetString(4));
                        var status = new CollectorStatus(reader.GetInt32(5));

                        var collector = new Collector(workerCode, id, firstName, lastName, phone, status);
                        collectors.Add(collector);
                    }
                }
            }

            return collectors;
        }
        public List<Collector> queryByStatus(int status)
        {
            var collectors = new List<Collector>();
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                const string query = "SELECT WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID FROM ADMINCAFEPAY.COLLECTOR WHERE STATUS_ID = :p_status_id ORDER BY WORKER_CODE";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("p_status_id", OracleDbType.Int32, status, ParameterDirection.Input));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var workerCode = new CollectorWorkerCode(reader.GetString(0));
                            var id = new CollectorId(reader.GetInt64(1));
                            var firstName = new CollectorFirstName(reader.GetString(2));
                            var lastName = new CollectorLastName(reader.GetString(3));
                            var phone = new CollectorPhone(reader.GetString(4));
                            var statusObj = new CollectorStatus(reader.GetInt32(5));
                            var collector = new Collector(workerCode, id, firstName, lastName, phone, statusObj);
                            collectors.Add(collector);
                        }
                    }
                }
            }
            return collectors;
        }
    }
}
