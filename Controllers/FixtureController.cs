using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/fixture")]
[Authorize]
public class FixtureController : Controller
{
    IFixtureRepository _repos;

    public FixtureController(IFixtureRepository repository)
    {
        _repos = repository;
    }
    
    [HttpGet("round/{roundId}",Name="roundFixtures")]
    public IEnumerable<Fixture> GetFixturesByRoundId(int roundId)
    {
        return _repos.GetFixtureByRoundId(roundId);
    }
    
    [HttpGet("{id}",Name="GetFixture")]
    public Fixture GetFixtureById(int id)
    {
        return _repos.GetFixtureById(id);
    }

    [HttpGet("team/{teamId}",Name="team")]
    public IEnumerable<Fixture> GetFixturesByTeamId(int teamId)
    {
        return _repos.GetFixturesByTeamId(teamId);
    }
}