using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Info;
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
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                var xy = new int[] { x, y };

                if (CellUnitWorker.HaveAnyUnit(xy))
                {
                    CellUnitWorker.RefreshAmountSteps(xy);
                }
            }
        }


        _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(true, false);
        _eGGUIM.DonerUIEnt_IsActivatedDictCom.SetActivated(false, false);

        _eGGUIM.MotionEnt_AmountCom.Amount += 1;

        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, true, 1);
        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, false, 1);


        if (0 > InfoResourcesWorker.AmountResources(ResourceTypes.Food, true))
        {
            ++_amountMotionsWithoutFoodForTruce[true];

            InfoResourcesWorker.SetAmountResources(ResourceTypes.Food, true, 0);
        }
        else
        {
            _amountMotionsWithoutFoodForTruce[true] = 0;
            _amountMotionsWithoutFood[true] = 0;
            _countForResetUnitMaster = 2;
        }



        if (0 > InfoResourcesWorker.AmountResources(ResourceTypes.Food, false))
        {
            ++_amountMotionsWithoutFoodForTruce[false];

            InfoResourcesWorker.SetAmountResources(ResourceTypes.Food, false, 0);


        }
        else
        {
            _amountMotionsWithoutFoodForTruce[false] = 0;
            _amountMotionsWithoutFood[false] = 0;
            _countForResetUnitOther = 2;
        }

        int amountAdultForest = 0;

        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                var xy = new int[] { x, y };

                if (CellEnvironmentWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy)
                    && CellWorker.IsActiveSelfGO(xy))
                {
                    ++amountAdultForest;
                }
            }

        if (amountAdultForest <= 3)
        {
            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
            _sMM.TryInvokeRunSystem(nameof(TruceMasterSystem), _sMM.RpcSystems);
        }



        if (_amountMotionsWithoutFoodForTruce[true] >= 2 && _amountMotionsWithoutFoodForTruce[false] >= 2)
        {
            PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);

            _sMM.TryInvokeRunSystem(nameof(TruceMasterSystem), _sMM.RpcSystems);

            _amountMotionsWithoutFoodForTruce[true] = 0;
            _amountMotionsWithoutFoodForTruce[false] = 0;
        }
        else
        {
            if (++_amountMotionsWithoutFood[true] >= _countForResetUnitMaster)
            {
                var isResetedUnit = false;
                for (int x = 0; x < _eGM.Xamount; x++)
                {
                    for (int y = 0; y < _eGM.Yamount; y++)
                    {
                        var xy = new int[] { x, y };

                        if (CellUnitWorker.HaveAnyUnit(xy))
                        {
                            if (CellUnitWorker.HaveOwner(xy))
                            {
                                if (CellUnitWorker.IsMasterClient(xy))
                                {
                                    if (!CellUnitWorker.IsUnitType(UnitTypes.King, xy))
                                    {
                                        CellUnitWorker.ResetPlayerUnit(xy);
                                        isResetedUnit = true;
                                        _amountMotionsWithoutFood[true] = 0;
                                        _countForResetUnitMaster = 1;
                                        break;
                                    }
                                }
                            }
                        }

                    }
                    if (isResetedUnit) break;
                }
            }

            if (++_amountMotionsWithoutFood[false] >= _countForResetUnitOther)
            {
                var isResetedUnit = false;
                for (int x = 0; x < _eGM.Xamount; x++)
                {
                    for (int y = 0; y < _eGM.Yamount; y++)
                    {
                        var xy = new int[] { x, y };

                        if (CellUnitWorker.HaveAnyUnit(xy))
                        {
                            if (CellUnitWorker.HaveOwner(xy))
                            {
                                if (!CellUnitWorker.IsMasterClient(xy))
                                {
                                    if (!CellUnitWorker.IsUnitType(UnitTypes.King, xy))
                                    {
                                        CellUnitWorker.ResetPlayerUnit(xy);

                                        isResetedUnit = true;
                                        _amountMotionsWithoutFood[false] = 0;
                                        _countForResetUnitOther = 1;
                                        break;
                                    }
                                }
                            }
                        }

                    }
                    if (isResetedUnit) break;
                }
            }

        }
    }
}
