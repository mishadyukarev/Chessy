using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Entities
{
    internal sealed class CellBuildingEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellBuildingEnts;
        internal ref CellBuildingComponent CellBuildEnt_CellBuilCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<CellBuildingComponent>();
        internal ref BuildingTypeComponent CellBuildEnt_BuilTypeCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();
        internal ref OwnerComponent CellBuildEnt_OwnerCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();
        internal ref OwnerBotComponent CellBuildEnt_OwnerBotCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerBotComponent>();
        internal ref SpriteRendererComponent CellBuildEnt_SpriteRendererCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellBackBuildingEnts;
        internal ref SpriteRendererComponent CellBackBuildingEnt_SpriteRendererCom(int[] xy) => ref _cellBackBuildingEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();

        internal CellBuildingEntsContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellBuildingEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellBackBuildingEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("Building").gameObject;

                    var sr = parentGO.GetComponent<SpriteRenderer>();

                    _cellBuildingEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr))
                        .Replace(new CellBuildingComponent())
                        .Replace(new BuildingTypeComponent())
                        .Replace(new OwnerComponent())
                        .Replace(new OwnerBotComponent());


                    sr = parentGO.transform.Find("BackBuilding").GetComponent<SpriteRenderer>();

                    _cellBackBuildingEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }
    }
}
