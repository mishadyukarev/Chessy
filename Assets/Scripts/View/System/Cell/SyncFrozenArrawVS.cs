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

                if (_unitWhereViewDataCs[cellIdxCurrent].HaveDataReference)
                {
                    var dataIdxCell = _unitWhereViewDataCs[cellIdxCurrent].DataIdxCellP;


                    if (_unitCs[dataIdxCell].HaveUnit)
                    {
                        if (_unitVisibleCs[dataIdxCell].IsVisible(_aboutGameC.CurrentPlayerIType))
                        {
                            if (_mainTWC[dataIdxCell].ToolWeaponType == ToolsWeaponsWarriorTypes.BowCrossbow)
                            {
                                if (_effectsUnitCs[dataIdxCell].HaveFrozenArrawArcherP)
                                {
                                    if (_unitCs[dataIdxCell].IsArcherDirectedToRightP)
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
                _rightSRCs[cellIdxCurrent].TrySetActiveGO(_needActiveRight[cellIdxCurrent]);
                _upSRCs[cellIdxCurrent].TrySetActiveGO(_needActiveUp[cellIdxCurrent]);
            }
        }
    }
}