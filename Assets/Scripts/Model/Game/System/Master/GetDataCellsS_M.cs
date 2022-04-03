using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed class GetDataCellsS_M : SystemModelGameAbs
    {
        readonly GetVisibleUnitS _getVisibleS;
        readonly GetCellForArsonArcherS _getCellForArsonArcherS;
        readonly GetCellsForAttackArcherS _getCellsForAttackArcherS;
        readonly GetCellsForShiftUnitS _getCellsForShiftUnitS;
        readonly GetEffectsForUnitsS _getEffectsForUnitsS;
        readonly GetDamageUnitsS _getDamageUnitsS;
        readonly GetAttackMeleeCellsS _getAttackMeleeCellsS;
        readonly GetAbilityUnitS_M _getAbilityUnitS;
        readonly PawnGetExtractAdultForestS _pawnGetExtractAdultForestS;
        readonly PawnExtractHillS _pawnExtractHillS;
        readonly GetTrailsVisibleS _getTrailsVisibleS;
        readonly GetBuildingVisibleS _getBuildingVisibleS;
        readonly GetWoodcutterExtractCellsS _getWoodcutterExtractCellsS;
        readonly GetFarmExtractCellsS _getFarmExtractCellsS;

        internal GetDataCellsS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
            _getBuildingVisibleS = new GetBuildingVisibleS(sMC, eMC, sMG, eMG);
            _getTrailsVisibleS = new GetTrailsVisibleS(sMC, eMC, sMG, eMG);
            _getWoodcutterExtractCellsS = new GetWoodcutterExtractCellsS(sMC, eMC, sMG, eMG);
            _getFarmExtractCellsS = new GetFarmExtractCellsS(sMC, eMC, sMG, eMG);
            _getAttackMeleeCellsS = new GetAttackMeleeCellsS(sMC, eMC, sMG, eMG);
            _getAbilityUnitS = new GetAbilityUnitS_M(sMC, eMC, sMG, eMG);
            _pawnGetExtractAdultForestS = new PawnGetExtractAdultForestS(sMC, eMC, sMG, eMG);
            _pawnExtractHillS = new PawnExtractHillS(sMC, eMC, sMG, eMG);
            _getVisibleS = new GetVisibleUnitS(sMC, eMC, sMG, eMG);
            _getCellForArsonArcherS = new GetCellForArsonArcherS(sMC, eMC, sMG, eMG);
            _getCellsForAttackArcherS = new GetCellsForAttackArcherS(sMC, eMC, sMG, eMG);
            _getCellsForShiftUnitS = new GetCellsForShiftUnitS(sMC, eMC, sMG, eMG);
            _getEffectsForUnitsS = new GetEffectsForUnitsS(sMC, eMC, sMG, eMG);
            _getDamageUnitsS = new GetDamageUnitsS(sMC, eMC, sMG, eMG);
        }

        internal void Run()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                eMG.PlayerInfoE(playerT).WhereKingEffects.Clear();
            }


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                _pawnGetExtractAdultForestS.Get(cell_0);
                _pawnExtractHillS.Get(cell_0);

                _getVisibleS.Get(cell_0);
                _getEffectsForUnitsS.Get(cell_0);

                _getDamageUnitsS.Get(cell_0);
                _getAbilityUnitS.Get(cell_0);

                //_getTrailsVisibleS.Get(cell_0);


                _getWoodcutterExtractCellsS.Get(cell_0);
                _getFarmExtractCellsS.Get(cell_0);
                //_getBuildingVisibleS.Get(cell_0);
            }

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                _getCellsForShiftUnitS.Get(cell_0);
                _getAttackMeleeCellsS.Get(cell_0);
                _getCellsForAttackArcherS.Get(cell_0);
                _getCellForArsonArcherS.Get(cell_0);
            }

 
        }
    }
}