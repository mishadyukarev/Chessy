﻿using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Cell
{
    internal sealed class ClearAvailCellsSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;

        private EcsFilter<CellsForAttackCom> _availCellsForAttackFilter = default;
        private EcsFilter<CellsArsonArcherComp> _cellsArsonFilter = default;

        public void Run()
        {
            ref var availCellsForAttackComp = ref _availCellsForAttackFilter.Get1(0);

            foreach (byte curIdxCell in _xyCellFilter)
            {
                availCellsForAttackComp.Clear(AttackTypes.Simple, true, curIdxCell);
                availCellsForAttackComp.Clear(AttackTypes.Simple, false, curIdxCell);
                availCellsForAttackComp.Clear(AttackTypes.Unique, true, curIdxCell);
                availCellsForAttackComp.Clear(AttackTypes.Unique, false, curIdxCell);

                _cellsArsonFilter.Get1(0).Clear(true, curIdxCell);
                _cellsArsonFilter.Get1(0).Clear(false, curIdxCell);
            }
        }
    }
}