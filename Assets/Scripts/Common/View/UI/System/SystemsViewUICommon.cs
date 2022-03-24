using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Common.Interface;
using Chessy.Common.View.System;
using Chessy.Game.System.View.UI.Center;
using System;

namespace Chessy.Common.View.UI.System
{
    public sealed class SystemsViewUICommon : IEcsRunSystem, IToggleScene
    {
        readonly EntitiesModelCommon _eMCommon;
        readonly EntitiesViewCommon _eVCommon;
        readonly EntitiesViewUICommon _eUICommon;

        public readonly SyncMusicSoundVS SyncMusicSoundS;
        public readonly SyncBookUIS SyncBookS;
        public readonly SyncSettingsUIS SyncSettingsS;


        public SystemsViewUICommon(in EntitiesModelCommon eMC, in EntitiesViewCommon eVC, in EntitiesViewUICommon eUIC)
        {
            _eMCommon = eMC;
            _eVCommon = eVC;
            _eUICommon = eUIC;
        }

        public void Run()
        {
            SyncBookS.Sync(_eUICommon.BookE, _eMCommon.BookE);
            SyncMusicSoundS.Sync(_eMCommon, _eVCommon);
            SyncSettingsS.Sync(_eMCommon.IsOpenSettings, _eUICommon.SettingsE);
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            switch (newSceneT)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        _eUICommon.CanvasE.MenuCanvasGOC.SetActive(true);
                        _eUICommon.CanvasE.GameCanvasGOC.SetActive(false);
                        break;
                    }

                case SceneTypes.Game:
                    {
                        _eUICommon.CanvasE.MenuCanvasGOC.SetActive(false);
                        _eUICommon.CanvasE.GameCanvasGOC.SetActive(true);
                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}