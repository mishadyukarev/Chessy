using System;

namespace Chessy.Menu
{
    public struct SystemsManager
    {
        static Action _runUpdate;
        static Action _launchLikeGame;
        public SystemsManager(in bool def)
        {
            new EventSys();

            _launchLikeGame 
                = new LaunchLikeGameAndShopSys().Run;

            _runUpdate =
                (Action)new SyncSys().Run + new ConnectorMenuSys().Run;


            LaunchLikeGame();
        }

        public static void RunUpdate() => _runUpdate.Invoke();
        public static void LaunchLikeGame() => _launchLikeGame.Invoke();
    }
}