using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Plots.Domain;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Plots.Infrastructure
{
    public class OraclePlotRepository : PlotRepository
    {
        private readonly string connectionString;
        public OraclePlotRepository(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public List<Domain.Plot> queryAll()
        {
            var plots = new List<Domain.Plot>();

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                const string query = "SELECT IDPLOT, IDOWNER, PLOTNAME, STATUS_ID FROM PLOT";

                using (var command = new OracleCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var plot = new Domain.Plot(
                            _idPlot: new PlotId(reader.GetInt64(0)),
                            _idOwner: new PlotOwnerId(reader.GetInt64(1)),
                            _name: new PlotName(reader.GetString(2)),
                            _status: new PlotStatus(reader.GetInt32(3))
                        );
                        plots.Add(plot);

                    }
                }
            }

            return plots;
        }
    }
}
