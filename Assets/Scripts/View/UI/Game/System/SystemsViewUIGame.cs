using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.System.View.UI.Down;
using Chessy.Game.View.UI;
using Chessy.Game.View.UI.System;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.System.View.UI
{
    public sealed class SystemsViewUIGame : IUpdate
    {
        readonly EntitiesModelGame _e;

        readonly List<Action> _syncUpdates;

        public SystemsViewUIGame(EntitiesModelCommon eMCommon, EntitiesViewUIGame eUIGame, EntitiesModelGame eMGame)
        {
            _e = eMGame;

            _syncUpdates = new List<Action>()
            {
                //Up
                new EconomyUpUIS(eUIGame.UpEs.EconomyE, eMGame).Sync,
                new UpWindUIS(eUIGame, eMGame).Sync,
                new UpSunsUIS(eUIGame, eMGame).Sync,

                //Center
                new MotionUpUIS(eUIGame.UpEs.MotionsTextC, eMGame).Sync,
                new SkipLessonUIS(eUIGame.CenterEs.SkipLessonE, eMGame).Sync,
                new LessonUIS(eUIGame.CenterEs, eMCommon, eMGame).Sync,
                new CenterEndGameUIS(eUIGame, eMGame).Sync,
                new CenterReadyUIS(eUIGame, eMGame).Sync,
                new CenterKingUIS(eUIGame, eMGame).Sync,
                new CenterHeroesUIS(eUIGame, eMGame).Sync,
                new CenterBuildingZonesUIS(eUIGame, eMGame).Sync,
                new CenterFriendUIS(eMCommon, eUIGame, eMGame).Sync,
                new MistakeUIS(eUIGame.CenterEs.MistakeE, eMGame).Sync,
                new MotionUIS(eUIGame, eMGame).Sync,

                //Down
                new DonerUIS(eUIGame.DownEs.DonerE, eMGame).Sync,
                new DownPawnUIS(eUIGame.DownEs.PawnE, eMGame).Sync,
                new DownToolWeaponUIS(eUIGame.DownEs.ToolWeaponE, eMGame).Sync,
                new DownHeroUIS(eUIGame.DownEs.HeroE, eMGame).Sync,
                new CostUIS(eUIGame.DownEs.ToolWeaponE.CostE, eMGame).Sync,
                new CityButtonUIS(eUIGame.DownEs.CityButtonUIE, eMGame).Sync,

                //Right
                new ProtectUIS(eUIGame.RightEs.ProtectE, eMGame).Sync,
                new RelaxUIS(eUIGame.RightEs.RelaxE,  eMGame).Sync,
                new EffectsUIS(eMGame.Resources, eUIGame, eMGame).Sync,
                new RightZoneUIS(eUIGame, eMGame).Sync,
                new StatsUIS(eUIGame.RightEs.StatsEs, eMGame).Sync,

                //Left
                new LeftZonesUIS(eUIGame, eMGame).Sync,
                new EnvUIS(eUIGame, eMGame).Sync,
                new LeftCityUIS(eUIGame, eMGame).Sync,


                () => 
                {
                    if (eMCommon.IsOpenedBook)
                    {
                        eUIGame.DownEs.BookLittleE.AnimationVC.Play();
                    }


                    if(eMGame.MistakeT == MistakeTypes.NeedMoreSteps)
                    {
                        eUIGame.RightEs.StatsEs.EnergyE.AnimationC.Play();
                    }

                    if (eMGame.LessonT == LessonTypes.RelaxExtractPawn)
                    {
                        eUIGame.RightEs.RelaxE.AnimationC.Play();
                    }

                    else if (eMGame.LessonT == LessonTypes.SettingPawn)
                    {
                        eUIGame.DownEs.PawnE.AnimationC.Play();
                    }

                },
            };



            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _syncUpdates.Add(new UniqueButtonUIS(buttonT, eUIGame.RightEs.Unique(buttonT), eMGame.Resources, eMGame).Sync);
            }
        }

        public void Update()
        {
            if (_e.NeedUpdateView)
            {
                _syncUpdates.ForEach((Action action) => action());





                _e.NeedUpdateView = false;
            }
        }
    }
}