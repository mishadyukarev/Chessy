using Leopotam.Ecs;
using System;

namespace Game.Common
{
    public sealed class CreateSs
    {
        public CreateSs(EcsSystems comSysts, Action<SceneTypes> toggleScene)
        {
            var comW = comSysts.World;

            var runUpdate = new EcsSystems(comW)
                .Add(new MyYodo())
                .Add(new AdLaunchS())
                .Add(new EventSys())
                .Add(new IAPCore());


            new DataSC(runUpdate.Run, toggleScene);


            comSysts
                .Add(runUpdate);
        }
    }
}