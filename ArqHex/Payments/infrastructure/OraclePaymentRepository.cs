using CAFEPAY.ArqHex.Payments.domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace CAFEPAY.ArqHex.Payments.infrastructure
{
    public class OraclePaymentRepository : PaymentRepository
    {
        private readonly string connectionString;

        public OraclePaymentRepository(string _connectionstring)
        {
            this.connectionString = _connectionstring;
        }

        public void save(Payment payment)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));

            const string sql = @"
                INSERT INTO ADMINCAFEPAY.PAYMENT (ID, DATE, WORKER_CODE)
                VALUES (:p_id, :p_date, :p_worker_code)";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add(new OracleParameter("p_id", OracleDbType.Int64,
                    payment.id.id, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_date", OracleDbType.Date,
                    payment.date.paymentDate, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_worker_code", OracleDbType.Varchar2, 30,
                    payment.workerCode.workerCodeValue, ParameterDirection.Input));

                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex) when (ex.Number == 1)
                {
                    throw new InvalidOperationException("Ya existe un pago con ese ID.", ex);
                }
            }
        }

        public void update(Payment payment, long oldId)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            if (oldId <= 0) throw new ArgumentException("oldId es requerido", nameof(oldId));

            const string sql = @"
                UPDATE ADMINCAFEPAY.PAYMENT
                   SET ID = :p_new_id,
                       DATE = :p_date,
                       WORKER_CODE = :p_worker_code
                 WHERE ID = :p_old_id";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add(new OracleParameter("p_new_id", OracleDbType.Int64,
                    payment.id.id, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_date", OracleDbType.Date,
                    payment.date.paymentDate, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_worker_code", OracleDbType.Varchar2, 30,
                    payment.workerCode.workerCodeValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_old_id", OracleDbType.Int64,
                    oldId, ParameterDirection.Input));

                connection.Open();
                try
                {
                    var rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new KeyNotFoundException("No existe un pago con ese ID.");
                    }
                }
                catch (OracleException ex) when (ex.Number == 1)
                {
                    throw new InvalidOperationException("Ya existe un pago con ese ID.", ex);
                }
            }
        }

        public List<Payment> queryAll()
        {
            var payments = new List<Payment>();

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                const string query = @"SELECT ID, DATE, WORKER_CODE 
                                      FROM ADMINCAFEPAY.PAYMENT 
                                      ORDER BY ID";

                using (var command = new OracleCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = new PaymentId(reader.GetInt64(0));
                        var date = new PaymentDate(reader.GetDateTime(1));
                        var workerCode = new PaymentWorkerCode(reader.GetString(2));

                        var payment = new Payment(id, date, workerCode);
                        payments.Add(payment);
                    }
                }
            }

            return payments;
        }
    }
}