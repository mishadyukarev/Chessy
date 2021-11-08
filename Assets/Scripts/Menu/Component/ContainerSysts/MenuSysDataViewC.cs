using System;

namespace Chessy.Menu
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

