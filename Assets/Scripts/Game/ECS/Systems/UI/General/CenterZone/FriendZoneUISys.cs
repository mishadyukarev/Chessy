using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class FriendZoneUISys : IEcsRunSystem
    {
        private EcsFilter<FriendZoneDataUICom, FriendZoneViewUICom> _friendZoneUIFilt = default;

        public void Run()
        {
            ref var friendViewCom = ref _friendZoneUIFilt.Get2(0);

            friendViewCom.SetActiveParent(false);

            if (GameModesCom.IsGameMode(GameModes.WithFriendOff))
            {
                ref var friendDataCom = ref _friendZoneUIFilt.Get1(0);

                if (friendDataCom.IsActiveFriendZone)
                {
                    friendViewCom.SetActiveParent(true);

                    if (WhoseMoveCom.CurPlayer == PlayerTypes.First)
                    {
                        friendViewCom.SetTextPlayerMotion("1");
                    }
                    else
                    {
                        friendViewCom.SetTextPlayerMotion("2");
                    }
                }
            }
        }
    }
}
