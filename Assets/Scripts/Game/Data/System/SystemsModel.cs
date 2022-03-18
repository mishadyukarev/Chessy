using Chessy.Game.System.Model.Master;

namespace Chessy.Game.System.Model
{
    public readonly struct SystemsModel
    {
        public readonly UpdateS UpdateS;
        public readonly SelectorS SelectorS;
        public readonly AttackUnitS AttackUnitS;
        public readonly KillUnitS KillUnitS;
        public readonly AttackShieldS AttackShieldS;

        public readonly IncreaseWindSnowyS_M IncreaseWindSnowyS_M;
        public readonly UpdateS_M UpdateS_M;
        public readonly GiveTakeToolWeaponS_M GiveTakeToolWeaponS_M;
        public readonly CurcularAttackKingS_M CurcularAttackKingS_M;
        public readonly AttackUnit_M AttackUnit_M;

        public readonly UnitEatFoodUpdateS_M UnitEatFoodUpdateS_M;
    }
}