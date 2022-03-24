using Chessy.Common.Extension;
using System;

namespace Chessy.Game
{
    public struct UnitTC
    {
        UnitTypes _unitT;

        public UnitTypes Unit
        {
            get => _unitT;
            set => _unitT = value;
        }
        public bool Is(params UnitTypes[] units) => _unitT.Is(units);
        public bool IsMelee(in ToolWeaponTypes mainTW) => _unitT.IsMelee(mainTW);
        public bool IsGod => _unitT.IsGod();
        public bool HaveUnit => _unitT.HaveUnit();
        public bool IsAnimal => _unitT.IsAnimal();

        public UnitTC(in UnitTypes unit) => _unitT = unit;
    }
}
