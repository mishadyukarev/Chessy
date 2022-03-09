namespace Chessy.Game
{
    public enum AbilityTypes
    {
        None,

        ///King
        CircularAttack,
        KingPassiveNearBonus,

        ///Pawn
        FirePawn,
        PutOutFirePawn,
        Seed,
        SetFarm,
        //Mine,
        SetCity,
        DestroyBuilding,

        ///Archer
        FireArcher,
        ChangeCornerArcher,

        ///Elfemale
        GrowAdultForest,
        StunElfemale,

        ///Snowy
        IceWall,
        ActiveAroundBonusSnowy,
        DirectWave,
        ChangeDirectionWind,
        //DefendAround,

        ///Undead
        Resurrect,
        SetTeleport,
        Teleport,
        InvokeSkeletons,

        End
    }
}