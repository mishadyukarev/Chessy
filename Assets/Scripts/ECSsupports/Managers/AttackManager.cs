using Leopotam.Ecs;


internal class AttackManager : CellReduction
{
    private EcsComponentRef<UnitPathsComponent> _unitPathComponentRef = default;

    internal AttackManager(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {

    }


}
