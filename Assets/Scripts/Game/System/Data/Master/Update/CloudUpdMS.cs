using Leopotam.Ecs;
using System;

namespace Game.Game
{
    public sealed class CloudUpdMS : IEcsRunSystem
    {
        private EcsFilter<CloudC> _cloudF = default;

        public void Run()
        {
            var weather_0 = _cloudF.Get1(CloudCenterC.Idx);
            var xy_0 = EntityPool.CellC<XyC>(CloudCenterC.Idx).Xy;


            var aroundList = CellSpaceC.XyAround(xy_0);

            weather_0.Have = false;

            foreach (var xyArount in aroundList)
            {
                var idx_1 = EntityPool.IdxCell(xyArount);

                _cloudF.Get1(idx_1).Have = false;
            }

            var xy_next = CellSpaceC.GetXyCellByDirect(xy_0, WindC.CurDirWind);


            if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
            {
                CloudCenterC.Idx = EntityPool.IdxCell(xy_next);
                _cloudF.Get1(CloudCenterC.Idx).Have = true;
            }
            else
            {
                var newDir = WindC.CurDirWind;

                newDir = newDir.Invert();
                var newDirInt = (int)newDir;
                newDirInt += UnityEngine.Random.Range(-1, 2);

                if (newDirInt <= 0) newDirInt = 1;
                else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                WindC.Set((DirectTypes)newDirInt);
            }

            CellSpaceC.TryGetXyAround(xy_next, out var dirs);

            foreach (var item in dirs)
            {
                var idx_1 = EntityPool.IdxCell(item.Value);

                _cloudF.Get1(idx_1).Have = true;

                WindC.Set(item.Key, idx_1);
            }
        }
    }
}