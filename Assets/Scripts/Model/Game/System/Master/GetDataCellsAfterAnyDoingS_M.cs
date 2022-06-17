﻿using Chessy.Common;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed class GetDataCellsAfterAnyDoingS_M : SystemModel
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

        readonly Dictionary<EffectTypes, bool> _isFilled = new Dictionary<EffectTypes, bool>();

        internal GetDataCellsAfterAnyDoingS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _getBuildingVisibleS = new GetBuildingVisibleS(sMG, eMG);
            _getTrailsVisibleS = new GetTrailsVisibleS(sMG, eMG);
            _getWoodcutterExtractCellsS = new GetWoodcutterExtractCellsS(sMG, eMG);
            _getFarmExtractCellsS = new GetFarmExtractCellsS(sMG, eMG);
            _getAttackMeleeCellsS = new GetAttackMeleeCellsS(sMG, eMG);
            _getAbilityUnitS = new GetAbilityUnitS_M(sMG, eMG);
            _pawnGetExtractAdultForestS = new PawnGetExtractAdultForestS(sMG, eMG);
            _pawnExtractHillS = new PawnExtractHillS(sMG, eMG);
            _getVisibleS = new GetVisibleUnitS(sMG, eMG);
            _getCellForArsonArcherS = new GetCellForArsonArcherS(sMG, eMG);
            _getCellsForAttackArcherS = new GetCellsForAttackArcherS(sMG, eMG);
            _getCellsForShiftUnitS = new GetCellsForShiftUnitS(sMG, eMG);
            _getEffectsForUnitsS = new GetEffectsForUnitsS(sMG, eMG);
            _getDamageUnitsS = new GetDamageUnitsS(sMG, eMG);

            for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled.Add(effectT, false);
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

                _getTrailsVisibleS.Get(cell_0);


                _getWoodcutterExtractCellsS.Get(cell_0);
                _getFarmExtractCellsS.Get(cell_0);
                _getBuildingVisibleS.Get(cell_0);
            }

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                _getCellsForShiftUnitS.Get(cell_0);
                _getAttackMeleeCellsS.Get(cell_0);
                _getCellsForAttackArcherS.Get(cell_0);
                _getCellForArsonArcherS.Get(cell_0);
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled[effectT] = false;


                if (eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {
                    for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                    {
                        eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.None);

                        if (!_isFilled[EffectTypes.Shield] && eMG.ShieldUnitEffectC(cellIdxCurrent).HaveAnyProtection)
                        {
                            eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.Shield);
                            _isFilled[EffectTypes.Shield] = true;
                        }
                        else if (!_isFilled[EffectTypes.Arraw] && eMG.FrozenArrawEffectC(cellIdxCurrent).HaveShoots)
                        {
                            eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.Arraw);
                            _isFilled[EffectTypes.Arraw] = true;
                        }
                        else if (!_isFilled[EffectTypes.Stun] && eMG.StunUnitC(cellIdxCurrent).IsStunned)
                        {
                            eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.Stun);
                            _isFilled[EffectTypes.Stun] = true;
                        }
                        else if (!_isFilled[EffectTypes.DamageAdd] && eMG.HaveKingEffect(cellIdxCurrent))
                        {
                            eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.DamageAdd);
                            _isFilled[EffectTypes.DamageAdd] = true;
                        }
                    }
                }
            }
        }
    }
}