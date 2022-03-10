namespace Chessy.Game.System.Model
{
    internal class ClearCellsForSetUnitS : SystemAbstract, IEcsRunSystem
    {
        readonly PlayerTypes _playerT;

        public ClearCellsForSetUnitS(in PlayerTypes playerT, in EntitiesModel eM) : base(eM)
        {
            _playerT = playerT;
        }

        public void Run()
        {
            E.PlayerInfoE(_playerT).ForSetUnitsC.Clear();
        }
    }
}