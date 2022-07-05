using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        void GetPawnExtractHill()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _e.ExtactionResourcesWithWarriorC(cellIdxCurrent).HowManyWarriourCanExtractHill = 0;

                if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.ExtraToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.Pick) && _e.UnitConditionT(cellIdxCurrent) == ConditionUnitTypes.Relaxed)
                {
                    if (_e.HillC(cellIdxCurrent).HaveAnyResources)
                    {
                        var extract = ExtractPawnValues.PAWN_PICK_EXTRACT_HILL;


                        if (_e.HillC(cellIdxCurrent).Resources < extract) extract = _e.HillC(cellIdxCurrent).Resources;


                        _e.ExtactionResourcesWithWarriorC(cellIdxCurrent).HowManyWarriourCanExtractHill = extract;

                    }
                }
            }
        }
    }
}