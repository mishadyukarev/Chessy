using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct CenterUpgradeUIE
    {
        static Dictionary<UnitTypes, Entity> _units;
        static Dictionary<BuildTypes, Entity> _builds;
        static Entity _upgWater;

        public static ref C Units<C>(in UnitTypes unit) where C : struct => ref _units[unit].Get<C>();
        public static ref C Builds<C>(in BuildTypes build) where C : struct => ref _builds[build].Get<C>();
        public static ref C Water<C>() where C : struct => ref _upgWater.Get<C>();


        public CenterUpgradeUIE(in EcsWorld gameW, in Transform centerZone)
        {
            _units = new Dictionary<UnitTypes, Entity>();
            _builds = new Dictionary<BuildTypes, Entity>();

            var parent = centerZone.Find("PickUpgradeZone");

            for (var unit = UnitTypes.First; unit <= UnitTypes.Scout; unit++)
                _units.Add(unit, gameW.NewEntity()
                    .Add(new ButtonUIC(parent.Find(unit + "_Button").GetComponent<Button>())));

            for (var build = BuildTypes.Farm; build <= BuildTypes.Mine; build++)
                _builds.Add(build, gameW.NewEntity()
                    .Add(new ButtonUIC(parent.Find(build + "_Button").GetComponent<Button>())));

            _upgWater = gameW.NewEntity()
                .Add(new ButtonUIC(parent.transform.Find(UnitStatTypes.Water.ToString() + "_Button").GetComponent<Button>()));
        }
    }
}