namespace DesafioQuakeLog.Models;

public class Game
{
    public string Name { get; set; }
    public int TotalKills { get; set; }
    public List<string> Players { get; set; } = [];
    public Dictionary<string, int> Kills { get; set; } = [];
}
