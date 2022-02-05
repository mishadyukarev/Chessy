namespace Game.Game
{
    sealed class CloudUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal CloudUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var xy_0 = CellEs(Es.WindCloudE.CenterCloud.Idx).CellE.XyC.Xy;
            var xy_next = CellWorker.GetXyCellByDirect(xy_0, Es.WindCloudE.DirectWind.Direct);


            if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
            {
                Es.WindCloudE.CenterCloud.Idx = CellWorker.GetIdxCell(xy_next);
            }
            else
            {
                var newDir = Es.WindCloudE.DirectWind.Direct;

                newDir = newDir.Invert();
                var newDirInt = (int)newDir;
                newDirInt += UnityEngine.Random.Range(-1, 2);

                if (newDirInt <= 0) newDirInt = 1;
                else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                Es.WindCloudE.DirectWind.Direct = (DirectTypes)newDirInt;
            }

            CellWorker.TryGetXyAround(xy_next, out var dirs);

            foreach (var item in dirs)
            {
                var idx_1 = CellWorker.GetIdxCell(item.Value);

                TrailEs(idx_1).DestroyAll();
            }
        }
    }
}