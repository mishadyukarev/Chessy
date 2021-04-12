using Leopotam.Ecs;
using System;
using TMPro;
using UnityEngine;

public class RefresherMasterSystem : CellReductionSystem, IEcsRunSystem
{
    private EcsComponentRef<RefresherMasterComponent> _refresherMasterComponent = default;
    private EcsComponentRef<EconomyMasterComponent> _economyMasterComponent = default;


    internal RefresherMasterSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _refresherMasterComponent = eCSmanager.EntitiesMasterManager.RefresherMasterComponentRef;
        _economyMasterComponent = eCSmanager.EntitiesMasterManager.EconomyMasterComponentRef;
    }


    public void Run()
    {
        _refresherMasterComponent.Unref().GetValues(out bool isDoneMaster, out bool isDoneOther);

        if (isDoneMaster && isDoneOther)
        {
            for (int x = 0; x < _nameValueManager.CELL_COUNT_X; x++)
            {
                for (int y = 0; y < _nameValueManager.CELL_COUNT_Y; y++)
                {
                    CellUnitComponent(x, y).RefreshAmountSteps();
                }
            }

            _refresherMasterComponent.Unref().SetValues(true);

            _economyMasterComponent.Unref().AddGoldMaster(20);
            _economyMasterComponent.Unref().AddGoldOther(10);
        }
    }
}
