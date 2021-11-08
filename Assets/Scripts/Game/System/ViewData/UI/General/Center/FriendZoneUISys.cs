using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class FriendZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            FriendZoneViewUIC.SetActiveParent(false);

            if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
            {
                if (FriendZoneDataUIC.IsActiveFriendZone)
                {
                    FriendZoneViewUIC.SetActiveParent(true);

                    if (WhoseMoveC.CurPlayerI == PlayerTypes.First)
                    {
                        FriendZoneViewUIC.SetTextPlayerMotion("1");
                    }
                    else
                    {
                        FriendZoneViewUIC.SetTextPlayerMotion("2");
                    }
                }
            }
        }
    }
}
