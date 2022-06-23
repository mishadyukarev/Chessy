using Chessy.Model.Model.Entity;

namespace Chessy.Model
{
    sealed class SyncStunVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly SpriteRendererVC _stunSRC;

        internal SyncStunVS(in SpriteRendererVC stunSRC, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _stunSRC = stunSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (_e.UnitT(_currentCell).HaveUnit())
            {
                if (_e.UnitVisibleC(_currentCell).IsVisible(_e.CurPlayerIT))
                {
                    _needActive = _e.StunUnitC(_currentCell).IsStunned;
                }
            }

            _stunSRC.SetActive(_needActive);
        }
    }
}