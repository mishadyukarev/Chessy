using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Newtonsoft.Json;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;


namespace Chessy.Model.System
{
    public class InputDataToRequest
    {
        public string time;
        public string unit_move_type;
        public Dictionary<DirectTypes, Dictionary<EnvironmentTypes, bool>> cells;
    }


    public sealed class BotAIS : SystemModelAbstract
    {
        readonly PlayerTypes _botPlayerT;
        readonly byte _botPlayer_byte;
        DateTime _dateTimeLastUpdate;


        internal BotAIS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            _botPlayerT = PlayerTypes.Second;
            _botPlayer_byte = (byte)_botPlayerT;
        }

        internal async void Update()
        {
            if ((DateTime.Now - _dateTimeLastUpdate).Seconds >= 1)
            {

                TryToSpawnKing();
                TryToSpawnPawn();
                await TryToMovePawnRandomlyAsync();

                _dateTimeLastUpdate = DateTime.Now;
            }
        }

        async Task TryToMovePawnRandomlyAsync()
        {
            for (byte iCell_0 = 0; iCell_0 < IndexCellsValues.CELLS; iCell_0++)
            {
                if (cellCs[iCell_0].IsBorder) continue;
                if (unitCs[iCell_0].UnitT != UnitTypes.Pawn) continue;
                if (shiftingUnitCs[iCell_0].IsShifting) continue;


                if (unitCs[iCell_0].PlayerT == _botPlayerT)
                {
                    var movesCanDo_ar = new bool[(int)UnitMoveTypes.End];


                    foreach (var iCell_around_1 in idxsAroundCellCs[iCell_0].IdxCellsAroundArray)
                    {
                        if (cellCs[iCell_around_1].IsBorder) continue;
                        if (unitCs[iCell_around_1].HaveUnit) continue;


                        var randUnitMoveT = (UnitMoveTypes)UnityEngine.Random.Range((int)UnitMoveTypes.None + 1, 9);




                        Debug.Log(randUnitMoveT);

                        var cellInfo_dict = new Dictionary<DirectTypes, Dictionary<EnvironmentTypes, bool>>();


                        for (var dirT = DirectTypes.None; dirT < DirectTypes.End; dirT++)
                        {
                            cellInfo_dict.Add(dirT, new Dictionary<EnvironmentTypes, bool>());

                            for (var envT = (EnvironmentTypes)1; envT < EnvironmentTypes.End; envT++)
                            {
                                cellInfo_dict[dirT].Add(envT, environmentCs[cellsByDirectAroundC[iCell_0].Get(dirT)].HaveEnvironment(envT));
                            }
                        }

                        var inputDataToRequest = new InputDataToRequest()
                        {
                            time = DateTime.Now.ToString("yyyyMMddHHmmssffffff"),
                            unit_move_type = randUnitMoveT.ToString(),
                            cells = cellInfo_dict
                        };


                        var httpClient = new HttpClient();
                        string url = "https://sirpoopy.pythonanywhere.com/put_data";

                        var json = JsonConvert.SerializeObject(inputDataToRequest);
                        json = json.Replace("\"", "'");

                        var result_url = $"{url}?input=\"{json}\"";

                        var response = await httpClient.GetAsync(result_url);

                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            //var data = JsonConvert.DeserializeObject<PostResponse>(result);

                            //Debug.Log(data.Input);
                            //Debug.Log(data.Timestamp);
                            //Debug.Log(data.Character_count);

                            Debug.Log(result);

                        }


                        rpcC.Action0(rpcC.PunRPCName, RpcTarget.MasterClient, new object[] { nameof(s.TryShiftUnitOntoOtherCellM), iCell_0, iCell_around_1 });

                        break;
                    }

                    break;
                }
            }
        }


        void TryToSpawnKing()
        {
            if (playerInfoCs[_botPlayer_byte].HaveKingInInventor)
            {
                for (byte iCell_0 = 0; iCell_0 < IndexCellsValues.CELLS; iCell_0++)
                {
                    if (cellCs[iCell_0].IsBorder) continue;

                    if (isStartedCellCs[iCell_0].IsStartedCellForPlayer(_botPlayerT) && !unitCs[iCell_0].HaveUnit)
                    {
                        s.SetNewUnitOnCellS.Set(UnitTypes.King, _botPlayerT, iCell_0);

                        break;
                    }
                }
            }
        }

        void TryToSpawnPawn()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (isStartedCellCs[cellIdxCurrent].IsStartedCellForPlayer(_botPlayerT) && !unitCs[cellIdxCurrent].HaveUnit && pawnPeopleInfoCs[_botPlayer_byte].CanGetPawn(playerInfoCs[_botPlayer_byte].AmountBuiltHouses))
                {
                    s.SetNewUnitOnCellS.Set(UnitTypes.Pawn, _botPlayerT, cellIdxCurrent);

                    break;
                }
            }
        }

        // v
        void GetAllStateValuesForUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (cellCs[cellIdxCurrent].IsBorder) continue;


                //foreach (var value in System. Enum.GetValues(typeof(DirectTypes)))
                //{
                //    // Your code here
                //}

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    foreach (var idxCellAround in idxsAroundCellCs[cellIdxCurrent].IdxCellsAroundArray)
                    {
                        if (environmentCs[idxCellAround].HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                        }
                    }
                }
            }
        }
    }
}