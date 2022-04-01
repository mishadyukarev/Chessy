namespace Chessy.Game
{
    public enum AbilityTypes
    {
        None,

        ///King
        CircularAttack,
        ///Passive
        KingPassiveNearBonus,


        ///Pawn
        FirePawn,
        PutOutFirePawn,
        Seed,
        SetFarm,
        //Mine,
        //SetCity,
        DestroyBuilding,


        ///Archer
        FireArcher,
        ChangeCornerArcher,


        ///Elfemale
        GrowAdultForest,
        StunElfemale,


        ///Snowy
        IncreaseWindSnowy,
        DecreaseWindSnowy,
        ChangeDirectionWind,


        ///Undead
        Resurrect,
        SetTeleport,
        Teleport,
        InvokeSkeletons,

        End
    }
}