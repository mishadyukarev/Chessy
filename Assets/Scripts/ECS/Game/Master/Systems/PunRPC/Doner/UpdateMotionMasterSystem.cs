using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

internal sealed class UpdateMotionMasterSystem : RPCMasterSystemReduction
{

    public override void Run()
    {
        base.Run();


        _sMM.TryInvokeRunSystem(nameof(FireUpdatorMasterSystem), _sMM.RpcSystems);
        _sMM.TryInvokeRunSystem(nameof(ExtractionUpdatorMasterSystem), _sMM.RpcSystems);
        //_sMM.TryInvokeRunSystem(nameof(FertilizeUpdatorMasterSystem), _sMM.RPCSystems);



        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                var xy = new int[] { x, y };

                if (CellUnitsDataWorker.HaveAnyUnit(xy))
                {
                    CellUnitsDataWorker.RefreshAmountSteps(xy);
                }
            }


        _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(true, false);
        _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(false, false);

        _eGGUIM.MotionEnt_AmountCom.Amount += 1;


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
    }
}
