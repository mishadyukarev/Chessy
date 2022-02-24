//namespace Game.Game
//{
//    sealed class CellUnitHpS : IEcsRunSystem
//    {
//        readonly CellUnitHpE _hpE;

//        internal CellUnitHpS(in CellUnitHpE unitHpE)
//        {
//            _hpE = unitHpE;

//            _hpE.Setting = -1;
//        }

//        public void Run()
//        {
//            _hpE.HealthC.Health += _hpE.Adding;
//            _hpE.HealthC.Health -= _hpE.Taking;
//            if(_hpE.Setting != -1)
//            {
//                _hpE.HealthC.Health = _hpE.Setting;
//                _hpE.Setting = -1;
//            }

//            _hpE.Adding = 0;
//            _hpE.Taking = 0;
//        }
//    }
//}