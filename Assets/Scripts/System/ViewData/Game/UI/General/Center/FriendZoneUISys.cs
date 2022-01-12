using Game.Common;
using static Game.Game.EntityCenterFriendUIPool;

namespace Game.Game
{
    struct FriendZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            Friend<ButtonUIC>().SetActiveParent(false);

            if (GameModeC.IsGameMode(GameModes.WithFriendOff))
            {
                if (EntityPool.FriendZone<IsActivatedC>().IsActivated)
                {
                    Friend<ButtonUIC>().SetActiveParent(true);

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
