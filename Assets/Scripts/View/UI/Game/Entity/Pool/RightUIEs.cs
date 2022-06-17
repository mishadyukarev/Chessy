using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Game.Entity.View.UI.Right;
using Chessy.Game.View.UI.Entity.Right;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct RightUIEs
    {
        readonly Dictionary<ButtonTypes, UniqueButtonUIE> _uniques;
        readonly EffectUIE[] _effects;
        public UniqueButtonUIE Unique(in ButtonTypes but) => _uniques[but];
        internal EffectUIE Effect(in ButtonTypes buttonT) => _effects[(byte)buttonT];


        public readonly GameObjectVC Zone;
        public RightStatsUIEs StatsEs;
        public RightProtectUIE ProtectE;
        public RelaxUIE RelaxE;

        public RightUIEs(in Transform rightZone)
        {
            Zone = new Chessy.Common.Component.GameObjectVC(rightZone.gameObject);
            StatsEs = new RightStatsUIEs(rightZone.gameObject);


            new UIEntExtraTW(rightZone);
            new RightEffectsUIE(rightZone);

            var conditionZone = rightZone.Find("Condition+");
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


            _effects = new EffectUIE[(byte)ButtonTypes.End];

            var effectZone = rightZone.Find("Effects");

            for (var buttonT = (ButtonTypes)1; buttonT <= ButtonTypes.Fifth; buttonT++)
            {
                _effects[(byte)buttonT] = new EffectUIE(effectZone, (byte)buttonT);
            }
        }
    }
}