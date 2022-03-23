namespace Chessy.Common
{
    public struct GameModeTC
    {
        public GameModes GameMode;
        public bool Is(params GameModes[] gameModes)
        {
            if (gameModes == default) throw new System.Exception();

            foreach (var gameMode in gameModes) 
                if (gameMode == GameMode) return true;
            return false;
        }
    }
}
