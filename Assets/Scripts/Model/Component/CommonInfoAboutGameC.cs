using Chessy.Common;
using System;

namespace Chessy
{
    public struct CommonInfoAboutGameC
    {
        public readonly TestModes TestModeT;
        public readonly DateTime StartGameTime;
        public GameModeTypes GameModeT { get; set; }
        public SceneTypes SceneT { get; internal set; }

        internal CommonInfoAboutGameC(in TestModes testModeT, in DateTime startGameTime) : this()
        {
            TestModeT = testModeT;
            StartGameTime = startGameTime;
        }
    }
}