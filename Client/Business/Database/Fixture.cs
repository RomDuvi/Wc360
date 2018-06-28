using System;
using ServiceStack.DataAnnotations;

[Alias("Fixtures")]
public class Fixture
{
    [AutoIncrement]
    public int Id { get; set; }
    public int Team1Id { get; set; }
    public int Team2Id { get; set; }
    public int? ScoreTeam1 { get; set; }
    public int? ScoreTeam2 { get; set; }
    public int RoundId { get; set; }
    public DateTime Date { get; set; }
    public bool Ended { get; set; }
    public int? WinningTeamId { get; set; }
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
}