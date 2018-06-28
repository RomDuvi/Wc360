using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/player")]
[Authorize]
public class PlayerController : Controller
{
    IPlayerRepository _repos;

    public PlayerController(IPlayerRepository repository)
    {
        _repos = repository;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IEnumerable<Player> GetAllPlayers(){
        return _repos.GetAllPlayers().Select(p => new Player{ 
                                                        DisplayName = p.DisplayName,
                                                        UserName = p.UserName,
                                                        Score = p.Score,
                                                        Id = p.Id
                                                    });
    }

    [HttpGet("{id}",Name="GetPlayer")]
    public Player GetPlayerById(int id)
    {
        var p = _repos.GetPlayerById(id);
        return new Player{
            DisplayName = p.DisplayName,
            UserName = p.UserName,
            Score = p.Score,
            Id = p.Id
        };
    }

    [HttpGet("username/{username}",Name="username")]
    public Player GetPlayerByUserName(string userName)
    {
        var p = _repos.GetPlayerByUserName(userName);
        return new Player{
            DisplayName = p.DisplayName,
            UserName = p.UserName,
            Score = p.Score,
            Id = p.Id
        };
    }
}