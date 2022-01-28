using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public struct RightUIEs
    {
        Dictionary<ButtonTypes, RightUniqueUIE> _uniques;
        Dictionary<string, RightUniqueZoneUIE> _uniqueZones;

        public RightZoneUIE Zone { get; private set; }

        public RightUniqueUIE Unique(in ButtonTypes but) => _uniques[but];
        public RightUniqueZoneUIE UniqueZone(in ButtonTypes but, in AbilityTypes ability) => _uniqueZones[but.ToString() + ability];


        public RightUIEs(in EcsWorld gameW)
        {
            var rightZone = CanvasC.FindUnderCurZone("RightZone").transform;
            Zone = new RightZoneUIE(gameW, rightZone.gameObject);
            new UIEntRightStats(gameW, rightZone.gameObject);


            new UIEntExtraTW(gameW, rightZone);
            new UIEntRightEffects(gameW, rightZone);
            var conditionZone = rightZone.Find("ConditionZone");
            new RightProtectUIE(gameW, conditionZone);
            new RightRelaxUIE(gameW, conditionZone);


            _uniques = new Dictionary<ButtonTypes, RightUniqueUIE>();
            _uniqueZones = new Dictionary<string, RightUniqueZoneUIE>();

            var uniqueZone = rightZone.Find("Unique");
            var buildingZone = rightZone.Find("Building");


            for (var buttonT = ButtonTypes.First; buttonT < ButtonTypes.End; buttonT++)
            {
                var button = uniqueZone.Find(buttonT.ToString());

                _uniques.Add(buttonT, new RightUniqueUIE(gameW, button));
                for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
                {
                    var zone = button.Find("Zones");
                    _uniqueZones.Add(buttonT.ToString() + ability, new RightUniqueZoneUIE(gameW, zone.Find(ability.ToString())));
                }
            }
        }
    }
}