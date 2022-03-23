using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.View.UI.Down;
using Chessy.Game.View.UI.System;

namespace Chessy.Game.System.View.UI
{
    public readonly struct UpdateViewUIS : IEcsRunSystem
    {
        readonly SystemsViewUIGame _systemsUI;
        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesViewUIGame _eUIGame;
        readonly EntitiesModelGame _eMGame;

        public UpdateViewUIS(in SystemsViewUIGame systemsUI, in EntitiesModelCommon eMCommon, in EntitiesViewUIGame eUIGame, in EntitiesModelGame eMGame)
        {
            _systemsUI = systemsUI;
            _eMCommon = eMCommon;
            _eUIGame = eUIGame;
            _eMGame = eMGame;
        }

        public void Run()
        {
            ///Right
            ///
            var rightEs = _eUIGame.RightEs;
            new RightZoneUIS(_eUIGame, _eMGame).Run();
            new StatsUIS(_eUIGame, _eMGame).Run();
            _systemsUI.ProtectS.Run(rightEs.ProtectE, _eMGame.UnitEs(_eMGame.CellsC.Selected), _eMGame.CurPlayerITC.Player);
            _systemsUI.RelaxS.Run(rightEs.RelaxE, _eMGame);
            new ShieldUIS(_eUIGame, _eMGame).Run();
            _systemsUI.EffectsS.Run(_eMGame.Resources, _eUIGame, _eMGame);
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                new UniqueButtonUIS(buttonT, _eUIGame.RightEs.Unique(buttonT), _eMGame.Resources, _eMGame).Run();
            }


            ///Down
            new DonerUIS(_eUIGame.DownEs.DonerE, _eMGame).Run();
            new DownPawnUIS(_eUIGame.DownEs.PawnE, _eMGame).Run();
            new DownToolWeaponUIS(_eUIGame.DownEs.ToolWeaponE, _eMGame).Run();
            new DownHeroUIS(_eUIGame.DownEs.HeroE, _eMGame).Run();
            _eUIGame.DownEs.CostE.Sync(_eMGame);


            ///Up
            _systemsUI.EconomyUpS.Run(_eUIGame, _eMGame);
            new UpWindUIS(_eUIGame, _eMGame).Run();
            new UpSunsUIS(_eUIGame, _eMGame).Run();


            ///Center
            new CenterSelectorUIS(_eUIGame, _eMGame).Run();
            new CenterEndGameUIS(_eUIGame, _eMGame).Run();
            new CenterReadyUIS(_eUIGame, _eMGame).Run();
            new CenterKingUIS(_eUIGame, _eMGame).Run();
            new CenterFriendUIS().Run(_eMCommon.GameModeTC, _eUIGame, _eMGame);
            new CenterHeroesUIS(_eUIGame, _eMGame).Run();
            new CenterBuildingZonesUIS(_eUIGame, _eMGame).Run();
            MotionUIS.Sync(_eMGame.TimerUpdateUIC.Timer, _eUIGame, _eMGame);
            _eUIGame.CenterEs.MistakeE.Sync(_eMGame.TimerUpdateUIC.Timer, _eMGame);


            ///Left
            new LeftZonesUIS(_eUIGame, _eMGame).Run();
            new EnvUIS(_eUIGame, _eMGame).Run();
            new LeftCityUIS(_eUIGame, _eMGame).Run();
        }
    }
}