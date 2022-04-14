using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System.Master;

namespace Chessy.Game.Model.System
{
    sealed class MasterSystems
    {
        internal readonly BuyFromMarketS_M BuyFromMarketS;
        internal readonly MeltS_M MeltS;
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

        internal readonly GetDataCellsS_M GetDataCellsS;
        internal readonly UpdateS_M UpdateS;

        internal readonly UnitSystems UnitSs;
        internal readonly BuildingSystems BuildingSs;

        internal MasterSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            BuyFromMarketS = new BuyFromMarketS_M(sMG, eMG);
            MeltS = new MeltS_M(sMG, eMG);
            BuyBuildingS_M = new BuyBuildingS_M(sMG, eMG);
            GetHeroS_M = new GetHeroS_M(sMG, eMG);
            ReadyS_M = new ReadyS_M(sMG, eMG);
            UpdateS = new UpdateS_M(sMG, eMG);
            DonerS_M = new TryExecuteDoneS_M(sMG, eMG);
            GetDataCellsS = new GetDataCellsS_M(sMG, eMG);
            AttackUnit_M = new AttackUnit_M(sMG, eMG);
            SetUnitS_M = new SetUnitS_M(sMG, eMG);
            ShiftUnitS_M = new TryShiftUnitS_M(sMG, eMG);
            SeedPawnS_M = new SeedPawnS_M(sMG, eMG);
            BuildFarmS_M = new BuildFarmS_M(sMG, eMG);
            SetConditionUnitS_M = new SetConditionUnitS_M(sMG, eMG);
            GiveTakeToolWeaponS_M = new GiveTakeToolWeaponS_M(sMG, eMG);

            RainyGiveWaterToUnitsAroundS_M = new RainyGiveWaterToUnitsAroundS_M(sMG, eMG);

            UnitSs = new UnitSystems(sMG, eMG);
            BuildingSs = new BuildingSystems(sMG, eMG);
        }
    }
}