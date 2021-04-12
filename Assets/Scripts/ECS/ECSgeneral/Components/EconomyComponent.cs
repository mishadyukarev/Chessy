internal struct EconomyComponent
{
    private int _gold;

    internal int Gold 
    { get { return _gold; } set { _gold = value; } }


    internal struct UnitsComponent
    {

    }


    internal struct BuildingsComponent
    {
        private bool _isSettedCity;
        private int[] _xySettedCity;


        internal bool IsSettedCity 
        { get { return _isSettedCity; } set { _isSettedCity = value; } }
        internal int[] XYsettedCity 
        { get { return _xySettedCity; } set { _xySettedCity = value; } }
    }
}
