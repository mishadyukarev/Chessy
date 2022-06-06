using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game.Model.Entity.Cell.Unit
{
    public sealed class UnitEs
    {
        public UnitMainE MainE = new UnitMainE();
        public StatsE StatsE = new StatsE();
        public UnitEffectsE EffectsE = new UnitEffectsE();
        public UnitAttackE AttackE = new UnitAttackE(new HashSet<byte>(), new HashSet<byte>());
        public MainToolWeaponE MainToolWeaponE = new MainToolWeaponE();
        public ExtraToolWeaponE ExtraToolWeaponE = new ExtraToolWeaponE();
        public WhoLastDiedHereE WhoLastDiedHereE = new WhoLastDiedHereE();
        public CellUnitExtractE ExtractE = new CellUnitExtractE();
        public ShiftUnitE ShiftE = new ShiftUnitE(new float[StartValues.CELLS], new HashSet<byte>());
        public AbilityUnitE AbilityE = new AbilityUnitE(new float[(byte)AbilityTypes.End - 1]);
    }
}