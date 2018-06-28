using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class TeamRepository : ITeamRepository
{
    private IConfiguration Configuration { get; set; }

    public TeamRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IEnumerable<Team> GetAllTeams()
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = "Select * from teams";
            return connection.Query<Team>(sql);
        }
    }

    public Team GetTeamById(int id)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"Select * from teams WHERE id = {id}";
            return connection.Query<Team>(sql).First();
        }
    }
}