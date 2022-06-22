using Chessy.Common;
using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game.Model.Entity.Cell.Unit
{
    public sealed class UnitEs
    {
        readonly Dictionary<ButtonTypes, EffectTypes> _effectBars = new Dictionary<ButtonTypes, EffectTypes>();

        public UnitMainE MainE = new UnitMainE();
        public UnitStatsE StatsE;
        public UnitEffectsE EffectsE;
        public UnitAttackE AttackE = new UnitAttackE(new HashSet<byte>(), new HashSet<byte>());
        public ToolWeaponMainUnitC MainToolWeaponE;
        public ExtraToolWeaponE ExtraToolWeaponE;
        public WhoLastDiedHereE WhoLastDiedHereE;
        public CellUnitExtractE ExtractE;
        public ShiftUnitE ShiftE = new ShiftUnitE(new float[StartValues.CELLS], new HashSet<byte>());
        public AbilityUnitE AbilityE = new AbilityUnitE(new float[(byte)AbilityTypes.End - 1]);


        internal void SetEffect(in ButtonTypes buttonT, in EffectTypes effectT) => _effectBars[buttonT] = effectT;
        public EffectTypes Effect(in ButtonTypes buttonT) => _effectBars[buttonT];


        internal UnitEs()
        {
            for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
            {
                _effectBars.Add(buttonT, default);
            }
        }
    }
}