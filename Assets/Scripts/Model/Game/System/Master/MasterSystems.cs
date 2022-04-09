using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System.Master;

namespace Chessy.Game.Model.System
{
    sealed class MasterSystems
    {
        #region Abilities

        internal readonly IncreaseWindSnowyS_M IncreaseWindSnowyS_M;
        internal readonly CurcularAttackKingS_M CurcularAttackKingS_M;
        internal readonly FirePawnS_M FirePawnS_M;
        internal readonly PutOutFirePawnS_M PutOutFirePawnS_M;
        internal readonly ChangeCornerArcherS_M ChangeCornerArcherS_M;
        internal readonly StunElfemaleS_M StunElfemaleS_M;
        internal readonly FireArcherS_M FireArcherS_M;
        internal readonly GrowAdultForestS_M GrowAdultForestS_M;
        internal readonly DestroyBuildingS_M DestroyBuildingS_M;
        internal readonly ChangeDirectionWindMS ChangeDirectionWindS_M;

        #endregion


        internal readonly BuyS_M BuyS_M;
        internal readonly MeltS_M MeltS_M;
        internal readonly BuyBuildingS_M BuyBuildingS_M;
        internal readonly GetHeroS_M GetHeroS_M;
        internal readonly ReadyS_M ReadyS_M;
        internal readonly TryExecuteDoneS_M DonerS_M;
        internal readonly AttackUnit_M AttackUnit_M;
        internal readonly SetUnitS_M SetUnitS_M;
        internal readonly TryShiftUnitS_M ShiftUnitS_M;
        internal readonly SeedPawnS_M SeedPawnS_M;
        internal readonly BuildFarmS_M BuildFarmS_M;
        internal readonly SetConditionUnitS_M SetConditionUnitS_M;
        internal readonly GiveTakeToolWeaponS_M GiveTakeToolWeaponS_M;

        internal readonly RainyGiveWaterToUnitsAroundS_M RainyGiveWaterToUnitsAroundS_M;


        internal readonly GetDataCellsS_M GetDataCellsS_M;

        internal readonly UpdateS_M UpdateS_M;

        internal MasterSystems(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            IncreaseWindSnowyS_M = new IncreaseWindSnowyS_M(sMC, eMC, sMG, eMG);
            BuyS_M = new BuyS_M(sMC, eMC, sMG, eMG);
            MeltS_M = new MeltS_M(sMC, eMC, sMG, eMG);
            BuyBuildingS_M = new BuyBuildingS_M(sMC, eMC, sMG, eMG);
            GetHeroS_M = new GetHeroS_M(sMC, eMC, sMG, eMG);
            ReadyS_M = new ReadyS_M(sMC, eMC, sMG, eMG);
            UpdateS_M = new UpdateS_M(sMC, eMC, sMG, eMG);
            DonerS_M = new TryExecuteDoneS_M(sMC, eMC, sMG, eMG);
            GetDataCellsS_M = new GetDataCellsS_M(sMC, eMC, sMG, eMG);
            AttackUnit_M = new AttackUnit_M(sMC, eMC, sMG, eMG);
            SetUnitS_M = new SetUnitS_M(sMC, eMC, sMG, eMG);
            ShiftUnitS_M = new TryShiftUnitS_M(sMC, eMC, sMG, eMG);
            SeedPawnS_M = new SeedPawnS_M(sMC, eMC, sMG, eMG);
            BuildFarmS_M = new BuildFarmS_M(sMC, eMC, sMG, eMG);
            SetConditionUnitS_M = new SetConditionUnitS_M(sMC, eMC, sMG, eMG);
            GiveTakeToolWeaponS_M = new GiveTakeToolWeaponS_M(sMC, eMC, sMG, eMG);
            CurcularAttackKingS_M = new CurcularAttackKingS_M(sMC, eMC, sMG, eMG);
            FirePawnS_M = new FirePawnS_M(sMC, eMC, sMG, eMG);
            PutOutFirePawnS_M = new PutOutFirePawnS_M(sMC, eMC, sMG, eMG);
            ChangeCornerArcherS_M = new ChangeCornerArcherS_M(sMC, eMC, sMG, eMG);
            StunElfemaleS_M = new StunElfemaleS_M(sMC, eMC, sMG, eMG);
            FireArcherS_M = new FireArcherS_M(sMC, eMC, sMG, eMG);
            GrowAdultForestS_M = new GrowAdultForestS_M(sMC, eMC, sMG, eMG);
            DestroyBuildingS_M = new DestroyBuildingS_M(sMC, eMC, sMG, eMG);
            ChangeDirectionWindS_M = new ChangeDirectionWindMS(sMC, eMC, sMG, eMG);

            RainyGiveWaterToUnitsAroundS_M = new RainyGiveWaterToUnitsAroundS_M(sMC, eMC, sMG, eMG);
        }
    }
}