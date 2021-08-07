//using Assets.Scripts.ECS.Components;
//using Assets.Scripts.ECS.Game.General.Components;
//using Leopotam.Ecs;
//using Photon.Realtime;
//using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

//namespace Assets.Scripts.ECS.System.Data.Game.General.Cell
//{
//    internal sealed class CellBuildDataSystem : IEcsInitSystem
//    {
//        private EcsWorld _gameWorld;

//        private static EcsEntity[,] _cellBuildingEnts;

//        internal static ref BuildingTypeComponent BuildTypeCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();
//        internal static ref OwnerComponent OwnerCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();
//        internal static ref OwnerBotComponent OwnerBotCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerBotComponent>();
//        internal static ref TimeStepsComponent TimeStepsCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<TimeStepsComponent>();

//        public void Init()
//        {
//            _cellBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

//            for (int x = 0; x < CELL_COUNT_X; x++)
//                for (int y = 0; y < CELL_COUNT_Y; y++)
//                {
//                    _cellBuildingEnts[x, y] = _gameWorld.NewEntity()
//                        .Replace(new BuildingTypeComponent())
//                        .Replace(new OwnerComponent())
//                        .Replace(new OwnerBotComponent())
//                        .Replace(new TimeStepsComponent());
//                }
//        }

//        internal static void SetPlayerBuilding(BuildingTypes buildingType, Player owner, int[] xy)
//        {
//            BuildTypeCom(xy).BuildingType = buildingType;
//            OwnerCom(xy).Owner = owner;
//        }
//        internal static void SetBotBuilding(BuildingTypes buildingType, int[] xy)
//        {
//            BuildTypeCom(xy).BuildingType = buildingType;
//            OwnerBotCom(xy).IsBot = true;
//        }
//        internal static void ResetBuild(int[] xy)
//        {
//            OwnerCom(xy).ResetOwner();
//            OwnerBotCom(xy).Reset();
//            BuildTypeCom(xy).Reset();
//        }
//    }
//}
