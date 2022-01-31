namespace Game.Game
{
    sealed class CloudUpdMS : SystemAbstract, IEcsRunSystem
    {
        public CloudUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var xy_0 = CellEs.CellE(Es.WindE.CenterCloud.Idx).XyC.Xy;
            var xy_next = CellEs.GetXyCellByDirect(xy_0, Es.WindE.DirectWind.Direct);


            if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
            {
                Es.WindE.CenterCloud.Idx = CellEs.GetIdxCell(xy_next);
            }
            else
            {
                var newDir = Es.WindE.DirectWind.Direct;

                newDir = newDir.Invert();
                var newDirInt = (int)newDir;
                newDirInt += UnityEngine.Random.Range(-1, 2);

                if (newDirInt <= 0) newDirInt = 1;
                else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                Es.WindE.DirectWind.Direct = (DirectTypes)newDirInt;
            }

            CellEs.TryGetXyAround(xy_next, out var dirs);

            foreach (var item in dirs)
            {
                var idx_1 = CellEs.GetIdxCell(item.Value);

                CellEs.TrailEs.ResetAll(idx_1);
            }
        }
    }
}