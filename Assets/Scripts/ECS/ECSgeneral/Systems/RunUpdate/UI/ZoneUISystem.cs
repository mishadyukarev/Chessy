using Leopotam.Ecs;
using System.Collections.Generic;

internal class ZoneUISystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<ZoneComponent> _zoneComponentRef = default;
    private PhotonPunRPC _photonPunRPC = default;


    internal ZoneUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _zoneComponentRef = eCSmanager.EntitiesGeneralManager.ZoneComponentRef;

        _zoneComponentRef.Unref().XYMasterZone = new List<int[]>();
        _zoneComponentRef.Unref().XYOtherZone = new List<int[]>();
    }

    public void Run()
    {
        //foreach (var item in _zoneComponentRef.Unref().XYMasterZone)
        //{
        //    CellSupportVisionComponent(item).ActiveVision(true, SupportVisionTypes.Zone, InstanceGame.MasterClient);
        //}
    }
}
