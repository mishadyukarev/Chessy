namespace Game.Game
{
    public enum UniqueAbilityTypes
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
        ChangeDirectionWind,

        End
    }
}