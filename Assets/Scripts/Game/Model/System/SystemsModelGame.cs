using Chessy.Game.System.Model.Master;

namespace Chessy.Game.System.Model
{
    public readonly struct SystemsModelGame
    {
        public readonly UpdateS UpdateS;
        public readonly SelectorS SelectorS;
        public readonly AttackUnitS AttackUnitS;
        public readonly KillUnitS KillUnitS;
        public readonly AttackShieldS AttackShieldS;
        public readonly SetLastDiedS SetLastDiedS;
        public readonly MistakeS MistakeS;


        #region UI

        //Down
        public readonly DoneClickS DoneClickS;
        public readonly OpenCityClickS OpenCityClickS;
        public readonly GetHeroDownS GetHeroClickDownS;
        public readonly GetPawnS GetPawnClickS;
        public readonly ToggleToolWeaponS ToggleToolWeaponClickS;
        //Left
        public readonly EnvironmentInfoS EnvironmentInfoClickS;
        public readonly ReadyS ReadyClickS;
        public readonly GetKingClickS GetKingClickS;
        public readonly BuildBuildingClickS BuildBuildingClickS;
        //Center
        public readonly GetHeroClickCenterS GetHeroClickCenterS;
        //Right
        public readonly AbilityClickS AbilityClickS;
        public readonly ConditionClickS ConditionClickS;

        #endregion


        #region Master

        public readonly IncreaseWindSnowyS_M IncreaseWindSnowyS_M;
        public readonly UpdateS_M UpdateS_M;
        public readonly GiveTakeToolWeaponS_M GiveTakeToolWeaponS_M;
        public readonly CurcularAttackKingS_M CurcularAttackKingS_M;
        public readonly AttackUnit_M AttackUnit_M;
        public readonly UnitEatFoodUpdateS_M UnitEatFoodUpdateS_M;
        public readonly BuyS_M BuyS_M;
        public readonly MeltS_M MeltS_M;
        public readonly BuyBuildingS_M BuyBuildingS_M;

        public readonly WorldMeltIceWallUpdateMS WorldMeltIceWallUpdateS_M;

        #endregion
    }
}