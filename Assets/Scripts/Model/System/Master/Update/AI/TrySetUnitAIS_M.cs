using Chessy.Model.Entity;
using Chessy.Model.System;
using Chessy.Model.Values;
using System.Collections.Generic;
using UnityEngine;
namespace Chessy.Model
{
    sealed class TrySetUnitAIS_M : SystemModelAbstract
    {
        readonly Dictionary<byte, byte> _pointsCellsForSettingKing = new Dictionary<byte, byte>();
        readonly Dictionary<byte, byte> _pointsCellsForSettingPawn = new Dictionary<byte, byte>();
        byte _theMostBigPointForSettingKing;
        byte _theMostBigPointForSettingPawn;

        internal TrySetUnitAIS_M(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
            for (byte cellIdxStart = 0; cellIdxStart < IndexCellsValues.CELLS; cellIdxStart++)
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

            for (byte cellIdxStart = 0; cellIdxStart < IndexCellsValues.CELLS; cellIdxStart++)
            {
                _pointsCellsForSettingKing[cellIdxStart] = 0;
                _pointsCellsForSettingPawn[cellIdxStart] = 0;

                if (!_isStartedCellCs[cellIdxStart].IsStartedCell(playerBotT) || _e.UnitT(cellIdxStart).HaveUnit()) continue;

                _pointsCellsForSettingKing[cellIdxStart]++;
                _pointsCellsForSettingPawn[cellIdxStart]++;


                if (_e.AdultForestC(cellIdxStart).HaveAnyResources)
                {
                    _pointsCellsForSettingKing[cellIdxStart]++;
                    _pointsCellsForSettingPawn[cellIdxStart]++;
                }
                if (_e.HillC(cellIdxStart).HaveAnyResources)
                {
                    _pointsCellsForSettingKing[cellIdxStart]++;
                    _pointsCellsForSettingPawn[cellIdxStart]++;
                }

                if (_xyCellsCs[cellIdxStart].Y == 8)
                {
                    _pointsCellsForSettingKing[cellIdxStart] += 3;
                    _pointsCellsForSettingPawn[cellIdxStart] += 3;

                    if (_xyCellsCs[cellIdxStart].X >= 4 && _xyCellsCs[cellIdxStart].X <= 7)
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

            if (_e.PlayerInfoE(playerBotT).PlayerInfoC.HaveKingInInventor)
            {
                byte cellIdx = 85;

                _s.ClearAllEnvironment(cellIdx);
                _s.SetNewUnitOnCellS.Set(UnitTypes.King, playerBotT, cellIdx);

                //TrySetUnit(ref _theMostBigPointForSettingKing, _pointsCellsForSettingKing, UnitTypes.King, playerBotT);
            }

            if (_e.PawnPeopleInfoC(playerBotT).CanGetPawn(_e.PlayerInfoC(playerBotT).AmountBuiltHouses))
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
                        if (!_e.UnitT(idxCell).HaveUnit())
                        {
                            if (Random.Range(0, 1f) < 0.75f)
                            {
                                _s.SetNewUnitOnCellS.Set(unitT, playerBotT, idxCell);

                                if (unitT == UnitTypes.King)
                                {
                                    _s.ClearAllEnvironment(idxCell);
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
                    if (!_e.PlayerInfoE(playerBotT).PlayerInfoC.HaveKingInInventor) break;
                }

                if (!_e.PawnPeopleInfoC(playerBotT).CanGetPawn(_e.PlayerInfoC(playerBotT).AmountBuiltHouses))
                {
                    break;
                }
            }
        }
    }
}