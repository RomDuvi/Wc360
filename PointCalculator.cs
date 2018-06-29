using System;
using System.Linq;

public class PointCalculator{

    public IPlayerRepository PlayerRepository { get; set; }
    public IFixtureRepository FixtureRepository { get; set; }
    public IRoundRepository RoundRepository { get; set; }
    public IBetRepository BetReposiory { get; set; }
    public ITeamRepository TeamRepository{ get; set; }

    public PointCalculator(IPlayerRepository playerRepository, IFixtureRepository fixtureRepository, IRoundRepository roundRepository, IBetRepository betRepository, ITeamRepository teamRepository){
        PlayerRepository = playerRepository;
        FixtureRepository = fixtureRepository;
        RoundRepository = roundRepository;
        BetReposiory = betRepository;
        TeamRepository = teamRepository;
    }


    public void CalculateBets(){
        var players = PlayerRepository.GetAllPlayers();
        foreach(var player in players){
            PlayerRepository.UpdateFixtureScores(player.Id);
            var bets = BetReposiory.GetBetsByPlayerId(player.Id);
            foreach(var bet in bets){
                if(bet.FixtureId != null){
                    CalculateSingleBetPoints(bet,player);
                    continue;
                }else if(bet.RoundTeamId != null){
                    CalculateRoundBetPoints(bet,player);
                    continue;
                }else if(bet.WinnerTeamId != null){
                    CalculateWinnerTeamBetPoints(bet,player);
                    continue;
                }
            }
            CalculateQualifTeamPoints(player);
        }
    }

    private void CalculateSingleBetPoints(Bet bet, Player player){
        var fixture = FixtureRepository.GetFixtureById(bet.FixtureId.Value);
        if(!fixture.Ended) return;
        BetReposiory.CheckBet(bet.Id);
    }

    private void CalculateRoundBetPoints(Bet bet, Player player){
        var team = TeamRepository.GetTeamById(bet.RoundTeamId.Value);
        double points = 0;
        if(!team.Eleminated || bet.Checked)return;

        if(team.Eleminated && team.CurrentRoundId == bet.RoundTeamRoundId){
            points = 10 * (1 + (team.Ranking/25));
        }else if(team.Eleminated && team.CurrentRoundId != bet.RoundTeamRoundId){
            points = -5 * (1 + (team.Ranking/25));
        }
        int.TryParse(Math.Ceiling(points).ToString(),out var intPoints);

        PlayerRepository.SetScore(intPoints, player.Id);
        BetReposiory.CheckBet(bet.Id);
    }

    private void CalculateWinnerTeamBetPoints(Bet bet, Player player){
        var betDate = bet.Date;
        var points = 0;
        var betRound = GetBetRoundByDate(bet.Date);
        var team = TeamRepository.GetTeamById(bet.WinnerTeamId.Value);
        if((team.CurrentRoundId < 4 && !team.Eleminated) || bet.Checked) return;
        
        if(team.CurrentRoundId == 1){
            points = -10;
        }

        if(team.CurrentRoundId == 2){
            switch(betRound){
                case 1:
                    points = -3;
                    break;
                case 2:
                    points = -5;
                    break;
            }
        }

        if(team.CurrentRoundId == 3){
            switch(betRound){
                case 1:
                    points = 0;
                    break;
                case 2:
                    points = -3;
                    break;
                case 3:
                    points = -5;
                    break;
            }
        }

        if(team.CurrentRoundId == 5){
            var fixture = FixtureRepository.GetFixtureByRoundId(5).First();
            var won = fixture.WinningTeamId == bet.WinnerTeamId;
            if(!fixture.Ended) return;
            switch(betRound){
                case 1:
                    if(won){
                        points = 6;
                    } else{
                        points = 4;
                    }
                    break;
                case 2:
                    if(won){
                        points = 3;
                    } else{
                        points = 0;
                    }
                    break;
                case 3:
                    if(won){
                        points = 1;
                    } else{
                        points = -2;
                    }
                    break;
            }
        }

        if(team.CurrentRoundId == 6){
            var fixture = FixtureRepository.GetFixtureByRoundId(6).First();
            var won = fixture.WinningTeamId == bet.WinnerTeamId;
            if(!fixture.Ended) return;
            switch(betRound){
                case 1:
                    if(won){
                        points = 20;
                    } else{
                        points = 10;
                    }
                    break;
                case 2:
                    if(won){
                        points = 13;
                    } else{
                        points = 5;
                    }
                    break;
                case 3:
                    if(won){
                        points = 8;
                    } else{
                        points = 3;
                    }
                    break;
            }
        }

        PlayerRepository.SetScore(points,player.Id);
        BetReposiory.CheckBet(bet.Id);
    }

    private int GetBetRoundByDate(DateTime date){
        var rounds = RoundRepository.GetAllRounds().ToList();
        var CurrentRoundId = 1;
        foreach(var round in rounds){
            if(date < round.StartDate) return CurrentRoundId;
            CurrentRoundId++;
        }
        return CurrentRoundId;
    }

    private void CalculateQualifTeamPoints(Player player){
        var fixtures = FixtureRepository.GetFixtureByRoundId(1);
        if(fixtures.Where(x=>!x.Ended).Count() > 0) return;

        var qualifiedTeams = TeamRepository.GetAllTeams().Where(x=>x.CurrentRoundId == 1);
        var betTeams = BetReposiory.GetQualifTeams(player.Id, false);
        if(betTeams.Count() != 16)return; //Reset to 16
        BetReposiory.CheckQualifTeams(player.Id);

        var crossTeams = qualifiedTeams.Where(x => betTeams.Contains(x.Id));
        var c = crossTeams.Count();
        
        if(c<2){
            PlayerRepository.SetScore(10, player.Id);
            return;
        }

        if(c<5){
            PlayerRepository.SetScore(5, player.Id);
            return;
        }

        if(c<8){
            return;
        }

        if(c<13){
            PlayerRepository.SetScore(5, player.Id);
            return;
        }

        if(c<16){
            PlayerRepository.SetScore(8, player.Id);
            return;
        }

        PlayerRepository.SetScore(10, player.Id);
        return;

    }
}