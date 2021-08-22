//using System.Collections.Generic;

//namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
//{
//    internal struct CellDataComponent
//    {
//        private Dictionary<bool, bool> _isStarted;
//        private Dictionary<bool, bool> _isAroundCity;

//        internal CellDataComponent(Dictionary<bool, bool> isStarted)
//        {
//            _isStarted = isStarted;

//            _isAroundCity = new Dictionary<bool, bool>();
//            _isAroundCity.Add(true, default);
//            _isAroundCity.Add(false, default);
//        }

//        internal bool IsStartedCell(bool key) => _isStarted[key];

//        internal bool IsAroundCity(bool key) => _isAroundCity[key];
//        internal void SetIsAroundCity(bool isMasterKey, bool value) => _isAroundCity[isMasterKey] = value;
//    }
//}
