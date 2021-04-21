using Leopotam.Ecs;


internal class AttackForMasterSystem : CellReduction
{
    private EcsComponentRef<UnitPathsComponent> _unitPathComponentRef = default;

    internal AttackForMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {

    }
}
