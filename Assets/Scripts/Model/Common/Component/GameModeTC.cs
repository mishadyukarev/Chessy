namespace Chessy.Common
{
    public struct GameModeTC
    {
        public GameModes GameModeT;

        public bool Is(params GameModes[] gameModes)
        {
            if (gameModes == default) throw new System.Exception();

            foreach (var gameMode in gameModes)
                if (gameMode == GameModeT) return true;
            return false;
        }
    }
}
