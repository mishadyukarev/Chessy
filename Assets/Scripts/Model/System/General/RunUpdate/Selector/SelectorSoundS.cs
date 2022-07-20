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
            var cell_0 = _e.CurrentCellIdx;

            if (_e.UnitT(cell_0).HaveUnit()
                && _e.UnitVisibleC(cell_0).IsVisible(_aboutGameC.CurrentPlayerIT) && !_e.UnitT(cell_0).Is(UnitTypes.Wolf))
            {
                if (_e.UnitT(cell_0).Is(UnitTypes.Tree))
                {
                    _e.SoundAction(ClipTypes.Leaf).Invoke();
                }

                else if (_e.UnitT(cell_0).IsMelee(_e.MainToolWeaponT(cell_0)))
                {
                    _e.SoundAction(ClipTypes.PickMelee).Invoke();
                }
                else
                {
                    _e.SoundAction(ClipTypes.PickArcher).Invoke();
                }
            }
            else
            {
                if (_e.AdultForestC(cell_0).HaveAnyResources)
                {
                    _e.SoundAction(ClipTypes.Leaf).Invoke();
                }
                else if (_e.HillC(cell_0).HaveAnyResources)
                {
                    _e.SoundAction(ClipTypes.Rock).Invoke();
                }
                else if (_e.MountainC(cell_0).HaveAnyResources)
                {
                    _e.SoundAction(ClipTypes.ShortWind).Invoke();
                }
                else
                {
                    _e.SoundAction(ClipTypes.KickGround).Invoke();
                }






                if (_e.IsCenterCloud(cell_0))
                {
                    _e.SoundAction(ClipTypes.ShortRain).Invoke();
                }
                else
                {
                    foreach (var item in _e.IdxsCellsAround(cell_0))
                    {
                        if (_e.IsCenterCloud(item))
                        {
                            _e.SoundAction(ClipTypes.ShortRain).Invoke();
                            break;
                        }
                    }
                }

            }
        }
    }
}