using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct EntityVPool
    {
        readonly static Dictionary<CellTypes, EcsEntity[]> _cells;

        public static ref T CellVC<T>(in byte idx) where T : struct, ICellVE => ref _cells[CellTypes.Cell][idx].Get<T>();
        public static ref T UnitCellVC<T>(in byte idx) where T : struct, IUnitCellV => ref _cells[CellTypes.Unit][idx].Get<T>();
        public static ref T BuildCellVC<T>(in byte idx) where T : struct, IBuildCellV => ref _cells[CellTypes.Build][idx].Get<T>();
        public static ref T EnvCellVC<T>(in byte idx) where T : struct, IEnvCellV => ref _cells[CellTypes.Env][idx].Get<T>();
        public static ref T TrailCellVC<T>(in byte idx) where T : struct, ITrailCellV => ref _cells[CellTypes.Trail][idx].Get<T>();
        public static ref T FireCellVC<T>(in byte idx) where T : struct, IFireCellV => ref _cells[CellTypes.Fire][idx].Get<T>();
        public static ref T CloudCellVC<T>(in byte idx) where T : struct, ICloudCellV => ref _cells[CellTypes.Cloud][idx].Get<T>();
        public static ref T RiverCellVC<T>(in byte idx) where T : struct, IRiverCellV => ref _cells[CellTypes.River][idx].Get<T>();
        public static ref T ElseCellVC<T>(in byte idx) where T : struct, IElseCellVE => ref _cells[CellTypes.Else][idx].Get<T>();

        static EntityVPool()
        {
            _cells = new Dictionary<CellTypes, EcsEntity[]>();

            for (var type = CellTypes.First; type < CellTypes.End; type++)
            {
                _cells.Add(type, new EcsEntity[CellValuesC.AMOUNT_ALL_CELLS]);
            }
        }
        public EntityVPool(in EcsWorld curGameW, in GameObject[] cells)
        {
            for (byte idx = 0; idx < CellValuesC.AMOUNT_ALL_CELLS; idx++)
            {
                _cells[CellTypes.Cell][idx] = curGameW.NewEntity()
                        .Replace(new CellVC(cells[idx]));


                _cells[CellTypes.Unit][idx] = curGameW.NewEntity()
                     .Replace(new UnitMainVC(cells[idx]))
                     .Replace(new UnitExtraVC(cells[idx]));


                var build_GO = cells[idx].transform.Find("Building").gameObject;

                _cells[CellTypes.Build][idx] = curGameW.NewEntity()
                     .Replace(new BuildVC(build_GO))
                     .Replace(new BuildBackVC(build_GO));


                _cells[CellTypes.Env][idx] = curGameW.NewEntity()
                    .Replace(new EnvVC(cells[idx]));


                _cells[CellTypes.Trail][idx] = curGameW.NewEntity()
                    .Replace(new TrailVC(cells[idx].transform));


                _cells[CellTypes.Fire][idx] = curGameW.NewEntity()
                    .Replace(new FireVC(cells[idx]));


                _cells[CellTypes.Cloud][idx] = curGameW.NewEntity()
                    .Replace(new CloudVC(cells[idx]));


                _cells[CellTypes.River][idx] = curGameW.NewEntity()
                    .Replace(new RiverVC(cells[idx].transform));


                _cells[CellTypes.Else][idx] = curGameW.NewEntity()
                    .Replace(new SupportVC(cells[idx]))
                    .Replace(new BlocksVC(cells[idx]))
                    .Replace(new BarsVC(cells[idx]))
                    .Replace(new StunVC(cells[idx].transform));
            }
        }
    }
}