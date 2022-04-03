using Chessy.Common;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    struct CenterFriendUIS
    {
        public void Run(in GameModeTC gameModeTC, in EntitiesViewUIGame eUI, in EntitiesModelGame e)
        {
            eUI.CenterEs.FriendE.ButtonC.SetActiveParent(false);

            if (gameModeTC.Is(GameModes.WithFriendOff))
            {
                if (e.ZoneInfoC.IsActiveFriend)
                {
                    eUI.CenterEs.FriendE.TextC.SetActiveParent(true);

                    if (e.CurPlayerITC.PlayerT == PlayerTypes.First)
                    {
                        eUI.CenterEs.FriendE.TextC.TextUI.text = "1";
                    }
                    else
                    {
                        eUI.CenterEs.FriendE.TextC.TextUI.text = "2";
                    }
                }
            }
        }
    }
}
