using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class AvailableCellsDataContainerEnts
    {
        private EcsEntity _availableCellsSettingEnt;
        internal ref AvailableCellsComponent AvailableCellsSettingEnt_AvailCellsCom => ref _availableCellsSettingEnt.Get<AvailableCellsComponent>();


        private EcsEntity _availableCellsShiftEnt;
        internal ref AvailableCellsComponent AvailableCellsShiftEnt_AvailCellsCom => ref _availableCellsShiftEnt.Get<AvailableCellsComponent>();


        private EcsEntity _availableCellsSimpleAttackEnt;
        internal ref AvailableCellsComponent AvailableCellsSimpleAttackEnt_AvailCellsCom => ref _availableCellsSimpleAttackEnt.Get<AvailableCellsComponent>();


        private EcsEntity _availableCellsUniqueAttackEnt;
        internal ref AvailableCellsComponent AvailableCellsUniqueAttackEnt_AvailCellsCom => ref _availableCellsUniqueAttackEnt.Get<AvailableCellsComponent>();

        internal AvailableCellsDataContainerEnts(EcsWorld gameWorld)
        {
            _availableCellsSettingEnt = gameWorld.NewEntity()
                 .Replace(new AvailableCellsComponent(new List<int[]>()));
            _availableCellsShiftEnt = gameWorld.NewEntity()
                .Replace(new AvailableCellsComponent(new List<int[]>()));
            _availableCellsSimpleAttackEnt = gameWorld.NewEntity()
                .Replace(new AvailableCellsComponent(new List<int[]>()));
            _availableCellsUniqueAttackEnt = gameWorld.NewEntity()
                .Replace(new AvailableCellsComponent(new List<int[]>()));
        }
    }
}
