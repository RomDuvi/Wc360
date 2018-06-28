using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/bet")]
[Authorize]
public class BetController : Controller
{
    IBetRepository _repos;

    public BetController(IBetRepository repository)
    {
        _repos = repository;
    }
    
    [HttpGet("{betId}")]
    public Bet GetBetById(int betId){
        return _repos.GetBetById(betId);
    }

    [HttpGet("fixture/{fixtureId}/player/{playerUsername}")]
    public Bet GetBetByFixtureId(int fixtureId,string playerUsername){
        return _repos.GetBetByFixtureId(playerUsername, fixtureId);
    }

    [HttpPost]
    public IActionResult SaveBet([FromBody]Bet bet){
        var res = _repos.SaveBet(bet);
        if(res == null){
            return BadRequest(); 
        }
        return Ok(res);
    }

    [HttpGet("player/{playerId}")]
    public IEnumerable<Bet> GetBetsByPlayerId(int playerId){
        return _repos.GetBetsByPlayerId(playerId);
    }

    [HttpGet("winnerTeam/{winnerTeamId}")]
    public Bet GetBetByWinnerTeamId(int winnerTeamId){
        return _repos.GetBetByWinnerTeam(winnerTeamId);
    }

    [HttpGet("winnerTeam/player/{username}")]
    public Bet GetBetWinnerTeamByUser(string userName){
        return _repos.GetWinnerTeamBet(userName);
    }

    [HttpGet("roundTeam/player/{username}")]
    public Bet GetBetRoundTeamByUser(string username){
        return _repos.GetRoundTeamBet(username);
    }

    [HttpGet("qualifteam/player/{playerId}")]
    public IEnumerable<int> GetQualifTeams(int playerId){
        return _repos.GetQualifTeams(playerId);
    }

    [HttpPost("qualifteam/{playerId}")]
    public IActionResult SaveQualifTeam([FromBody]IList<QualifTeam> teams, int playerId){
        var res = _repos.SaveQualifTeam(teams,playerId);

        if(res){
            return Ok();
        }else{
            return BadRequest();
        }
    }
}