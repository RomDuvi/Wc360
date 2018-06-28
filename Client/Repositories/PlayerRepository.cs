using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class PlayerRepository : IPlayerRepository
{
    private IConfiguration Configuration { get; set; }

    public PlayerRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IEnumerable<Player> GetAllPlayers()
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = "Select * from players ORDER BY score DESC";
            return connection.Query<Player>(sql);
        }
    }

    public Player GetPlayerById(int id)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"Select * from players WHERE id = {id}";
            return connection.Query<Player>(sql).First();
        }
    }

    public Player GetPlayerByUserName(string userName)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"Select * from players WHERE username = '{userName}'";
            return connection.Query<Player>(sql).First();
        }
    }



    public void SetScore(int points, int playerId)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"UPDATE players SET score = score + {points} WHERE id = {playerId}";
            connection.Execute(sql);
        }
    }

    public void UpdateFixtureScores(int playerId){
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var scoreQuery = $@"SELECT 
                                    SUM(
                                    CASE 
                                        WHEN b.scoreTeam1 = f.scoreTeam1 AND b.scoreTeam2 = f.scoreTeam2 THEN 
                                            f.RoundId * 2
                                        WHEN b.ScoreTeam1 > b.scoreTeam2 AND f.scoreTeam1 > f.scoreTeam2 OR (b.ScoreTeam1 < b.scoreTeam2 AND f.scoreTeam1 < f.scoreTeam2) OR (b.ScoreTeam1 = b.scoreTeam2 AND f.scoreTeam1 = f.scoreTeam2) THEN 
                                            f.RoundId
                                        ELSE 0
                                    END) CalcPoints
                                FROM bets b
                                INNER JOIN fixtures f ON f.Id = b.fixtureid AND f.ended
                                INNER JOIN players p ON p.ID = b.playerId
                                WHERE b.checked = 0 AND b.fixtureID IS NOT NULL
                                GROUP BY b.playerID
                                HAVING b.playerID = {playerId}";
            
            int points = connection.Query<int>(scoreQuery).FirstOrDefault();  
            SetScore(points,playerId);
        }
    }
}