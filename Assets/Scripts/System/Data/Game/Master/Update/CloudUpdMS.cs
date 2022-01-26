using static Game.Game.CellEs;

namespace Game.Game
{
    struct CloudUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            var xy_0 = Cell(Entities.WindE.CenterCloud.Idx).XyC.Xy;
            var xy_next = CellSpaceSupport.GetXyCellByDirect(xy_0, Entities.WindE.DirectWind.Direct);


            if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
            {
                Entities.WindE.CenterCloud.Idx = IdxCell(xy_next);
            }
            else
            {
                var newDir = Entities.WindE.DirectWind.Direct;

                newDir = newDir.Invert();
                var newDirInt = (int)newDir;
                newDirInt += UnityEngine.Random.Range(-1, 2);

                if (newDirInt <= 0) newDirInt = 1;
                else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                Entities.WindE.DirectWind.Direct = (DirectTypes)newDirInt;
            }

            CellSpaceSupport.TryGetXyAround(xy_next, out var dirs);

            foreach (var item in dirs)
            {
                var idx_1 = IdxCell(item.Value);

                CellTrailEs.ResetAll(idx_1);
            }
        }
    }
}