﻿using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    public sealed class CloudUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellCloudsDataC> _cellWeatherFilt = default;

        public void Run()
        {
            var weather_0 = _cellWeatherFilt.Get1(WhereCloudsC.Cloud);
            var xyStart = _xyCellFilter.Get1(WhereCloudsC.Cloud).XyCell;


            var aroundList = CellSpaceSupport.TryGetXyAround(xyStart);

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


                aroundList = CellSpaceSupport.TryGetXyAround(xyNext);
                curIdx = 0;

                foreach (var xYArount in aroundList)
                {
                    curIdx = _xyCellFilter.GetIdxCell(xYArount);

                    _cellWeatherFilt.Get1(curIdx).HaveCloud = true;
                }
            }
            else
            {
                aroundList = CellSpaceSupport.TryGetXyAround(xyNext);
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