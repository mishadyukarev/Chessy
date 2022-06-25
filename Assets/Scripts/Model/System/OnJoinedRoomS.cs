using Chessy.Common;

namespace Chessy.Model
{
    public sealed partial class SystemsModel : IUpdate
    {
        public void OnJoinedRoom()
        {
            ToggleScene(SceneTypes.Game);
            StartGame(_e.GameModeT == GameModeTypes.TrainingOffline);
            SyncDataM();
        }
    }
}