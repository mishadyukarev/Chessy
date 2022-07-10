﻿using Chessy.Model.Entity;
using Chessy.Model.Values;
using System.Collections.Generic;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        readonly GetCellsForShiftUnitsS _getCellsForShiftUnitsS;
        readonly Dictionary<EffectTypes, bool> _isFilled = new Dictionary<EffectTypes, bool>();

        internal GetDataCellsAfterAnyDoingS_M(in SystemsModel s, in EntitiesModel e) : base(s, e)
        {
            _getCellsForShiftUnitsS = new GetCellsForShiftUnitsS(s, e);
            for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled.Add(effectT, false);
        }

        internal void GetDataCellsM()
        {
            for (byte cellIdxStart = 0; cellIdxStart < IndexCellsValues.CELLS; cellIdxStart++)
            {
                for (byte cellIdxDirect = 0; cellIdxDirect < IndexCellsValues.CELLS; cellIdxDirect++)
                {
                    _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxStart).Set(cellIdxDirect, false);
                    _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxStart).Set(cellIdxDirect, false);
                }
            }

            PawnGetExtractAdultForest();
            GetPawnExtractHill();
            GetVisibleUnits();
            GetKingEffectsForUnits();
            GetDamageUnits();
            GetAbilityUnit();
            GetTrailsVisible();
            GetWoodcutterExtractCells();
            GetFarmExtractCells();
            GetBuildingVisible();
            _getCellsForShiftUnitsS.Get();
            GetAttackMeleeCells();
            GetCellsForAttackArcher();
            GetCellForArsonArcher();
            FillEffectsForVision();

            _e.NeedUpdateView = true;
        }

        void FillEffectsForVision()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled[effectT] = false;


                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                    {
                        _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.None);

                        if (!_isFilled[EffectTypes.Shield] && _e.UnitEffectsC(cellIdxCurrent).HaveAnyProtectionRainyMagicShield)
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.Shield);
                            _isFilled[EffectTypes.Shield] = true;
                        }
                        else if (!_isFilled[EffectTypes.Arraw] && _e.UnitEffectsC(cellIdxCurrent).HaveFrozenArrawArcher)
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.Arraw);
                            _isFilled[EffectTypes.Arraw] = true;
                        }
                        else if (!_isFilled[EffectTypes.Stun] && _e.UnitEffectsC(cellIdxCurrent).IsStunned)
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.Stun);
                            _isFilled[EffectTypes.Stun] = true;
                        }
                        else if (!_isFilled[EffectTypes.DamageAdd] && _e.HasKingEffectHereC(cellIdxCurrent).Has(_e.UnitPlayerT(cellIdxCurrent)))
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.DamageAdd);
                            _isFilled[EffectTypes.DamageAdd] = true;
                        }
                    }
                }
            }
        }
    }
}