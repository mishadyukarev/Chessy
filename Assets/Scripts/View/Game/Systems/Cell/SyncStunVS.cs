using Chessy.Game.Entity;
using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class SyncStunVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly SpriteRendererVC _stunSRC;

        internal SyncStunVS(in SpriteRendererVC stunSRC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _stunSRC = stunSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (_e.UnitTC(_currentCell).HaveUnit)
            {
                if (_e.UnitVisibleC(_currentCell).IsVisible(_e.CurPlayerITC.PlayerT))
                {
                    _needActive = _e.StunUnitC(_currentCell).IsStunned;
                }
            }

            _stunSRC.SetActive(_needActive);
        }
    }
}