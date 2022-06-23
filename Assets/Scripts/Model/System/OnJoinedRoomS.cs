﻿using Chessy.Common;

namespace Chessy.Model.Model.System
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