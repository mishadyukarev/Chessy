using Chessy.Common.Enum;
using System;
using System.Collections.Generic;

namespace Chessy.Common.Entity
{
    public sealed class EntitiesModelCommon
    {
        public ShopC ShopC;
        public AdC AdC;  
        public UpdateAllViewC UpdateAllViewC;
        public SettingsC SettingsC;
        public BookC BookC;
        public CommonInfoAboutGameC CommonInfoAboutGameC;
        public SoundMusicActionsC SoundMusicActionsC;

        public bool NeedUpdateView
        {
            get => UpdateAllViewC.NeedUpdateView;
            set => UpdateAllViewC.NeedUpdateView = value;
        }
        internal ref float ForUpdateViewTimer => ref UpdateAllViewC.ForUpdateViewTimer;
        public PageBookTypes OpenedNowPageBookT
        {
            get => BookC.OpenedNowPageBookT;
            set => BookC.OpenedNowPageBookT = value;
        }
        public TestModes TestModeT => CommonInfoAboutGameC.TestModeT;
        public DateTime StartGameTime => CommonInfoAboutGameC.StartGameTime;
        public GameModeTypes GameModeT
        {
            get => CommonInfoAboutGameC.GameModeT;
            set => CommonInfoAboutGameC.GameModeT = value;
        }
        public SceneTypes SceneT
        {
            get => CommonInfoAboutGameC.SceneT;
            internal set => CommonInfoAboutGameC.SceneT = value;
        }
        public Action SoundActionC(in ClipCommonTypes clipT) => SoundMusicActionsC.SoundActionC(clipT);

        public EntitiesModelCommon(in TestModes testModeT, in Dictionary<ClipCommonTypes, Action> sound)
        {
            var sound2 = new Dictionary<ClipCommonTypes, Action>();
            foreach (var item in sound) sound2.Add(item.Key, new Action(item.Value));
            SoundMusicActionsC = new SoundMusicActionsC(sound2);

            CommonInfoAboutGameC = new CommonInfoAboutGameC(testModeT, DateTime.Now);    
        }
    }
}