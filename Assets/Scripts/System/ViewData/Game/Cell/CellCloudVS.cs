namespace Game.Game
{
    sealed class CellCloudVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellCloudVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                CellCloudVEs.CloudCellVC<SpriteRendererVC>(idx_0).SetActive(false);
            }

            var centerCloud = E.CenterCloudIdxC.Idx;

            foreach (var cellE in E.CellEs(centerCloud).AroundCellEs)
            {
                CellCloudVEs.CloudCellVC<SpriteRendererVC>(cellE.IdxC.Idx).SetActive(true);
            }

            CellCloudVEs.CloudCellVC<SpriteRendererVC>(centerCloud).SetActive(true);
        }
    }
}