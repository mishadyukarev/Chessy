using Chessy.Common;
using Chessy.Model.Model.Entity;
using Chessy.Model.Values;
using System.Collections.Generic;

namespace Chessy.Model.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        readonly Dictionary<EffectTypes, bool> _isFilled = new Dictionary<EffectTypes, bool>();

        internal GetDataCellsAfterAnyDoingS_M(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled.Add(effectT, false);
        }

        internal void GetDataCells()
        {
            for (byte cellIdxCell = 0; cellIdxCell < StartValues.CELLS; cellIdxCell++)
            {
                _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxCell).Set(cellIdxCell, false);
                _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxCell).Set(cellIdxCell, false);
            }

            PawnGetExtractAdultForest();
            GetPawnExtractHill();
            GetVisibleUnits();
            GetEffectsForUnits();
            GetDamageUnits();
            GetAbilityUnit();
            GetTrailsVisible();
            GetWoodcutterExtractCells();
            GetFarmExtractCells();
            GetBuildingVisible();
            GetCellsForShiftUnit();
            GetAttackMeleeCells();
            GetCellsForAttackArcher();
            GetCellForArsonArcher();
            FillEffectsForVision();

            _e.NeedUpdateView = true;
        }

        void FillEffectsForVision()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
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
                        else if (!_isFilled[EffectTypes.Arraw] && _e.UnitEffectsC(cellIdxCurrent).HaveShoots)
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.Arraw);
                            _isFilled[EffectTypes.Arraw] = true;
                        }
                        else if (!_isFilled[EffectTypes.Stun] && _e.UnitEffectsC(cellIdxCurrent).IsStunned)
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.Stun);
                            _isFilled[EffectTypes.Stun] = true;
                        }
                        else if (!_isFilled[EffectTypes.DamageAdd] && _e.HaveKingEffect(cellIdxCurrent))
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