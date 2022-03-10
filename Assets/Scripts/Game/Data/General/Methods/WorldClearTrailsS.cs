namespace Chessy.Game.System.Model
{
    sealed class WorldClearTrailsS : CellSystem, IEcsRunSystem
    {
        internal WorldClearTrailsS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            if (!E.AdultForestC(Idx).HaveAnyResources)
            {
                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    E.CellEs(Idx).TrailHealthC(dirT).Health = 0;
                }
            }
        }
    }
}