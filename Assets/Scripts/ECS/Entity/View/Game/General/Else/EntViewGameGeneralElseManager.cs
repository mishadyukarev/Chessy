using Assets.Scripts.ECS.Entities.Game.General.Else.View.Containers;
using Assets.Scripts.Workers;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Entities.Game.General.Else.Vis
{
    public sealed class EntViewGameGeneralElseManager
    {
        private SoundElseViewContainer _soundElseViewContrainer;

        internal EntViewGameGeneralElseManager(EcsWorld gameWorld, EntDataCommonElseManager entCommonManager)
        {
            _soundElseViewContrainer = new SoundElseViewContainer(gameWorld, entCommonManager);
            new SoundGameGeneralViewWorker(_soundElseViewContrainer);
        }
    }
}
