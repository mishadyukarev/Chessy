using Chessy.Game.Entity;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class SyncShieldVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly SpriteRendererVC _shieldSRC;

        internal SyncShieldVS(in SpriteRendererVC shieldSRC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _shieldSRC = shieldSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (e.ShieldUnitEffectC(_currentCell).HaveAnyProtection)
            {
                if (e.UnitTC(_currentCell).HaveUnit)
                {
                    if (e.UnitVisibleC(_currentCell).IsVisible(e.CurPlayerITC.PlayerT))
                    {
                        _needActive = true;
                    }
                }
            }

            _shieldSRC.SetActive(_needActive);
        }
    }
}