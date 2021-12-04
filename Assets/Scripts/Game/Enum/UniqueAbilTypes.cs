namespace Game.Game
{
    public enum UniqueAbilTypes
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