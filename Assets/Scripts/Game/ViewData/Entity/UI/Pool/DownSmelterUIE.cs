using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct DownSmelterUIE
    {
        public readonly GameObjectVC Zone;
        public readonly ButtonUIC ButtonUIC;

        internal DownSmelterUIE(in Transform leftZone)
        {
            var zone = leftZone.Find("Smelter+");

            var button = zone.Find("Toggler+").Find("Button+").GetComponent<Button>();

            Zone = new GameObjectVC(zone.gameObject);
            ButtonUIC = new ButtonUIC(button);
        }
    }
}