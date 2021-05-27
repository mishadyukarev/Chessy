using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using static MainGame;

internal class BuilderMasterSystem : RPCMasterSystemReduction, IEcsRunSystem
{
    private int[] XyCell => _eMM.RPCMasterEnt_RPCMasterCom.XyCell;
    private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;
    private BuildingTypes BuildingType => _eMM.RPCMasterEnt_RPCMasterCom.BuildingType;

    internal BuilderMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();


        if (_eGM.CellEnt_CellUnitCom(XyCell).HaveMaxSteps && !_eGM.CellEnt_CellBuildingCom(XyCell).HaveBuilding)
        {
            var isMasterInfo = Info.Sender.IsMasterClient;

            bool canSet = false;

            bool haveFood = true;
            bool haveWood = true;
            bool haveOre = true;
            bool haveIron = true;
            bool haveGold = true;

            Dictionary<bool, int> currentBuildingsDict = new Dictionary<bool, int>();

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

                    _eGM.CellEnt_CellBuildingCom(XyCell).SetBuilding(BuildingType, Info.Sender);
                    _eGM.CellEnt_CellUnitCom(XyCell).AmountSteps = 0;

                    if (_eGM.CellEnt_CellEnvCom(XyCell).HaveAdultTree) _eGM.CellEnt_CellEnvCom(XyCell).SetResetEnvironment(false, EnvironmentTypes.AdultForest);
                    if (_eGM.CellEnt_CellEnvCom(XyCell).HaveFertilizer) _eGM.CellEnt_CellEnvCom(XyCell).SetResetEnvironment(false, EnvironmentTypes.Fertilizer);

                    break;


                case BuildingTypes.Farm:

                    canSet = _eGM.CellEnt_CellEnvCom(XyCell).HaveFertilizer;

                    if (canSet)
                    {
                        minusFood = StartValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                        minusWood = StartValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                        minusOre = StartValuesGameConfig.ORE_FOR_BUILDING_FARM;
                        minusIron = StartValuesGameConfig.IRON_FOR_BUILDING_FARM;
                        minusGold = StartValuesGameConfig.GOLD_FOR_BUILDING_FARM;

                        currentBuildingsDict = _eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict;
                    }

                    break;


                case BuildingTypes.Woodcutter:

                    canSet = _eGM.CellEnt_CellEnvCom(XyCell).HaveAdultTree;

                    if (canSet)
                    {
                        minusFood = StartValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                        minusWood = StartValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                        minusOre = StartValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                        minusIron = StartValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                        minusGold = StartValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;

                        currentBuildingsDict = _eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict;
                    }

                    break;


                case BuildingTypes.Mine:

                    canSet = _eGM.CellEnt_CellEnvCom(XyCell).HaveHill;

                    if (canSet)
                    {
                        minusFood = StartValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                        minusWood = StartValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                        minusOre = StartValuesGameConfig.ORE_FOR_BUILDING_MINE;
                        minusIron = StartValuesGameConfig.IRON_FOR_BUILDING_MINE;
                        minusGold = StartValuesGameConfig.GOLD_FOR_BUILDING_MINE;

                        currentBuildingsDict = _eGM.InfoEnt_BuildingsInfoCom.AmountMineDict;
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


                        _eGM.CellEnt_CellBuildingCom(XyCell).SetBuilding(BuildingType, Info.Sender);
                        currentBuildingsDict[isMasterInfo] += 1; // !!!!!

                        _eGM.CellEnt_CellUnitCom(XyCell).AmountSteps = 0;
                    }
                }
            }

            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
        }
    }
}
