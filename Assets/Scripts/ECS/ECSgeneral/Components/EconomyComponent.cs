internal struct EconomyComponent
{
    private int _gold;

    internal int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }


    internal struct UnitsComponent
    {
        private bool _isSettedKing;

        internal bool IsSettedKing
        {
            get { return _isSettedKing; }
            set { _isSettedKing = value; }
        }
    }


    internal struct BuildingsComponent
    {
        private bool _isSettedCity;
        private int[] _xySettedCity;


        internal bool IsSettedCity
        {
            get { return _isSettedCity; }
            set { _isSettedCity = value; }
        }
        internal int[] XYsettedCity
        {
            get { return _xySettedCity; }
            set { _xySettedCity = value; }
        }
    }
}
