using Chessy.Model.Entity;
using Chessy.Model.System;
using System.Linq;
namespace Chessy.Model
{
    sealed class SelectorSoundS : SystemModelAbstract
    {
        internal SelectorSoundS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
        }

        internal void Sound()
        {
            var cell_0 = _cellsC.Current;

            if (_unitCs[cell_0].HaveUnit
                && _unitVisibleCs[cell_0].IsVisible(_aboutGameC.CurrentPlayerIT) && _unitCs[cell_0].UnitT != UnitTypes.Wolf)
            {
                if (_unitCs[cell_0].UnitT == UnitTypes.Tree)
                {
                    _dataFromViewC.SoundAction(ClipTypes.Leaf).Invoke();
                }

                else if (_unitCs[cell_0].UnitT.IsMelee(_mainTWC[cell_0].ToolWeaponT))
                {
                    _dataFromViewC.SoundAction(ClipTypes.PickMelee).Invoke();
                }
                else
                {
                    _dataFromViewC.SoundAction(ClipTypes.PickArcher).Invoke();
                }
            }
            else
            {
                if (_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    _dataFromViewC.SoundAction(ClipTypes.Leaf).Invoke();
                }
                else if (_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.Hill))
                {
                    _dataFromViewC.SoundAction(ClipTypes.Rock).Invoke();
                }
                else if (_environmentCs[cell_0].HaveEnvironment(EnvironmentTypes.Mountain))
                {
                    _dataFromViewC.SoundAction(ClipTypes.ShortWind).Invoke();
                }
                else
                {
                    _dataFromViewC.SoundAction(ClipTypes.KickGround).Invoke();
                }






                if (_cloudCs[cell_0].IsCenterP)
                {
                    _dataFromViewC.SoundAction(ClipTypes.ShortRain).Invoke();
                }
                else
                {
                    foreach (var item in _idxsAroundCellCs[cell_0].IdxCellsAroundArray)
                    {
                        if (_cloudCs[item].IsCenterP)
                        {
                            _dataFromViewC.SoundAction(ClipTypes.ShortRain).Invoke();
                            break;
                        }
                    }
                }

            }
        }
    }
}