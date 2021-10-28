using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class FriendZoneUISys : IEcsRunSystem
    {
        public void Run()
        {
            FriendZoneViewUIC.SetActiveParent(false);

            if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
            {
                if (FriendZoneDataUIC.IsActiveFriendZone)
                {
                    FriendZoneViewUIC.SetActiveParent(true);

                    if (WhoseMoveC.CurPlayer == PlayerTypes.First)
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
