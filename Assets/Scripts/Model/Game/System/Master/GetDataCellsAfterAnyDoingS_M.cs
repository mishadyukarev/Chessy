using Chessy.Common;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        readonly Dictionary<EffectTypes, bool> _isFilled = new Dictionary<EffectTypes, bool>();

        internal GetDataCellsAfterAnyDoingS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled.Add(effectT, false);
        }

        internal void GetDataCells()
        {
            for (byte cellIdxCell = 0; cellIdxCell < StartValues.CELLS; cellIdxCell++)
            {
                _eMG.AttackSimpleCellsC(cellIdxCell).Clear();
                _eMG.AttackUniqueCellsC(cellIdxCell).Clear();
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
        }

        void FillEffectsForVision()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                for (var effectT = EffectTypes.None; effectT < EffectTypes.End; effectT++) _isFilled[effectT] = false;


                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {
                    for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                    {
                        _eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.None);

                        if (!_isFilled[EffectTypes.Shield] && _eMG.ShieldUnitEffectC(cellIdxCurrent).HaveAnyProtection)
                        {
                            _eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.Shield);
                            _isFilled[EffectTypes.Shield] = true;
                        }
                        else if (!_isFilled[EffectTypes.Arraw] && _eMG.FrozenArrawEffectC(cellIdxCurrent).HaveShoots)
                        {
                            _eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.Arraw);
                            _isFilled[EffectTypes.Arraw] = true;
                        }
                        else if (!_isFilled[EffectTypes.Stun] && _eMG.StunUnitC(cellIdxCurrent).IsStunned)
                        {
                            _eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.Stun);
                            _isFilled[EffectTypes.Stun] = true;
                        }
                        else if (!_isFilled[EffectTypes.DamageAdd] && _eMG.HaveKingEffect(cellIdxCurrent))
                        {
                            _eMG.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.DamageAdd);
                            _isFilled[EffectTypes.DamageAdd] = true;
                        }
                    }
                }
            }
        }
    }
}