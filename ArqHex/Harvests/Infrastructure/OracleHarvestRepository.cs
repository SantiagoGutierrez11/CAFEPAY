using CAFEPAY.ArqHex.Harvests.Domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace CAFEPAY.ArqHex.Harvests.Infrastructure
{
    public class OracleHarvestRepository : HarvestRepository
    {
        private readonly string connectionString;

        public OracleHarvestRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public void save(Harvest harvest)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    MERGE INTO HARVEST h
                    USING (SELECT :Id AS Id FROM DUAL) src
                    ON (src.Id = h.Id)
                    WHEN MATCHED THEN
                        UPDATE SET 
                            h.StartDate = :StartDate,
                            h.EndDate = :EndDate,
                            h.PricePerKilo = :PricePerKilo,
                            h.Location = :Location
                    WHEN NOT MATCHED THEN
                        INSERT (Id, StartDate, EndDate, PricePerKilo, Location)
                        VALUES (:Id, :StartDate, :EndDate, :PricePerKilo, :Location)";

                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", harvest.Id.getValue()));
                    command.Parameters.Add(new OracleParameter("StartDate", harvest.StartDate.getValue()));
                    command.Parameters.Add(new OracleParameter("EndDate", harvest.EndDate.getValue()));
                    command.Parameters.Add(new OracleParameter("PricePerKilo", harvest.PricePerKilo.getValue()));
                    command.Parameters.Add(new OracleParameter("Location", harvest.Location.getValue()));
                    command.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<HarvestId, Harvest> queryAll()
        {
            var harvests = new Dictionary<HarvestId, Harvest>();

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, StartDate, EndDate, PricePerKilo, Location FROM HARVEST";

                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HarvestId id = new HarvestId(reader.GetDecimal(0));
                            HarvestStartDate startDate = new HarvestStartDate(reader.GetDateTime(1));
                            HarvestEndDate endDate = new HarvestEndDate(reader.GetDateTime(2));
                            HarvestPricePerKilo price = new HarvestPricePerKilo(reader.GetDecimal(3));
                            HarvestLocation location = new HarvestLocation(reader.GetString(4));

                            Harvest harvest = new Harvest(id, startDate, endDate, price, location);
                            harvests.Add(id, harvest);
                        }
                    }
                }
            }

            return harvests;
        }
    }
}
