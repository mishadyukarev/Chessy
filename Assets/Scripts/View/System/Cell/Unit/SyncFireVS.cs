using Chessy.Model;

namespace Chessy.Model
{
    sealed class SyncFireVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly SpriteRendererVC _fireSRC;

        internal SyncFireVS(SpriteRendererVC fireSRC, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _fireSRC = fireSRC;
        }

        internal sealed override void Sync()
        {
            if (_e.HaveFire(_currentCell))
            {
                _needActive = true;
            }

            else
            {
                _needActive = false;
            }

            _fireSRC.SetActive(_needActive);
        }
    }
}