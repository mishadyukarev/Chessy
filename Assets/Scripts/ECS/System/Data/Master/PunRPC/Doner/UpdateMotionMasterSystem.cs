using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;

internal sealed class UpdateMotionMasterSystem : SystemMasterReduction
{

    public override void Run()
    {
        base.Run();

        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                if (CellUnitsDataContainer.HaveAnyUnit(xy))
                {
                    CellUnitsDataContainer.RefreshAmountSteps(xy);
                }
            }


        DownDonerUIDataContainer.SetDoned(true, false);
        DownDonerUIDataContainer.SetDoned(false, false);

        Main.Instance.ECSmanager.EntGameGeneralUIViewManager.MotionEnt_AmountCom.AmountMotions += 1;


        int amountAdultForest = 0;

        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.AdultForest, xy)
                    && CellViewContainer.IsActiveSelfParentCell(xy))
                {
                    ++amountAdultForest;
                }
            }

        if (amountAdultForest <= 3)
        {
            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
            SysGameMasterManager.TryInvokeRunSystem(nameof(TruceMasterSystem), SysGameMasterManager.RpcSystems);
        }
    }
}
