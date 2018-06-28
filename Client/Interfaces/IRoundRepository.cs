using System.Collections.Generic;

public interface IRoundRepository
{
    IEnumerable<Round> GetAllRounds();
    Round GetRoundById(int id);
}