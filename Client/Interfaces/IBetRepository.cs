using System.Collections.Generic;

public interface IBetRepository {
    IEnumerable<Bet> GetBetsByPlayerId(int playerId, bool? check = null);
    Bet GetBetByWinnerTeam(int winnerTeamId);    
    Bet GetBetById(int betId);
    Bet GetWinnerTeamBet(string playerUsername);
    Bet GetRoundTeamBet(string playerUsername);
    Bet GetBetByFixtureId(string playerUsername,int fixtureId);
    Bet SaveBet(Bet bet);
    IEnumerable<int> GetQualifTeams(int playerId, bool? check = null);
    bool SaveQualifTeam(IList<QualifTeam> teams, int playerId);
    void CheckBet(int betId);
    void CheckQualifTeams(int playerId);
} 