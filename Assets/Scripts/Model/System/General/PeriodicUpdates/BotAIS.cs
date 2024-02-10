using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.System;
using Chessy.Model.Values;
using Newtonsoft.Json;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static log4net.Appender.RollingFileAppender;


[System.Serializable]
public class ExtractedBotData
{
    public DateTime date_time;
    public int id;

    public int has_tree_on_cell;

    public int has_tree_up;
    public int has_tree_up_right;
    public int has_tree_right;
    public int has_tree_right_down;
    public int has_tree_down;
    public int has_tree_down_left;
    public int has_tree_left;
    public int has_tree_left_up;

    public Dictionary<int, object[]> data = new Dictionary<int, object[]>();

    public ExtractedBotData(params object[] args)
    {
        var i = 0;
        date_time = (DateTime)args[i++];
        id = (int)args[i++];
        has_tree_on_cell = (int)args[i++];

        var hasTreeArr = (int[])args[i++];

        var i_tree = 0;

        has_tree_up = hasTreeArr[i_tree++];
        has_tree_up_right = hasTreeArr[i_tree++];
        has_tree_right = hasTreeArr[i_tree++];
        has_tree_right_down = hasTreeArr[i_tree++];
        has_tree_down = hasTreeArr[i_tree++];
        has_tree_down_left = hasTreeArr[i_tree++];
        has_tree_left = hasTreeArr[i_tree++];
        has_tree_left_up = hasTreeArr[i_tree++];
    }
}

public sealed class BotAIS : SystemModelAbstract
{
    readonly PlayerTypes _botPlayerT;
    DateTime _dateTimeLastUpdate;

    int _idxMove = 0;
    ExtractedBotData[] _df = new ExtractedBotData[10000];



    internal BotAIS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
    {
        _botPlayerT = PlayerTypes.Second;
    }

    internal void Update()
    {
        if ((DateTime.Now - _dateTimeLastUpdate).Seconds >= 1)
        {

            TryToSpawnKing();
            TryToSpawnPawn();
            TryToMovePawn();



            string json = JsonConvert.SerializeObject(_df.Where(x => x != null).ToArray());

            File.WriteAllText(@"C:\Users\misha\OneDrive\Desktop\DS\competitions\playground-series-s4e2\data\myFile.json", json);

 
            Debug.Log(json);


            _dateTimeLastUpdate = DateTime.Now;
        }
    }

    void TryToMovePawn()
    {
        for (byte iCell_0 = 0; iCell_0 < IndexCellsValues.CELLS; iCell_0++)
        {
            if (_cellCs[iCell_0].IsBorder) continue;
            if (_unitCs[iCell_0].UnitT != UnitTypes.Pawn) continue;
            if (_shiftingUnitCs[iCell_0].IsShifting) continue;


            if (_unitCs[iCell_0].PlayerT == _botPlayerT)
            {
                foreach (var iCell_around_1 in _idxsAroundCellCs[iCell_0].IdxCellsAroundArray)
                {
                    if (_cellCs[iCell_around_1].IsBorder) continue;
                    if (_unitCs[iCell_around_1].HaveUnit) continue;
                    if (UnityEngine.Random.value >= 0.3) continue;

                    var hasTreeOnCellInt = _environmentCs[iCell_0].HaveEnvironment(EnvironmentTypes.AdultForest) ? 1 : 0;

                    var hasTreeArr = new int[(int)DirectTypes.End];

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        hasTreeArr[(int)dirT] = _environmentCs[_cellsByDirectAroundC[iCell_0].Get(dirT)].HaveEnvironment(EnvironmentTypes.AdultForest) ? 1 : 0;
                    }

                    _df[_idxMove] = new ExtractedBotData(new object[] { DateTime.Now, 0, hasTreeOnCellInt, hasTreeArr });
                    _idxMove++;


                    _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(_s.TryShiftUnitOntoOtherCellM), iCell_0, iCell_around_1 });

                    break;
                }

                break;
            }
        }
    }


    void TryToSpawnKing()
    {
        if (_playerInfoCs[(int)_botPlayerT].HaveKingInInventor)
        {
            for (byte iCell_0 = 0; iCell_0 < IndexCellsValues.CELLS; iCell_0++)
            {
                if (_cellCs[iCell_0].IsBorder) continue;

                if (_isStartedCellCs[iCell_0].IsStartedCellForPlayer(_botPlayerT) && !_unitCs[iCell_0].HaveUnit)
                {
                    _s.SetNewUnitOnCellS.Set(UnitTypes.King, _botPlayerT, iCell_0);

                    break;
                }
            }
        }
    }

    void TryToSpawnPawn()
    {
        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
        {
            if (_isStartedCellCs[cellIdxCurrent].IsStartedCellForPlayer(_botPlayerT) && !UnitC(cellIdxCurrent).HaveUnit && PawnPeopleInfoC(_botPlayerT).CanGetPawn(PlayerInfoC(_botPlayerT).AmountBuiltHouses))
            {
                _s.SetNewUnitOnCellS.Set(UnitTypes.Pawn, _botPlayerT, cellIdxCurrent);

                break;
            }
        }
    }

    // v
    void GetAllStateValuesForUnits()
    {
        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
        {
            if (_cellCs[cellIdxCurrent].IsBorder) continue;


            foreach (var value in Enum.GetValues(typeof(DirectTypes)))
            {
                // Your code here
            }

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                foreach (var idxCellAround in _idxsAroundCellCs[cellIdxCurrent].IdxCellsAroundArray)
                {
                    if (EnvironmentC(idxCellAround).HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                    }
                }
            }
        }
    }
}