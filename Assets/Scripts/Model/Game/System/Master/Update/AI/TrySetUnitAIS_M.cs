using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    sealed class TrySetUnitAIS_M : SystemModel
    {
        readonly Dictionary<byte, byte> _pointsCellsForSettingKing = new Dictionary<byte, byte>();
        readonly Dictionary<byte, byte> _pointsCellsForSettingPawn = new Dictionary<byte, byte>();
        byte _theMostBigPointForSettingKing;
        byte _theMostBigPointForSettingPawn;

        internal TrySetUnitAIS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            for (byte cellIdxStart = 0; cellIdxStart < StartValues.CELLS; cellIdxStart++)
            {
                _pointsCellsForSettingKing.Add(cellIdxStart, default);
                _pointsCellsForSettingPawn.Add(cellIdxStart, default);
            }
        }

        internal void TrySet()
        {
            var playerBotT = PlayerTypes.Second;

            _theMostBigPointForSettingKing = 0;
            _theMostBigPointForSettingPawn = 0;

            for (byte cellIdxStart = 0; cellIdxStart < StartValues.CELLS; cellIdxStart++)
            {
                _pointsCellsForSettingKing[cellIdxStart] = 0;
                _pointsCellsForSettingPawn[cellIdxStart] = 0;

                if (!eMG.IsStartedCellC(cellIdxStart).IsStartedCell(playerBotT) || eMG.UnitTC(cellIdxStart).HaveUnit) continue;

                _pointsCellsForSettingKing[cellIdxStart]++;
                _pointsCellsForSettingPawn[cellIdxStart]++;


                if (eMG.AdultForestC(cellIdxStart).HaveAnyResources)
                {
                    _pointsCellsForSettingKing[cellIdxStart]++;
                    _pointsCellsForSettingPawn[cellIdxStart]++;
                }
                if (eMG.HillC(cellIdxStart).HaveAnyResources)
                {
                    _pointsCellsForSettingKing[cellIdxStart]++;
                    _pointsCellsForSettingPawn[cellIdxStart]++;
                }

                if (eMG.XyCellC(cellIdxStart).Y == 8)
                {
                    _pointsCellsForSettingKing[cellIdxStart] += 3;
                    _pointsCellsForSettingPawn[cellIdxStart] += 3;

                    if (eMG.XyCellC(cellIdxStart).X >= 4 && eMG.XyCellC(cellIdxStart).X <= 7)
                    {
                        _pointsCellsForSettingKing[cellIdxStart]++;
                        _pointsCellsForSettingPawn[cellIdxStart]++;
                    }
                }

                if (_theMostBigPointForSettingKing < _pointsCellsForSettingKing[cellIdxStart])
                {
                    _theMostBigPointForSettingKing = _pointsCellsForSettingKing[cellIdxStart];
                }

                if (_theMostBigPointForSettingPawn < _pointsCellsForSettingPawn[cellIdxStart])
                {
                    _theMostBigPointForSettingPawn = _pointsCellsForSettingPawn[cellIdxStart];
                }
            }

            if (eMG.PlayerInfoE(playerBotT).KingInfoE.HaveInInventor)
            {
                byte cellIdx = 85;

                sMG.MasterSs.ClearAllEnvironmentS.Clear(cellIdx);
                sMG.SetNewUnitOnCellS(UnitTypes.King, playerBotT, cellIdx);

                //TrySetUnit(ref _theMostBigPointForSettingKing, _pointsCellsForSettingKing, UnitTypes.King, playerBotT);
            }

            if (eMG.PlayerInfoE(playerBotT).PawnInfoC.CanGetPawn)
            {
                TrySetUnit(ref _theMostBigPointForSettingPawn, _pointsCellsForSettingPawn, UnitTypes.Pawn, playerBotT);
            }


        }

        void TrySetUnit(ref byte theMostBigPoint, in Dictionary<byte, byte> pointsCellsForSettingUnit, in UnitTypes unitT, in PlayerTypes playerBotT)
        {
            var theMostBigPointForSettingPawn = theMostBigPoint;

            for (var i = 0; i < theMostBigPointForSettingPawn; i++)
            {
                foreach (var item in pointsCellsForSettingUnit)
                {
                    var idxCell = item.Key;
                    var currentPoint = item.Value;

                    if (theMostBigPoint == currentPoint)
                    {
                        if (!eMG.UnitTC(idxCell).HaveUnit)
                        {
                            if (Random.Range(0, 1f) < 0.75f)
                            {
                                sMG.SetNewUnitOnCellS(unitT, playerBotT, idxCell);

                                if(unitT == UnitTypes.King)
                                {
                                    sMG.MasterSs.ClearAllEnvironmentS.Clear(idxCell);
                                }

                                break;
                            }
                        }

                    }
                }

                if (theMostBigPoint == 0)
                {
                    i = -1;
                    theMostBigPoint = theMostBigPointForSettingPawn;
                }
                else
                {
                    theMostBigPoint--;
                }

                if (unitT == UnitTypes.King)
                {
                    if (!eMG.PlayerInfoE(playerBotT).KingInfoE.HaveInInventor) break;
                }

                if (!eMG.PlayerInfoE(playerBotT).PawnInfoC.CanGetPawn)
                {
                    break;
                }
            }
        }
    }
}