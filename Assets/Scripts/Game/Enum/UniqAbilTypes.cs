namespace Chessy.Game
{
    public enum UniqAbilTypes
    {
        Start,
        None = Start,

        //King
        CircularAttack,
        First = CircularAttack,
        BonusNear,

        //Pawn
        FirePawn,
        PutOutFirePawn,
        Seed,

        //Archer
        FireArcher,
        ChangeCornerArcher,

        //Elfemale
        GrowAdultForest,
        StunElfemale,
        ChangeDirWind,

        End
    }
}