using Chessy.Model;

namespace Chessy.Model
{
    sealed class SyncCloudVS : SystemViewCellGameAbs
    {
        bool _needActive;
        readonly SpriteRendererVC _cloudSRC;

        public SyncCloudVS(in SpriteRendererVC cloudSRC, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _cloudSRC = cloudSRC;
        }

        internal sealed override void Sync()
        {
            _needActive = false;

            if (_e.CenterCloudCellIdx == _currentCell)
            {
                _needActive = true;
            }
            else
            {
                foreach (var startCell in _e.AroundCellsE(_e.CenterCloudCellIdx).CellsAround)
                {
                    if (startCell == _currentCell)
                    {
                        _needActive = true;
                        break;
                    }
                }
            }

            _cloudSRC.SetActive(_needActive);
        }
    }
}