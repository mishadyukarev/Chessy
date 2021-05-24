﻿using Leopotam.Ecs;
using UnityEngine;
using static MainGame;

internal class EntitiesMasterManager : EntitiesManager
{
    private EcsEntity _masterRPCEntity;
    internal ref RPCMasterComponent RPCMasterEnt_RPCMasterCom => ref _masterRPCEntity.Get<RPCMasterComponent>();


    public EntitiesMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateEntities(ECSmanager eCSmanager)
    {
        var eGM = eCSmanager.EntitiesGeneralManager;

        #region Cells

        for (int x = 0; x < Instance.StartValuesGameConfig.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < Instance.StartValuesGameConfig.CELL_COUNT_Y; y++)
            {
                int random;

                random = Random.Range(1, 100);
                if (random <= Instance.StartValuesGameConfig.PERCENT_MOUNTAIN)
                    eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(true, EnvironmentTypes.Mountain);

                if(!eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).HaveMountain)
                {
                    random = Random.Range(1, 100);
                    if (random <= Instance.StartValuesGameConfig.PERCENT_TREE)
                        eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(true, EnvironmentTypes.Tree);

                    random = Random.Range(1, 100);
                    if (random <= Instance.StartValuesGameConfig.PERCENT_HILL)
                        eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(true, EnvironmentTypes.Hill);


                    if (!eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).HaveTree)
                    {
                        random = Random.Range(1, 100);
                        if (random <= Instance.StartValuesGameConfig.PERCENT_FOOD)
                            eCSmanager.EntitiesGeneralManager.CellEnvEnt_CellEnvironmentCom(x, y).SetResetEnvironment(true, EnvironmentTypes.Food);
                    }
                }
            }
        }

        #endregion


        #region Economy

        eGM.FoodEnt_AmountDictCom.AmountDict[true] = Instance.StartValuesGameConfig.AMOUNT_FOOD_MASTER;
        eGM.WoodEAmountDictC.AmountDict[true] = Instance.StartValuesGameConfig.AMOUNT_WOOD_MASTER;
        eGM.OreEAmountDictC.AmountDict[true] = Instance.StartValuesGameConfig.AMOUNT_ORE_MASTER;
        eGM.IronEAmountDictC.AmountDict[true] = Instance.StartValuesGameConfig.AMOUNT_IRON_MASTER;
        eGM.GoldEAmountDictC.AmountDict[true] = Instance.StartValuesGameConfig.AMOUNT_GOLD_MASTER;

        eGM.FoodEnt_AmountDictCom.AmountDict[false] = Instance.StartValuesGameConfig.AMOUNT_FOOD_OTHER;
        eGM.WoodEAmountDictC.AmountDict[false] = Instance.StartValuesGameConfig.AMOUNT_WOOD_OTHER;
        eGM.OreEAmountDictC.AmountDict[false] = Instance.StartValuesGameConfig.AMOUNT_ORE_OTHER;
        eGM.IronEAmountDictC.AmountDict[false] = Instance.StartValuesGameConfig.AMOUNT_IRON_OTHER;
        eGM.GoldEAmountDictC.AmountDict[false] = Instance.StartValuesGameConfig.AMOUNT_GOLD_OTHER;

        #endregion


        #region Info

        eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[true] = false;
        eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[false] = false;

        eGM.InfoEnt_UnitsInfoCom.AmountKingDict[true] = Instance.StartValuesGameConfig.AMOUNT_KING_MASTER;
        eGM.InfoEnt_UnitsInfoCom.AmountKingDict[false] = Instance.StartValuesGameConfig.AMOUNT_KING_OTHER;

        eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[true] = Instance.StartValuesGameConfig.AMOUNT_PAWN_MASTER;
        eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[false] = Instance.StartValuesGameConfig.AMOUNT_PAWN_OTHER;

        eGM.InfoEnt_UnitsInfoCom.AmountRookDict[true] = Instance.StartValuesGameConfig.AMOUNT_ROOK_MASTER;
        eGM.InfoEnt_UnitsInfoCom.AmountRookDict[false] = Instance.StartValuesGameConfig.AMOUNT_ROOK_OTHER;

        eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[true] = Instance.StartValuesGameConfig.AMOUNT_BISHOP_MASTER;
        eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[false] = Instance.StartValuesGameConfig.AMOUNT_BISHOP_OTHER;

        eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[true] = default;
        eGM.InfoEnt_BuildingsInfoCom.AmountFarmDict[false] = default;

        eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[true] = default;
        eGM.InfoEnt_BuildingsInfoCom.AmountWoodcutterDict[false] = default;

        eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[true] = default;
        eGM.InfoEnt_BuildingsInfoCom.AmountMineDict[false] = default;

        #endregion


        #region BuilderEntity

        _masterRPCEntity = GameWorld.NewEntity();

        _masterRPCEntity.Replace(new RPCMasterComponent());

        #endregion

    }
}
