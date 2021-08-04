using Assets.Scripts.ECS.Entities.Game.General.Else.View.Containers;
using Assets.Scripts.Workers;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.View.Game.General
{
    public sealed class SysViewGameGeneralElseManager : SystemAbstManager
    {
        internal SysViewGameGeneralElseManager(EcsWorld gameWorld) : base(gameWorld)
        {
            //UpdateSystems.Add()

            new SoundGameGeneralViewWorker(new SoundElseViewContainer(gameWorld));
        }
    }
}
