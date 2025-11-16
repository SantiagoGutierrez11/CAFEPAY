using CAFEPAY.ArqHex.PaymentsDetails.domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace CAFEPAY.ArqHex.PaymentsDetails.infrastructure
{
    public class OraclePaymentsDetailsRepository : PaymentsDetailsRepository
    {
        private readonly string connectionString;

        public OraclePaymentsDetailsRepository(string _connectionstring)
        {
            this.connectionString = _connectionstring;
        }

        public void save(PaymentsDetailsEntity PaymentsDetails)
        {
            if (PaymentsDetails == null) throw new ArgumentNullException(nameof(PaymentsDetails));

            const string sql = @"
                INSERT INTO ADMINCAFEPAY.PAYMENT_DETAIL (AMOUNT_TO_PAY, ID, COLLECT_ID, HARVEST_ID, PAYMENT_ID, PLOT_ID, WORKER_CODE)
                VALUES (:p_amount_to_pay, :p_id, :p_collect_id, :p_harvest_id, :p_payment_id, :p_plot_id, :p_worker_code)";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add(new OracleParameter("p_amount_to_pay", OracleDbType.Int64,
                    PaymentsDetails.amountToPay.PaymentsDetailsAmountToPayValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_id", OracleDbType.Int64,
                    PaymentsDetails.id.idValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_collect_id", OracleDbType.Int64,
                    PaymentsDetails.collectId.idCollectValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_harvest_id", OracleDbType.Int64,
                    PaymentsDetails.harvestId.idHarvestValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_payment_id", OracleDbType.Int64,
                    PaymentsDetails.paymentId.idPaymentValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_plot_id", OracleDbType.Int64,
                    PaymentsDetails.plotId.idPlotValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_worker_code", OracleDbType.Varchar2, 30,
    PaymentsDetails.workerCode.Value, ParameterDirection.Input));

                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (OracleException ex) when (ex.Number == 1)
                {
                    throw new InvalidOperationException("Ya existe un detalle de pago con ese ID.", ex);
                }
            }
        }

        public void update(PaymentsDetailsEntity PaymentsDetails, long oldId)
        {
            if (PaymentsDetails == null) throw new ArgumentNullException(nameof(PaymentsDetails));
            if (oldId <= 0) throw new ArgumentException("oldId es requerido", nameof(oldId));

            const string sql = @"
                UPDATE ADMINCAFEPAY.PAYMENT_DETAIL
                   SET AMOUNT_TO_PAY = :p_amount_to_pay,
                       ID = :p_new_id,
                       COLLECT_ID = :p_collect_id,
                       HARVEST_ID = :p_harvest_id,
                       PAYMENT_ID = :p_payment_id,
                       PLOT_ID = :p_plot_id,
                       WORKER_CODE = :p_worker_code
                 WHERE ID = :p_old_id";

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand(sql, connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add(new OracleParameter("p_amount_to_pay", OracleDbType.Int64,
                    PaymentsDetails.amountToPay.PaymentsDetailsAmountToPayValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_new_id", OracleDbType.Int64,
                    PaymentsDetails.id.idValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_collect_id", OracleDbType.Int64,
                    PaymentsDetails.collectId.idCollectValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_harvest_id", OracleDbType.Int64,
                    PaymentsDetails.harvestId.idHarvestValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_payment_id", OracleDbType.Int64,
                    PaymentsDetails.paymentId.idPaymentValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_plot_id", OracleDbType.Int64,
                    PaymentsDetails.plotId.idPlotValue, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_worker_code", OracleDbType.Varchar2, 30,
    PaymentsDetails.workerCode.Value, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("p_old_id", OracleDbType.Int64,
                    oldId, ParameterDirection.Input));

                connection.Open();
                try
                {
                    var rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new KeyNotFoundException("No existe un detalle de pago con ese ID.");
                    }
                }
                catch (OracleException ex) when (ex.Number == 1)
                {
                    throw new InvalidOperationException("Ya existe un detalle de pago con ese ID.", ex);
                }
            }
        }

        public List<PaymentsDetailsEntity> queryAll()
        {
            var PaymentsDetailss = new List<PaymentsDetailsEntity>();

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                const string query = @"SELECT AMOUNT_TO_PAY, ID, COLLECT_ID, HARVEST_ID, PAYMENT_ID, PLOT_ID, WORKER_CODE 
                                      FROM ADMINCAFEPAY.PAYMENT_DETAIL 
                                      ORDER BY ID";

                using (var command = new OracleCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var amountToPay = new PaymentsDetailsAmountToPay(reader.GetInt64(0));
                        var id = new PaymentsDetailsId(reader.GetInt64(1));
                        var collectId = new PaymentsDetailsIdCollect(reader.GetInt64(2));
                        var harvestId = new PaymentsDetailsIdHarvest(reader.GetInt64(3));
                        var paymentId = new PaymentsDetailsIdPayment(reader.GetInt64(4));
                        var plotId = new PaymentsDetailsIdPlot(reader.GetInt64(5));
                        var workerCode = new PaymentsDetailsWorkerCode(reader.GetString(6));

                        var PaymentsDetails = new PaymentsDetailsEntity(amountToPay, id, collectId, harvestId, paymentId, plotId, workerCode);
                        PaymentsDetailss.Add(PaymentsDetails);
                    }
                }
            }

            return PaymentsDetailss;
        }

        public List<PaymentsDetailsEntity> queryByPaymentId(long paymentId)
        {
            var PaymentsDetailss = new List<PaymentsDetailsEntity>();

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                const string query = @"SELECT AMOUNT_TO_PAY, ID, COLLECT_ID, HARVEST_ID, PAYMENT_ID, PLOT_ID, WORKER_CODE 
                                      FROM ADMINCAFEPAY.PAYMENT_DETAIL 
                                      WHERE PAYMENT_ID = :p_payment_id 
                                      ORDER BY ID";

                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("p_payment_id", OracleDbType.Int64, paymentId, ParameterDirection.Input));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var amountToPay = new PaymentsDetailsAmountToPay(reader.GetInt64(0));
                            var id = new PaymentsDetailsId(reader.GetInt64(1));
                            var collectId = new PaymentsDetailsIdCollect(reader.GetInt64(2));
                            var harvestId = new PaymentsDetailsIdHarvest(reader.GetInt64(3));
                            var paymentIdObj = new PaymentsDetailsIdPayment(reader.GetInt64(4));
                            var plotId = new PaymentsDetailsIdPlot(reader.GetInt64(5));
                            var workerCode = new PaymentsDetailsWorkerCode(reader.GetString(6));

                            var PaymentsDetails = new PaymentsDetailsEntity(amountToPay, id, collectId, harvestId, paymentIdObj, plotId, workerCode);
                            PaymentsDetailss.Add(PaymentsDetails);
                        }
                    }
                }
            }

            return PaymentsDetailss;
        }
    }
}