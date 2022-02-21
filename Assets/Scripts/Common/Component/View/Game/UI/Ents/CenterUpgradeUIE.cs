using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct CenterUpgradeUIE
    {
        static Entity _ents;
        static Dictionary<UnitTypes, Entity> _units;
        static Dictionary<BuildingTypes, Entity> _builds;
        static Entity _upgWater;

        public static ref GameObjectVC Paren => ref _ents.Get<GameObjectVC>();
        public static ref ButtonUIC Units(in UnitTypes unit) => ref _units[unit].Get<ButtonUIC>();
        public static ref ButtonUIC Builds(in BuildingTypes build) => ref _builds[build].Get<ButtonUIC>();
        public static ref ButtonUIC Water => ref _upgWater.Get<ButtonUIC>();


        public CenterUpgradeUIE(in EcsWorld gameW, in Transform centerZone)
        {
            _units = new Dictionary<UnitTypes, Entity>();
            _builds = new Dictionary<BuildingTypes, Entity>();


            var parent = centerZone.Find("PickUpgradeZone");

            _ents = gameW.NewEntity()
                .Add(new GameObjectVC(parent.gameObject));

            for (var unit = UnitTypes.None + 1; unit <= UnitTypes.Scout; unit++)
                _units.Add(unit, gameW.NewEntity()
                    .Add(new ButtonUIC(parent.Find(unit + "_Button").GetComponent<Button>())));

            for (var build = BuildingTypes.Farm; build <= BuildingTypes.Woodcutter; build++)
                _builds.Add(build, gameW.NewEntity()
                    .Add(new ButtonUIC(parent.Find(build + "_Button").GetComponent<Button>())));

            _upgWater = gameW.NewEntity()
                .Add(new ButtonUIC(parent.transform.Find(UnitStatTypes.Water.ToString() + "_Button").GetComponent<Button>()));
        }
    }
}