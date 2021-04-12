using Leopotam.Ecs;

public sealed class GetterCellSystem : CellReductionSystem, IEcsRunSystem
{
    private EcsComponentRef<GetterCellComponent> _getterCellComponentRef = default;

    internal GetterCellSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _getterCellComponentRef = eCSmanager.EntitiesGeneralManager.GetterCellComponentRef;
    }

    public void Run()
    {
        if (_getterCellComponentRef.Unref().RaycastHit2dIN != default)
        {
            for (int x = 0; x < Xcount; x++)
            {
                for (int y = 0; y < Ycount; y++)
                {
                    int one = CellComponent((sbyte)x, (sbyte)y).InstanceIDcell;
                    int two = _getterCellComponentRef.Unref().RaycastHit2dIN.transform.gameObject.GetInstanceID();

                    if (one == two)
                    {
                        _getterCellComponentRef.Unref().XYcurrentCellOUT[0] = (sbyte)x;
                        _getterCellComponentRef.Unref().XYcurrentCellOUT[1] = (sbyte)y;

                        _getterCellComponentRef.Unref().SetIsReceiveOUT(true);
                        break;
                    }

                    else _getterCellComponentRef.Unref().SetIsReceiveOUT(false);
                }

                if (_getterCellComponentRef.Unref().IsReceivedOUT) break;
            }
        }
    }
}
