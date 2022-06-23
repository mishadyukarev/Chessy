namespace Chessy.Model
{
    public enum AbilityTypes : byte
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