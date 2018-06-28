using System.Collections.Generic;

public interface IFixtureRepository{
    Fixture GetFixtureById(int id);

    IEnumerable<Fixture> GetFixturesByTeamId(int teamId);

    IEnumerable<Fixture> GetFixtureByRoundId(int roundId);
}