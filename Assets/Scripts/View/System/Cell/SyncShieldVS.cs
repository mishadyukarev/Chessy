using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;

namespace Chessy.View.System
{
    sealed class SyncShieldVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _shieldSRCs;

        internal SyncShieldVS(in SpriteRendererVC[] shieldSRCs, in EntitiesModel eM) : base(eM)
        {
            _shieldSRCs = shieldSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;

                if (_e.UnitEffectsC(cellIdxCurrent).HaveAnyProtectionRainyMagicShield)
                {
                    if (_e.UnitT(cellIdxCurrent).HaveUnit())
                    {
                        if (_e.UnitVisibleC(cellIdxCurrent).IsVisible(_e.CurrentPlayerIT))
                        {
                            _needActive[cellIdxCurrent] = true;
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _shieldSRCs[cellIdxCurrent].SetActiveGO(_needActive[cellIdxCurrent]);
            }



        }
    }
}