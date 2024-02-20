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

                if (!effectsUnitCs[cellIdxCurrent].IsStunned)
                {
                    if (unitCs[cellIdxCurrent].UnitT == UnitTypes.Pawn && mainTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = cellsByDirectAroundC[cellIdxCurrent].Get(dirT);

                            if (!fireCs[idx_1].HaveFire)
                            {
                                if (environmentCs[idx_1].HaveEnvironment(EnvironmentTypes.AdultForest))
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