using Leopotam.Ecs;

internal class ZoneUISystem : CellGeneralReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC = default;

    internal ZoneUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }

    public void Run()
    {
        //foreach (var item in _zoneComponentRef.Unref().XYMasterZone)
        //{
        //    CellSupportVisionComponent(item).ActiveVision(true, SupportVisionTypes.Zone, InstanceGame.MasterClient);
        //}
    }
}
