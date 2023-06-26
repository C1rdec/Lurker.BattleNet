using System.IO;
using System.Threading.Tasks;
using Lurker.Common.Models;

namespace Lurker.BattleNet.Models
{
    public class BattleNetGame : GameBase
    {
        private readonly string _installationFolder;

        public BattleNetGame(string installationFolder)
        {
            _installationFolder = installationFolder;
            Name = Path.GetFileName(_installationFolder);
        }

        public override LauncherType Launcher => LauncherType.BattleNet;

        public override string Id => _installationFolder;

        public override void Initialize()
        {
            SetExeFile(_installationFolder, "Launcher");
        }

        public override Task Open()
            => CliWrap.Cli.Wrap(ExeFilePath).ExecuteAsync();
    }
}
