using Chessy.Game.Model.Entity;
using System.Linq;

namespace Chessy.Game.Model.System
{
    sealed class SelectorSoundS : SystemModel
    {
        internal SelectorSoundS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Sound()
        {
            var cell_0 = _eMG.CellsC.Current;

            if (_eMG.UnitTC(cell_0).HaveUnit
                && _eMG.UnitVisibleC(cell_0).IsVisible(_eMG.CurPlayerITC.PlayerT) && !_eMG.UnitTC(cell_0).Is(UnitTypes.Wolf))
            {
                if (_eMG.UnitTC(cell_0).Is(UnitTypes.Tree))
                {
                    _eMG.SoundAction(ClipTypes.Leaf).Invoke();
                }

                else if (_eMG.UnitTC(cell_0).IsMelee(_eMG.MainToolWeaponTC(cell_0).ToolWeaponT))
                {
                    _eMG.SoundAction(ClipTypes.PickMelee).Invoke();
                }
                else
                {
                    _eMG.SoundAction(ClipTypes.PickArcher).Invoke();
                }
            }
            else
            {
                if (_eMG.AdultForestC(cell_0).HaveAnyResources)
                {
                    _eMG.SoundAction(ClipTypes.Leaf).Invoke();
                }
                else if (_eMG.HillC(cell_0).HaveAnyResources)
                {
                    _eMG.SoundAction(ClipTypes.Rock).Invoke();
                }
                else if (_eMG.MountainC(cell_0).HaveAnyResources)
                {
                    _eMG.SoundAction(ClipTypes.ShortWind).Invoke();
                }
                else
                {
                    _eMG.SoundAction(ClipTypes.KickGround).Invoke();
                }


                if (_eMG.AroundCellsE(_eMG.WeatherE.CloudC.Center).CellsAround.Contains(cell_0) || _eMG.WeatherE.CloudC.Center == cell_0)
                {
                    _eMG.SoundAction(ClipTypes.ShortRain).Invoke();
                }
            }
        }
    }
}