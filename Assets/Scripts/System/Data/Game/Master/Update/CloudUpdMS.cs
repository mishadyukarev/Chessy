namespace Game.Game
{
    sealed class CloudUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal CloudUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_0 = Es.CenterCloudIdxC.Idx;
            var xy_next = Es.CellEs(idx_0).AroundCellE(Es.DirectWindTC.Direct).XyC.Xy;
            var idx_next = Es.CellEs(idx_0).AroundCellE(Es.DirectWindTC.Direct).IdxC.Idx;


            if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
            {
                Es.CenterCloudIdxC.Idx = Es.GetIdxCellByXy(xy_next);
            }
            else
            {
                var newDir = Es.DirectWindTC.Direct;

                newDir = newDir.Invert();
                var newDirInt = (int)newDir;
                newDirInt += UnityEngine.Random.Range(-1, 2);

                if (newDirInt <= 0) newDirInt = 1;
                else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                Es.DirectWindTC.Direct = (DirectTypes)newDirInt;
            }

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                Es.CellEs(idx_next).TrailHealthC(dirT).Health = 0;
            }
        }
    }
}