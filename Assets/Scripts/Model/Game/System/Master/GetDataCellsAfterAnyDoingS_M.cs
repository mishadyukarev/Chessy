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

        internal void Run()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                eMG.PlayerInfoE(playerT).WhereKingEffects.Clear();
            }


            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                PawnGetExtractAdultForest(cell_0);
                GetPawnExtractHill(cell_0);

                GetVisibleUnits(cell_0);
                GetEffectsForUnits(cell_0);

                GetDamageUnits(cell_0);
                GetAbilityUnit(cell_0);

                GetTrailsVisible(cell_0);


                GetWoodcutterExtractCells(cell_0);
                GetFarmExtractCells(cell_0);
                GetBuildingVisible(cell_0);
            }

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                GetCellsForShiftUnit(cell_0);
                GetCellsForAttackArcher(cell_0);
                GetCellForArsonArcher(cell_0);
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