using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.UI.Game.General.Center;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Center;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.CenterZone
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
                        friendViewCom.SetTextPlayerMotion("1 player");
                    }
                    else
                    {
                        friendViewCom.SetTextPlayerMotion("2 player");
                    }
                }
            }
        }
    }
}
