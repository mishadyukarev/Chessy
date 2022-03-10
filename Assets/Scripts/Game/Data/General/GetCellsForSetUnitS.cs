namespace Chessy.Game.System.Model
{
    sealed class GetCellsForSetUnitS : CellSystem, IEcsRunSystem
    {
        readonly PlayerTypes _playerT;

        internal GetCellsForSetUnitS(in PlayerTypes playerT, in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
            _playerT = playerT;
        }

        public void Run()
        {
            var xy = E.CellEs(Idx).CellE.XyC.Xy;
            var x = xy[0];
            var y = xy[1];

            if (!E.UnitTC(Idx).HaveUnit)
            {
                if (_playerT == PlayerTypes.First)
                {
                    if (y < 3 && x > 3 && x < 12)
                    {
                        E.PlayerInfoE(_playerT).ForSetUnitsC.Add(Idx);
                    }
                }
                else
                {
                    if (y > 7 && x > 3 && x < 12)
                    {
                        E.PlayerInfoE(_playerT).ForSetUnitsC.Add(Idx);
                    }
                }
            }
        }
    }
}