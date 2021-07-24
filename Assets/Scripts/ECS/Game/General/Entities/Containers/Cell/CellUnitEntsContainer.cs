using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;

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



        internal CellUnitEntsContainer(EcsEntity[,] cellUnitEnts) : base(cellUnitEnts)
        {
            _cellUnitEnts = cellUnitEnts;
        }
    }
}
