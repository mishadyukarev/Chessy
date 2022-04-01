using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using System.Linq;

namespace Chessy.Game.Model.System
{
    sealed class SelectorSoundS : SystemModelGameAbs
    {
        internal SelectorSoundS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Sound()
        {
            var cell_0 = eMG.CellsC.Current;

            if (eMG.UnitTC(cell_0).HaveUnit
                && eMG.UnitEs(cell_0).ForPlayer(eMG.CurPlayerITC.PlayerT).IsVisible && !eMG.UnitTC(cell_0).Is(UnitTypes.Wolf))
            {
                if (eMG.UnitTC(cell_0).Is(UnitTypes.Tree))
                {
                    eMG.SoundActionC(ClipTypes.Leaf).Invoke();
                }

                else if (eMG.UnitTC(cell_0).IsMelee(eMG.UnitMainTWTC(cell_0).ToolWeaponT))
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


                if (eMG.CellEs(eMG.WeatherE.CloudC.Center).AroundCellsEs.IdxsAround.Contains(cell_0) || eMG.WeatherE.CloudC.Center == cell_0)
                {
                    eMG.SoundActionC(ClipTypes.ShortRain).Invoke();
                }
            }
        }
    }
}