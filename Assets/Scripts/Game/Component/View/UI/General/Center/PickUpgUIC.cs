using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct PickUpgUIC
    {
        private static GameObject _parent_GO;
        private static Dictionary<UnitTypes, Button> _units;
        private static Dictionary<BuildTypes, Button> _builds;

        public static bool IsActiveZone => _parent_GO.activeSelf;

        public PickUpgUIC(Transform center_Trans)
        {
            _parent_GO = center_Trans.Find("PickUpgradeZone").gameObject;

            _units = new Dictionary<UnitTypes, Button>();
            for (var unit = UnitTypes.First; unit <= UnitTypes.Scout; unit++)
            {
                _units.Add(unit, _parent_GO.transform.Find(unit + "_Button").GetComponent<Button>());
            }

            _builds = new Dictionary<BuildTypes, Button>();
            for (var build = BuildTypes.Farm; build <= BuildTypes.Mine; build++)
            {
                _builds.Add(build, _parent_GO.transform.Find(build + "_Button").GetComponent<Button>());
            }
        }

        public static void SetActiveZone(bool isActive) => _parent_GO.SetActive(isActive);

        public static void AddList(UnitTypes unit, UnityAction action)
        {
            if (!_units.ContainsKey(unit)) throw new Exception();

            _units[unit].onClick.AddListener(action);
        }
        public static void AddList(BuildTypes build, UnityAction action)
        {
            if (!_builds.ContainsKey(build)) throw new Exception();

            _builds[build].onClick.AddListener(action);
        }
    }
}