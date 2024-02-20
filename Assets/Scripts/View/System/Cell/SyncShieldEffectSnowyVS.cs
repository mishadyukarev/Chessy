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

                if (unitWhereViewDataCs[cellIdxCurrent].HaveDataReference)
                {
                    var dataIdxCell = unitWhereViewDataCs[cellIdxCurrent].DataIdxCellP;


                    if (effectsUnitCs[dataIdxCell].HaveAnyProtectionRainyMagicShield)
                    {
                        if (unitCs[dataIdxCell].HaveUnit)
                        {
                            if (_unitVisibleCs[dataIdxCell].IsVisible(aboutGameC.CurrentPlayerIType))
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