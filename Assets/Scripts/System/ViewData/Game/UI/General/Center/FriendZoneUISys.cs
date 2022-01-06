using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class FriendZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            FriendZoneUIC.SetActiveParent(false);

            if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
            {
                if (FriendC.IsActiveFriendZone)
                {
                    FriendZoneUIC.SetActiveParent(true);

                    if (WhoseMoveC.CurPlayerI == PlayerTypes.First)
                    {
                        FriendZoneUIC.SetTextPlayerMotion("1");
                    }
                    else
                    {
                        FriendZoneUIC.SetTextPlayerMotion("2");
                    }
                }
            }
        }
    }
}
