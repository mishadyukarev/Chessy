using System;

namespace Game.Menu
{
    public struct MenuSVC
    {
        public static Action LaunchLikeGame { get; private set; }

        public MenuSVC(Action launchLikeGame)
        {
            LaunchLikeGame = launchLikeGame;
        }
    }
}

