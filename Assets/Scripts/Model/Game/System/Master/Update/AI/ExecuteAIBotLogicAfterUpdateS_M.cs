using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class ExecuteAIBotLogicAfterUpdateS_M : SystemModel
    {
        readonly TrySetUnitAIS_M _trySetUnitAIS;
        readonly TryShiftUnitAIS_M _tryShiftUnitAIS;
        readonly TryGiveToolOrWeaponAIS_M _tryGiveToolOrWeaponAIS;

        internal ExecuteAIBotLogicAfterUpdateS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _trySetUnitAIS = new TrySetUnitAIS_M(sMG, eMG);
            _tryShiftUnitAIS = new TryShiftUnitAIS_M(sMG, eMG);
            _tryGiveToolOrWeaponAIS = new TryGiveToolOrWeaponAIS_M(sMG, eMG);
        }

        internal void Execute()
        {
            _trySetUnitAIS.TrySet();
            _tryGiveToolOrWeaponAIS.TryGive();

            var playerBotT = PlayerTypes.Second;

            for (byte cellIdxStart = 0; cellIdxStart < StartValues.CELLS; cellIdxStart++)
            {
                if (_e.UnitT(cellIdxStart) == UnitTypes.Pawn && _e.UnitPlayerT(cellIdxStart) == playerBotT)
                {
                    foreach (var cellIdxDirect in _e.AroundCellsE(cellIdxStart).CellsAround)
                    {
                        if (_e.UnitT(cellIdxDirect).HaveUnit())
                        {
                            if (!_e.UnitT(cellIdxDirect).IsAnimal())
                            {
                                if (_e.AttackSimpleCellsC(cellIdxStart).Contains(cellIdxDirect)
                                    || _e.AttackUniqueCellsC(cellIdxStart).Contains(cellIdxDirect))
                                {
                                    _s.AttackUnitFromTo(cellIdxStart, cellIdxDirect);
                                }
                            }
                        }
                    }



                    //eMG.Unit
                }
            }

            _tryShiftUnitAIS.TryShift();
        }
    }
}