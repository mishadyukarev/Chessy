using Game.Common;
using static Game.Game.EntityCenterFriendUIPool;

namespace Game.Game
{
    struct FriendZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            Friend<ButtonVC>().SetActiveParent(false);

            if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
            {
                if (FriendC.IsActiveFriendZone)
                {
                    Friend<ButtonVC>().SetActiveParent(true);

                    if (WhoseMoveC.CurPlayerI == PlayerTypes.First)
                    {
                        Friend<TextUIC>().Text = "1";
                    }
                    else
                    {
                        Friend<TextUIC>().Text = "2";
                    }
                }
            }
        }
    }
}
