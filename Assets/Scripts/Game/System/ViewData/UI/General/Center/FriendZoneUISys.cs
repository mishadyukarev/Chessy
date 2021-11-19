using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class FriendZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            FriendZoneViewUIC.SetActiveParent(false);

            if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
            {
                if (FriendC.IsActiveFriendZone)
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
