namespace Xarajat.Api.Models;

public class CalculateRoomOutlays
{
    public int UserCount { get; set; }
    public int TotalCost { get; set; }
    public int CostPerUser => TotalCost / UserCount;
}
