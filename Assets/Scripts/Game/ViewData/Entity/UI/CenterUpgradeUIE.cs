using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct CenterUpgradeUIE
    {
        //readonly Dictionary<UnitTypes, ButtonUIC> _units;
        //readonly Dictionary<BuildingTypes, ButtonUIC> _builds;

        //public readonly GameObjectVC Paren;
        //public readonly ButtonUIC Water;

        //public ButtonUIC Units(in UnitTypes unit) => _units[unit];
        //public ButtonUIC Builds(in BuildingTypes build) => _builds[build];

        public readonly GameObjectVC Parent;


        public CenterUpgradeUIE(in Transform centerZone)
        {
            var parent = centerZone.Find("PickUpgradeZone");

            Parent = new GameObjectVC(parent.gameObject);


            //_units = new Dictionary<UnitTypes, ButtonUIC>();
            //_builds = new Dictionary<BuildingTypes, ButtonUIC>();

            //for (var unit = UnitTypes.None + 1; unit <= UnitTypes.Scout; unit++)
            //    _units.Add(unit, new ButtonUIC(parent.Find(unit + "_Button").GetComponent<Button>()));

            //for (var build = BuildingTypes.Farm; build <= BuildingTypes.Woodcutter; build++)
            //    _builds.Add(build, new ButtonUIC(parent.Find(build + "_Button").GetComponent<Button>()));

            //Water = new ButtonUIC(parent.transform.Find(UnitStatTypes.Water.ToString() + "_Button").GetComponent<Button>());
        }
    }
}