using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/round")]
[Authorize]
public class RoundController : Controller
{
    IRoundRepository _repos;

    public RoundController(IRoundRepository repository)
    {
        _repos = repository;
    }
    
    [HttpGet]
    public IEnumerable<Round> GetAllRounds()
    {
        return _repos.GetAllRounds();
    }
    
    [HttpGet("{id}",Name="GetRound")]
    public Round GetRoundById(int id)
    {
        return _repos.GetRoundById(id);
    }
}