﻿using Chessy.Common;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        public void OnJoinedRoom()
        {
            CommonSs.ToggleScene(SceneTypes.Game);
            StartGame(_e.Com.GameModeT == GameModeTypes.TrainingOffline);
            SyncDataM();
        }
    }
}