using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.View.UI.Entity;
using System;
using System.Collections.Generic;

namespace Chessy.View.UI.System
{
    public sealed class SystemsViewUI : SystemAbstract, IUpdate
    {
        readonly Action[] _syncUpdates;


        public SystemsViewUI(EntitiesViewUI eUI, EntitiesModel eM) : base(eM)
        {
            var syncUpdates = new List<Action>()
            {
                //Up
                new EconomyUpUIS(eUI.UpEs.EconomyE, eM).Sync,
                new UpWindUIS(eUI, eM).Sync,
                new UpSunsUIS(eUI, eM).Sync,
                new MotionUpUIS(eUI.UpEs.MotionsTextC, eM).Sync,

                //Center
                new SkipLessonUIS(eUI.CenterEs.SkipLessonE, eM).Sync,
                new LessonUIS(eUI.CenterEs, eM).Sync,
                new CenterEndGameUIS(eUI, eM).Sync,
                new CenterReadyUIS(eUI, eM).Sync,
                new CenterKingUIS(eUI, eM).Sync,
                new CenterHeroesUIS(eUI, eM).Sync,
                new CenterBuildingZonesUIS(eUI, eM).Sync,
                new CenterFriendUIS(eUI, eM).Sync,
                new MistakeUIS(eUI.CenterEs.MistakeE, eM).Sync,

                //Down
                new DonerUIS(eUI.DownEs.DonerE, eM).Sync,
                new DownPawnUIS(eUI.DownEs.PawnE, eM).Sync,
                new DownToolWeaponUIS(eUI.DownEs.ToolWeaponE, eM).Sync,
                new DownHeroUIS(eUI.DownEs.HeroE, eM).Sync,
                new CostUIS(eUI.DownEs.ToolWeaponE.CostE, eM).Sync,
                new CityButtonUIS(eUI.DownEs.CityButtonUIE, eM).Sync,

                //Right
                new ProtectUIS(eUI.RightEs.ProtectE, eM).Sync,
                new RelaxUIS(eUI.RightEs.RelaxE,  eM).Sync,
                new EffectsUIS(eUI, eM).Sync,
                new RightZoneUIS(eUI, eM).Sync,
                new StatsUIS(eUI.RightEs.StatsEs, eM).Sync,

                //Left
                new LeftZonesUIS(eUI, eM).Sync,
                new EnvUIS(eUI, eM).Sync,
                new LeftCityUIS(eUI, eM).Sync,


                new SyncBookUIS(eUI.BookE, eM).Sync,
                new SyncSettingsUIS(eUI.SettingsE, eM).Sync,
                new ConnectorMenuUIS(eUI, eM).Sync,


                () =>
                {
                    if (_bookC.IsOpenedBook())
                    {
                        eUI.DownEs.BookLittleE.AnimationVC.Play();
                    }


                    if(_mistakeC.MistakeT == MistakeTypes.NeedMoreSteps)
                    {
                        eUI.RightEs.StatsEs.EnergyE.AnimationC.Play();
                    }

                    if (AboutGameC.LessonType == LessonTypes.RelaxExtractPawn)
                    {
                        eUI.RightEs.RelaxE.AnimationC.Play();
                    }

                    else if (AboutGameC.LessonType == LessonTypes.SettingPawn)
                    {
                        eUI.DownEs.PawnE.AnimationC.Play();
                    }


                    eUI.UpEs.SettingsButtonC.SetActiveParent(!AboutGameC.LessonType.HaveLesson());
                    eUI.UpEs.DiscordButtonC.SetActive(!AboutGameC.LessonType.HaveLesson());
                    eUI.UpEs.LeaveButtonC.SetActiveParent(!AboutGameC.LessonType.HaveLesson() || AboutGameC.LessonType >= LessonTypes.MenuInfo);
                    eUI.DownEs.BookLittleE.ButtonC.SetActiveParent(!AboutGameC.LessonType.HaveLesson() || AboutGameC.LessonType > LessonTypes.HoldPressWarrior);

                    eUI.GameCanvasGOC.TrySetActive(AboutGameC.SceneType == SceneTypes.Game);
                    eUI.ShopE.ShopZoneGOC.TrySetActive(_shopC.IsOpenedShopZone);
                    eUI.MenuCanvasGOC.TrySetActive(AboutGameC.SceneType == SceneTypes.Menu);
                },
            };


            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                syncUpdates.Add(new UniqueButtonUIS(buttonT, eUI.RightEs.Unique(buttonT), eM).Sync);
            }


            _syncUpdates = syncUpdates.ToArray();
        }

        public void Update()
        {
            if (_updateAllViewC.NeedUpdateView)
            {
                for (int i = 0; i < _syncUpdates.Length; i++)
                {
                    _syncUpdates[i].Invoke();
                }
            }
        }
    }
}