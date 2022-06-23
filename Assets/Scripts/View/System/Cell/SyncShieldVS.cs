using Chessy.Model.Model.Entity;

namespace Chessy.Model
{
    sealed class SyncShieldVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly SpriteRendererVC _shieldSRC;

        internal SyncShieldVS(in SpriteRendererVC shieldSRC, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _shieldSRC = shieldSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (_e.ShieldUnitEffectC(_currentCell).HaveAnyProtection())
            {
                if (_e.UnitT(_currentCell).HaveUnit())
                {
                    if (_e.UnitVisibleC(_currentCell).IsVisible(_e.CurPlayerIT))
                    {
                        _needActive = true;
                    }
                }
            }

            _shieldSRC.SetActive(_needActive);
        }
    }
}