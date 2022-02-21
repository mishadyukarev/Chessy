using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct LeftSmelterUIEs
    {
        public readonly GameObjectVC Zone;
        public readonly LeftSmelterTogglerUIE TogglerE;

        internal LeftSmelterUIEs(in Transform leftZone, in EcsWorld gameW)
        {
            var zone = leftZone.Find("Smelter+");

            var button = zone.Find("Toggler+").Find("Button+").GetComponent<Button>();

            Zone = new GameObjectVC(zone.gameObject);
            TogglerE = new LeftSmelterTogglerUIE(button, gameW);
        }
    }
}