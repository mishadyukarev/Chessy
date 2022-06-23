﻿using Chessy.Common;
using System;

namespace Chessy
{
    public struct CommonInfoAboutGameC
    {
        public readonly TestModeTypes TestModeT;
        public readonly DateTime StartGameTime;
        public GameModeTypes GameModeT { get; set; }
        public SceneTypes SceneT { get; internal set; }

        internal CommonInfoAboutGameC(in TestModeTypes testModeT, in DateTime startGameTime) : this()
        {
            TestModeT = testModeT;
            StartGameTime = startGameTime;
        }
    }
}