using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.System.View.UI.Center;
using Chessy.Game.System.View.UI.Down;
using Chessy.Game.View.UI;
using Chessy.Game.View.UI.System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.System.View.UI
{
    public sealed class SystemsViewUIGame : IEcsRunSystem
    {
        readonly List<IEcsRunSystem> _runs;

        float _timer;
        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesViewUIGame _eUIGame;
        readonly EntitiesModelGame _eMGame;

        readonly MotionUpUIS _motionUpS;
        readonly RelaxUIS _relaxS;
        readonly EconomyUpUIS _economyUpS;
        readonly EffectsUIS _effectsS;
        readonly SyncBookUIS _syncBookS;
        readonly ProtectUIS _protectS;


        public SystemsViewUIGame(in EntitiesModelCommon eMCommon, in EntitiesViewUIGame eUIGame, in EntitiesModelGame eMGame)
        {
            _eMCommon = eMCommon;
            _eUIGame = eUIGame;
            _eMGame = eMGame;

            _economyUpS = new EconomyUpUIS(new Dictionary<ResourceTypes, float>());
            _effectsS = new EffectsUIS(new Dictionary<EffectTypes, bool>());


            _motionUpS = new MotionUpUIS(eUIGame.UpEs.MotionsTextC, eMGame);

            _runs = new List<IEcsRunSystem>()
            {
                new LessonUIS(eUIGame.CenterEs, eMCommon, eMGame),
            };
        }

        public void Run()
        {
            if (_eMCommon.SceneTC.Scene != SceneTypes.Game) return;
            

            _timer += Time.deltaTime;

            if (_eMGame.NeedUpdateView || _timer > 0.5f)
            {
                _runs.ForEach((IEcsRunSystem iRun) => iRun.Run());

                ///Right
                ///
                var rightEs = _eUIGame.RightEs;
                new RightZoneUIS(_eUIGame, _eMGame).Run();
                new StatsUIS(_eUIGame, _eMGame).Run();
                _protectS.Run(rightEs.ProtectE, _eMGame.UnitEs(_eMGame.CellsC.Selected), _eMGame.CurPlayerITC.Player);
                _relaxS.Run(rightEs.RelaxE, _eMGame);
                _effectsS.Run(_eMGame.Resources, _eUIGame, _eMGame);
                for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
                {
                    new UniqueButtonUIS(buttonT, _eUIGame.RightEs.Unique(buttonT), _eMGame.Resources, _eMGame).Run();
                }


                ///Down
                new DonerUIS(_eUIGame.DownEs.DonerE, _eMGame).Run();
                new DownPawnUIS(_eUIGame.DownEs.PawnE, _eMGame).Run();
                new DownToolWeaponUIS(_eUIGame.DownEs.ToolWeaponE, _eMGame).Run();
                new DownHeroUIS(_eUIGame.DownEs.HeroE, _eMGame).Run();
                new CostUIS().Sync(_eUIGame.DownEs.ToolWeaponE.CostE, _eMGame);

                if (_eMGame.LessonTC.LessonT  == LessonTypes.None || _eMGame.LessonTC.LessonT >= LessonTypes.OpeningTown)
                {
                    _eUIGame.DownEs.CityButtonUIE.ParentGOC.SetActive(true);
                }
                else
                {
                    _eUIGame.DownEs.CityButtonUIE.ParentGOC.SetActive(false);
                }










                ///Up
                _economyUpS.Run(_eUIGame, _eMGame);
                new UpWindUIS(_eUIGame, _eMGame).Run();
                new UpSunsUIS(_eUIGame, _eMGame).Run();
                _motionUpS.Run();


                ///Center
                new CenterEndGameUIS(_eUIGame, _eMGame).Run();
                new CenterReadyUIS(_eUIGame, _eMGame).Run();
                new CenterKingUIS(_eUIGame, _eMGame).Run();
                new CenterFriendUIS().Run(_eMCommon.GameModeTC, _eUIGame, _eMGame);
                new CenterHeroesUIS(_eUIGame, _eMGame).Run();
                new CenterBuildingZonesUIS(_eUIGame, _eMGame).Run();
                _eUIGame.CenterEs.MistakeE.Sync(_timer, _eMGame);
                MotionUIS.Sync(_timer, _eUIGame, _eMGame);


                ///Left
                new LeftZonesUIS(_eUIGame, _eMGame).Run();
                new EnvUIS(_eUIGame, _eMGame).Run();
                new LeftCityUIS(_eUIGame, _eMGame).Run();

                _eMGame.NeedUpdateView = false;
                _timer = 0;
            }
        }
    }
}