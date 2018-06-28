using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Linq;

public class FixtureRepository : IFixtureRepository
{
    private IConfiguration Configuration { get; set; }

    public FixtureRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public Fixture GetFixtureById(int id)
    {
        string sql = $"Select f.id, f.date, f.team1Id, f.team2Id, f.scoreTeam1, f.scoreTeam2, f.roundId, f.winningTeamId, f.ended, f.team1Id AS id, team1.name, f.team2Id AS id, team2.name FROM fixtures f INNER JOIN teams team1 ON team1.id = f.team1id INNER JOIN teams team2 ON team2.id = f.team2id WHERE f.id = {id}";
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var data = connection.Query<Fixture, Team, Team, Fixture>(sql,
                                                                    (fixture, team1, team2) =>
                                                                    {
                                                                        fixture.Team1 = team1;
                                                                        fixture.Team2 = team2;
                                                                        return fixture;
                                                                    },
                                                                    splitOn: "id,id,id");
            return data.First();
        }
    }

    public IEnumerable<Fixture> GetFixturesByTeamId(int teamId)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"Select * from fixtures WHERE team1id = {teamId} OR team2id = {teamId} ORDER BY date ASC";
            
            return connection.Query<Fixture>(sql);

        }
    }

    public IEnumerable<Fixture> GetFixtureByRoundId(int roundId)
    {
        string sql = $"Select f.id, f.date, f.team1Id, f.team2Id, f.scoreTeam1, f.scoreTeam2, f.roundId, f.winningTeamId, f.ended, f.team1Id AS id, team1.name, f.team2Id AS id, team2.name FROM fixtures f INNER JOIN teams team1 ON team1.id = f.team1id INNER JOIN teams team2 ON team2.id = f.team2id WHERE f.roundid = {roundId} ORDER BY f.date ASC";
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var data = connection.Query<Fixture, Team, Team, Fixture>(sql,
                                                                    (fixture, team1, team2) =>
                                                                    {
                                                                        fixture.Team1 = team1;
                                                                        fixture.Team2 = team2;
                                                                        return fixture;
                                                                    },
                                                                    splitOn: "id,id,id");
            return data;
        }
    }
}