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

                if (!_eMG.IsStartedCellC(cellIdxStart).IsStartedCell(playerBotT) || _eMG.UnitTC(cellIdxStart).HaveUnit) continue;

                _pointsCellsForSettingKing[cellIdxStart]++;
                _pointsCellsForSettingPawn[cellIdxStart]++;


                if (_eMG.AdultForestC(cellIdxStart).HaveAnyResources)
                {
                    _pointsCellsForSettingKing[cellIdxStart]++;
                    _pointsCellsForSettingPawn[cellIdxStart]++;
                }
                if (_eMG.HillC(cellIdxStart).HaveAnyResources)
                {
                    _pointsCellsForSettingKing[cellIdxStart]++;
                    _pointsCellsForSettingPawn[cellIdxStart]++;
                }

                if (_eMG.XyCellC(cellIdxStart).Y == 8)
                {
                    _pointsCellsForSettingKing[cellIdxStart] += 3;
                    _pointsCellsForSettingPawn[cellIdxStart] += 3;

                    if (_eMG.XyCellC(cellIdxStart).X >= 4 && _eMG.XyCellC(cellIdxStart).X <= 7)
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

            if (_eMG.PlayerInfoE(playerBotT).KingInfoE.HaveInInventor)
            {
                byte cellIdx = 85;

                _sMG.ClearAllEnvironment(cellIdx);
                _sMG.SetNewUnitOnCellS(UnitTypes.King, playerBotT, cellIdx);

                //TrySetUnit(ref _theMostBigPointForSettingKing, _pointsCellsForSettingKing, UnitTypes.King, playerBotT);
            }

            if (_eMG.PlayerInfoE(playerBotT).PawnInfoC.CanGetPawn)
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
                        if (!_eMG.UnitTC(idxCell).HaveUnit)
                        {
                            if (Random.Range(0, 1f) < 0.75f)
                            {
                                _sMG.SetNewUnitOnCellS(unitT, playerBotT, idxCell);

                                if(unitT == UnitTypes.King)
                                {
                                    _sMG.ClearAllEnvironment(idxCell);
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
                    if (!_eMG.PlayerInfoE(playerBotT).KingInfoE.HaveInInventor) break;
                }

                if (!_eMG.PlayerInfoE(playerBotT).PawnInfoC.CanGetPawn)
                {
                    break;
                }
            }
        }
    }
}