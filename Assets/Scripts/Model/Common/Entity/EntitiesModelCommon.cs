using Chessy.Common.Enum;
using System;
using System.Collections.Generic;

namespace Chessy.Common.Entity
{
    public sealed class EntitiesModelCommon
    {
        readonly Dictionary<Enum.ClipCommonTypes, Action> _sound = new Dictionary<Enum.ClipCommonTypes, Action>();

        public ShopC ShopC;

        public readonly TestModes TestModeT;
        public AdC AdC;
        public readonly DateTime TimeStartGameC;

        public GameModeTypes GameModeT { get; set; }

        public PageBookTypes PageBookT { get; set; }
        public bool IsOpenedBook => PageBookT > PageBookTypes.None;

        public SceneTypes SceneT { get; internal set; }

        public bool WasLikeGameZone { get; internal set; }
        public bool IsOpenSettings;

        public bool NeedUpdateView;

        public Action SoundActionC(in Enum.ClipCommonTypes clipT) => _sound[clipT];

        public EntitiesModelCommon(in TestModes testModeT, in Dictionary<Enum.ClipCommonTypes, Action> sound)
        {
            foreach (var item in sound) _sound.Add(item.Key, new Action(item.Value));

            TimeStartGameC = DateTime.Now;
            TestModeT = testModeT;
        }
    }
}