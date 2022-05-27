using Chessy.Common;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    public sealed class OnJoinedRoomS : SystemModel
    {
        internal OnJoinedRoomS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        public void OnJoinedRoom(in Rpc rpc)
        {
            sMG.CommonSs.ToggleScene(SceneTypes.Game);
            sMG.MasterSs.StartGameS.Start(eMG.Common.GameModeT == GameModeTypes.TrainingOffline);
            rpc.SyncAllMaster();
        }
    }
}