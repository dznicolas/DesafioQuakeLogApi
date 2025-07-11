using DesafioQuakeLog.Services;

namespace DesafioQuakeLogTest
{
    public class QuakeLogTests
    {
        [Fact]
        public void GameByName_Returns_WhenExists()
        {
            var service = new GameService();

            var game = service.GetGameName("game_1");

            Assert.NotNull(game);
            Assert.Equal("game_1", game.Name);
        }

        [Fact]
        public void Players_ReturnsExpectedNames()
        {
            var service = new GameService();
            var game = service.GetGameName("game_2");

            Assert.NotNull(game);
            Assert.Contains("Isgalamido", game.Players);
            Assert.Contains("Dono da Bola", game.Players);
            Assert.Contains("Mocinha", game.Players);
        }

        [Fact]
        public void GetPlayerKills_ReturnsCorrectValues()
        {
            var service = new GameService();
            var game = service.GetGameName("game_3");

            Assert.NotNull(game);
            Assert.Equal(-1, game.Kills["Dono da Bola"]);
            Assert.Equal(0, game.Kills["Mocinha"]);
            Assert.Equal(1, game.Kills["Isgalamido"]);
            Assert.Equal(-2, game.Kills["Zeh"]);
        }

        [Fact]
        public void World_IsNotRegisteredAsPlayer()
        {
            var service = new GameService();
            var games = service.GetAllGames();

            foreach (var game in games)
            {
                Assert.DoesNotContain("<world>", game.Players);
                Assert.DoesNotContain("<world>", game.Kills.Keys);
            }
        }

        [Fact]
        public void Players_DoNotContainEmptyOrNullNames()
        {
            var service = new GameService();
            var games = service.GetAllGames();

            foreach (var game in games)
            {
                Assert.DoesNotContain("", game.Players);
                Assert.DoesNotContain(null, game.Players);
            }
        }

        [Fact]
        public void TotalKills_ShouldBeCorrect()
        {
            var service = new GameService();
            var game = service.GetGameName("game_2");

            Assert.NotNull(game);

            var totalIndividualKills = game.Kills.Values.Sum(x => Math.Abs(x));

            Assert.True(game.TotalKills >= totalIndividualKills);
        }
    }
}
