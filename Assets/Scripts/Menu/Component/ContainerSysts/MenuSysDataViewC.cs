using System;

namespace Scripts.Menu
{
    public struct MenuSysDataViewC
    {
        public static Action LaunchLikeGame { get; private set; }

        public MenuSysDataViewC(Action launchLikeGame)
        {
            LaunchLikeGame = launchLikeGame;
        }
    }
}

