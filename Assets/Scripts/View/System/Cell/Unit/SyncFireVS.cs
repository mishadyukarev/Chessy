using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;

namespace Chessy.View.System
{
    sealed class SyncFireVS : SystemViewAbstract
    {
        bool[] _needActive = new bool[StartValues.CELLS];
        readonly SpriteRendererVC[] _fireSRCs;

        internal SyncFireVS(SpriteRendererVC[] fireSRCs,  in EntitiesModel eM) : base(eM)
        {
            _fireSRCs = fireSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.HaveFire(cellIdxCurrent))
                {
                    _needActive[cellIdxCurrent] = true;
                }

                else
                {
                    _needActive[cellIdxCurrent] = false;
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _fireSRCs[cellIdxCurrent].SetActiveGO(_needActive[cellIdxCurrent]);
            }
        }
    }
}