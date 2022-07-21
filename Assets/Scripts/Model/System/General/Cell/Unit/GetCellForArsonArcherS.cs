using Chessy.Model.Enum;
using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetCellForArsonArcher()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _whereUnitCanFireAdultForestCs[cellIdxCurrent].Set(cellIdxCurrent, false);

                if (!_effectsUnitCs[cellIdxCurrent].IsStunned)
                {
                    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _mainTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _e.GetIdxCellByDirectAround(cellIdxCurrent, dirT);

                            if (!_fireCs[idx_1].HaveFire)
                            {
                                if (_e.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    _whereUnitCanFireAdultForestCs[cellIdxCurrent].Set(idx_1, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}