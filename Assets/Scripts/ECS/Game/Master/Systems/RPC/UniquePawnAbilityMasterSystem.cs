using Assets.Scripts;
using Photon.Pun;

internal sealed class UniquePawnAbilityMasterSystem : RPCMasterSystemReduction
{
    internal PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;
    private UniqueAbilitiesPawnTypes UniqueAbilitiesPawnType => _eMM.RPCMasterEnt_RPCMasterCom.UniqueAbilitiesPawnType;
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMinAmountSteps && _eGM.CellBuildEnt_BuilTypeCom(XyCell).BuildingType != BuildingTypes.City)
        {
            bool haveFood = true;
            bool haveWood = true;
            bool haveOre = true;
            bool haveIron = true;
            bool haveGold = true;

            int minusFood = default;
            int minusWood = default;
            int minusOre = default;
            int minusIron = default;
            int minusGold = default;

            switch (UniqueAbilitiesPawnType)
            {
                case UniqueAbilitiesPawnTypes.AbilityOne:
                    if (_eGM.CellEffectEnt_CellEffectCom(XyCell).HaveFire)
                    {
                        _eGM.CellEffectEnt_CellEffectCom(XyCell).SetResetEffect(false, EffectTypes.Fire);
                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps -= 1;
                    }
                    else if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveAdultTree)
                    {
                        minusFood = 0;
                        minusWood = 5;
                        minusOre = 0;
                        minusIron = 0;
                        minusGold = 0;

                        haveFood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient) >= minusFood;
                        haveWood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood,Info.Sender.IsMasterClient) >= minusWood;
                        haveOre = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient) >= minusOre;
                        haveIron = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient) >= minusIron;
                        haveGold = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient) >= minusGold;

                        if (haveFood && haveWood && haveOre && haveIron && haveGold)
                        {
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient, minusFood);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient, minusWood);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient, minusOre);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient, minusIron);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient, minusGold);

                            _eGM.CellEffectEnt_CellEffectCom(XyCell).SetResetEffect(true, EffectTypes.Fire);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps -= 1;
                        }
                        else
                        {
                            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
                        }
                    }
                    break;

                case UniqueAbilitiesPawnTypes.AbilityTwo:

                    if (!_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveFertilizer && !_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveAdultTree && !_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveYoungTree)
                    {
                        minusFood = 0;
                        minusWood = 5;
                        minusOre = 0;
                        minusIron = 0;
                        minusGold = 0;

                        haveFood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient) >= minusFood;
                        haveWood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient) >= minusWood;
                        haveOre = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient) >= minusOre;
                        haveIron = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient) >= minusIron;
                        haveGold = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient) >= minusGold;

                        if (haveFood && haveWood && haveOre && haveIron && haveGold)
                        {
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient, minusFood);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient, minusWood);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient, minusOre);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient, minusIron);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient, minusGold);

                            _eGM.CellEnvEnt_CellEnvCom(XyCell).SetNewEnvironment(EnvironmentTypes.Fertilizer);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps -= 1;
                        }
                        else
                        {
                            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
                        }
                    }
                    break;

                case UniqueAbilitiesPawnTypes.AbilityThree:
                    if (!_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveFertilizer && !_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveAdultTree && !_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveYoungTree)
                    {
                        minusFood = 5;
                        minusWood = 0;
                        minusOre = 0;
                        minusIron = 0;
                        minusGold = 0;

                        haveFood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient) >= minusFood;
                        haveWood = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood,Info.Sender.IsMasterClient) >= minusWood;
                        haveOre = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient) >= minusOre;
                        haveIron = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient) >= minusIron;
                        haveGold = _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient) >= minusGold;

                        if (haveFood && haveWood && haveOre && haveIron && haveGold)
                        {
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Food, Info.Sender.IsMasterClient, minusFood);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Wood, Info.Sender.IsMasterClient, minusWood);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Ore, Info.Sender.IsMasterClient, minusOre);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Iron, Info.Sender.IsMasterClient, minusIron);
                            _eGM.EconomyEnt_EconomyCom.TakeAmountResources(ResourceTypes.Gold, Info.Sender.IsMasterClient, minusGold);
                            _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps -= 1;

                            _eGM.CellEnvEnt_CellEnvCom(XyCell).SetNewEnvironment(EnvironmentTypes.YoungForest);
                        }
                        else
                        {
                            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
                        }
                    }
                    break;

                default:
                    break;
            }

        }
    }
}