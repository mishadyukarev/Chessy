using Game.Common;
using static Game.Game.CenterFriendUIE;

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

                    if (EntWhoseMove.CurPlayerI == PlayerTypes.First)
                    {
                        Friend<TextMPUGUIC>().Text = "1";
                    }
                    else
                    {
                        Friend<TextMPUGUIC>().Text = "2";
                    }
                }
            }
        }
    }
}
