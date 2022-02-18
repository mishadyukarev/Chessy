namespace Game.Game
{
    sealed class CellCloudVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellCloudVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                CellCloudVEs.CloudCellVC<SpriteRendererVC>(idx_0).SetActive(false);
            }

            var centerCloud = Es.CenterCloudIdxC.Idx;

            foreach (var cellE in Es.CellEs(centerCloud).AroundCellEs)
            {
                CellCloudVEs.CloudCellVC<SpriteRendererVC>(cellE.IdxC.Idx).SetActive(true);
            }

            CellCloudVEs.CloudCellVC<SpriteRendererVC>(centerCloud).SetActive(true);
        }
    }
}