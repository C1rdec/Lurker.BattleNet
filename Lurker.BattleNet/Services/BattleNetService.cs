using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Lurker.BattleNet.Models;
using Lurker.Common.Services;
using static System.Environment;

namespace Lurker.BattleNet.Services
{
    public class BattleNetService : ServiceBase<BattleNetGame>
    {
        protected override string ProcessName => "BattleNet";

        protected override string OpenLink => string.Empty;

        private static string ConfigFilePath => Path.Combine(GetFolderPath(SpecialFolder.ApplicationData), "Battle.net", "Battle.net.config");

        public override List<BattleNetGame> FindGames()
        {
            var games = new List<BattleNetGame>();
            if (!File.Exists(ConfigFilePath))
            {
                return games;
            }

            var document = JsonDocument.Parse(File.ReadAllText(ConfigFilePath));
            var defaultInstallPath = document.RootElement.GetProperty("Client").GetProperty("Install").GetProperty("DefaultInstallPath").GetString();

            if (string.IsNullOrEmpty(defaultInstallPath))
            {
                return games;
            }

            try
            {
                foreach (var directory in Directory.GetDirectories(defaultInstallPath))
                {
                    var game = new BattleNetGame(directory);
                    game.Initialize();
                    if (string.IsNullOrEmpty(game.ExeFilePath))
                    {
                        continue;
                    }

                    games.Add(game);
                }
            }
            catch
            {
            }

            return games;
        }
    }
}
