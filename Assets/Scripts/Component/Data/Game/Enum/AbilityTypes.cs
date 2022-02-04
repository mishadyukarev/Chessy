namespace Game.Game
{
    public enum AbilityTypes
    {
        None,

        ///King
        CircularAttack,
        BonusNear,

        ///Pawn
        FirePawn,
        PutOutFirePawn,
        Seed,
        Farm,
        Mine,
        City,
        DestroyBuilding,

        ///Archer
        FireArcher,
        ChangeCornerArcher,

        ///Elfemale
        GrowAdultForest,
        StunElfemale,
        ChangeDirectionWind,

        ///Snowy
        IceWall,
        ActiveAroundBonusSnowy,
        DirectWave,
        //DefendAround,

        ///Undead
        Resurrect,


        End
    }
}