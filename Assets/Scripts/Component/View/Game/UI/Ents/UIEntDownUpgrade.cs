using ECS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct UIEntDownUpgrade
    {
        static Entity _upgrade;

        public static ref C Upgrade<C>() where C : struct => ref _upgrade.Get<C>();

        public UIEntDownUpgrade(in EcsWorld gameW, Transform downZone)
        {
            _upgrade = gameW.NewEntity()
                .Add(new ButtonUIC(downZone.Find(CellClickTypes.UpgradeUnit.ToString() + "_Button").GetComponent<Button>()));
        }
    }
}