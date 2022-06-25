using Chessy.Common;
using Chessy.Common.View.UI;
using Chessy.Common.View.UI.System;
using Chessy.Model.Enum;
using Chessy.Model;
using Chessy.Model.System.View.UI.Down;
using Chessy.Model.View.UI;
using Chessy.Model.View.UI.System;
using Chessy.Menu;
using System;
using System.Collections.Generic;

namespace Chessy.Model.System.View.UI
{
    public sealed class SystemsViewUI : IUpdate
    {
        readonly EntitiesModel _e;
        readonly EntitiesViewUI _eUI;

        readonly List<Action> _syncUpdates;


        readonly SyncBookUIS _syncBookS;
        readonly SyncSettingsUIS _syncSettingsS;
        readonly ConnectorMenuUIS _connectorMenuS;

        public SystemsViewUI(EntitiesViewUI eUI, EntitiesModel eM)
        {
            _e = eM;
            _eUI = eUI;

            _syncUpdates = new List<Action>()
            {
                //Up
                new EconomyUpUIS(eUI.UpEs.EconomyE, eM).Sync,
                new UpWindUIS(eUI, eM).Sync,
                new UpSunsUIS(eUI, eM).Sync,

                //Center
                new MotionUpUIS(eUI.UpEs.MotionsTextC, eM).Sync,
                new SkipLessonUIS(eUI.CenterEs.SkipLessonE, eM).Sync,
                new LessonUIS(eUI.CenterEs, eM).Sync,
                new CenterEndGameUIS(eUI, eM).Sync,
                new CenterReadyUIS(eUI, eM).Sync,
                new CenterKingUIS(eUI, eM).Sync,
                new CenterHeroesUIS(eUI, eM).Sync,
                new CenterBuildingZonesUIS(eUI, eM).Sync,
                new CenterFriendUIS(eUI, eM).Sync,
                new MistakeUIS(eUI.CenterEs.MistakeE, eM).Sync,
                new MotionUIS(eUI, eM).Sync,

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
                new EffectsUIS(eM.Resources, eUI, eM).Sync,
                new RightZoneUIS(eUI, eM).Sync,
                new StatsUIS(eUI.RightEs.StatsEs, eM).Sync,

                //Left
                new LeftZonesUIS(eUI, eM).Sync,
                new EnvUIS(eUI, eM).Sync,
                new LeftCityUIS(eUI, eM).Sync,


                () =>
                {
                    if (eM.BookC.IsOpenedBook())
                    {
                        eUI.DownEs.BookLittleE.AnimationVC.Play();
                    }


                    if(eM.MistakeT == MistakeTypes.NeedMoreSteps)
                    {
                        eUI.RightEs.StatsEs.EnergyE.AnimationC.Play();
                    }

                    if (eM.LessonT == LessonTypes.RelaxExtractPawn)
                    {
                        eUI.RightEs.RelaxE.AnimationC.Play();
                    }

                    else if (eM.LessonT == LessonTypes.SettingPawn)
                    {
                        eUI.DownEs.PawnE.AnimationC.Play();
                    }


                    eUI.UpEs.SettingsButtonC.SetActiveParent(!eM.LessonT.HaveLesson());
                    eUI.UpEs.DiscordButtonC.SetActive(!eM.LessonT.HaveLesson());
                    eUI.UpEs.LeaveC.SetActiveParent(!eM.LessonT.HaveLesson() || eM.LessonT >= LessonTypes.MenuInfo);
                    eUI.DownEs.BookLittleE.ButtonC.SetActiveParent(!eM.LessonT.HaveLesson() || eM.LessonT > LessonTypes.HoldPressReady);

                },
            };

            _syncBookS = new SyncBookUIS(eUI.BookE, eM);
            _syncSettingsS = new SyncSettingsUIS(eUI.SettingsE, eM);
            _connectorMenuS = new ConnectorMenuUIS();

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _syncUpdates.Add(new UniqueButtonUIS(buttonT, eUI.RightEs.Unique(buttonT), eM.Resources, eM).Sync);
            }
        }

        public void Update()
        {
            if (_e.NeedUpdateView)
            {
                _eUI.GameCanvasGOC.SetActive(_e.SceneT == SceneTypes.Game);

                _syncUpdates.ForEach((Action action) => action());

                _syncBookS.Sync();
                _syncSettingsS.Sync();

                _eUI.ShopE.ShopZoneGOC.SetActive(_e.ShopC.IsOpenedShopZone);

                _eUI.MenuCanvasGOC.SetActive(_e.SceneT == SceneTypes.Menu);

                if (_e.SceneT == SceneTypes.Menu)
                {
                    _connectorMenuS.Run(_eUI);
                }
            }
        }
    }
}