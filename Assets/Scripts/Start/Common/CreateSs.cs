using System;

namespace Chessy.Common
{
    public sealed class CreateSs
    {
        public CreateSs(Action<SceneTypes> toggleScene)
        {
            new EventSys();
            new IAPCore();

            new DataSC((Action)new MyYodo().Run
                + new AdLaunchS().Run, toggleScene);
        }
    }
}