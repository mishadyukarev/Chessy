using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct EntitiesPool
    {


        public static SelectedIdxE SelectedIdxE { get; private set; }
        public static CurrentIdxE CurrentIdxE { get; private set; }


        public EntitiesPool(in EcsWorld gameW, in List<object> forData, in List<string> namesMethods)
        {
            var i = 0;

            var actions = (List<object>)forData[i++];
            var isActiveParenCells = (bool[])forData[i++];
            var idCells = (int[])forData[i++];
            var sounds0 = (Dictionary<ClipTypes, System.Action>)forData[i++];
            var sounds1 = (Dictionary<UniqueAbilityTypes, System.Action>)forData[i++];


            new EntityPool(gameW, actions, namesMethods);

            new CellTrailEs(gameW);
            new CellBuildE(gameW);
            new CellEnvironmentEs(gameW);
            new CellFireEs(gameW);
            new EntityCellCloudPool(gameW);
            new CellRiverE(gameW);
            new CellEs(gameW, isActiveParenCells, idCells);
            new CellParentE(gameW);

            new CurrentDirectWindE(gameW);
            new CenterCloudEnt(gameW);
            new DirectsWindForElfemaleE(gameW);

            new AvailableCenterUpgradeEs(gameW);
            new AvailableCenterHeroEs(gameW);
            new UnitStatUpgradesEs(gameW);
            new BuildingUpgradesEs(gameW);

            SelectedIdxE = new SelectedIdxE(gameW);
            CurrentIdxE = new CurrentIdxE(gameW);

            new EntWhereEnviroments(gameW);
            new WhereUnitsE(gameW);
            new WhereBuildsE(gameW);

            new InventorUnitsE(gameW);
            new InventorResourcesE(gameW);
            new InventorToolWeaponE(gameW);

            new CellsForSetUnitsEs(gameW);
            new CellsForShiftUnitsEs(gameW);
            new CellsForAttackUnitsEs(gameW);
            new CellsForArsonArcherEs(gameW);

            new SelectedToolWeaponE(gameW);
            new WhoseMoveE(gameW);
            new MistakeE(gameW);
            new EntHint(gameW);
            new SelectedUnitE(gameW);
            new StatUnitsUpgradesE(gameW);
            new GetterUnitsEs(gameW);
            new SoundE(gameW, sounds0, sounds1);
            new SunSidesE(gameW);
            new SelectedUniqueAbilityC(gameW);



            new EntityMPool(gameW);
            new FreezeDirectEnemyME(gameW);
            new IceWallME(gameW);


        }
    }
}