using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        void GetPawnExtractHill()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eMG.PawnExtractHillC(cellIdxCurrent).Resources = 0;

                if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Pawn) && _eMG.ExtraToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.Pick) && _eMG.UnitConditionT(cellIdxCurrent) == ConditionUnitTypes.Relaxed)
                {
                    if (_eMG.HillC(cellIdxCurrent).HaveAnyResources)
                    {
                        var extract = ExtractPawnValues.PAWN_PICK_EXTRACT_HILL;


                        if (_eMG.HillC(cellIdxCurrent).Resources < extract) extract = _eMG.HillC(cellIdxCurrent).Resources;


                        _eMG.PawnExtractHillC(cellIdxCurrent).Resources = extract;

                    }
                }
            }
        }
    }
}