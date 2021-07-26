using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellFireEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellFireEnts;
        internal ref SpriteRendererComponent CellFireEnt_SprRendCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref HaveFireComponent CellFireEnt_HaverEffectCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<HaveFireComponent>();
        internal ref TimeStepsComponent CellFireEnt_TimeStepsCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<TimeStepsComponent>();


        internal CellFireEntsContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellFireEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("Fire").gameObject;

                    var sr = parentGO.GetComponent<SpriteRenderer>();
                    _cellFireEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr))
                        .Replace(new HaveFireComponent())
                        .Replace(new TimeStepsComponent());
                }
        }
    }
}
