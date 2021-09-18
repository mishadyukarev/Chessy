namespace Assets.Scripts.Abstractions.Enums
{
    public enum GameLanguageTypes
    {
        None,

        WaitReady,
        ReadyBeforeGame,
        JoinForFind,

        SetKing,

        NeedMoreResources,
        NeedSetKing,
        NeedMoreSteps,
        NeedOtherPlace,
        NeedMoreHealth,
        PawnMustHaveTool,
        PawnHaveTool,
        NeedSetCity,
        ThatsForOtherUnit,
        NearBorder,

        Motion,

        GiveOrTakeTool,
        PickAdultForest,

        YouAreWinner,
        YouAreLoser,


        GiveTake,
        Done,
        WaitPlayer,

        Create,
        Pawn,
        Rook,
        Bishop,


        EnvironmentInfo,
        Fertilizer,
        Wood,
        Ore,


        Melt,
        UpgradeFarm,
        UpgradeWoodcutter,
        UpgradeMine,



        ConditAbilities,
        Protect,
        Relax,
        Extract,

        UniqueAbilities,
        SeedForest,
        FireForest,
        PutOutFire,
        CircularAttack,

        BuildingAbilities,
        BuildFarm,
        BuildMine,
        BuildCity,
        DestroyBuilding,

    }
}