using System.Collections.Generic;

public interface IPlayerRepository {
    Player GetPlayerById(int id);
    Player GetPlayerByUserName(string userName);
    IEnumerable<Player> GetAllPlayers();
    void SetScore(int score, int playerId);

    void UpdateFixtureScores(int playerId);
}