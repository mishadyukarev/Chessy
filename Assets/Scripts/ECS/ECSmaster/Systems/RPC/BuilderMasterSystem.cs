using Photon.Pun;

internal sealed class BuilderMasterSystem : RPCMasterSystemReduction
{
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;
    private BuildingTypes BuildingType => _eMM.RPCMasterEnt_RPCMasterCom.BuildingType;

    internal BuilderMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();


        if (_eGM.CellEnt_CellUnitCom(XyCell).HaveMaxSteps && !_eGM.CellBuildingEnt_BuildingTypeCom(XyCell).HaveBuilding)
        {
            var isMasterInfo = Info.Sender.IsMasterClient;

            bool canSet = false;

            bool haveFood = true;
            bool haveWood = true;
            bool haveOre = true;
            bool haveIron = true;
            bool haveGold = true;

            var foodAmountDict = _eGM.FoodEnt_AmountDictCom.AmountDict;
            var woodAmountDict = _eGM.WoodEAmountDictC.AmountDict;
            var oreAmountDict = _eGM.OreEAmountDictC.AmountDict;
            var ironAmountDict = _eGM.IronEAmountDictC.AmountDict;
            var goldAmountDict = _eGM.GoldEAmountDictC.AmountDict;

            int minusFood = default;
            int minusWood = default;
            int minusOre = default;
            int minusIron = default;
            int minusGold = default;


            switch (BuildingType)
            {
                case BuildingTypes.None:
                    break;


                case BuildingTypes.City:

                    _cellWorker.SetBuilding(BuildingType, Info.Sender, XyCell);
                    //_eGM.CellBuildingEnt_CellBuildingCom(XyCell).SetBuilding(BuildingType, Info.Sender);
                    _eGM.CellEnt_CellUnitCom(XyCell).AmountSteps = 0;

                    _eGM.InfoEnt_BuildingsInfoCom.IsSettedCityDict[Info.Sender.IsMasterClient] = true;
                    _eGM.InfoEnt_BuildingsInfoCom.XySettedCityDict[Info.Sender.IsMasterClient] = XyCell;

                    if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveAdultTree) _eGM.CellEnvEnt_CellEnvCom(XyCell).ResetEnvironment(EnvironmentTypes.AdultForest);
                    if (_eGM.CellEnvEnt_CellEnvCom(XyCell).HaveFertilizer) _eGM.CellEnvEnt_CellEnvCom(XyCell).ResetEnvironment(EnvironmentTypes.Fertilizer);

                    break;


                case BuildingTypes.Farm:

                    canSet = _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveFertilizer;

                    if (canSet)
                    {
                        minusFood = _startValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                        minusWood = _startValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                        minusOre = _startValuesGameConfig.ORE_FOR_BUILDING_FARM;
                        minusIron = _startValuesGameConfig.IRON_FOR_BUILDING_FARM;
                        minusGold = _startValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                    }

                    break;


                case BuildingTypes.Woodcutter:

                    canSet = _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveAdultTree;

                    if (canSet)
                    {
                        minusFood = _startValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                        minusWood = _startValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                        minusOre = _startValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                        minusIron = _startValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                        minusGold = _startValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                    }

                    break;


                case BuildingTypes.Mine:

                    canSet = _eGM.CellEnvEnt_CellEnvCom(XyCell).HaveHill;

                    if (canSet)
                    {
                        minusFood = _startValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                        minusWood = _startValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                        minusOre = _startValuesGameConfig.ORE_FOR_BUILDING_MINE;
                        minusIron = _startValuesGameConfig.IRON_FOR_BUILDING_MINE;
                        minusGold = _startValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                    }

                    break;


                default:
                    break;
            }

            if (BuildingType != BuildingTypes.City)
            {
                if (canSet)
                {
                    haveFood = foodAmountDict[isMasterInfo] >= minusFood;
                    haveWood = woodAmountDict[isMasterInfo] >= minusWood;
                    haveOre = oreAmountDict[isMasterInfo] >= minusOre;
                    haveIron = ironAmountDict[isMasterInfo] >= minusIron;
                    haveGold = goldAmountDict[isMasterInfo] >= minusGold;

                    if (haveFood && haveWood && haveOre && haveIron && haveGold)
                    {
                        foodAmountDict[isMasterInfo] -= minusFood;
                        woodAmountDict[isMasterInfo] -= minusWood;
                        oreAmountDict[isMasterInfo] -= minusOre;
                        ironAmountDict[isMasterInfo] -= minusIron;
                        goldAmountDict[isMasterInfo] -= minusGold;


                        _cellWorker.SetBuilding(BuildingType, Info.Sender, XyCell);
                        _eGM.CellEnt_CellUnitCom(XyCell).AmountSteps = 0;
                    }
                }
            }

            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
        }
    }
}
