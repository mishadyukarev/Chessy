using ECS;
using Game.Common;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct RightUIEs
    {
        readonly Dictionary<ButtonTypes, RightUniqueUIE> _uniques;
        readonly Dictionary<string, RightUniqueZoneUIE> _uniqueZones;
        readonly Dictionary<byte, RightEffectUIE> _effects;
        public RightUniqueUIE Unique(in ButtonTypes but) => _uniques[but];
        public RightUniqueZoneUIE UniqueZone(in ButtonTypes but, in AbilityTypes ability) => _uniqueZones[but.ToString() + ability];
        public RightEffectUIE Effect(in byte numberEffect) => _effects[numberEffect];


        public readonly RightZoneUIE Zone;


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


            _effects = new Dictionary<byte, RightEffectUIE>();

            var effectZone = rightZone.Find("Effects");

            for (byte idx_effect = 0; idx_effect < 5; idx_effect++)
            {
                _effects.Add(idx_effect, new RightEffectUIE(effectZone, idx_effect, gameW));
            }
        }
    }
}