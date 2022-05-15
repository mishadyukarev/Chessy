using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    internal sealed class ExecuteAIBotLogicAfterUpdateS_M : SystemModel
    {
        readonly Dictionary<byte, byte> _pointsCellsForSettingKing;
        byte _theMostBigPointsForSettingKing;

        internal ExecuteAIBotLogicAfterUpdateS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _pointsCellsForSettingKing = new Dictionary<byte, byte>();
            for (byte cellIdxStart = 0; cellIdxStart < StartValues.CELLS; cellIdxStart++)
            {
                _pointsCellsForSettingKing.Add(cellIdxStart, default);
            }
        }

        internal void Execute()
        {
            var playerBotT = PlayerTypes.Second;


            if (eMG.PlayerInfoE(playerBotT).KingInfoE.HaveInInventor)
            {
                for (byte cellIdxStart = 0; cellIdxStart < StartValues.CELLS; cellIdxStart++)
                {
                    _pointsCellsForSettingKing[cellIdxStart] = default;

                    if (eMG.IsStartedCellC(cellIdxStart).IsStartedCell(playerBotT))
                    {
                        _pointsCellsForSettingKing[cellIdxStart]++;

                        if(eMG.XyCellC(cellIdxStart).X >= 4 && eMG.XyCellC(cellIdxStart).X <= 7 && eMG.XyCellC(cellIdxStart).Y == 8)
                        {
                            _pointsCellsForSettingKing[cellIdxStart]++;

                            if (eMG.AdultForestC(cellIdxStart).HaveAnyResources)
                            {
                                _pointsCellsForSettingKing[cellIdxStart]++;
                            }
                            if (eMG.HillC(cellIdxStart).HaveAnyResources)
                            {
                                _pointsCellsForSettingKing[cellIdxStart]++;
                            }
                        }
                    }
                }

                _theMostBigPointsForSettingKing = 0;

                for (byte cellIdxStart = 0; cellIdxStart < StartValues.CELLS; cellIdxStart++)
                {
                    if (_theMostBigPointsForSettingKing < _pointsCellsForSettingKing[cellIdxStart])
                    {
                        _theMostBigPointsForSettingKing = _pointsCellsForSettingKing[cellIdxStart];
                    }
                }


                for (var i = 0; i < 100; i++)
                {
                    foreach (var item in _pointsCellsForSettingKing)
                    {
                        var idxCell = item.Key;
                        var point = item.Value;

                        if (_theMostBigPointsForSettingKing == point)
                        {
                            //sMG.MasterSs.ClearAllEnvironmentS.Clear(idxCell);

                            if (Random.Range(0, 1f) > 0.8f)
                            {
                                sMG.UnitSs.SetNewOnCellS.Set(UnitTypes.King, playerBotT, idxCell);
                                break;
                            }
                        }
                    }

                    if (!eMG.PlayerInfoE(playerBotT).KingInfoE.HaveInInventor) break;
                }
            }
        }
    }
}