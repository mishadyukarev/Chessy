using System;
namespace Chessy.Model.Values
{
    public static class StartValues
    {
        public const byte X_AMOUNT = 15;
        public const byte Y_AMOUNT = 11;
        public const byte CELLS = X_AMOUNT * Y_AMOUNT;

        public const int PEOPLE_IN_CITY = 15;

        public const float MIN_RESOURCES_ENVIRONMENT = 0.1f;

        public const byte CELL_IDX_START_GAME_CLOUD = 60;
        public const byte SPEED_WIND_IN_START_GAME = 1;
        public const float MAX_SPEED_WIND = 3;
        public const float MIN_SPEED_WIND = 0;

        public const DirectTypes DIRECT_WIND = DirectTypes.Right;
        public const SunSideTypes SUN_SIDE = SunSideTypes.Dawn;
        public const PlayerTypes WHOSE_MOVE = PlayerTypes.First;
        public const CellClickTypes CELL_CLICK = CellClickTypes.SimpleClick;
        public const ToolWeaponTypes SELECTED_TOOL_WEAPON = ToolWeaponTypes.Axe;
        public const LevelTypes SELECTED_LEVEL_TOOL_WEAPON = LevelTypes.Second;

        public const float NEED_WOOD_FOR_BUILDING_HOUSE = 0.10f;


        #region Lesson

        public const byte CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON = 69;
        public const byte CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter = 58;
        public const byte CELL_MOUNTAIN_LESSON = 81;

        #endregion


        public const byte CELL_FOR_CLEAR_FOREST_FOR_1_PLAYER = 59;
        public const byte CELL_FOR_CLEAR_FOREST_FOR_2_PLAYER = 61;


        public const int BUILDINGS_FOR_SKIP_LESSON = 6;


        #region Bot

        public const float PERCENT_SHIELD_LEVEL_FIRST_OR_SECOND_FOR_BOT = 0.5f;

        #endregion

        public static float Volume(in ClipTypes clipT, in TestModeTypes testMode)
        {
            switch (clipT)
            {
                case ClipTypes.AttackArcher: return 0.6f;
                case ClipTypes.AttackMelee: return 1;
                case ClipTypes.Building: return 0.1f;
                case ClipTypes.Mistake: return 0.4f;
                case ClipTypes.SoundGoldPack: return 0.3f;
                case ClipTypes.Melting: return 0.3f;
                case ClipTypes.Destroy: return 0.3f;
                case ClipTypes.ClickToTable: return 0.6f;
                case ClipTypes.Truce: return 0.6f;
                case ClipTypes.PickMelee: return 0.1f;
                case ClipTypes.PickArcher: return 0.7f;
                case ClipTypes.WritePensil: return 0.2f;
                case ClipTypes.Leaf: return 0.4f;
                case ClipTypes.KickGround: return 0.1f;
                case ClipTypes.Rock: return 0.2f;
                case ClipTypes.ShortWind: return 0.2f;
                case ClipTypes.ShortRain: return 0.2f;
                case ClipTypes.Music: return testMode == TestModeTypes.Standart ? 0 : 0.2f;
                case ClipTypes.Click: return 0.25f;

                case ClipTypes.Background1: return 1;
                case ClipTypes.Background2: return 0.05f;

                default: return 1;
            }
        }
        public static float Volume(in AbilityTypes abilityT)
        {
            switch (abilityT)
            {
                case AbilityTypes.KingPassiveNearBonus: return 0.3f;

                case AbilityTypes.DestroyBuilding: return 0.1f;
                case AbilityTypes.SetFarm: return 0.1f;
                case AbilityTypes.Seed: return 0.2f;
                case AbilityTypes.FirePawn: return 0.2f;

                case AbilityTypes.FireArcher: return 0.2f;

                case AbilityTypes.GrowAdultForest: return 0.3f;
                case AbilityTypes.StunElfemale: return 0.3f;
                case AbilityTypes.ChangeDirectionWind: return 0.1f;

                case AbilityTypes.Resurrect: return 0.1f;
                case AbilityTypes.SetTeleport: return 0.1f;
                case AbilityTypes.Teleport: return 0.1f;
                case AbilityTypes.InvokeSkeletons: return 0.1f;

                default: return 1;
            }
        }



        public static float SpawnPercent(in EnvironmentTypes env)
        {
            switch (env)
            {
                case EnvironmentTypes.Fertilizer: return 0.3f;
                case EnvironmentTypes.AdultForest: return 0.5f;
                case EnvironmentTypes.Hill: return 0.25f;
                case EnvironmentTypes.Mountain: return 0.3f;
                default: throw new Exception();
            }
        }

        public static float Resources(in ResourceTypes res)
        {
            switch (res)
            {
                case ResourceTypes.None: throw new Exception();
                case ResourceTypes.Food: return 0;
                case ResourceTypes.Wood: return 0;
                case ResourceTypes.Ore: return 0;
                case ResourceTypes.Iron: return 0;
                case ResourceTypes.Gold: return 0;
                default: throw new Exception();
            }
        }
    }
}
