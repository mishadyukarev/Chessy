using Chessy.Model.Model.Entity;
using System.Linq;

namespace Chessy.Model.Model.System
{
    sealed class SelectorSoundS : SystemModel
    {
        internal SelectorSoundS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
        }

        internal void Sound()
        {
            var cell_0 = _e.CellsC.Current;

            if (_e.UnitT(cell_0).HaveUnit()
                && _e.UnitVisibleC(cell_0).IsVisible(_e.CurPlayerIT) && !_e.UnitT(cell_0).Is(UnitTypes.Wolf))
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


                if (_e.AroundCellsE(_e.WeatherE.CellIdxCenterCloud).CellsAround.Contains(cell_0) || _e.WeatherE.CellIdxCenterCloud == cell_0)
                {
                    _e.SoundAction(ClipTypes.ShortRain).Invoke();
                }
            }
        }
    }
}