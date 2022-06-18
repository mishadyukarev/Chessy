using Chessy.Game.Model.Entity;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetCellForArsonArcher()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eMG.UnitForArsonC(cellIdxCurrent).Clear();

                if (!_eMG.StunUnitC(cellIdxCurrent).IsStunned)
                {
                    if (_eMG.UnitTC(cellIdxCurrent).Is(UnitTypes.Pawn) && _eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.BowCrossbow))
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = _eMG.AroundCellsE(cellIdxCurrent).IdxCell(dirT);

                            if (!_eMG.HaveFire(idx_1))
                            {
                                if (_eMG.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    _eMG.UnitForArsonC(cellIdxCurrent).Add(idx_1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}