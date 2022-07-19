using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;

namespace Chessy.View.System
{
    sealed class SyncShieldEffectSnowyVS : SystemViewAbstract
    {
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly SpriteRendererVC[] _shieldSRCs;

        internal SyncShieldEffectSnowyVS(in SpriteRendererVC[] shieldSRCs, in EntitiesModel eM) : base(eM)
        {
            _shieldSRCs = shieldSRCs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;

                if (_e.WhereViewDataUnitC(cellIdxCurrent).HaveDataReference)
                {
                    var dataIdxCell = _e.WhereViewDataUnitC(cellIdxCurrent).DataIdxCellP;


                    if (_e.UnitEffectsC(dataIdxCell).HaveAnyProtectionRainyMagicShield)
                    {
                        if (_e.UnitT(dataIdxCell).HaveUnit())
                        {
                            if (_e.UnitVisibleC(dataIdxCell).IsVisible(_e.CurrentPlayerIT))
                            {
                                _needActive[cellIdxCurrent] = true;
                            }
                        }
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _shieldSRCs[cellIdxCurrent].TrySetActiveGO(_needActive[cellIdxCurrent]);
            }



        }
    }
}