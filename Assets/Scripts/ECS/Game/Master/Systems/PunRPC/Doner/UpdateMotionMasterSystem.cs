using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;
using System.Collections.Generic;

internal sealed class UpdateMotionMasterSystem : RPCMasterSystemReduction
{
    private Dictionary<bool, int> _amountMotionsWithoutFood = new Dictionary<bool, int>();
    private int _countForResetUnitMaster = 2;
    private int _countForResetUnitOther = 2;
    private Dictionary<bool, int> _amountMotionsWithoutFoodForTruce = new Dictionary<bool, int>();

    public override void Init()
    {
        base.Init();

        _amountMotionsWithoutFood.Add(true, 0);
        _amountMotionsWithoutFood.Add(false, 0);

        _amountMotionsWithoutFoodForTruce.Add(true, 0);
        _amountMotionsWithoutFoodForTruce.Add(false, 0);
    }


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

                if (CellUnitWorker.HaveAnyUnit(xy))
                {
                    CellUnitWorker.RefreshAmountSteps(xy);
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

                if (CellEnvironmentWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy)
                    && CellWorker.IsActiveSelfCell(xy))
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
