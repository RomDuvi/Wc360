using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/team")]
[Authorize]
public class TeamController : Controller
{
    ITeamRepository _repos;

    public TeamController(ITeamRepository repository)
    {
        _repos = repository;
    }
    
    [HttpGet]
    public IEnumerable<Team> GetAllTeams()
    {
        return _repos.GetAllTeams();
    }
    
    [HttpGet("{id}",Name="getTeam")]
    public Team GetTeamById(int id)
    {
        return _repos.GetTeamById(id);
    }
}