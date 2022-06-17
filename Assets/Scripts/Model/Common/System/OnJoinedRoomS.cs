using Chessy.Common;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        public void OnJoinedRoom(in Rpc rpc)
        {
            CommonSs.ToggleScene(SceneTypes.Game);
            StartGame(_eMG.Common.GameModeT == GameModeTypes.TrainingOffline);
            rpc.SyncAllMaster();
        }
    }
}