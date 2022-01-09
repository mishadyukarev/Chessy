using System;
using UnityEditor;
using UnityEngine;

namespace Game.Menu
{
    public sealed class SystemsManager
    {
        public SystemsManager()
        {
            new EventSys();

            new MenuSVC(new LaunchLikeGameAndShopSys().Run);
            new DataSC((Action)new SyncSys().Run + new ConnectorMenuSys().Run);


            MenuSVC.LaunchLikeGame.Invoke();
        }
    }
}