using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;

namespace Chessy.View.System
{
    sealed class SyncFrozenArrawVS : SystemViewAbstract
    {
        readonly SpriteRendererVC[] _rightSRCs;
        readonly SpriteRendererVC[] _upSRCs;
        readonly bool[] _needActiveRight = new bool[IndexCellsValues.CELLS];
        readonly bool[] _needActiveUp = new bool[IndexCellsValues.CELLS];

        internal SyncFrozenArrawVS(in SpriteRendererVC[] srRightCs, in SpriteRendererVC[] upSRCs, in EntitiesModel eM) : base(eM)
        {
            _rightSRCs = srRightCs;
            _upSRCs = upSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActiveUp[cellIdxCurrent] = false;
                _needActiveRight[cellIdxCurrent] = false;


                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    if (_e.UnitVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                    {
                        if (_e.MainToolWeaponT(cellIdxCurrent).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                        {
                            if (_e.UnitEffectsC(cellIdxCurrent).HaveShoots)
                            {
                                if (_e.IsRightArcherUnit(cellIdxCurrent))
                                {
                                    _needActiveRight[cellIdxCurrent] = true;
                                }
                                else
                                {
                                    _needActiveUp[cellIdxCurrent] = true;
                                }
                            }
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _rightSRCs[cellIdxCurrent].SetActiveGO(_needActiveRight[cellIdxCurrent]);
                _upSRCs[cellIdxCurrent].SetActiveGO(_needActiveUp[cellIdxCurrent]);
            }
        }
    }
}