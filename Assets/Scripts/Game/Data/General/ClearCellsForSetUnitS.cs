namespace Chessy.Game.System.Model
{
    internal class ClearCellsForSetUnitS : SystemAbstract, IEcsRunSystem
    {
        internal ClearCellsForSetUnitS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                //E.PlayerInfoE(playerT).ForSetUnitsC.Clear();
            }
        }
    }
}