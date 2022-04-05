using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game.Model.Entity.Cell.Unit
{
    public struct UnitEs
    {
        public UnitMainE MainE;
        public StatsE StatsE;
        public EffectsE EffectsE;
        public UnitAttackE AttackE;
        public MainToolWeaponE MainToolWeaponE;
        public ExtraToolWeaponE ExtraToolWeaponE;
        public WhoLastDiedHereE WhoLastDiedHereE;
        public CellUnitExtractE ExtractE;
        public ShiftUnitE ShiftE;
        public AbilityUnitE AbilityE;

        internal UnitEs(in bool b) : this()
        {
            MainE = new UnitMainE(default);
            AttackE = new UnitAttackE(new HashSet<byte>(), new HashSet<byte>());
            ShiftE = new ShiftUnitE(new float[StartValues.CELLS], new HashSet<byte>());
            AbilityE = new AbilityUnitE(new float[(byte)AbilityTypes.End - 1]);
        }
    }
}