using Chessy.Common.Component;
using Chessy.Common.Enum;
using Chessy.Common.Model.Component;
using Chessy.Common.Model.Entity;
using System;
using System.Collections.Generic;

namespace Chessy.Common.Entity
{
    public sealed class EntitiesModelCommon
    {
        readonly Dictionary<Enum.ClipCommonTypes, ActionC> _sound = new Dictionary<Enum.ClipCommonTypes, ActionC>();

        public ShopC ShopC;

        public TestModeC TestModeC;
        public AdC AdC;
        public TimeStartGameC TimeStartGameC;
        public GameModeTC GameModeTC;

        public BookE BookE;
        public ref PageBookTC PageBookTC => ref BookE.PageBookTC;
        public PageBookTypes PageBookT => PageBookTC.PageBookT;
        public ref bool IsOpenedBook => ref BookE.IsOpenedBook;

        public SceneTC SceneTC;
        public SceneTypes SceneT => SceneTC.SceneT;

        public bool IsOnHint { get; internal set; }
        public bool WasLikeGameZone { get; internal set; }
        public bool IsOpenSettings;

        public ActionC SoundActionC(in Enum.ClipCommonTypes clipT) => _sound[clipT];

        public EntitiesModelCommon(in TestModes testMode, in Dictionary<Enum.ClipCommonTypes, Action> sound)
        {
            foreach (var item in sound) _sound.Add(item.Key, new ActionC(item.Value));

            IsOnHint = testMode != TestModes.Standart;

            var nowTime = DateTime.Now;
            AdC = new AdC(nowTime);
            TimeStartGameC = new TimeStartGameC(nowTime);
            TestModeC = new TestModeC(testMode);



            BookE = new BookE(PageBookTypes.Main, false);

            SceneTC.SceneT = SceneTypes.Menu;
        }
    }
}