using Leopotam.Ecs;
using System;

namespace Chessy.Game
{
    public sealed class CloudUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyCellFilter = default;
        private EcsFilter<CloudC> _cellWeatherFilt = default;

        public void Run()
        {
            var weather_0 = _cellWeatherFilt.Get1(WhereCloudsC.Cloud);
            var xyStart = _xyCellFilter.Get1(WhereCloudsC.Cloud).Xy;


            var aroundList = CellSpaceSupport.GetXyAround(xyStart);

            byte curIdx = 0;

            foreach (var xYArount in aroundList)
            {
                curIdx = _xyCellFilter.GetIdxCell(xYArount);

                _cellWeatherFilt.Get1(curIdx).HaveCloud = false;
            }

            var xyNext = CellSpaceSupport.GetXyCellByDirect(xyStart, WindC.DirectWind);

            if (xyNext[0] > 3 && xyNext[0] < 12 && xyNext[1] > 1 && xyNext[1] < 9)
            {
                weather_0.CloudWidthType = CloudWidthTypes.None;
                WhereCloudsC.Remove(WhereCloudsC.Cloud);
                weather_0.HaveCloud = false;


                var idxNext = _xyCellFilter.GetIdxCell(xyNext);

                _cellWeatherFilt.Get1(idxNext).CloudWidthType = CloudWidthTypes.TwoBlocks;
                WhereCloudsC.Add(idxNext);
                _cellWeatherFilt.Get1(idxNext).HaveCloud = true;


                aroundList = CellSpaceSupport.GetXyAround(xyNext);
                curIdx = 0;

                foreach (var xYArount in aroundList)
                {
                    curIdx = _xyCellFilter.GetIdxCell(xYArount);

                    _cellWeatherFilt.Get1(curIdx).HaveCloud = true;
                }
            }
            else
            {
                aroundList = CellSpaceSupport.GetXyAround(xyNext);
                curIdx = 0;

                foreach (var xYArount in aroundList)
                {
                    curIdx = _xyCellFilter.GetIdxCell(xYArount);

                    _cellWeatherFilt.Get1(curIdx).HaveCloud = true;
                }

                RandomCloud();
            }
           
        }

        private void RandomCloud()
        {
            var newDir = WindC.DirectWind;

            newDir = newDir.Invert();
            var newDirInt = (int)newDir;
            newDirInt += UnityEngine.Random.Range(-1, 2);

            if (newDirInt <= 0) newDirInt = 1;
            else if(newDirInt >= typeof(DirectTypes).GetEnumNames().Length) newDirInt = newDirInt = 1;
            WindC.DirectWind = (DirectTypes)newDirInt;
        }
    }
}