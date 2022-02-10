using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct DownUpgradeUIE
    {
        static Entity _upgrade;

        public static ref ButtonUIC ButtonUIC => ref _upgrade.Get<ButtonUIC>();

        public DownUpgradeUIE(in EcsWorld gameW, Transform downZone)
        {
            var upgZone = downZone.Find(CellClickTypes.UpgradeUnit.ToString());

            _upgrade = gameW.NewEntity()
                .Add(new ButtonUIC(upgZone.Find("Button").GetComponent<Button>()));
        }
    }
}