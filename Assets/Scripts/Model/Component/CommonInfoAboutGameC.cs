﻿using Chessy.Model;
using Chessy.Model.Enum;
using System;

namespace Chessy
{
    public sealed class CommonInfoAboutGameC
    {
        internal GameModeTypes GameModeT;
        internal SceneTypes SceneT;
        internal LessonTypes LessonT;
        internal PlayerTypes WinnerPlayerT;
        internal PlayerTypes CurrentPlayerIT;
        internal bool IsStartedGame;
        internal AbilityTypes AbilityT;

        public readonly TestModeTypes TestModeT;
        public readonly DateTime StartGameTime;

        public GameModeTypes GameModeType => GameModeT;
        public SceneTypes SceneType => SceneT;
        public LessonTypes LessonType => LessonT;
        public PlayerTypes WinnerPlayerType => WinnerPlayerT;
        public PlayerTypes CurrentPlayerIType => CurrentPlayerIT;
        public bool IsStartedGameP => IsStartedGame;
        public AbilityTypes AbilityType => AbilityT;

        internal CommonInfoAboutGameC(in TestModeTypes testModeT, in DateTime startGameTime)
        {
            TestModeT = testModeT;
            StartGameTime = startGameTime;
        }

        internal void Dispose()
        {
            IsStartedGame = default;
        }
    }
}