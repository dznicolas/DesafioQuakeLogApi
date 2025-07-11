using DesafioQuakeLog.Models;
using System.Text.RegularExpressions;

namespace DesafioQuakeLog.Services
{
    public class GameService
    {
        private readonly List<Game> _games = [];
        private readonly string filePath;

        public GameService()
        {
            filePath = Path.Combine(AppContext.BaseDirectory, "Resources", "games.log");

            if (File.Exists(filePath))
                ParserGameFile();
        }

        private void ParserGameFile()
        {
            string[] lines = File.ReadAllLines(filePath);
            int count = 0;
            Game? currentGame = null;

            foreach (var line in lines)
            {
                if (line.Contains("InitGame"))
                {
                    count++;
                    currentGame = new Game
                    {
                        Name = $"game_{count}",
                        Players = [],
                        Kills = []
                    };

                    _games.Add(currentGame);
                }
                else if (line.Contains("Kill") && currentGame != null)
                {
                    currentGame.TotalKills++;

                    var match = Regex.Match(line, @"Kill: \d+ \d+ \d+: (.+?) killed (.+?) by", RegexOptions.Compiled);

                    if (match.Success)
                    {
                        string killer = match.Groups[1].Value;
                        string victim = match.Groups[2].Value;

                        if (killer != "<world>")
                        {
                            if (!currentGame.Players.Contains(killer))
                            {
                                currentGame.Players.Add(killer);
                            }
                        }

                        if (!currentGame.Players.Contains(victim))
                        {
                            currentGame.Players.Add(victim);
                        }

                        if (killer == "<world>")
                        {
                            currentGame.Kills.TryAdd(victim, 0);
                            currentGame.Kills[victim]--;
                        }
                        else if (killer == victim)
                        {
                            currentGame.Kills.TryAdd(killer, 0);
                            currentGame.Kills[killer]--;
                        }
                        else
                        {
                            currentGame.Kills.TryAdd(killer, 0);
                            currentGame.Kills[killer]++;
                        }
                    }
                }
                else if (line.Contains("ClientUserinfoChanged", StringComparison.Ordinal) && currentGame != null)
                {
                    var match = Regex.Match(line, @"n\\(.*?)\\");

                    if (match.Success)
                    {
                        string player = match.Groups[1].Value;
                        if (player != "<world>" && !currentGame.Players.Contains(player))
                        {
                            currentGame.Players.Add(player);
                        }
                    }
                }
                else if (line.Contains("ShutdownGame") && currentGame != null)
                {
                    foreach (var player in currentGame.Players)
                    {
                        currentGame.Kills.TryAdd(player, 0);
                    }
                }
            }
        }

        public List<Game> GetAllGames()
        {
            return _games;
        }

        public Game? GetGameName(string name)
        {
            return _games.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}