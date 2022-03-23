using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Game.System.View.UI.Down;
using Chessy.Game.View.UI.System;

namespace Chessy.Game.System.View.UI
{
    public readonly struct SystemViewUIUpdate
    {
        public void Run(in float timer, in SystemsViewUI systemsUI, in EntitiesModelCommon eMC, in EntitiesViewUIGame eUI, in Entity.Model.EntitiesModelGame e)
        {
            ///Right
            ///
            var rightEs = eUI.RightEs;
            new RightZoneUIS(eUI, e).Run();
            new StatsUIS(eUI, e).Run();
            systemsUI.ProtectS.Run(rightEs.ProtectE, e.UnitEs(e.CellsC.Selected), e.CurPlayerITC.Player);
            systemsUI.RelaxS.Run(rightEs.RelaxE, e);
            new ShieldUIS(eUI, e).Run();
            systemsUI.EffectsS.Run(e.Resources, eUI, e);
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                new UniqueButtonUIS(buttonT, eUI.RightEs.Unique(buttonT), e.Resources, e).Run();
            }


            ///Down
            new DonerUIS(eUI.DownEs.DonerE, e).Run();
            new DownPawnUIS(eUI.DownEs.PawnE, e).Run();
            new DownToolWeaponUIS(eUI.DownEs.ToolWeaponE, e).Run();
            new DownHeroUIS(eUI.DownEs.HeroE, e).Run();
            eUI.DownEs.CostE.Sync(e);


            ///Up
            systemsUI.EconomyUpS.Run(eUI, e);
            new UpWindUIS(eUI, e).Run();
            new UpSunsUIS(eUI, e).Run();


            ///Center
            new CenterSelectorUIS(eUI, e).Run();
            new CenterEndGameUIS(eUI, e).Run();
            new CenterReadyUIS(eUI, e).Run();
            new CenterKingUIS(eUI, e).Run();
            new CenterFriendUIS().Run(eMC.GameModeTC, eUI, e);
            new CenterHeroesUIS(eUI, e).Run();
            new CenterBuildingZonesUIS(eUI, e).Run();
            MotionUIS.Sync(timer, eUI, e);
            eUI.CenterEs.MistakeE.Sync(timer, e);


            ///Left
            new LeftZonesUIS(eUI, e).Run();
            new EnvUIS(eUI, e).Run();
            new LeftCityUIS(eUI, e).Run();
        }
    }
}