using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using Chessy.Game.Extensions;
using Chessy.Game.Model;
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
        readonly EntitiesModelCommon _eMC;
        readonly EntitiesViewUIGame _eUIGame;
        readonly EntitiesModelGame _e;

        readonly MotionUpUIS _motionUpS;
        readonly RelaxUIS _relaxS;
        readonly EconomyUpUIS _economyUpS;
        readonly EffectsUIS _effectsS;
        readonly SyncBookUIS _syncBookS;
        readonly ProtectUIS _protectS;
        readonly MistakeUIS _mistakeS;
        readonly SkipLessonUIS _skipLessonUIS;

        public SystemsViewUIGame(in EntitiesModelCommon eMCommon, in EntitiesViewUIGame eUIGame, in EntitiesModelGame eMGame)
        {
            _eMC = eMCommon;
            _eUIGame = eUIGame;
            _e = eMGame;

            _economyUpS = new EconomyUpUIS(new Dictionary<ResourceTypes, float>());
            _effectsS = new EffectsUIS(new Dictionary<EffectTypes, bool>());
            _mistakeS = new MistakeUIS(_eUIGame.CenterEs.MistakeE, eMGame);
            _skipLessonUIS = new SkipLessonUIS(_eUIGame.CenterEs.SkipLessonE, eMGame);


            _motionUpS = new MotionUpUIS(eUIGame.UpEs.MotionsTextC, eMGame);

            _runs = new List<IEcsRunSystem>()
            {
                new LessonUIS(eUIGame.CenterEs, eMCommon, eMGame),
            };
        }

        public void Run()
        {
            _timer += Time.deltaTime;

            if (_e.NeedUpdateView || _timer > 0.5f)
            {
                _mistakeS.Sync(_timer);
                MotionUIS.Sync(_timer, _eUIGame, _e);

                _timer = 0;

                if (_e.NeedUpdateView)
                {
                    _runs.ForEach((IEcsRunSystem iRun) => iRun.Run());

                    ///Right
                    ///
                    var rightEs = _eUIGame.RightEs;
                    new RightZoneUIS(_eUIGame, _e).Run();
                    new StatsUIS(_eUIGame, _e).Run();
                    _protectS.Run(rightEs.ProtectE, _e.UnitEs(_e.CellsC.Selected), _e.CurPlayerITC.PlayerT);
                    _relaxS.Run(rightEs.RelaxE, _e);
                    _effectsS.Run(_e.Resources, _eUIGame, _e);
                    for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
                    {
                        new UniqueButtonUIS(buttonT, _eUIGame.RightEs.Unique(buttonT), _e.Resources, _e).Run();
                    }


                    ///Down
                    new DonerUIS(_eUIGame.DownEs.DonerE, _e).Run();
                    new DownPawnUIS(_eUIGame.DownEs.PawnE, _e).Run();
                    new DownToolWeaponUIS(_eUIGame.DownEs.ToolWeaponE, _e).Run();
                    new DownHeroUIS(_eUIGame.DownEs.HeroE, _e).Run();
                    new CostUIS().Sync(_eUIGame.DownEs.ToolWeaponE.CostE, _e);

                    if (!_e.LessonTC.HaveLesson || _e.LessonTC.LessonT >= LessonTypes.OpeningTown)
                    {
                        _eUIGame.DownEs.CityButtonUIE.ParentGOC.SetActive(true);
                    }
                    else
                    {
                        _eUIGame.DownEs.CityButtonUIE.ParentGOC.SetActive(false);
                    }





                    ///Up
                    _economyUpS.Run(_eUIGame, _e);
                    new UpWindUIS(_eUIGame, _e).Run();
                    _motionUpS.Run();


                    ///Center
                    new CenterEndGameUIS(_eUIGame, _e).Run();
                    new CenterReadyUIS(_eUIGame, _e).Run();
                    new CenterKingUIS(_eUIGame, _e).Run();
                    new CenterFriendUIS().Run(_eMC.GameModeTC, _eUIGame, _e);
                    new CenterHeroesUIS(_eUIGame, _e).Run();
                    new CenterBuildingZonesUIS(_eUIGame, _e).Run();
                    _skipLessonUIS.Sync();

                    new UpSunsUIS(_eUIGame, _e).Run();


                    var nextPlayerT = _e.CurPlayerITC.PlayerT.NextPlayer();
                    var haveElfemaleEnemy = _e.PlayerInfoE(nextPlayerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale);
                    var haveSnowyEnemy = _e.PlayerInfoE(nextPlayerT).GodInfoE.UnitTC.Is(UnitTypes.Snowy);
                    _eUIGame.CenterEs.HeroE(UnitTypes.Elfemale).ButtonC.SetActiveParent(!haveElfemaleEnemy);
                    _eUIGame.CenterEs.HeroE(UnitTypes.Snowy).ButtonC.SetActiveParent(!haveSnowyEnemy);


                    ///Left
                    new LeftZonesUIS(_eUIGame, _e).Run();
                    new EnvUIS(_eUIGame, _e).Run();
                    new LeftCityUIS(_eUIGame, _e).Run();

                    _e.NeedUpdateView = false;
                }
            }
        }
    }
}