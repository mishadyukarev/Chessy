using Chessy.Model.Entity;
using Chessy.Model.Values;
using System.Collections.Generic;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        readonly GetCellsForShiftUnitsS _getCellsForShiftUnitsS;
        readonly bool[] _isFilled = new bool[(byte)EffectTypes.End];

        readonly WhereUnitCanAttackToEnemyC[] _whereSimple = new WhereUnitCanAttackToEnemyC[IndexCellsValues.CELLS];
        readonly WhereUnitCanAttackToEnemyC[] _whereUnque = new WhereUnitCanAttackToEnemyC[IndexCellsValues.CELLS];

        internal GetDataCellsAfterAnyDoingS_M(in SystemsModel s, in EntitiesModel e) : base(s, e)
        {
            _getCellsForShiftUnitsS = new GetCellsForShiftUnitsS(s, e);

            for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            {
                ref var v = ref _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdx);
                ref var vv = ref _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdx);

                _whereSimple[cellIdx] = v;
                _whereUnque[cellIdx] = vv;
            }
        }

        internal void GetDataCells()
        {
            for (byte cellIdxStart = 0; cellIdxStart < IndexCellsValues.CELLS; cellIdxStart++)
            {
                for (byte cellIdxDirect = 0; cellIdxDirect < IndexCellsValues.CELLS; cellIdxDirect++)
                {
                    _e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxStart).Set(cellIdxDirect, false);
                    _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxStart).Set(cellIdxDirect, false);

                    //_whereSimple[cellIdxStart].Set(cellIdxDirect, false);
                    //_whereUnque[cellIdxStart].Set(cellIdxDirect, false);
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
                for (byte effectIdx = 0; effectIdx < _isFilled.Length; effectIdx++) _isFilled[effectIdx] = false;


                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                    {
                        _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.None);

                        if (!_isFilled[(byte)EffectTypes.Shield] && _e.UnitEffectsC(cellIdxCurrent).HaveAnyProtectionRainyMagicShield)
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.Shield);
                            _isFilled[(byte)EffectTypes.Shield] = true;
                        }
                        else if (!_isFilled[(byte)EffectTypes.Arraw] && _e.UnitEffectsC(cellIdxCurrent).HaveFrozenArrawArcher)
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.Arraw);
                            _isFilled[(byte)EffectTypes.Arraw] = true;
                        }
                        else if (!_isFilled[(byte)EffectTypes.Stun] && _e.UnitEffectsC(cellIdxCurrent).IsStunned)
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.Stun);
                            _isFilled[(byte)EffectTypes.Stun] = true;
                        }
                        else if (!_isFilled[(byte)EffectTypes.DamageAdd] && _e.HasKingEffectHereC(cellIdxCurrent).Has(_e.UnitPlayerT(cellIdxCurrent)))
                        {
                            _e.EffectsUnitsRightBarsC(cellIdxCurrent).Set(buttonT, EffectTypes.DamageAdd);
                            _isFilled[(byte)EffectTypes.DamageAdd] = true;
                        }
                    }
                }
            }
        }
    }
}