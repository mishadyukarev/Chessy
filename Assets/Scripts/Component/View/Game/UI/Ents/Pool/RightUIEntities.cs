using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public struct RightUIEntities
    {
        static Dictionary<ButtonTypes, RightUniqueUIE> _uniques;
        static Dictionary<string, RightUniqueZoneUIE> _uniqueZones;

        static Dictionary<ButtonTypes, RightBuildUIE> _buildings;
        static Dictionary<string, RightBuildingZoneUIE> _buildingZones;

        public static RightUniqueUIE Unique(in ButtonTypes but) => _uniques[but];
        public static RightUniqueZoneUIE UniqueZone(in ButtonTypes but, in UniqueAbilityTypes ability) => _uniqueZones[but.ToString() + ability];

        public static RightBuildUIE Building(in ButtonTypes but) => _buildings[but];
        public static RightBuildingZoneUIE BuildingZone(in ButtonTypes but, in BuildingTypes build) => _buildingZones[but.ToString() + build];





        public RightUIEntities(in EcsWorld gameW)
        {
            var rightZone = CanvasC.FindUnderCurZone("RightZone").transform;
            new UIEntRight(gameW, rightZone.gameObject);
            new UIEntRightStats(gameW, rightZone.gameObject);


            new UIEntExtraTW(gameW, rightZone);
            new UIEntRightEffects(gameW, rightZone);
            var conditionZone = rightZone.Find("ConditionZone");
            new RightProtectUIE(gameW, conditionZone);
            new RightRelaxUIE(gameW, conditionZone);


            _uniques = new Dictionary<ButtonTypes, RightUniqueUIE>();
            _uniqueZones = new Dictionary<string, RightUniqueZoneUIE>();
            _buildings = new Dictionary<ButtonTypes, RightBuildUIE>();
            _buildingZones = new Dictionary<string, RightBuildingZoneUIE>();


            var uniqueZone = rightZone.Find("Unique");
            var buildingZone = rightZone.Find("Building");


            for (var buttonT = ButtonTypes.First; buttonT < ButtonTypes.End; buttonT++)
            {
                var button = uniqueZone.Find(buttonT.ToString());
                button.Find("Building").gameObject.SetActive(false);

                _uniques.Add(buttonT, new RightUniqueUIE(gameW, button));
                for (var ability = UniqueAbilityTypes.None + 1; ability < UniqueAbilityTypes.End; ability++)
                {
                    var zone = button.Find("Unique");
                    _uniqueZones.Add(buttonT.ToString() + ability, new RightUniqueZoneUIE(gameW, zone.Find(ability.ToString())));
                }


                button = buildingZone.Find(buttonT.ToString());
                button.Find("Cooldown").gameObject.SetActive(false);
                button.Find("Unique").gameObject.SetActive(false);

                _buildings.Add(buttonT, new RightBuildUIE(gameW, button));
                for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
                {
                    if (buildT == BuildingTypes.Farm || buildT == BuildingTypes.Mine || buildT == BuildingTypes.City)
                    {
                        var zone = button.Find("Building");
                        _buildingZones.Add(buttonT.ToString() + buildT, new RightBuildingZoneUIE(gameW, zone.Find(buildT.ToString())));
                    }
                }
            }
        }
    }
}