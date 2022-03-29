using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.System.Model.Master;
using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    internal sealed class CellSs
    {
        #region Unit

        internal readonly SetMainUnitS SetMainS;
        internal readonly SetStatsUnitS SetStatsS;
        internal readonly SetEffectsUnitS SetEffectsS;
        internal readonly SetMainToolWeaponUnitS SetMainTWS;
        internal readonly SetExtraToolWeaponS SetExtraTWS;
        internal readonly SetLastDiedS SetLastDiedS;
        internal readonly AttackShieldS AttackShieldS;
        internal readonly ClearUnitS ClearUnitS;

        internal readonly SetUnitS SetUnitS;
        internal readonly AttackUnitS AttackUnitS;
        internal readonly GetAttackMeleeCellsS GetAttackMeleeCellsS;
        internal readonly GetAbilityUnitS GetAbilityUnitS;
        internal readonly PawnGetExtractAdultForestS PawnGetExtractAdultForestS;
        internal readonly PawnExtractHillS PawnExtractHillS;
        internal readonly GetVisibleUnitS GetVisibleS;
        internal readonly GetCellForArsonArcherS GetCellForArsonArcherS;
        internal readonly GetCellsForAttackArcherS GetCellsForAttackArcherS;
        internal readonly GetCellsForShiftUnitS GetCellsForShiftUnitS;
        internal readonly GetEffectsForUnitsS GetEffectsForUnitsS;
        internal readonly GetDamageUnitsS GetDamageUnitsS;
        internal readonly KillUnitS KillUnitS;

        #endregion


        #region Environment

        internal readonly ClearAllEnvironmentS ClearAllEnvironmentS;

        #endregion


        #region Building

        internal readonly BuildS BuildS;
        internal readonly GetBuildingVisibleS GetBuildingVisibleS;
        internal readonly GetWoodcutterExtractCellsS GetWoodcutterExtractCellsS;
        internal readonly AttackBuildingS DestroyBuildingS;
        internal readonly GetFarmExtractCellsS GetFarmExtractCellsS;

        #endregion



        internal readonly GetTrailsVisibleS GetTrailsVisibleS;


        internal readonly GiveTakeToolWeaponS_M GiveTakeToolWeaponS_M;
        internal readonly CurcularAttackKingS_M CurcularAttackKingS_M;
        internal readonly FirePawnS_M FirePawnS_M;
        internal readonly PutOutFirePawnS_M PutOutFirePawnS_M;
        internal readonly ChangeCornerArcherS_M ChangeCornerArcherS_M;
        internal readonly StunElfemaleS_M StunElfemaleS_M;
        internal readonly FireArcherS_M FireArcherS_M;
        internal readonly GrowAdultForestS_M GrowAdultForestS_M;
        internal readonly DestroyBuildingS_M DestroyBuildingS_M;

        internal readonly ChangeDirectionWindMS ChangeDirectionWindS_M;
  

        internal CellSs(in byte cell_0, in SystemsModelGame sMGame, in EntitiesModelGame eMGame)
        {
            var cellE_0 = eMGame.CellEs(cell_0);

            var cellUnitE = cellE_0.UnitEs;


            #region Unit


            for (byte cell_1 = 0; cell_1 < StartValues.CELLS; cell_1++)
            {
                var cellE_to = eMGame.CellEs(cell_1);
                var cellS_to = sMGame.CellSs(cell_1);

                //_attack[cell_1] = new AttackUnit_M(eMGame, cellE_0, cellE_to, this, cellS_to);
            }




            SetMainS = new SetMainUnitS(cellUnitE.MainE);
            SetStatsS = new SetStatsUnitS(cellUnitE.StatsE);
            SetEffectsS = new SetEffectsUnitS(cellUnitE.EffectsE);
            SetMainTWS = new SetMainToolWeaponUnitS(cellUnitE.MainToolWeaponE);
            SetExtraTWS = new SetExtraToolWeaponS(cellUnitE.ExtraToolWeaponE);
            SetLastDiedS = new SetLastDiedS(cellUnitE.WhoLastDiedHereE, cellUnitE.MainE);
            ClearUnitS = new ClearUnitS(cellUnitE);

            SetUnitS = new SetUnitS(this, cellUnitE);    
            AttackShieldS = new AttackShieldS(cellUnitE.ExtraToolWeaponE);
            GetAttackMeleeCellsS = new GetAttackMeleeCellsS(cellE_0, eMGame);
            GetAbilityUnitS = new GetAbilityUnitS(eMGame);
            PawnGetExtractAdultForestS = new PawnGetExtractAdultForestS(cellE_0, eMGame);
            PawnExtractHillS = new PawnExtractHillS(cellE_0);
            GetVisibleS = new GetVisibleUnitS(cellE_0, eMGame);

            #endregion





            GetBuildingVisibleS = new GetBuildingVisibleS(eMGame);
            GetCellForArsonArcherS = new GetCellForArsonArcherS(eMGame);
            GetCellsForAttackArcherS = new GetCellsForAttackArcherS(eMGame);
            GetCellsForShiftUnitS = new GetCellsForShiftUnitS(eMGame);
            GetEffectsForUnitsS = new GetEffectsForUnitsS(eMGame);
            GetDamageUnitsS = new GetDamageUnitsS(eMGame);
            GetTrailsVisibleS = new GetTrailsVisibleS(eMGame);
            GetWoodcutterExtractCellsS = new GetWoodcutterExtractCellsS(eMGame);
            GetFarmExtractCellsS = new GetFarmExtractCellsS(eMGame);
            KillUnitS = new KillUnitS(cellE_0.UnitMainE, this, eMGame);
            AttackUnitS = new AttackUnitS(cellE_0, KillUnitS);
            ClearAllEnvironmentS = new ClearAllEnvironmentS(cellE_0.EnvironmentEs);


            GiveTakeToolWeaponS_M = new GiveTakeToolWeaponS_M(cellE_0, this, eMGame);
            CurcularAttackKingS_M = new CurcularAttackKingS_M(cellE_0, sMGame, eMGame);
            FirePawnS_M = new FirePawnS_M(cellE_0, eMGame);
            PutOutFirePawnS_M = new PutOutFirePawnS_M(cellE_0, eMGame);
            ChangeCornerArcherS_M = new ChangeCornerArcherS_M(cellE_0, eMGame);
            StunElfemaleS_M = new StunElfemaleS_M(cellE_0, eMGame);
            FireArcherS_M = new FireArcherS_M(cellE_0, eMGame);
            GrowAdultForestS_M = new GrowAdultForestS_M(cellE_0, eMGame);




            BuildS = new BuildS(cellE_0.BuildEs.MainE);
            DestroyBuildingS = new AttackBuildingS(cellE_0, eMGame);
            DestroyBuildingS_M = new DestroyBuildingS_M(cellE_0, DestroyBuildingS, eMGame);

            ChangeDirectionWindS_M = new ChangeDirectionWindMS(cellE_0, eMGame);
        }

    }
}