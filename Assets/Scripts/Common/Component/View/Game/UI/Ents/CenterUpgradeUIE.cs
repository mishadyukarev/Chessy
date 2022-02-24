using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct CenterUpgradeUIE
    {
        static Dictionary<UnitTypes, ButtonUIC> _units;
        static Dictionary<BuildingTypes, ButtonUIC> _builds;

        public static GameObjectVC Paren;
        public static ButtonUIC Water;
        public static ButtonUIC Units(in UnitTypes unit) => _units[unit];
        public static ButtonUIC Builds(in BuildingTypes build) => _builds[build];



        public CenterUpgradeUIE(in Transform centerZone)
        {
            _units = new Dictionary<UnitTypes, ButtonUIC>();
            _builds = new Dictionary<BuildingTypes, ButtonUIC>();


            var parent = centerZone.Find("PickUpgradeZone");

            Paren = new GameObjectVC(parent.gameObject);

            for (var unit = UnitTypes.None + 1; unit <= UnitTypes.Scout; unit++)
                _units.Add(unit, new ButtonUIC(parent.Find(unit + "_Button").GetComponent<Button>()));

            for (var build = BuildingTypes.Farm; build <= BuildingTypes.Woodcutter; build++)
                _builds.Add(build, new ButtonUIC(parent.Find(build + "_Button").GetComponent<Button>()));

            Water = new ButtonUIC(parent.transform.Find(UnitStatTypes.Water.ToString() + "_Button").GetComponent<Button>());
        }
    }
}