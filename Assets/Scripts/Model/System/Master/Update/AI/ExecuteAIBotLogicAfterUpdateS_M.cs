using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed class ExecuteAIBotLogicAfterUpdateS_M : SystemModelAbstract
    {
        readonly TrySetUnitAIS_M _trySetUnitAIS;
        readonly TryShiftUnitAIS_M _tryShiftUnitAIS;
        readonly TryGiveToolOrWeaponAIS_M _tryGiveToolOrWeaponAIS;

        internal ExecuteAIBotLogicAfterUpdateS_M(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
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

            for (byte cellIdxStart = 0; cellIdxStart < IndexCellsValues.CELLS; cellIdxStart++)
            {
                if (_e.UnitT(cellIdxStart) == UnitTypes.Pawn && _e.UnitPlayerT(cellIdxStart) == playerBotT)
                {
                    foreach (var cellIdxDirect in _e.IdxsCellsAround(cellIdxStart))
                    {
                        if (_e.UnitT(cellIdxDirect).HaveUnit())
                        {
                            if (!_e.UnitT(cellIdxDirect).IsAnimal())
                            {
                                if (_e.WhereUnitCanAttackSimpleAttackToEnemyC(cellIdxStart).Can(cellIdxDirect)
                                    || _e.WhereUnitCanAttackUniqueAttackToEnemyC(cellIdxStart).Can(cellIdxDirect))
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