using Chessy.Common;
using Chessy.Game.Entity.View.UI.Right;
using Chessy.Game.View.UI.Entity.Right;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct RightUIEs
    {
        readonly Dictionary<ButtonTypes, UniqueButtonUIE> _uniques;
        readonly Dictionary<byte, EffectUIE> _effects;
        public UniqueButtonUIE Unique(in ButtonTypes but) => _uniques[but];
        public EffectUIE Effect(in byte numberEffect) => _effects[numberEffect];


        public readonly Chessy.Common.Component.GameObjectVC Zone;
        public RightStatsUIEs StatsE;
        public RightProtectUIE ProtectE;
        public RelaxUIE RelaxE;

        public RightUIEs(in Transform rightZone)
        {
            Zone = new Chessy.Common.Component.GameObjectVC(rightZone.gameObject);
            StatsE = new RightStatsUIEs(rightZone.gameObject);


            new UIEntExtraTW(rightZone);
            new RightEffectsUIE(rightZone);

            var conditionZone = rightZone.Find("ConditionZone");
            ProtectE = new RightProtectUIE(conditionZone);
            RelaxE = new RelaxUIE(conditionZone);


            _uniques = new Dictionary<ButtonTypes, UniqueButtonUIE>();

            var uniqueZone = rightZone.Find("Unique+");
            var buildingZone = rightZone.Find("Building");


            for (var buttonT = ButtonTypes.First; buttonT < ButtonTypes.End; buttonT++)
            {
                var button = uniqueZone.Find(buttonT.ToString() + "+");

                _uniques.Add(buttonT, new UniqueButtonUIE(buttonT, button));
            }


            _effects = new Dictionary<byte, EffectUIE>();

            var effectZone = rightZone.Find("Effects");

            for (byte idx_effect = 0; idx_effect < 5; idx_effect++)
            {
                _effects.Add(idx_effect, new EffectUIE(effectZone, idx_effect));
            }
        }
    }
}