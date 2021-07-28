using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
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

        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
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

        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                var xy = new int[] { x, y };

                if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy)
                    && CellWorker.IsActiveSelfParentCell(xy))
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
