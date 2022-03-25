using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.System.Model.Master;

namespace Chessy.Game.Model.System
{
    sealed class SystemsModelGameMaster : SystemModelGameAbs
    {
        internal readonly IncreaseWindSnowyS_M IncreaseWindSnowyS_M;
        internal readonly GiveTakeToolWeaponS_M GiveTakeToolWeaponS_M;
        internal readonly CurcularAttackKingS_M CurcularAttackKingS_M;
        internal readonly AttackUnit_M AttackUnit_M;
        internal readonly BuyS_M BuyS_M;
        internal readonly MeltS_M MeltS_M;
        internal readonly BuyBuildingS_M BuyBuildingS_M;
        internal readonly SetUnitS_M SetUnitS_M;
        internal readonly GetHeroS_M GetHeroS_M;
        internal readonly ReadyS_M ReadyS_M;
        internal readonly WorldMeltIceWallUpdateS_M WorldMeltIceWallUpdateS_M;
        internal readonly FirePawnS_M FirePawnS_M;
        internal readonly PutOutFirePawnS_M PutOutFirePawnS_M;
        internal readonly SeedPawnS_M SeedPawnS_M;
        internal readonly BuildFarmS_M BuildFarmS_M;
        internal readonly SetConditionUnitS_M SetConditionUnitS_M;
        internal readonly ShiftUnitS_M ShiftUnitS_M;
        internal readonly DonerS_M DonerS_M;
        internal readonly UpdateS_M UpdateS_M;
        internal readonly ChangeCornerArcherS_M ChangeCornerArcherS_M;
        internal readonly ChangeDirectionWindMS ChangeDirectionWindS_M;
        internal readonly StunElfemaleS_M StunElfemaleS_M;
        internal readonly DestroyBuildingS_M DestroyBuildingS_M;
        internal readonly FireArcherS_M FireArcherS_M;
        internal readonly GrowAdultForestS_M GrowAdultForestS_M;
        internal readonly UnitEatFoodUpdateS_M UnitEatFoodUpdateS_M;
        internal readonly GetDataCellsS_M GetDataCellsS;

        internal SystemsModelGameMaster(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            IncreaseWindSnowyS_M = new IncreaseWindSnowyS_M(eMGame);
            GiveTakeToolWeaponS_M = new GiveTakeToolWeaponS_M(sMGame.UnitSystems.SetExtraTWS, eMGame);
            CurcularAttackKingS_M = new CurcularAttackKingS_M(sMGame.UnitSystems.AttackUnitS, sMGame.UnitSystems.AttackShieldS, eMGame);
            AttackUnit_M = new AttackUnit_M(sMGame.UnitSystems.AttackUnitS, sMGame.UnitSystems.AttackShieldS, sMGame.UnitSystems.ShiftUnitS, eMGame);
            BuyS_M = new BuyS_M(eMGame);
            MeltS_M = new MeltS_M(eMGame);
            BuyBuildingS_M = new BuyBuildingS_M(eMGame);
            SetUnitS_M = new SetUnitS_M(sMGame.UnitSystems.SetNewUnitS, eMGame);
            GetHeroS_M = new GetHeroS_M(eMGame);
            ReadyS_M = new ReadyS_M(eMGame);
            WorldMeltIceWallUpdateS_M = new WorldMeltIceWallUpdateS_M(eMGame);
            FirePawnS_M = new FirePawnS_M(eMGame);
            PutOutFirePawnS_M = new PutOutFirePawnS_M(eMGame);
            SeedPawnS_M = new SeedPawnS_M(eMGame);
            BuildFarmS_M = new BuildFarmS_M(sMGame.BuildS, eMGame);
            SetConditionUnitS_M = new SetConditionUnitS_M(sMGame.BuildS, eMGame);
            ShiftUnitS_M = new ShiftUnitS_M(sMGame.UnitSystems.ShiftUnitS, eMGame);
            ChangeCornerArcherS_M = new ChangeCornerArcherS_M(eMGame);
            ChangeDirectionWindS_M = new ChangeDirectionWindMS(eMGame);
            StunElfemaleS_M = new StunElfemaleS_M(eMGame);
            DestroyBuildingS_M = new DestroyBuildingS_M(eMGame);
            FireArcherS_M = new FireArcherS_M(eMGame);
            GrowAdultForestS_M = new GrowAdultForestS_M(eMGame);
            UpdateS_M = new UpdateS_M(sMGame, eMGame);
            DonerS_M = new DonerS_M(UpdateS_M, eMGame);
            UnitEatFoodUpdateS_M = new UnitEatFoodUpdateS_M(sMGame.UnitSystems.KillUnitS, eMGame);
            GetDataCellsS = new GetDataCellsS_M(eMGame);
        }
    }
}