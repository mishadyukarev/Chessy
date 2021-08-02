using Assets.Scripts.ECS.Entities.Game.General.Else.View.Containers;
using Assets.Scripts.Workers;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Entities.Game.General.Else.Vis
{
    public sealed class EntGameGeneralElseViewManager
    {
        private SoundElseViewContainer _soundElseViewContrainer;

        internal EntGameGeneralElseViewManager(EcsWorld gameWorld, EntCommonManager entCommonManager)
        {
            _soundElseViewContrainer = new SoundElseViewContainer(gameWorld, entCommonManager);
            new SoundGameGeneralViewWorker(_soundElseViewContrainer);
        }
    }
}
