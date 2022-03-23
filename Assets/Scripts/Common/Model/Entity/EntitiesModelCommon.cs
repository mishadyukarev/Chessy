using Chessy.Common.Component;
using Chessy.Common.Enum;
using Chessy.Common.Interface;
using System;
using System.Collections.Generic;

namespace Chessy.Common.Entity
{
    public sealed class EntitiesModelCommon : IToggleScene
    {
        readonly Dictionary<ClipTypes, ActionC> _sound;

        public bool IsOnHint;
        public bool WasLikeGameZone;
        public bool IsOpenSettings;
        public float VolumeMusic;


        public BookC BookC;
        public TestModeC TestModeC;
        public AdC AdC;
        public TimeStartGameC TimeStartGameC;
        public SceneC SceneC;
        public GameModeTC GameModeTC;

        public ActionC SoundActionC(in ClipTypes clipT) => _sound[clipT];


        public EntitiesModelCommon(in TestModes testMode, in Dictionary<ClipTypes, ActionC> sound)
        {
            _sound = sound;

            IsOnHint = testMode != TestModes.Standart;
            VolumeMusic = testMode == TestModes.Standart ? 0 : 0.2f;

            var nowTime = DateTime.Now;
            AdC = new AdC(nowTime);
            TimeStartGameC = new TimeStartGameC(nowTime);
            BookC = new BookC(PageBoookTypes.Main, false);
            TestModeC = new TestModeC(testMode);

            SceneC.Scene = SceneTypes.Menu;
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            if (SceneC.Is(newSceneT)) throw new Exception("Need other scene");

            SceneC.Scene = newSceneT;

            switch (newSceneT)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        break;
                    }

                case SceneTypes.Game:
                    {
                        BookC.IsOpenedBook = true;
                        BookC.PageBookT = PageBoookTypes.Main;
                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}