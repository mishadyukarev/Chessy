using Leopotam.Ecs;

internal sealed class ZoneUISystem : SystemGeneralReduction
{
    internal ZoneUISystem() { }

    public override void Run()
    {
        base.Run();

        //foreach (var item in _zoneComponentRef.Unref().XYMasterZone)
        //{
        //    CellSupportVisionComponent(item).ActiveVision(true, SupportVisionTypes.Zone, InstanceGame.MasterClient);
        //}
    }
}
