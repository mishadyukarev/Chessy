using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct Entities
    {
        public static SelectedIdxE SelectedIdxE { get; private set; }
        public static CurrentIdxE CurrentIdxE { get; private set; }
        public static WindE WindE { get; private set; }
        public static WinnerE WinnerE { get; private set; }
        public static WhoseMoveE WhoseMoveE { get; private set; }


        public Entities(in EcsWorld gameW, in List<object> forData, in List<string> namesMethods, out int i)
        {
            i = 0;

            var actions = (List<object>)forData[i++];
            var sounds0 = (Dictionary<ClipTypes, System.Action>)forData[i++];
            var sounds1 = (Dictionary<UniqueAbilityTypes, System.Action>)forData[i++];


            new EntityPool(gameW, actions, namesMethods);

            WindE = new WindE(gameW);
            WinnerE = new WinnerE(gameW);
            WhoseMoveE = new WhoseMoveE(gameW);
            SelectedIdxE = new SelectedIdxE(gameW);
            CurrentIdxE = new CurrentIdxE(gameW);

            new AvailableCenterUpgradeEs(gameW);
            new AvailableCenterHeroEs(gameW);
            new UnitStatUpgradesEs(gameW);
            new BuildingUpgradesEs(gameW);


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