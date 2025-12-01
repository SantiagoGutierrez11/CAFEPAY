using CAFEPAY.ArqHex.Collects.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.PaymentDetails.domain;
using CAFEPAY.ArqHex.Payments.domain;
using CAFEPAY.ArqHex.Plots.Domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace CAFEPAY.ArqHex.PaymentDetails.infrastructure
{
    public class OraclePaymentDetailRepository : PaymentDetailRepository
    {
        private readonly string connectionString;

        public OraclePaymentDetailRepository(string _connectionstring)
        {
            this.connectionString = _connectionstring;
        }

        // ============================================================
        // MÉTODO: save
        // ============================================================
        public long save(domain.PaymentDetail paymentDetail)
        {
            if (paymentDetail == null)
                throw new ArgumentNullException(nameof(paymentDetail));

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_PAYMENT_MANAGEMENT.create_payment_detail", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetros de entrada
                cmd.Parameters.Add("p_idpaymentdetail", OracleDbType.Int64).Value =
                    paymentDetail.id?.idValue > 0 ?
                    (object)paymentDetail.id.idValue : DBNull.Value;

                cmd.Parameters.Add("p_idpayment", OracleDbType.Int64).Value =
                    paymentDetail.paymentId.idPaymentValue;

                cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2, 30).Value =
                    paymentDetail.workerCode.Value;

                cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value =
                    paymentDetail.plotId.idPlotValue;

                cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value =
                    paymentDetail.harvestId.idHarvestValue;

                cmd.Parameters.Add("p_idcollect", OracleDbType.Int64).Value =
                    paymentDetail.collectId.idCollectValue;

                cmd.Parameters.Add("p_amount_to_pay", OracleDbType.Decimal).Value =
                    paymentDetail.amountToPay.amountToPayValue;

                // Parámetros de salida
                var outIdParam = new OracleParameter("p_idpaymentdetail_out", OracleDbType.Int64)
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
                        throw new PaymentDetailOperationException(errorMessage);
                    }

                    if (outIdParam.Value != null && outIdParam.Value != DBNull.Value)
                    {
                        return Convert.ToInt64(outIdParam.Value.ToString());
                    }

                    throw new PaymentDetailOperationException(
                        "No se pudo obtener el ID del detalle de pago creado");
                }
                catch (OracleException ex)
                {
                    throw MapSaveOracleException(ex);
                }
            }
        }

        // ============================================================
        // MÉTODO: update
        // ============================================================
        public void update(domain.PaymentDetail paymentDetail)
        {
            if (paymentDetail == null)
                throw new ArgumentNullException(nameof(paymentDetail),
                    "El detalle de pago no puede ser nulo");

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_PAYMENT_MANAGEMENT.update_payment_detail", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Validar que el ID existe
                    if (paymentDetail.id?.idValue == null || paymentDetail.id.idValue <= 0)
                    {
                        throw new ArgumentException(
                            "El ID del detalle de pago es requerido para actualizar");
                    }

                    // Parámetros de entrada
                    cmd.Parameters.Add("p_idpaymentdetail", OracleDbType.Int64).Value =
                        paymentDetail.id.idValue;

                    cmd.Parameters.Add("p_idpayment", OracleDbType.Int64).Value =
                        paymentDetail.paymentId.idPaymentValue;

                    cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2, 30).Value =
                        paymentDetail.workerCode.Value;

                    cmd.Parameters.Add("p_idplot", OracleDbType.Int64).Value =
                        paymentDetail.plotId.idPlotValue;

                    cmd.Parameters.Add("p_idharvest", OracleDbType.Int64).Value =
                        paymentDetail.harvestId.idHarvestValue;

                    cmd.Parameters.Add("p_idcollect", OracleDbType.Int64).Value =
                        paymentDetail.collectId.idCollectValue;

                    cmd.Parameters.Add("p_amount_to_pay", OracleDbType.Decimal).Value =
                        paymentDetail.amountToPay.amountToPayValue;

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
                        throw new PaymentDetailOperationException(errorMessage);
                    }
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (PaymentDetailOperationException)
                {
                    throw;
                }
                catch (OracleException ex)
                {
                    throw MapUpdateOracleException(ex);
                }
                catch (Exception ex)
                {
                    throw new PaymentDetailOperationException(
                        $"Error al actualizar el detalle de pago: {ex.Message}", ex);
                }
            }
        }

        // ============================================================
        // MÉTODO: queryByPaymentId
        // ============================================================
        public List<domain.PaymentDetail> queryByPaymentId(long? paymentId)
        {
            var paymentDetails = new List<domain.PaymentDetail>();

            if (!paymentId.HasValue || paymentId.Value <= 0)
            {
                throw new ArgumentException(
                    "El ID del pago debe ser un valor válido mayor a cero",
                    nameof(paymentId));
            }

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_PAYMENT_MANAGEMENT.query_details_by_payment", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                cmd.Parameters.Add("p_idpayment", OracleDbType.Int64).Value = paymentId.Value;

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
                            paymentDetails.Add(MapReaderToPaymentDetail(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    throw MapQueryByIdOracleException(ex, paymentId.Value);
                }
            }

            return paymentDetails;
        }

        // ============================================================
        // MÉTODO: queryAll
        // ============================================================
        public List<domain.PaymentDetail> queryAll()
        {
            var paymentDetails = new List<domain.PaymentDetail>();

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_PAYMENT_MANAGEMENT.query_all_payment_details", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

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
                            paymentDetails.Add(MapReaderToPaymentDetail(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    throw new PaymentDetailOperationException(
                        "Error al consultar todos los detalles de pago: " + ex.Message, ex);
                }
            }

            return paymentDetails;
        }

        // ============================================================
        // MÉTODO: deleteByPaymentDetailId
        // ============================================================
        public void deleteByPaymentDetailId(long? paymentDetailId, string reason)
        {
            if (!paymentDetailId.HasValue || paymentDetailId.Value <= 0)
                throw new ArgumentException(
                    "El ID del detalle de pago debe ser un valor válido mayor a cero",
                    nameof(paymentDetailId));

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException(
                    "La razón de eliminación es requerida",
                    nameof(reason));

            using (var connection = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_PAYMENT_MANAGEMENT.delete_payment_detail", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetros de entrada
                cmd.Parameters.Add("p_idpaymentdetail", OracleDbType.Int64).Value = paymentDetailId.Value;
                cmd.Parameters.Add("p_reason", OracleDbType.Varchar2, 1000).Value = reason;
                cmd.Parameters.Add("p_deleted_by", OracleDbType.Varchar2, 50).Value =
                    Environment.UserName; // O usar el usuario de tu sistema de autenticación

                // Parámetro de salida
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
                        throw new PaymentDetailOperationException(errorMessage);
                    }
                }
                catch (PaymentDetailOperationException)
                {
                    throw;
                }
                catch (OracleException ex)
                {
                    throw MapDeletePaymentDetailOracleException(ex, paymentDetailId.Value);
                }
                catch (Exception ex)
                {
                    throw new PaymentDetailOperationException(
                        $"Error inesperado al eliminar el detalle de pago: {ex.Message}", ex);
                }
            }
        }
        

        // ============================================================
        // MÉTODOS AUXILIARES - MAPEO
        // ============================================================

        private domain.PaymentDetail MapReaderToPaymentDetail(IDataReader reader)
        {
            return new domain.PaymentDetail(
                new PaymentDetailAmountToPay(reader.GetDecimal(reader.GetOrdinal("AMOUNT_TO_PAY"))),
                new PaymentDetailId(reader.GetInt64(reader.GetOrdinal("IDPAYMENTDETAIL"))),
                new PaymentDetailIdCollect(reader.GetInt64(reader.GetOrdinal("IDCOLLECT"))),
                new PaymentDetailIdHarvest(reader.GetInt64(reader.GetOrdinal("IDHARVEST"))),
                new PaymentDetailIdPayment(reader.GetInt64(reader.GetOrdinal("IDPAYMENT"))),
                new PaymentDetailIdPlot(reader.GetInt64(reader.GetOrdinal("IDPLOT"))),
                new PaymentDetailWorkerCode(reader.GetString(reader.GetOrdinal("WORKER_CODE")))
            );
        }

        // ============================================================
        // MÉTODOS AUXILIARES - MAPEO DE EXCEPCIONES
        // ============================================================

        private Exception MapSaveOracleException(OracleException ex)
        {
            switch (ex.Number)
            {
                case 20206:
                    return new PaymentNotFoundException(
                        "No existe el pago especificado", ex);

                case 20207:
                    return new PaymentDetailDuplicateException(
                        "Ya existe un detalle de pago con ese ID", ex);

                case 20208:
                    return new CollectNotFoundException(
                        "No existe la recolecta especificada", ex);

                case 20209:
                    return new InvalidCollectStatusException(
                        "La recolecta debe estar pendiente de pago (estado REGISTRADO)", ex);

                case 20210:
                    return new CollectAlreadyPaidException(
                        "Esta recolecta ya tiene un pago registrado", ex);

                case 20211:
                    return new InvalidAmountException(
                        "El monto no coincide con el valor de la recolecta", ex);

                case 20212:
                    return new PaymentDetailOperationException(
                        "No se pudo actualizar el estado de la recolecta a PAGADO", ex);

                case 20069:
                    return new InvalidAmountException(
                        "El pago excede el monto permitido para la recolecta", ex);

                default:
                    return new PaymentDetailOperationException(
                        "Error al guardar el detalle de pago: " + ex.Message, ex);
            }
        }

        private Exception MapUpdateOracleException(OracleException ex)
        {
            switch (ex.Number)
            {
                case 20223:
                    return new ArgumentException(
                        "El ID del detalle de pago es requerido", ex);

                case 20224:
                    return new PaymentDetailNotFoundException(
                        "No existe el detalle de pago especificado", ex);

                case 20225:
                    return new PaymentNotFoundException(
                        "No existe el pago especificado", ex);

                case 20226:
                    return new CollectNotFoundException(
                        "No existe la recolecta especificada", ex);

                case 20227:
                    return new InvalidAmountException(
                        "El monto del detalle no es válido", ex);

                case 20228:
                    return new PaymentDetailOperationException(
                        "No se pudo completar la actualización del detalle", ex);

                case 1: // ORA-00001 unique constraint
                    if (ex.Message.IndexOf("UNQ_PAYMENTDETAIL_COLLECT",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return new CollectAlreadyPaidException(
                            "Esta recolecta ya está asociada a otro pago", ex);
                    }
                    return new PaymentDetailOperationException(
                        "Ya existe un detalle con estos datos", ex);

                default:
                    return new PaymentDetailOperationException(
                        "Error al actualizar el detalle: " + ex.Message, ex);
            }
        }

        private Exception MapQueryByIdOracleException(OracleException ex, long paymentId)
        {
            switch (ex.Number)
            {
                case 20220:
                    return new ArgumentException(
                        "El ID del pago es requerido", ex);

                case 20221:
                    return new PaymentNotFoundException(
                        $"No existe el pago con ID {paymentId}", ex);

                case 20222:
                    return new PaymentDetailOperationException(
                        "Error al consultar los detalles del pago", ex);

                default:
                    return new PaymentDetailOperationException(
                        "Error en la consulta de detalles: " + ex.Message, ex);
            }
        }

        private Exception MapDeletePaymentDetailOracleException(OracleException ex, long paymentDetailId)
        {
            switch (ex.Number)
            {
                case 20248:
                    return new CollectorInactiveException(
                        "No se puede eliminar el detalle de pago porque el recolector está inactivo", ex);

                case 20249:
                    return new HarvestFinalizedException(
                        "No se puede eliminar el detalle de pago porque la cosecha está finalizada", ex);

                case 20250:
                    return new PaymentDetailOperationException(
                        "Error al restaurar el estado de la recolecta a REGISTRADO", ex);

                case 20253:
                    return new PaymentDetailNotFoundException(
                        $"No existe el detalle de pago con ID {paymentDetailId}", ex);

                default:
                    return new PaymentDetailOperationException(
                        "Error al eliminar el detalle de pago: " + ex.Message, ex);
            }
        }
    }

    // ============================================================
    // EXCEPCIONES PERSONALIZADAS
    // ============================================================

    public class PaymentDetailOperationException : Exception
    {
        public PaymentDetailOperationException(string message) : base(message) { }
        public PaymentDetailOperationException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class PaymentDetailNotFoundException : PaymentDetailOperationException
    {
        public PaymentDetailNotFoundException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class PaymentDetailDuplicateException : PaymentDetailOperationException
    {
        public PaymentDetailDuplicateException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class PaymentNotFoundException : PaymentDetailOperationException
    {
        public PaymentNotFoundException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class CollectNotFoundException : PaymentDetailOperationException
    {
        public CollectNotFoundException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class InvalidCollectStatusException : PaymentDetailOperationException
    {
        public InvalidCollectStatusException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class CollectAlreadyPaidException : PaymentDetailOperationException
    {
        public CollectAlreadyPaidException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class InvalidAmountException : PaymentDetailOperationException
    {
        public InvalidAmountException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class CollectorInactiveException : PaymentDetailOperationException
    {
        public CollectorInactiveException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class HarvestFinalizedException : PaymentDetailOperationException
    {
        public HarvestFinalizedException(string message, Exception inner = null)
            : base(message, inner) { }
    }
}