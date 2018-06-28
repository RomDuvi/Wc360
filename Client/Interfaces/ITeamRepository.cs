using System.Collections.Generic;

public interface ITeamRepository {
    Team GetTeamById(int id);

    IEnumerable<Team> GetAllTeams();
}