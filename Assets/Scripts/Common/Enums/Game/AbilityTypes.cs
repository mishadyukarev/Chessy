namespace Chessy.Game
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
        ChangeDirectionWind,

        ///Snowy
        IceWall,
        ActiveAroundBonusSnowy,
        DirectWave,
        //DefendAround,

        ///Undead
        Resurrect,
        SetTeleport,
        Teleport,
        InvokeSkeletons,

        End
    }
}