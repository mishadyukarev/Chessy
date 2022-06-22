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
                _e.AttackSimpleCellsC(cellIdxCell).Clear();
                _e.AttackUniqueCellsC(cellIdxCell).Clear();
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
                        _e.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.None);

                        if (!_isFilled[EffectTypes.Shield] && _e.ShieldUnitEffectC(cellIdxCurrent).HaveAnyProtection())
                        {
                            _e.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.Shield);
                            _isFilled[EffectTypes.Shield] = true;
                        }
                        else if (!_isFilled[EffectTypes.Arraw] && _e.FrozenArrawEffectC(cellIdxCurrent).HaveShoots)
                        {
                            _e.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.Arraw);
                            _isFilled[EffectTypes.Arraw] = true;
                        }
                        else if (!_isFilled[EffectTypes.Stun] && _e.StunUnitC(cellIdxCurrent).IsStunned)
                        {
                            _e.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.Stun);
                            _isFilled[EffectTypes.Stun] = true;
                        }
                        else if (!_isFilled[EffectTypes.DamageAdd] && _e.HaveKingEffect(cellIdxCurrent))
                        {
                            _e.UnitEs(cellIdxCurrent).SetEffect(buttonT, EffectTypes.DamageAdd);
                            _isFilled[EffectTypes.DamageAdd] = true;
                        }
                    }
                }
            }
        }
    }
}