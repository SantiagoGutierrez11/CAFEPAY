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

        // insertar un nuevo recolector
        public void save(Collector collector)
        {
            if (collector == null)
                throw new ArgumentNullException(nameof(collector));

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECTOR_MANAGEMENT.save_collector", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetros de entrada
                cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2, 6).Value =
                    collector.workerCode.collectorWorkerCode;
                cmd.Parameters.Add("p_id", OracleDbType.Int64).Value =
                    collector.id.collectorId;
                cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2, 30).Value =
                    collector.firstName.collectorFirstName;
                cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2, 30).Value =
                    collector.lastName.collectorLastName;
                cmd.Parameters.Add("p_phone", OracleDbType.Int64).Value =
                    long.Parse(collector.phone.collectorPhone);
                cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value =
                    collector.status.collectorStatus;

                // Parámetro de salida
                var outResultParam = new OracleParameter("p_result", OracleDbType.Varchar2, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outResultParam);

                conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();

                    // Verificar el resultado
                    string result = outResultParam.Value?.ToString() ?? "";

                    if (result.StartsWith("ERROR:"))
                    {
                        string errorMessage = result.Substring(7).Trim();
                        throw new CollectorOperationException(errorMessage);
                    }
                }
                catch (OracleException ex)
                {
                    throw MapSaveOracleException(ex, collector.workerCode.collectorWorkerCode);
                }
            }
        }

        // actualiza recolector
        public void update(Collector collector, long oldId)
        {
            if (collector == null)
                throw new ArgumentNullException(nameof(collector));
            if (oldId <= 0)
                throw new ArgumentException("El ID anterior es requerido", nameof(oldId));

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECTOR_MANAGEMENT.update_collector", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                try
                {
                    // Parámetros de entrada
                    cmd.Parameters.Add("p_worker_code", OracleDbType.Varchar2, 6).Value =
                        collector.workerCode.collectorWorkerCode;
                    cmd.Parameters.Add("p_old_id", OracleDbType.Int64).Value = oldId;
                    cmd.Parameters.Add("p_new_id", OracleDbType.Int64).Value =
                        collector.id.collectorId;
                    cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2, 30).Value =
                        collector.firstName.collectorFirstName;
                    cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2, 30).Value =
                        collector.lastName.collectorLastName;
                    cmd.Parameters.Add("p_phone", OracleDbType.Int64).Value =
                        long.Parse(collector.phone.collectorPhone);
                    cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value =
                        collector.status.collectorStatus;

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
                        throw new CollectorOperationException(errorMessage);
                    }
                }
                catch (OracleException ex)
                {
                    throw MapUpdateOracleException(ex, collector.workerCode.collectorWorkerCode, oldId);
                }
            }
        }

        // consulta todos los recolectores
        public List<Collector> queryAll()
        {
            var collectors = new List<Collector>();

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECTOR_MANAGEMENT.query_all_collectors", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetro de salida (cursor)
                var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outCursor);

                conn.Open();

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            collectors.Add(MapReaderToCollector(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    throw new CollectorOperationException(
                        "Error al consultar los recolectores: " + ex.Message, ex);
                }
            }

            return collectors;
        }

        // consulta recolectores por status
        public List<Collector> queryByStatus(int status)
        {
            var collectors = new List<Collector>();

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECTOR_MANAGEMENT.query_collectors_by_status", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetro de entrada
                cmd.Parameters.Add("p_status_id", OracleDbType.Int32).Value = status;

                // Parámetro de salida (cursor)
                var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outCursor);

                conn.Open();

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            collectors.Add(MapReaderToCollector(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    if (ex.Number == 20418)
                    {
                        throw new InvalidOperationException(
                            "Estado inválido. Use 1 para recolectores activos o 2 para inactivos", ex);
                    }
                    throw new CollectorOperationException(
                        "Error al consultar los recolectores: " + ex.Message, ex);
                }
            }

            return collectors;
        }

        // consulta una string de recolectores en formato "W00001, W00002 ..."
        public List<Collector> queryByIn(string workerCodes)
        {
            if (string.IsNullOrWhiteSpace(workerCodes))
                throw new ArgumentException("Debe proporcionar al menos un código", nameof(workerCodes));

            var collectors = new List<Collector>();

            using (var conn = new OracleConnection(connectionString))
            using (var cmd = new OracleCommand("PKG_COLLECTOR_MANAGEMENT.query_collectors_by_codes", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                // Parámetro de entrada (debe venir en formato: 'W00001','W00002')
                cmd.Parameters.Add("p_worker_codes", OracleDbType.Varchar2, workerCodes.Length).Value =
                    workerCodes;

                // Parámetro de salida (cursor)
                var outCursor = new OracleParameter("p_result", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outCursor);

                conn.Open();

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            collectors.Add(MapReaderToCollector(reader));
                        }
                    }
                }
                catch (OracleException ex)
                {
                    if (ex.Number == 20419)
                    {
                        throw new ArgumentException(
                            "Debe proporcionar al menos un código de recolector", ex);
                    }
                    if (ex.Number == 20420)
                    {
                        throw new CollectorOperationException(
                            "Error al consultar recolectores. Verifique el formato de los códigos", ex);
                    }
                    throw new CollectorOperationException(
                        "Error al consultar los recolectores: " + ex.Message, ex);
                }
            }

            return collectors;
        }

        // metodos auxiliares

        private Collector MapReaderToCollector(IDataReader reader)
        {
            var workerCode = new CollectorWorkerCode(reader.GetString(0));
            var id = new CollectorId(reader.GetInt64(1));
            var firstName = new CollectorFirstName(reader.GetString(2));
            var lastName = new CollectorLastName(reader.GetString(3));
            var phone = new CollectorPhone(reader.GetInt64(4).ToString());
            var status = new CollectorStatus(reader.GetInt32(5));

            return new Collector(workerCode, id, firstName, lastName, phone, status);
        }

        private Exception MapSaveOracleException(OracleException ex, string workerCode)
        {
            switch (ex.Number)
            {
                case 20401:
                    return new CollectorOperationException("El código del recolector es obligatorio", ex);

                case 20402:
                    return new CollectorOperationException("El número de identificación es obligatorio", ex);

                case 20403:
                    return new CollectorOperationException("Los nombres del recolector son obligatorios", ex);

                case 20404:
                    return new CollectorOperationException("El teléfono es obligatorio", ex);

                case 20405:
                    return new CollectorOperationException("El estado debe ser 1 (Activo) o 2 (Inactivo)", ex);

                case 20406:
                    return new InvalidPhoneFormatException("El teléfono debe tener exactamente 10 dígitos", ex);

                case 20407:
                    return new CollectorAlreadyExistsException(workerCode, "código", ex);

                case 20408:
                    return new CollectorAlreadyExistsException(workerCode, "identificación", ex);

                case 1: // ORA-00001 unique constraint
                    if (ex.Message.IndexOf("COLLECTOR_PK", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return new CollectorAlreadyExistsException(workerCode, "código", ex);
                    }
                    if (ex.Message.IndexOf("COLLECTOR_UQ_ID", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return new CollectorAlreadyExistsException(workerCode, "identificación", ex);
                    }
                    return new CollectorOperationException("Ya existe un recolector con estos datos", ex);

                default:
                    return new CollectorOperationException(
                        "Error al guardar el recolector: " + ex.Message, ex);
            }
        }

        private Exception MapUpdateOracleException(OracleException ex, string workerCode, long oldId)
        {
            switch (ex.Number)
            {
                case 20409:
                    return new CollectorOperationException("El código del recolector es obligatorio", ex);

                case 20410:
                    return new CollectorNotFoundException(workerCode, oldId, ex);

                case 20411:
                    return new CollectorOperationException("Los nombres del recolector son obligatorios", ex);

                case 20412:
                    return new CollectorOperationException("El teléfono es obligatorio", ex);

                case 20413:
                    return new InvalidPhoneFormatException("El teléfono debe tener exactamente 10 dígitos", ex);

                case 20414:
                    return new CollectorOperationException("El estado debe ser 1 (Activo) o 2 (Inactivo)", ex);

                case 20415:
                    return new CollectorAlreadyExistsException(workerCode, "identificación", ex);

                case 20416:
                    return new CollectorHasActiveCollectsException(workerCode, ex);

                case 20417:
                    return new CollectorOperationException("No se pudo actualizar el recolector", ex);

                case 20071:
                    return new CollectorHasActiveCollectsException(workerCode, ex);

                case 1: // ORA-00001 unique constraint
                    if (ex.Message.IndexOf("COLLECTOR_UQ_ID", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return new CollectorAlreadyExistsException(workerCode, "identificación", ex);
                    }
                    return new CollectorOperationException("Ya existe un recolector con estos datos", ex);

                default:
                    return new CollectorOperationException(
                        "Error al actualizar el recolector: " + ex.Message, ex);
            }
        }
    }
    // Excepciones específicas del repositorio de recolectores

    public class CollectorOperationException : Exception
    {
        public CollectorOperationException(string message) : base(message) { }
        public CollectorOperationException(string message, Exception inner) : base(message, inner) { }
    }

    public class CollectorAlreadyExistsException : CollectorOperationException
    {
        public string WorkerCode { get; }
        public string Field { get; }

        public CollectorAlreadyExistsException(string workerCode, string field, Exception inner = null)
            : base($"Ya existe un recolector con ese {field}", inner)
        {
            WorkerCode = workerCode;
            Field = field;
        }
    }

    public class CollectorNotFoundException : CollectorOperationException
    {
        public string WorkerCode { get; }
        public long Id { get; }

        public CollectorNotFoundException(string workerCode, long id, Exception inner = null)
            : base($"No se encontró el recolector con código {workerCode} e identificación {id}", inner)
        {
            WorkerCode = workerCode;
            Id = id;
        }
    }

    public class InvalidPhoneFormatException : CollectorOperationException
    {
        public InvalidPhoneFormatException(string message, Exception inner = null)
            : base(message, inner) { }
    }

    public class CollectorHasActiveCollectsException : CollectorOperationException
    {
        public string WorkerCode { get; }

        public CollectorHasActiveCollectsException(string workerCode, Exception inner = null)
            : base($"No se puede desactivar el recolector porque tiene recolecciones pendientes en cosechas activas", inner)
        {
            WorkerCode = workerCode;
        }
    }
}