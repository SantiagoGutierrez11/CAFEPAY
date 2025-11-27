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

        public OraclePaymentRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public long save(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_PAYMENT_MANAGEMENT.create_payment", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true; // Usar nombres de parámetros

                // Parámetros de entrada
                cmd.Parameters.Add("p_idpayment", OracleDbType.Int64).Value =
                    payment.id?.id > 0 ? (object)payment.id.id : DBNull.Value;

                cmd.Parameters.Add("p_datepayment", OracleDbType.Date).Value =
                    payment.date.paymentDate;

                cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2, 30).Value =
                    payment.workerCode.workerCodeValue;

                // Parámetros de salida
                var outIdParam = new OracleParameter("p_idpayment_out", OracleDbType.Int64)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outIdParam);

                var outResultParam = new OracleParameter("p_result", OracleDbType.Varchar2, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outResultParam);

                connection.Open();

                try
                {
                    cmd.ExecuteNonQuery();

                    string result = outResultParam.Value?.ToString() ?? "";

                    if (result.StartsWith("ERROR:"))
                    {
                        string errorMessage = result.Substring(7).Trim();
                        throw new PaymentOperationException(errorMessage);
                    }

                    if (outIdParam.Value != null && outIdParam.Value != DBNull.Value)
                    {
                        return Convert.ToInt64(outIdParam.Value.ToString());
                    }

                    throw new PaymentOperationException("No se pudo obtener el ID del pago creado");
                }
                catch (OracleException ex)
                {
                    throw MapSaveOracleException(ex, payment);
                }
            }
        }

        public void update(Payment payment, long oldId)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment),
                    "El pago no puede ser nulo");

            if (oldId <= 0)
                throw new ArgumentException(
                    "El ID del pago a actualizar debe ser mayor a cero",
                    nameof(oldId));

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_PAYMENT_MANAGEMENT.update_payment", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Validar datos requeridos
                    if (payment.id?.id == null || payment.id.id <= 0)
                    {
                        throw new ArgumentException(
                            "El nuevo ID del pago es requerido y debe ser mayor a cero");
                    }

                    if (payment.date?.paymentDate == null)
                    {
                        throw new ArgumentException("La fecha de pago es requerida");
                    }

                    if (string.IsNullOrWhiteSpace(payment.workerCode?.workerCodeValue))
                    {
                        throw new ArgumentException("El código del recolector es requerido");
                    }

                    // Parámetros de entrada
                    cmd.Parameters.Add("p_old_idpayment", OracleDbType.Int64).Value = oldId;
                    cmd.Parameters.Add("p_new_idpayment", OracleDbType.Int64).Value = payment.id.id;
                    cmd.Parameters.Add("p_datepayment", OracleDbType.Date).Value = payment.date.paymentDate;
                    cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2, 30).Value =
                        payment.workerCode.workerCodeValue;

                    // Parámetro de salida
                    var outResultParam = new OracleParameter("p_result", OracleDbType.Varchar2, 500)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outResultParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    string result = outResultParam.Value?.ToString() ?? "";

                    if (result.StartsWith("ERROR:"))
                    {
                        string errorMessage = result.Substring(7).Trim();
                        throw new PaymentOperationException(errorMessage);
                    }
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (PaymentOperationException)
                {
                    throw;
                }
                catch (OracleException ex)
                {
                    throw MapUpdateOracleException(ex, oldId, payment.id?.id ?? 0);
                }
                catch (Exception ex)
                {
                    throw new PaymentOperationException(
                        $"Error inesperado al actualizar el pago: {ex.Message}", ex);
                }
            }
        }

        public List<Payment> queryAll()
        {
            var payments = new List<Payment>();

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_PAYMENT_MANAGEMENT.query_all_payments", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetro de salida (cursor)
                var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outCursor);

                connection.Open();

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(MapReaderToPayment(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    throw new PaymentOperationException(
                        "Error al consultar todos los pagos: " + ex.Message, ex);
                }
            }

            return payments;
        }

        public List<Payment> queryByWorkerCode(string workerCode)
        {
            if (string.IsNullOrWhiteSpace(workerCode))
                throw new ArgumentException(
                    "El código del recolector es requerido y no puede estar vacío",
                    nameof(workerCode));

            var payments = new List<Payment>();

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_PAYMENT_MANAGEMENT.query_payments_by_worker", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetro de entrada
                cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2, 30).Value = workerCode;

                // Parámetro de salida (cursor)
                var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outCursor);

                connection.Open();

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(MapReaderToPayment(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    throw MapQueryByWorkerOracleException(ex, workerCode);
                }
            }

            return payments;
        }

        public decimal getTotalAmountByWorkerCodeAndPaymentId(string workerCode, long? paymentID)
        {
            if (string.IsNullOrWhiteSpace(workerCode))
                throw new ArgumentException(
                    "El código del recolector es requerido",
                    nameof(workerCode));

            if (!paymentID.HasValue || paymentID.Value <= 0)
                throw new ArgumentException(
                    "El ID del pago debe ser un valor válido mayor a cero",
                    nameof(paymentID));

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("SELECT PKG_PAYMENT_MANAGEMENT.get_total_amount_by_payment(:p_worker_code, :p_idpayment) FROM DUAL", connection))
            {
                cmd.BindByName = true;

                cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2, 30).Value = workerCode;
                cmd.Parameters.Add("p_idpayment", OracleDbType.Int64).Value = paymentID.Value;

                connection.Open();

                try
                {
                    var result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDecimal(result);
                    }

                    return 0;
                }
                catch (OracleException ex)
                {
                    throw MapGetTotalAmountOracleException(ex, workerCode, paymentID.Value);
                }
            }
        }

        // ============================================================
        // MÉTODOS AUXILIARES
        // ============================================================

        private Payment MapReaderToPayment(IDataReader reader)
        {
            return new Payment(
                new PaymentId(reader.GetInt64(reader.GetOrdinal("IDPAYMENT"))),
                new PaymentDate(reader.GetDateTime(reader.GetOrdinal("DATEPAYMENT"))),
                new PaymentWorkerCode(reader.GetString(reader.GetOrdinal("WORKER_CODE")))
            );
        }

        private Exception MapSaveOracleException(OracleException ex, Payment payment)
        {
            switch (ex.Number)
            {
                case 20202:
                    return new ArgumentException("payment.date",
                        "La fecha de pago no puede estar vacía", ex);

                case 20203:
                    return new ArgumentException("payment.workerCode",
                        "El código del recolector no puede estar vacío", ex);

                case 20204:
                    return new CollectorNotFoundException(
                        $"No existe el recolector con código {payment.workerCode?.workerCodeValue}", ex);

                case 20205:
                    return new PaymentDuplicateException(
                        $"Ya existe un pago con el ID {payment.id?.id}", ex);

                default:
                    return new PaymentOperationException(
                        "Error al guardar el pago: " + ex.Message, ex);
            }
        }

        private Exception MapUpdateOracleException(OracleException ex, long oldId, long newId)
        {
            switch (ex.Number)
            {
                case 20238:
                    return new ArgumentException(
                        "El ID del pago a actualizar es requerido", ex);

                case 20239:
                    return new PaymentNotFoundException(
                        $"No existe un pago con el ID {oldId}", ex);

                case 20240:
                    return new ArgumentException("payment.date",
                        "La fecha de pago es requerida", ex);

                case 20241:
                    return new ArgumentException("payment.workerCode",
                        "El código del recolector es requerido", ex);

                case 20242:
                    return new CollectorNotFoundException(
                        "No existe el recolector especificado", ex);

                case 20243:
                    return new PaymentDuplicateException(
                        $"Ya existe un pago con el ID {newId}", ex);

                case 20244:
                    return new PaymentHasDetailsException(
                        "No se puede modificar un pago que tiene detalles asociados. " +
                        "Debe eliminar primero los detalles del pago", ex);

                case 20245:
                    return new PaymentOperationException(
                        "No se pudo completar la actualización del pago", ex);

                case 1: // ORA-00001 unique constraint
                    return new PaymentDuplicateException(
                        "Ya existe un pago con ese ID", ex);

                default:
                    return new PaymentOperationException(
                        "Error al actualizar el pago: " + ex.Message, ex);
            }
        }

        private Exception MapQueryByWorkerOracleException(OracleException ex, string workerCode)
        {
            switch (ex.Number)
            {
                case 20231:
                    return new ArgumentException(
                        "El código del recolector es requerido", ex);

                case 20232:
                    return new CollectorNotFoundException(
                        $"No existe el recolector con código {workerCode}", ex);

                case 20233:
                    return new PaymentOperationException(
                        "Error al consultar los pagos del recolector", ex);

                default:
                    return new PaymentOperationException(
                        "Error en la consulta de pagos: " + ex.Message, ex);
            }
        }

        private Exception MapGetTotalAmountOracleException(OracleException ex, string workerCode, long paymentId)
        {
            switch (ex.Number)
            {
                case 20234:
                    return new ArgumentException(
                        "El código del recolector es requerido", ex);

                case 20235:
                    return new ArgumentException(
                        "El ID del pago es requerido", ex);

                case 20236:
                    return new PaymentNotFoundException(
                        $"No existe el pago {paymentId} para el recolector {workerCode}", ex);

                case 20237:
                    return new PaymentOperationException(
                        "Error al calcular el monto total del pago", ex);

                default:
                    return new PaymentOperationException(
                        "Error al obtener el monto total: " + ex.Message, ex);
            }
        }
    }

    // ============================================================
    // EXCEPCIONES PERSONALIZADAS
    // ============================================================

    public class PaymentOperationException : Exception
    {
        public PaymentOperationException(string message) : base(message) { }
        public PaymentOperationException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class PaymentNotFoundException : PaymentOperationException
    {
        public PaymentNotFoundException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class PaymentDuplicateException : PaymentOperationException
    {
        public PaymentDuplicateException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class CollectorNotFoundException : PaymentOperationException
    {
        public CollectorNotFoundException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class PaymentHasDetailsException : PaymentOperationException
    {
        public PaymentHasDetailsException(string message, Exception inner = null)
            : base(message, inner) { }
    }
}