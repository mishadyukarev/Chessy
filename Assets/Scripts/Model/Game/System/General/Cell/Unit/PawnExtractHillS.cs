using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        void GetPawnExtractHill()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _e.PawnExtractHillC(cellIdxCurrent).Resources = 0;

                if (_e.UnitT(cellIdxCurrent).Is(UnitTypes.Pawn) && _e.ExtraToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.Pick) && _e.UnitConditionT(cellIdxCurrent) == ConditionUnitTypes.Relaxed)
                {
                    if (_e.HillC(cellIdxCurrent).HaveAnyResources)
                    {
                        var extract = ExtractPawnValues.PAWN_PICK_EXTRACT_HILL;


                        if (_e.HillC(cellIdxCurrent).Resources < extract) extract = _e.HillC(cellIdxCurrent).Resources;


                        _e.PawnExtractHillC(cellIdxCurrent).Resources = extract;

                    }
                }
            }
        }
    }
}