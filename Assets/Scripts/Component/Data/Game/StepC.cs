//using System;

//namespace Game.Game
//{
//    public struct StepC : IUnitCellE, IUnitConditionCellE
//    {
//        public int Steps;

//        public bool Have => Steps > 0;
//        public bool IsMinus => Steps < 0;
//        public bool IsNull => Steps == 0;


//        public void Set(in StepC stepC) => Steps = stepC.Steps;

//        public void Add(in int adding = 1)
//        {
//            if (adding < 0) throw new Exception("Need a positive number");
//            else if (adding == 0) throw new Exception("You're adding zero");
//            Steps += adding;
//        }
//        public void Take(in int taking = 1)
//        {
//            if (taking < 0) throw new Exception("Need a positive number");
//            else if (taking == 0) throw new Exception("You're taking zero");
//            Steps -= taking;
//            if (IsMinus) Reset();
//        }
//        public void Reset() => Steps = 0;
//    }
//}