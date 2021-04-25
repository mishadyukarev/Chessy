using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public partial class PhotonPunRPC : MonoBehaviour
{
    internal void RefreshAll() => _photonView.RPC("RefreshAllMaster", RpcTarget.MasterClient);

    [PunRPC]
    private void RefreshAllMaster()
    {
        List<object> listObjects = new List<object>();
        for (int x = 0; x < _startValues.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < _startValues.CELL_COUNT_Y; y++)
            {
                listObjects.Add(CellUnitComponent(x, y).UnitType);
                listObjects.Add(CellUnitComponent(x, y).ActorNumber);
                listObjects.Add(CellUnitComponent(x, y).AmountSteps);
                listObjects.Add(CellUnitComponent(x, y).AmountHealth);
                listObjects.Add(CellUnitComponent(x, y).PowerDamage);
                listObjects.Add(CellUnitComponent(x, y).IsProtected);
                listObjects.Add(CellUnitComponent(x, y).IsRelaxed);

                listObjects.Add(CellEnvironmentComponent(x, y).HaveFood);
                listObjects.Add(CellEnvironmentComponent(x, y).HaveTree);
                listObjects.Add(CellEnvironmentComponent(x, y).HaveHill);
                listObjects.Add(CellEnvironmentComponent(x, y).HaveMountain);

                listObjects.Add(CellBuildingComponent(x, y).BuildingType);
            }
        }
        object[] objects = new object[listObjects.Count];
        for (int i = 0; i < objects.Length; i++) objects[i] = listObjects[i];

        _photonView.RPC("RefreshCellsGeneral", RpcTarget.Others, objects);

        objects = new object[]
        {
            _economyMasterComponentRef.Unref().GoldOther,
            _economyBuildingsMasterComponentRef.Unref().IsBuildedCityOther,
            _economyBuildingsMasterComponentRef.Unref().XYsettedCityOther,
            _economyUnitsMasterComponentRef.Unref().IsSettedKingOther,
        };
        _photonView.RPC("RefreshEconomyGeneral", RpcTarget.Others, objects);

        objects = new object[]
        {
            _economyMasterComponentRef.Unref().GoldMaster,
            _economyBuildingsMasterComponentRef.Unref().IsBuildedCityMaster,
            _economyBuildingsMasterComponentRef.Unref().XYsettedCityMaster,
            _economyUnitsMasterComponentRef.Unref().IsSettedKingMaster,
        };
        _photonView.RPC("RefreshEconomyGeneral", RpcTarget.MasterClient, objects);
    }

    [PunRPC]
    private void RefreshCellsGeneral(object[] objects)
    {
        int i = 0;
        for (int x = 0; x < _startValues.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < _startValues.CELL_COUNT_Y; y++)
            {
                UnitTypes unitType = (UnitTypes)objects[i++];
                int actorNumber = (int)objects[i++];
                int amountSteps = (int)objects[i++];
                int amountHealth = (int)objects[i++];
                int powerDamage = (int)objects[i++];
                bool isProtected = (bool)objects[i++];
                bool isRelaxed = (bool)objects[i++];

                bool haveFood = (bool)objects[i++];
                bool haveTree = (bool)objects[i++];
                bool haveHill = (bool)objects[i++];
                bool haveMountain = (bool)objects[i++];

                BuildingTypes buildingType = (BuildingTypes)objects[i++];



                Player player;
                if (actorNumber == -1) player = default;
                else player = PhotonNetwork.PlayerList[actorNumber - 1];
                CellUnitComponent(x, y).SetUnit(unitType, amountHealth, powerDamage, amountSteps, isProtected, isRelaxed, player);

                CellEnvironmentComponent(x, y).SetResetEnvironment(haveFood, EnvironmentTypes.Food);
                CellEnvironmentComponent(x, y).SetResetEnvironment(haveTree, EnvironmentTypes.Tree);
                CellEnvironmentComponent(x, y).SetResetEnvironment(haveHill, EnvironmentTypes.Hill);
                CellEnvironmentComponent(x, y).SetResetEnvironment(haveMountain, EnvironmentTypes.Mountain);

                CellBuildingComponent(x, y).SetBuilding(buildingType);
            }
        }
    }

    [PunRPC]
    private void RefreshEconomyGeneral(object[] objects)
    {
        var gold = (int)objects[0];
        var isSettedCity = (bool)objects[1];
        int[] xySettedCity = (int[])objects[2];
        bool isSettedKing = (bool)objects[3];

        _economyComponentRef.Unref().Gold = gold;
        _economyBuildingsComponentRef.Unref().IsSettedCity = isSettedCity;
        _economyBuildingsComponentRef.Unref().XYsettedCity = xySettedCity;

        _economyUnitsComponentRef.Unref().IsSettedKing = isSettedKing;
    }

}
