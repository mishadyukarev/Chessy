using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;

namespace Chessy.View.System
{
    sealed class SyncFireVS : SystemViewAbstract
    {
        bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _fireSRCs;

        internal SyncFireVS(SpriteRendererVC[] fireSRCs, in EntitiesModel eM) : base(eM)
        {
            _fireSRCs = fireSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (FireC(cellIdxCurrent).HaveFire)
                {
                    _needActive[cellIdxCurrent] = true;
                }

                else
                {
                    _needActive[cellIdxCurrent] = false;
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _fireSRCs[cellIdxCurrent].TrySetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}