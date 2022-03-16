namespace Chessy.Game.System.Model
{
    public static class TakeAdultForestResourcesS
    {
        public static void TakeAdultForestResources(in float extract, in byte idx, in EntitiesModel e)
        {
            e.AdultForestC(idx).Resources -= extract;

            if (!e.AdultForestC(idx).HaveAnyResources)
            {
                e.AdultForestC(idx).Resources = 0;

                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    e.CellEs(idx).TrailHealthC(dirT).Health = 0;
                }
            }
        }
    }
}