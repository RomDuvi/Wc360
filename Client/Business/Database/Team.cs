using ServiceStack.DataAnnotations;
using System.Collections.Generic;

[Alias("Teams")]
public class Team
{
    [AutoIncrement]
    public int Id { get; set; }
    [Reference]
    public List<Fixture> Fixtures { get; set; }

    public string Name { get; set; }

    public bool Eleminated{ get; set; }

    public int CurrentRoundId { get; set; }

    public int Ranking { get; set; }
}