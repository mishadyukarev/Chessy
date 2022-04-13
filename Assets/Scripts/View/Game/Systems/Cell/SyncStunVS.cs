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

            if (e.UnitTC(_currentCell).HaveUnit)
            {
                if (e.UnitVisibleC(_currentCell).IsVisible(e.CurPlayerITC.PlayerT))
                {
                    _needActive = e.StunUnitC(_currentCell).IsStunned;
                }
            }

            _stunSRC.SetActive(_needActive);
        }
    }
}