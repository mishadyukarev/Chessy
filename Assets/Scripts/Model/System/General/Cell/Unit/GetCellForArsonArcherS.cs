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
                _e.WhereUnitCanFireAdultForestC(cellIdxCurrent).Set(cellIdxCurrent, false);

                if (!_e.UnitEffectsC(cellIdxCurrent).IsStunned)
                {
                    if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _e.GetIdxCellByDirect(cellIdxCurrent, DistanceFromCellTypes.First, dirT);

                            if (!_e.HaveFire(idx_1))
                            {
                                if (_e.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    _e.WhereUnitCanFireAdultForestC(cellIdxCurrent).Set(idx_1, true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}