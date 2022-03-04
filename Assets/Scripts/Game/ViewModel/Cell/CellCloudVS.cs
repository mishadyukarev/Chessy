namespace Chessy.Game
{
    sealed class CellCloudVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellCloudVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                VEs.CellEs(idx_0).CloudCellVC.SetActive(false);
            }

            var centerCloud = E.CenterCloudIdxC.Idx;

            foreach (var cellE in E.CellEs(centerCloud).AroundCellEs)
            {
                VEs.CellEs(cellE.IdxC.Idx).CloudCellVC.SetActive(true);
            }

            VEs.CellEs(centerCloud).CloudCellVC.SetActive(true);
        }
    }
}