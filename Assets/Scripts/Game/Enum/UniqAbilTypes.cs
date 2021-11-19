namespace Game.Game
{
    public enum UniqAbilTypes
    {
        None,
        Start = None,

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