namespace Chessy.Model.Values
{
    public static partial class ValuesChessy
    {
        #region NOT_CHANGE!!!!!!!!!!!!!!!!!!!!!!!

        public const float MAX_RESOURCES = 1;
        public const float MIN_RESOURCES_FOR_SPAWN = 0.1f;


        #endregion NOT_CHANGE!!!!!!!!!!!!!!!!!!!!!!!


        #region Fertilize

        public const float DRY_FERTILIZE_DURING_UPDATE_TAKING = MAX_RESOURCES * 0.05f;
        public const float RIVER_FERTILIZE_AROUND = MAX_RESOURCES * 0.1f;

        public const float FARM_EXTRACT = MAX_RESOURCES * 0.1f;
        public const float FARM_CENTER_UPGRADE = FARM_EXTRACT * 0.5f;

        public const float ADDING_FROM_ICE_WALL = MAX_RESOURCES;

        #endregion


        #region AdultForest

        public const float FIRE_ADULT_FOREST = MAX_RESOURCES / 4;
        public const float AddingAfterBuildingFarm = MAX_RESOURCES / 2;

        public const float WOODCUTTER_EXTRACT = MAX_RESOURCES / 10;
        public const float WOODCUTTER_CENTER_UPGRADE = WOODCUTTER_EXTRACT * 0.5f;

        #endregion


        #region YoungFores

        public const float PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE = 0.75f;

        #endregion


        #region Mountain

        public const float ADDING_FROM_MOUNTAIN = 0.1f;

        #endregion
    }
}
