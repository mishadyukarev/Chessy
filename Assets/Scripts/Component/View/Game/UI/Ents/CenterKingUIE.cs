using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct CenterKingUIE
    {
        static Entity _entity;

        public static ref C Button<C>() where C : struct => ref _entity.Get<C>();

        public CenterKingUIE(in EcsWorld gameW, in Transform centerZone)
        {
            _entity = gameW.NewEntity()
                .Add(new ButtonUIC(centerZone.Find("KingZone").Find("SetKing_Button").GetComponent<Button>()));
        }
    }
}
