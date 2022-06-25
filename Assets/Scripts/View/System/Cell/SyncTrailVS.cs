using Chessy.Model;

namespace Chessy.Model
{
    sealed class SyncTrailVS : SystemViewCellGameAbs
    {
        readonly DirectTypes _currentDirectTrail;
        bool _needActive;
        readonly SpriteRendererVC _trailSRC;

        internal SyncTrailVS(in DirectTypes currentDirectTrail, in SpriteRendererVC trailSRC, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _currentDirectTrail = currentDirectTrail;
            _trailSRC = trailSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (_e.TrailVisibleC(_currentCell).IsVisible(_e.CurPlayerIT))
            {
                _needActive = _e.HealthTrail(_currentCell).IsAlive(_currentDirectTrail);
            }

            _trailSRC.SetActive(_needActive);
        }
    }
}