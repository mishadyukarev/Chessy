using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class SyncTrailVS : SystemViewCellGameAbs
    {
        readonly DirectTypes _currentDirectTrail;
        bool _needActive;
        readonly SpriteRendererVC _trailSRC;

        internal SyncTrailVS(in DirectTypes currentDirectTrail, in SpriteRendererVC trailSRC, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _currentDirectTrail = currentDirectTrail;
            _trailSRC = trailSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (e.TrailVisibleC(_currentCell).IsVisible(e.CurPlayerITC.PlayerT))
            {
                _needActive = e.HealthTrail(_currentCell).IsAlive(_currentDirectTrail);
            }

            _trailSRC.SetActive(_needActive);
        }
    }
}