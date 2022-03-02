using Chessy.Common;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct RightUIEs
    {
        readonly Dictionary<ButtonTypes, RightUniqueUIE> _uniques;
        readonly Dictionary<string, GameObjectVC> _uniqueZones;
        readonly Dictionary<byte, RightEffectUIE> _effects;
        public RightUniqueUIE Unique(in ButtonTypes but) => _uniques[but];
        public GameObjectVC UniqueZone(in ButtonTypes but, in AbilityTypes ability) => _uniqueZones[but.ToString() + ability];
        public RightEffectUIE Effect(in byte numberEffect) => _effects[numberEffect];


        public readonly GameObjectVC Zone;
        public RightStatsUIEs StatsE;
        public RightProtectUIE ProtectE;
        public RightRelaxUIE RelaxE;

        public RightUIEs(in bool def)
        {
            var rightZone = CanvasC.FindUnderCurZone("RightZone").transform;
            Zone = new GameObjectVC(rightZone.gameObject);
            StatsE = new RightStatsUIEs(rightZone.gameObject);


            new UIEntExtraTW(rightZone);
            new RightEffectsUIE(rightZone);

            var conditionZone = rightZone.Find("ConditionZone");
            ProtectE = new RightProtectUIE(conditionZone);
            RelaxE = new RightRelaxUIE(conditionZone);


            _uniques = new Dictionary<ButtonTypes, RightUniqueUIE>();
            _uniqueZones = new Dictionary<string, GameObjectVC>();

            var uniqueZone = rightZone.Find("Unique");
            var buildingZone = rightZone.Find("Building");


            for (var buttonT = ButtonTypes.First; buttonT < ButtonTypes.End; buttonT++)
            {
                var button = uniqueZone.Find(buttonT.ToString());

                _uniques.Add(buttonT, new RightUniqueUIE(button));
                for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
                {
                    var zone = button.Find("Zones");
                    _uniqueZones.Add(buttonT.ToString() + ability, new GameObjectVC(zone.Find(ability.ToString()).gameObject));
                }
            }


            _effects = new Dictionary<byte, RightEffectUIE>();

            var effectZone = rightZone.Find("Effects");

            for (byte idx_effect = 0; idx_effect < 5; idx_effect++)
            {
                _effects.Add(idx_effect, new RightEffectUIE(effectZone, idx_effect));
            }
        }
    }
}