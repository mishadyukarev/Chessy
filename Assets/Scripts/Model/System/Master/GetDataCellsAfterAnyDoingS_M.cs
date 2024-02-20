using Chessy.Model.Entity;
using Chessy.Model.Values;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        readonly GetCellsForShiftUnitsS _getCellsForShiftUnitsS;
        readonly GetButtonAbilitiesUnitsS_M _getAbilityUnitS;
        readonly GetCellsForAttackArcherS _getCellsForAttackArcherS;
        readonly GetAttackMeleeCellsS _getAttackMeleeCellsS;

        readonly bool[] _isFilled = new bool[(byte)EffectTypes.End];

        internal GetDataCellsAfterAnyDoingS_M(in SystemsModel s, EntitiesModel e) : base(s, e)
        {
            _getCellsForShiftUnitsS = new GetCellsForShiftUnitsS(s, e);
            _getAbilityUnitS = new GetButtonAbilitiesUnitsS_M(s, e);
            _getCellsForAttackArcherS = new GetCellsForAttackArcherS(s, e);
            _getAttackMeleeCellsS = new GetAttackMeleeCellsS(s, e);
        }

        internal void GetDataCells()
        {
            for (byte cellIdxStart = 0; cellIdxStart < IndexCellsValues.CELLS; cellIdxStart++)
            {
                for (byte cellIdxDirect = 0; cellIdxDirect < IndexCellsValues.CELLS; cellIdxDirect++)
                {
                    _whereSimple[cellIdxStart][cellIdxDirect] = false;
                    _whereUnque[cellIdxStart][cellIdxDirect] = false;
                }
            }

            PawnGetExtractAdultForest();
            GetPawnExtractHill();
            GetVisibleUnits();
            GetKingEffectsForUnits();
            GetDamageUnits();
            _getAbilityUnitS.GetAbilityUnit();
            GetTrailsVisible();
            GetWoodcutterExtractCells();
            GetFarmExtractCells();
            GetBuildingVisible();
            _getCellsForShiftUnitsS.Get();
            _getAttackMeleeCellsS.Get();
            _getCellsForAttackArcherS.Get();
            GetCellForArsonArcher();
            FillEffectsForVision();

            updateAllViewC.NeedUpdateView = true;
        }

        void FillEffectsForVision()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (byte effectIdx = 0; effectIdx < _isFilled.Length; effectIdx++) _isFilled[effectIdx] = false;


                if (unitCs[cellIdxCurrent].UnitT.HaveUnit())
                {
                    for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                    {
                        _effectsUnitsRightBarsCs[cellIdxCurrent].Set(buttonT, EffectTypes.None);

                        if (!_isFilled[(byte)EffectTypes.Shield] && effectsUnitCs[cellIdxCurrent].HaveAnyProtectionRainyMagicShield)
                        {
                            _effectsUnitsRightBarsCs[cellIdxCurrent].Set(buttonT, EffectTypes.Shield);
                            _isFilled[(byte)EffectTypes.Shield] = true;
                        }
                        else if (!_isFilled[(byte)EffectTypes.Arraw] && effectsUnitCs[cellIdxCurrent].HaveFrozenArrawArcher)
                        {
                            _effectsUnitsRightBarsCs[cellIdxCurrent].Set(buttonT, EffectTypes.Arraw);
                            _isFilled[(byte)EffectTypes.Arraw] = true;
                        }
                        else if (!_isFilled[(byte)EffectTypes.Stun] && effectsUnitCs[cellIdxCurrent].IsStunned)
                        {
                            _effectsUnitsRightBarsCs[cellIdxCurrent].Set(buttonT, EffectTypes.Stun);
                            _isFilled[(byte)EffectTypes.Stun] = true;
                        }
                        else if (!_isFilled[(byte)EffectTypes.DamageAdd] && _hasUnitKingEffectHereCs[cellIdxCurrent].Has(unitCs[cellIdxCurrent].PlayerT))
                        {
                            _effectsUnitsRightBarsCs[cellIdxCurrent].Set(buttonT, EffectTypes.DamageAdd);
                            _isFilled[(byte)EffectTypes.DamageAdd] = true;
                        }
                    }
                }
            }
        }
    }
}