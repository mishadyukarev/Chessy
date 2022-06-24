using Chessy.Model.Model.Entity;

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

            if (_e.WeatherE.CloudC.CellIdxCenterCloud == _currentCell)
            {
                _needActive = true;
            }
            else
            {
                foreach (var startCell in _e.AroundCellsE(_e.WeatherE.CloudC.CellIdxCenterCloud).CellsAround)
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