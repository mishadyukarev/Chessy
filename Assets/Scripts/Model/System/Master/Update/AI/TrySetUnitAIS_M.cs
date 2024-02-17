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

                if (!isStartedCellCs[cellIdxStart].IsStartedCellForPlayer(playerBotT) || unitCs[cellIdxStart].HaveUnit) continue;

                _pointsCellsForSettingKing[cellIdxStart]++;
                _pointsCellsForSettingPawn[cellIdxStart]++;


                if (environmentCs[cellIdxStart].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    _pointsCellsForSettingKing[cellIdxStart]++;
                    _pointsCellsForSettingPawn[cellIdxStart]++;
                }
                if (environmentCs[cellIdxStart].HaveEnvironment(EnvironmentTypes.Hill))
                {
                    _pointsCellsForSettingKing[cellIdxStart]++;
                    _pointsCellsForSettingPawn[cellIdxStart]++;
                }

                if (XyCellC(cellIdxStart).Y == 8)
                {
                    _pointsCellsForSettingKing[cellIdxStart] += 3;
                    _pointsCellsForSettingPawn[cellIdxStart] += 3;

                    if (XyCellC(cellIdxStart).X >= 4 && XyCellC(cellIdxStart).X <= 7)
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

            if (PlayerInfoE(playerBotT).PlayerInfoC.HaveKingInInventor)
            {
                byte cellIdx = 85;

                environmentCs[cellIdx].Dispose();
                s.SetNewUnitOnCellS.Set(UnitTypes.King, playerBotT, cellIdx);

                //TrySetUnit(ref _theMostBigPointForSettingKing, _pointsCellsForSettingKing, UnitTypes.King, playerBotT);
            }

            if (PawnPeopleInfoC(playerBotT).CanGetPawn(PlayerInfoC(playerBotT).AmountBuiltHouses))
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
                        if (!unitCs[idxCell].HaveUnit)
                        {
                            if (Random.Range(0, 1f) < 0.75f)
                            {
                                s.SetNewUnitOnCellS.Set(unitT, playerBotT, idxCell);

                                if (unitT == UnitTypes.King)
                                {
                                    environmentCs[idxCell].Dispose();
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
                    if (!PlayerInfoE(playerBotT).PlayerInfoC.HaveKingInInventor) break;
                }

                if (!PawnPeopleInfoC(playerBotT).CanGetPawn(PlayerInfoC(playerBotT).AmountBuiltHouses))
                {
                    break;
                }
            }
        }
    }
}