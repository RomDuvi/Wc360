using System;

public class Bet {
    public int Id { get; set; }
    public int? FixtureId { get; set; }
    public bool IsDraw { get; set; }
    public int PlayerId { get; set; }
    public int? ScoreTeam1 { get; set; }
    public int? ScoreTeam2 { get; set; }
    public int? WinnerTeamId { get; set; }
    public DateTime Date { get; set; }
    public int? RoundTeamId { get; set; }
    public int? RoundTeamRoundId { get; set; }
    public bool Checked { get; set; }
}