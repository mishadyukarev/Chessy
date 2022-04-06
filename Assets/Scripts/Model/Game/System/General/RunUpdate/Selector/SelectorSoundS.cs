using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using System.Linq;

namespace Chessy.Game.Model.System
{
    sealed class SelectorSoundS : SystemModel
    {
        internal SelectorSoundS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
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
                    eMG.SoundActionC(ClipTypes.Leaf).Invoke();
                }

                else if (eMG.UnitTC(cell_0).IsMelee(eMG.MainToolWeaponTC(cell_0).ToolWeaponT))
                {
                    eMG.SoundActionC(ClipTypes.PickMelee).Invoke();
                }
                else
                {
                    eMG.SoundActionC(ClipTypes.PickArcher).Invoke();
                }
            }
            else
            {
                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                {
                    eMG.SoundActionC(ClipTypes.Leaf).Invoke();
                }
                else if (eMG.HillC(cell_0).HaveAnyResources)
                {
                    eMG.SoundActionC(ClipTypes.Rock).Invoke();
                }
                else if (eMG.MountainC(cell_0).HaveAnyResources)
                {
                    eMG.SoundActionC(ClipTypes.ShortWind).Invoke();
                }
                else
                {
                    eMG.SoundActionC(ClipTypes.KickGround).Invoke();
                }


                if (eMG.AroundCellsE(eMG.WeatherE.CloudC.Center).CellsAround.Contains(cell_0) || eMG.WeatherE.CloudC.Center == cell_0)
                {
                    eMG.SoundActionC(ClipTypes.ShortRain).Invoke();
                }
            }
        }
    }
}