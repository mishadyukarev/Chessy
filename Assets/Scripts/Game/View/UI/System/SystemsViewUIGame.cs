using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.View.UI.Center;
using Chessy.Game.System.View.UI.Down;
using Chessy.Game.View.UI.System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.System.View.UI
{
    public sealed class SystemsViewUIGame : IEcsRunSystem
    {
        float _timer;
        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesViewUIGame _eUIGame;
        readonly EntitiesModelGame _eMGame;

        public readonly RelaxUIS RelaxS;
        public readonly EconomyUpUIS EconomyUpS;
        public readonly EffectsUIS EffectsS;
        public readonly SyncBookUIS SyncBookUIS;
        public readonly ProtectUIS ProtectS;

        public SystemsViewUIGame(in EntitiesModelCommon eMCommon, in EntitiesViewUIGame eUIGame, in EntitiesModelGame eMGame)
        {
            _eMCommon = eMCommon;
            _eUIGame = eUIGame;
            _eMGame = eMGame;

            EconomyUpS = new EconomyUpUIS(new Dictionary<ResourceTypes, float>());
            EffectsS = new EffectsUIS(new Dictionary<EffectTypes, bool>());
        }

        public void Run()
        {
            if (_eMCommon.SceneC.Scene == SceneTypes.Game)
            {
                _timer += Time.deltaTime;

                if (_timer >= 0.04f)
                {
                    ///Right
                    ///
                    var rightEs = _eUIGame.RightEs;
                    new RightZoneUIS(_eUIGame, _eMGame).Run();
                    new StatsUIS(_eUIGame, _eMGame).Run();
                    ProtectS.Run(rightEs.ProtectE, _eMGame.UnitEs(_eMGame.CellsC.Selected), _eMGame.CurPlayerITC.Player);
                    RelaxS.Run(rightEs.RelaxE, _eMGame);
                    new ShieldUIS(_eUIGame, _eMGame).Run();
                    EffectsS.Run(_eMGame.Resources, _eUIGame, _eMGame);
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
                    EconomyUpS.Run(_eUIGame, _eMGame);
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
                    MotionUIS.Sync(_timer, _eUIGame, _eMGame);
                    _eUIGame.CenterEs.MistakeE.Sync(_timer, _eMGame);


                    ///Left
                    new LeftZonesUIS(_eUIGame, _eMGame).Run();
                    new EnvUIS(_eUIGame, _eMGame).Run();
                    new LeftCityUIS(_eUIGame, _eMGame).Run();

                    _timer = 0;
                }
            }
        }
    }
}