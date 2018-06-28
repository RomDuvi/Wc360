using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class BetRepository : IBetRepository
{
    public IConfiguration Configuration { get; set; }

    public IPlayerRepository PlayerRepository { get; set; }

    public BetRepository(IConfiguration configuration, IPlayerRepository playerRepos)
    {
        Configuration = configuration;
        PlayerRepository = playerRepos;
    }

    public Bet GetBetByFixtureId(string playerUsername, int fixtureId)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var player = PlayerRepository.GetPlayerByUserName(playerUsername);

            var sql = $"Select * from bets where playerId = {player.Id} AND fixtureId = {fixtureId}";
            
            var res = connection.Query<Bet>(sql);

            return res.Count() > 0 ? res.First() : null;
        }
    }

    public Bet GetBetById(int betId)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"Select * from bets where id = {betId}";
            return connection.Query<Bet>(sql).First();
        }
    }

    public Bet SaveBet(Bet bet)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {   
            var existingBet = GetBetsByPlayerId(bet.PlayerId)
                .Where(x => bet.FixtureId != null && x.FixtureId == bet.FixtureId || 
                            bet.WinnerTeamId != null && x.WinnerTeamId == bet.WinnerTeamId ||
                            bet.RoundTeamId != null && x.RoundTeamId == bet.RoundTeamId);
            
            if(existingBet.Any()) return null;

            var lastBetId = connection.Query<int>("SELECT CASE WHEN MAX(id) IS NULL THEN 0 ELSE MAX(id) END AS id FROM bets").First() + 1;
            bool isDraw;
            
            if(bet.ScoreTeam1 == null){
                isDraw = false;
            }else{
                isDraw = bet.ScoreTeam1 == bet.ScoreTeam2; 
            }

            var sql = $"INSERT INTO bets (Id,FixtureId,IsDraw,PlayerId,ScoreTeam1,ScoreTeam2,WinnerTeamId,Date,RoundTeamId, RoundTeamRoundId) VALUES (@lastBetId,@FixtureId,@IsDraw,@PlayerId,@ScoreTeam1,@ScoreTeam2,@WinnerTeamId,@Date,@RoundTeamId,@RoundTeamRoundId)";
            var res = connection.Execute(sql, new {lastBetId, bet.FixtureId, isDraw, bet.PlayerId, bet.ScoreTeam1, bet.ScoreTeam2, bet.WinnerTeamId, bet.Date, bet.RoundTeamId, bet.RoundTeamRoundId});

            if(res == 1){
                return bet;
            } else{
                return null; 
            }
        }
    }

    public IEnumerable<Bet> GetBetsByPlayerId(int playerId, bool? check = null)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"Select * from bets where playerId = {playerId}";
            if(check != null){
                sql += $" AND checked = {check}";
            }
            var res =  connection.Query<Bet>(sql);
            return res;
        }
    }

    public Bet GetBetByWinnerTeam(int winnerTeamId)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"Select * from bets where winnerTeamId = {winnerTeamId}";
            return connection.Query<Bet>(sql).First();
        }
    }

    public Bet GetWinnerTeamBet(string playerUsername)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var player = PlayerRepository.GetPlayerByUserName(playerUsername);

            var sql = $"Select * from bets where winnerTeamId IS NOT NULL AND playerId = {player.Id} ORDER BY date DESC";
            var res = connection.Query<Bet>(sql);
            return res.Count() > 0 ? res.First() : null;
        }
    }

    public Bet GetRoundTeamBet(string playerUsername)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var player = PlayerRepository.GetPlayerByUserName(playerUsername);

            var sql = $"Select * from bets where roundTeamId IS NOT NULL AND playerId = {player.Id}";
            var res = connection.Query<Bet>(sql);
            return res.Count() > 0 ? res.First() : null;
        }
    }

    public IEnumerable<int> GetQualifTeams(int playerId, bool? check = null)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"Select DISTINCT(teamId) from playerTeams where playerId = {playerId}";
            if(check != null){
                sql += $" AND Checked = {check}";
            }
            return connection.Query<int>(sql);
        }
    }

    public void CheckQualifTeams(int playerId){
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"UPDATE playerTeams SET checked = 1 WHERE playerId = {playerId}";
            connection.Execute(sql);
        }
    }

    public bool SaveQualifTeam(IList<QualifTeam> teams, int playerId)
    {
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var player = PlayerRepository.GetPlayerById(playerId);
            var res = 0;
            foreach(var team in teams){
                var sql = $"INSERT INTO playerTeams (playerId,teamId) VALUES (@playerId, @teamId)";
                res += connection.Execute(sql,new {playerId, team.TeamId});
            }
            return res > 0;
        }
    }

    public void CheckBet(int betId){
        using (var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection")))
        {
            var sql = $"UPDATE bets SET checked = 1 WHERE id = {betId}";
            connection.Execute(sql);
        }
    }
}