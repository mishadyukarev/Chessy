using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellPoolEs
    {
        readonly CellE[] _aroundEs;
        readonly Dictionary<byte, DirectTypes> _aroundDirs;

        readonly HealthC[] _trailHealthCs;
        readonly bool[] _trailsVisibled;

        public bool IsActiveParentSelf;

        public readonly CellE CellE;
        public WhoLastDiedHereE WhoLastDiedHereE;
        public CellUnitPoolEs UnitEs;
        public CellBuildingE BuildE;
        public CellEnvironmentEs EnvironmentEs;
        public CellEffectE EffectEs;
        public CellRiverPoolEs RiverEs;


        public CellE AroundCellE(in byte idx_array) => _aroundEs[idx_array];
        public CellE AroundCellE(in DirectTypes dirT) => _aroundEs[(byte)dirT - 1];
        public CellE[] AroundCellEs => (CellE[])_aroundEs.Clone();
        public DirectTypes Direct(in byte idx) => _aroundDirs[idx];

        public ref HealthC TrailHealthC(in DirectTypes dir) => ref _trailHealthCs[(byte)dir - 1];
        public ref bool IsVisible(in PlayerTypes player) => ref _trailsVisibled[(byte)player - 1];

        internal CellPoolEs(in bool isActiveParent, in int idCell, byte[] xy, in byte idx, in EcsWorld gameW, in Entities e) : this()
        {
            IsActiveParentSelf = isActiveParent;

            if (isActiveParent)
            {
                _aroundEs = new CellE[(byte)DirectTypes.End - 1];
                _aroundDirs = new Dictionary<byte, DirectTypes>();

                var idxsAround = new byte[(byte)DirectTypes.End - 1];

                var i = 0;
                for (var dir = DirectTypes.None + 1; dir < DirectTypes.End; dir++)
                {
                    var xyDirect = new short[Entities.XY_FOR_ARRAY];

                    switch (dir)
                    {
                        case DirectTypes.None:
                            throw new Exception();

                        case DirectTypes.Right:
                            xyDirect[Entities.X] = 1;
                            xyDirect[Entities.Y] = 0;
                            break;

                        case DirectTypes.Left:
                            xyDirect[Entities.X] = -1;
                            xyDirect[Entities.Y] = 0;
                            break;

                        case DirectTypes.Up:
                            xyDirect[Entities.X] = 0;
                            xyDirect[Entities.Y] = 1;
                            break;

                        case DirectTypes.Down:
                            xyDirect[Entities.X] = 0;
                            xyDirect[Entities.Y] = -1;
                            break;

                        case DirectTypes.UpRight:
                            xyDirect[Entities.X] = 1;
                            xyDirect[Entities.Y] = 1;
                            break;

                        case DirectTypes.UpLeft:
                            xyDirect[Entities.X] = -1;
                            xyDirect[Entities.Y] = 1;
                            break;

                        case DirectTypes.DownRight:
                            xyDirect[Entities.X] = 1;
                            xyDirect[Entities.Y] = -1;
                            break;

                        case DirectTypes.DownLeft:
                            xyDirect[Entities.X] = -1;
                            xyDirect[Entities.Y] = -1;
                            break;

                        default:
                            throw new Exception();
                    }


                    var x = (byte)(xy[Entities.X] + xyDirect[Entities.X]);
                    var y = (byte)(xy[Entities.Y] + xyDirect[Entities.Y]);

                    idxsAround[i] = e.GetIdxCell(new[] { x, y });

                    _aroundDirs.Add(idxsAround[i], dir);
                    _aroundEs[(byte)dir - 1] = new CellE(idxsAround[i], new byte[] { x, y }, default);
                }

                //IdxsAround = new IdxsArrayC(idxsAround);
            }

            _trailHealthCs = new HealthC[(byte)DirectTypes.End - 1];
            _trailsVisibled = new bool[(byte)PlayerTypes.End - 1];

            CellE = new CellE(idx, xy, idCell);
            BuildE = new CellBuildingE((byte)PlayerTypes.End);
            UnitEs = new CellUnitPoolEs(this, gameW);
            RiverEs = new CellRiverPoolEs(default);
        }
    }
}