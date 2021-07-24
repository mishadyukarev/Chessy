using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class AvailableCellEntsContainer
    {
        private EcsEntity _availableCellsSettingEnt;
        internal ref AvailableCellsComponent AvailableCellsSettingEnt_AvailCellsCom => ref _availableCellsSettingEnt.Get<AvailableCellsComponent>();


        private EcsEntity _availableCellsShiftEnt;
        internal ref AvailableCellsComponent AvailableCellsShiftEnt_AvailCellsCom => ref _availableCellsShiftEnt.Get<AvailableCellsComponent>();


        private EcsEntity _availableCellsSimpleAttackEnt;
        internal ref AvailableCellsComponent AvailableCellsSimpleAttackEnt_AvailCellsCom => ref _availableCellsSimpleAttackEnt.Get<AvailableCellsComponent>();


        private EcsEntity _availableCellsUniqueAttackEnt;
        internal ref AvailableCellsComponent AvailableCellsUniqueAttackEnt_AvailCellsCom => ref _availableCellsUniqueAttackEnt.Get<AvailableCellsComponent>();

        internal AvailableCellEntsContainer((EcsEntity, EcsEntity, EcsEntity, EcsEntity) ents)
        {
            _availableCellsSettingEnt = ents.Item1;
            _availableCellsShiftEnt = ents.Item2;
            _availableCellsSimpleAttackEnt = ents.Item3;
            _availableCellsUniqueAttackEnt = ents.Item4;
        }
    }
}
