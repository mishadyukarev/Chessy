using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    public sealed class CloudUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellCloudsDataC> _cellWeatherFilt = default;

        public void Run()
        {
            var startWeatherC = _cellWeatherFilt.Get1(WhereCloudsC.Cloud);
            var xyStart = _xyCellFilter.Get1(WhereCloudsC.Cloud).XyCell;


            var aroundList = CellSpaceSupport.TryGetXyAround(xyStart);

            byte curIdx = 0;

            foreach (var xYArount in aroundList)
            {
                curIdx = _xyCellFilter.GetIdxCell(xYArount);

                _cellWeatherFilt.Get1(curIdx).HaveCloud = false;
            }

            var xyNext = CellSpaceSupport.GetXyCellByDirect(xyStart, WindC.DirectWind);

            if (xyNext[0] > 3 && xyNext[0] < 12 && xyNext[1] > 0 && xyNext[1] < 10)
            {
                startWeatherC.CloudWidthType = CloudWidthTypes.None;
                WhereCloudsC.Remove(WhereCloudsC.Cloud);
                startWeatherC.HaveCloud = false;


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
                startWeatherC.HaveCloud = false;

                RandomCloud();
            }
           
        }

        private void RandomCloud()
        {
            //var rand = UnityEngine.Random.Range(0, 100);
            //if (rand <= 30) 
                WindC.DirectWind = (DirectTypes)UnityEngine.Random.Range(1, Enum.GetNames(typeof(DirectTypes)).Length);
        }
    }
}