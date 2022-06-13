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
        public GameModeTypes GameModeT
        {
            get => GameModeTC.GameModeT;
            set => GameModeTC.GameModeT = value;
        }

        public BookE BookE;
        public ref PageBookTC PageBookTC => ref BookE.PageBookTC;
        public PageBookTypes PageBookT
        {
            get => PageBookTC.PageBookT;
            set => PageBookTC.PageBookT = value;
        }
        public ref bool IsOpenedBook => ref BookE.IsOpenedBook;

        public SceneTC SceneTC;
        public SceneTypes SceneT => SceneTC.SceneT;

        public bool WasLikeGameZone { get; internal set; }
        public bool IsOpenSettings;

        public ActionC SoundActionC(in Enum.ClipCommonTypes clipT) => _sound[clipT];

        public EntitiesModelCommon(in Dictionary<Enum.ClipCommonTypes, Action> sound)
        {
            foreach (var item in sound) _sound.Add(item.Key, new ActionC(item.Value));
        }
    }
}