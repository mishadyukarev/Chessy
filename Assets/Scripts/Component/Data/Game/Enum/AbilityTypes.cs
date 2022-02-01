namespace Game.Game
{
    public enum AbilityTypes
    {
        None,

        //King
        CircularAttack,
        BonusNear,

        //Pawn
        FirePawn,
        PutOutFirePawn,
        Seed,
        ///Building
        Farm,
        Mine,
        City,
        DestroyBuilding,

        //Archer
        FireArcher,
        ChangeCornerArcher,

        //Elfemale
        GrowAdultForest,
        StunElfemale,
        ChangeDirectionWind,

        //Snowy
        IceWall,
        ActiveIceWall,
        //DefendAround,

        End
    }
}