using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using static MainGame;

internal class BuilderMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC;
    private int[] XyCell => _eMM.MasterRPCEntXyCellCom.XyCell;
    private PhotonMessageInfo Info => _eGM.GeneralRPCEntFromInfoCom.FromInfo;
    private BuildingTypes BuildingType => _eMM.MasterRPCEntBuildingTypeCom.BuildingType;

    internal BuilderMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = Instance.PhotonGameManager.PhotonPunRPC;
    }

    public void Run()
    {
        if (_eGM.CellUnitEnt_CellUnitCom(XyCell).HaveMaxSteps && !_eGM.CellBuildingEnt_BuildingTypeCom(XyCell).HaveBuilding)
        {
            var isMasterInfo = Info.Sender.IsMasterClient;

            bool canSet = false;

            bool haveFood = true;
            bool haveWood = true;
            bool haveOre = true;
            bool haveIron = true;
            bool haveGold = true;

            Dictionary<bool, int> currentBuildingsDict = new Dictionary<bool, int>();

            var foodAmountDict = _eGM.FoodEAmountDictC.AmountDict;
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

                    _eGM.CellBuildingEnt_CellBuildingCom(XyCell).SetBuilding(BuildingType, Info.Sender);
                    _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps = 0;

                    if (_eGM.CellEnvEnt_CellEnvironmentCom(XyCell).HaveTree) _eGM.CellEnvEnt_CellEnvironmentCom(XyCell).SetResetEnvironment(false, EnvironmentTypes.Tree);
                    if (_eGM.CellEnvEnt_CellEnvironmentCom(XyCell).HaveFood) _eGM.CellEnvEnt_CellEnvironmentCom(XyCell).SetResetEnvironment(false, EnvironmentTypes.Food);

                    break;


                case BuildingTypes.Farm:

                    canSet = _eGM.CellEnvEnt_CellEnvironmentCom(XyCell).HaveFood;

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

                    canSet = _eGM.CellEnvEnt_CellEnvironmentCom(XyCell).HaveTree;

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

                    canSet = _eGM.CellEnvEnt_CellEnvironmentCom(XyCell).HaveHill;

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


                        _eGM.CellBuildingEnt_CellBuildingCom(XyCell).SetBuilding(BuildingType, Info.Sender);
                        currentBuildingsDict[isMasterInfo] += 1; // !!!!!

                        _eGM.CellUnitEnt_CellUnitCom(XyCell).AmountSteps = 0;
                    }
                }
            }

            _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haveFood, haveWood, haveOre, haveIron, haveGold);
        }
    }
}
