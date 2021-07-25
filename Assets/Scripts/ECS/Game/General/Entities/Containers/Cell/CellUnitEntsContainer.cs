using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellUnitEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellUnitEnts;
        internal ref CellUnitComponent CellUnitEnt_CellUnitCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>();
        internal ref OwnerComponent CellUnitEnt_CellOwnerCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<OwnerComponent>();
        internal ref OwnerBotComponent CellUnitEnt_CellOwnerBotCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<OwnerBotComponent>();
        internal ref UnitTypeComponent CellUnitEnt_UnitTypeCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<UnitTypeComponent>();
        internal ref IsVisibleDictComponent CellUnitEnt_IsVisibleDictCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<IsVisibleDictComponent>();
        internal ref ProtectRelaxComponent CellUnitEnt_ProtectRelaxCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<ProtectRelaxComponent>();
        internal ref SpriteRendererComponent CellUnitEnt_SpriteRendererCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();



        internal CellUnitEntsContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellUnitEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var sr = cellParentGOs[x, y].transform.Find("Unit").GetComponent<SpriteRenderer>();
                    _cellUnitEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new CellUnitComponent())
                        .Replace(new UnitTypeComponent())
                        .Replace(new OwnerComponent())
                        .Replace(new OwnerBotComponent())
                        .Replace(new IsVisibleDictComponent(new Dictionary<bool, bool>()))
                        .Replace(new ProtectRelaxComponent())
                        .Replace(new SpriteRendererComponent(sr));
                }
        }
    }
}
