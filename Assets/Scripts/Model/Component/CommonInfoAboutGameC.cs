using Chessy.Model;
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
        internal RaycastTypes RaycastT;
        internal CellClickTypes CellClickT;
        internal bool IsSelectedCity;
        internal bool HaveTreeUnitInGame;
        internal bool IsActivatedIdxAndXyInfoCells;
        internal int AmountPlantedYoungForests;

        public readonly TestModeTypes TestModeT;
        public readonly DateTime StartGameTime;

        public GameModeTypes GameModeType => GameModeT;
        public SceneTypes SceneType => SceneT;
        public LessonTypes LessonType => LessonT;
        public PlayerTypes WinnerPlayerType => WinnerPlayerT;
        public PlayerTypes CurrentPlayerIType => CurrentPlayerIT;
        public bool IsStartedGameP => IsStartedGame;
        public AbilityTypes AbilityType => AbilityT;
        public RaycastTypes RaycastType => RaycastT;
        public CellClickTypes CellClickType => CellClickT;
        public bool IsSelectedCityP => IsSelectedCity;
        public bool IsActivatedIdxAndXyInfoCellsP => IsActivatedIdxAndXyInfoCells;

        internal CommonInfoAboutGameC(in TestModeTypes testModeT, in DateTime startGameTime)
        {
            TestModeT = testModeT;
            StartGameTime = startGameTime;
        }

        internal void Dispose()
        {
            IsStartedGame = default;
            LessonT = default;
            CellClickT = default;
            IsSelectedCity = default;
            HaveTreeUnitInGame = default;
            AmountPlantedYoungForests = default;
            WinnerPlayerT = default;
        }
    }
}