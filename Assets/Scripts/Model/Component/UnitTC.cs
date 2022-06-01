using Chessy.Common.Extension;

namespace Chessy.Game
{
    public struct UnitTC
    {
        public UnitTypes UnitT { get; internal set; }

        public bool Is(params UnitTypes[] units) => UnitT.Is(units);
        public bool IsMelee(in ToolWeaponTypes mainTW) => UnitT.IsMelee(mainTW);
        public bool IsGod => UnitT.IsGod();
        public bool HaveUnit => UnitT.HaveUnit();
        public bool IsAnimal => UnitT.IsAnimal();
    }
}
