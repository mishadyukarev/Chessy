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
            var cell_0 = eMG.CellsC.Current;

            if (eMG.UnitTC(cell_0).HaveUnit
                && eMG.UnitVisibleC(cell_0).IsVisible(eMG.CurPlayerITC.PlayerT) && !eMG.UnitTC(cell_0).Is(UnitTypes.Wolf))
            {
                if (eMG.UnitTC(cell_0).Is(UnitTypes.Tree))
                {
                    eMG.SoundAction(ClipTypes.Leaf).Invoke();
                }

                else if (eMG.UnitTC(cell_0).IsMelee(eMG.MainToolWeaponTC(cell_0).ToolWeaponT))
                {
                    eMG.SoundAction(ClipTypes.PickMelee).Invoke();
                }
                else
                {
                    eMG.SoundAction(ClipTypes.PickArcher).Invoke();
                }
            }
            else
            {
                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                {
                    eMG.SoundAction(ClipTypes.Leaf).Invoke();
                }
                else if (eMG.HillC(cell_0).HaveAnyResources)
                {
                    eMG.SoundAction(ClipTypes.Rock).Invoke();
                }
                else if (eMG.MountainC(cell_0).HaveAnyResources)
                {
                    eMG.SoundAction(ClipTypes.ShortWind).Invoke();
                }
                else
                {
                    eMG.SoundAction(ClipTypes.KickGround).Invoke();
                }


                if (eMG.AroundCellsE(eMG.WeatherE.CloudC.Center).CellsAround.Contains(cell_0) || eMG.WeatherE.CloudC.Center == cell_0)
                {
                    eMG.SoundAction(ClipTypes.ShortRain).Invoke();
                }
            }
        }
    }
}