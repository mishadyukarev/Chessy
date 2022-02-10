using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct LeftSmelterUIEs
    {
        public readonly LeftRightZoneUIE Zone;
        public readonly LeftSmelterTogglerUIE TogglerE;

        internal LeftSmelterUIEs(in Transform leftZone, in EcsWorld gameW)
        {
            var zone = leftZone.Find("Smelter+");

            var button = zone.Find("Toggler+").Find("Button+").GetComponent<Button>();

            Zone = new LeftRightZoneUIE(zone.gameObject, gameW);
            TogglerE = new LeftSmelterTogglerUIE(button, gameW);
        }
    }
}