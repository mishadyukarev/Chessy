﻿using System;
using System.Collections.Generic;
using static Game.Game.CellValues;

namespace Game.Game
{
    public readonly struct CellSpaceC
    {
        readonly static Dictionary<DirectTypes, sbyte[]> _directs;

        public static List<byte[]> XyAround(byte[] xy_start)
        {
            var xyAvail = new List<byte[]>();
            var xyResult = new byte[XY_FOR_ARRAY];

            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                var xyDirectCell = _directs[dir];

                xyResult[X] = (byte)(xy_start[X] + xyDirectCell[X]);
                xyResult[Y] = (byte)(xy_start[Y] + xyDirectCell[Y]);

                xyAvail.Add((byte[])xyResult.Clone());
            }

            return xyAvail;
        }
        public static void TryGetXyAround(byte[] xy_start, out Dictionary<DirectTypes, byte[]> directs)
        {
            directs = new Dictionary<DirectTypes, byte[]>();
            var xyResult = new byte[XY_FOR_ARRAY];

            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                var xyDirect = _directs[dir];

                xyResult[X] = (byte)(xy_start[X] + xyDirect[X]);
                xyResult[Y] = (byte)(xy_start[Y] + xyDirect[Y]);

                directs.Add(dir, (byte[])xyResult.Clone());
            }
        }

        public static DirectTypes GetDirect(byte[] xy_from, byte[] xy_to)
        {
            TryGetXyAround(xy_from, out var xy_around);

            foreach (var item_1 in xy_around)
            {
                if (item_1.Value.Compare(xy_to)) return item_1.Key;
            }

            throw new Exception();
        }

        public static byte[] GetXyCellByDirect(byte[] xyStart, DirectTypes directType)
        {
            var xyResult = new byte[XY_FOR_ARRAY];

            var xyDirect = _directs[directType];

            xyResult[X] = (byte)(xyStart[X] + xyDirect[X]);
            xyResult[Y] = (byte)(xyStart[Y] + xyDirect[Y]);

            return xyResult;
        }

        static CellSpaceC()
        {
            _directs = new Dictionary<DirectTypes, sbyte[]>();
    
            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                var xyDirect = new sbyte[XY_FOR_ARRAY];

                switch (dir)
                {
                    case DirectTypes.None:
                        throw new Exception();

                    case DirectTypes.Right:
                        xyDirect[X] = 1;
                        xyDirect[Y] = 0;
                        break;

                    case DirectTypes.Left:
                        xyDirect[X] = -1;
                        xyDirect[Y] = 0;
                        break;

                    case DirectTypes.Up:
                        xyDirect[X] = 0;
                        xyDirect[Y] = 1;
                        break;

                    case DirectTypes.Down:
                        xyDirect[X] = 0;
                        xyDirect[Y] = -1;
                        break;

                    case DirectTypes.UpRight:
                        xyDirect[X] = 1;
                        xyDirect[Y] = 1;
                        break;

                    case DirectTypes.UpLeft:
                        xyDirect[X] = -1;
                        xyDirect[Y] = 1;
                        break;

                    case DirectTypes.DownRight:
                        xyDirect[X] = 1;
                        xyDirect[Y] = -1;
                        break;

                    case DirectTypes.DownLeft:
                        xyDirect[X] = -1;
                        xyDirect[Y] = -1;
                        break;

                    default:
                        throw new Exception();
                }

                _directs.Add(dir, xyDirect);
            }
        } 
    }
}
