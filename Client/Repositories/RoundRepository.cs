using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class RoundRepository : IRoundRepository
{
    private IConfiguration Configuration { get; set; }

    public RoundRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IEnumerable<Round> GetAllRounds()
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = "Select * from rounds";
            return connection.Query<Round>(sql);
        }
    }

    public Round GetRoundById(int id)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"Select * from rounds WHERE id = {id}";
            return connection.Query<Round>(sql).First();
        }
    }
}