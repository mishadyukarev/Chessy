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

        internal MasterSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            IncreaseWindSnowyS_M = new IncreaseWindSnowyS_M(sMG, eMG);
            BuyS_M = new BuyS_M(sMG, eMG);
            MeltS_M = new MeltS_M(sMG, eMG);
            BuyBuildingS_M = new BuyBuildingS_M(sMG, eMG);
            GetHeroS_M = new GetHeroS_M(sMG, eMG);
            ReadyS_M = new ReadyS_M(sMG, eMG);
            UpdateS_M = new UpdateS_M(sMG, eMG);
            DonerS_M = new TryExecuteDoneS_M(sMG, eMG);
            GetDataCellsS_M = new GetDataCellsS_M(sMG, eMG);
            AttackUnit_M = new AttackUnit_M(sMG, eMG);
            SetUnitS_M = new SetUnitS_M(sMG, eMG);
            ShiftUnitS_M = new TryShiftUnitS_M(sMG, eMG);
            SeedPawnS_M = new SeedPawnS_M(sMG, eMG);
            BuildFarmS_M = new BuildFarmS_M(sMG, eMG);
            SetConditionUnitS_M = new SetConditionUnitS_M(sMG, eMG);
            GiveTakeToolWeaponS_M = new GiveTakeToolWeaponS_M(sMG, eMG);
            CurcularAttackKingS_M = new CurcularAttackKingS_M(sMG, eMG);
            FirePawnS_M = new FirePawnS_M(sMG, eMG);
            PutOutFirePawnS_M = new PutOutFirePawnS_M(sMG, eMG);
            ChangeCornerArcherS_M = new ChangeCornerArcherS_M(sMG, eMG);
            StunElfemaleS_M = new StunElfemaleS_M(sMG, eMG);
            FireArcherS_M = new FireArcherS_M(sMG, eMG);
            GrowAdultForestS_M = new GrowAdultForestS_M(sMG, eMG);
            DestroyBuildingS_M = new DestroyBuildingS_M(sMG, eMG);
            ChangeDirectionWindS_M = new ChangeDirectionWindMS(sMG, eMG);

            RainyGiveWaterToUnitsAroundS_M = new RainyGiveWaterToUnitsAroundS_M(sMG, eMG);
        }
    }
}