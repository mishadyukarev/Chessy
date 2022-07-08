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

                if (_e.SkinInfoUnitC(cellIdxCurrent).HaveData)
                {
                    var dataIdxCell = _e.SkinInfoUnitC(cellIdxCurrent).DataIdxCell;


                    if (_e.UnitT(dataIdxCell).HaveUnit())
                    {
                        if (_e.UnitVisibleC(dataIdxCell).IsVisible(_e.CurrentPlayerIT))
                        {
                            if (_e.MainToolWeaponT(dataIdxCell).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                            {
                                if (_e.UnitEffectsC(dataIdxCell).HaveFrozenArrawArcher)
                                {
                                    if (_e.IsRightArcherUnit(dataIdxCell))
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
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _rightSRCs[cellIdxCurrent].SetActiveGO(_needActiveRight[cellIdxCurrent]);
                _upSRCs[cellIdxCurrent].SetActiveGO(_needActiveUp[cellIdxCurrent]);
            }
        }
    }
}