namespace Chessy.Common
{
    public struct GameModeTC
    {
        public GameModeTypes GameModeT;

        public bool Is(params GameModeTypes[] gameModes)
        {
            if (gameModes == default) throw new System.Exception();

            foreach (var gameMode in gameModes)
                if (gameMode == GameModeT) return true;
            return false;
        }
        public bool IsOffline => Is(GameModeTypes.TrainingOffline, GameModeTypes.WithFriendOffline);
        public bool IsOnline => !IsOffline;
    }
}
