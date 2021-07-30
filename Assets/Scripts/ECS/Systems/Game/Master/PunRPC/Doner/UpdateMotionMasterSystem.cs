using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;
using UnityEngine;

internal sealed class UpdateMotionMasterSystem : RPCMasterSystemReduction
{

    public override void Run()
    {
        base.Run();

        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                if (CellUnitsDataWorker.HaveAnyUnit(xy))
                {
                    CellUnitsDataWorker.RefreshAmountSteps(xy);
                }
            }


        DownDonerUIWorker.SetDoned(true, false);
        DownDonerUIWorker.SetDoned(false, false);

        _eGGUIM.MotionEnt_AmountCom.AmountMotions += 1;


        int amountAdultForest = 0;

        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy)
                    && CellViewWorker.IsActiveSelfParentCell(xy))
                {
                    ++amountAdultForest;
                }
            }

        if (amountAdultForest <= 3)
        {
            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
            _sMM.TryInvokeRunSystem(nameof(TruceMasterSystem), _sMM.RpcSystems);
        }


        Debug.Log("Units " + InfoAmountUnitsWorker.GetAmountAllUnitsInGame());
        Debug.Log("Buildings " + InfoBuidlingsWorker.GetAmountAllBuild());
    }
}
