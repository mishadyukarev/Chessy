using static Game.Game.CellEs;
using static Game.Game.EntityCellCloudPool;

namespace Game.Game
{
    struct CloudUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            var weather_0 = Cloud<HaveEffectC>(CenterCloudEnt.CenterCloud<IdxC>().Idx);
            var xy_0 = Cell<XyC>(CenterCloudEnt.CenterCloud<IdxC>().Idx).Xy;

            weather_0.Have = false;

            foreach (var idx_1 in CellSpaceSupport.GetIdxAround(CenterCloudEnt.CenterCloud<IdxC>().Idx))
            {
                Cloud<HaveEffectC>(idx_1).Have = false;
            }

            var xy_next = CellSpaceSupport.GetXyCellByDirect(xy_0, CurrentDirectWindE.Direct<DirectTC>().Direct);

            

            if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
            {
                CenterCloudEnt.CenterCloud<IdxC>().Idx = IdxCell(xy_next);
                Cloud<HaveEffectC>(CenterCloudEnt.CenterCloud<IdxC>().Idx).Have = true;
            }
            else
            {
                var newDir = CurrentDirectWindE.Direct<DirectTC>().Direct;

                newDir = newDir.Invert();
                var newDirInt = (int)newDir;
                newDirInt += UnityEngine.Random.Range(-1, 2);

                if (newDirInt <= 0) newDirInt = 1;
                else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                CurrentDirectWindE.Direct<DirectTC>().Direct = (DirectTypes)newDirInt;
            }

            CellSpaceSupport.TryGetXyAround(xy_next, out var dirs);

            foreach (var item in dirs)
            {
                var idx_1 = IdxCell(item.Value);

                CellTrailEs.ResetAll(idx_1);

                Cloud<HaveEffectC>(idx_1).Have = true;

                DirectsWindForElfemaleE.Idx<IdxC>(item.Key).Idx = idx_1;
            }
        }
    }
}