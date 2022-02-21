using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct CenterKingUIE
    {
        static Entity _entity;

        public static ref GameObjectVC Paren => ref _entity.Get<GameObjectVC>();
        public static ref ButtonUIC Button => ref _entity.Get<ButtonUIC>();

        public CenterKingUIE(in EcsWorld gameW, in Transform centerZone)
        {
            var king = centerZone.Find("KingZone");

            _entity = gameW.NewEntity()
                .Add(new GameObjectVC(king.gameObject))
                .Add(new ButtonUIC(king.Find("SetKing_Button").GetComponent<Button>()));
        }
    }
}
