//namespace Game.Game
//{
//    public readonly struct CellAroundE
//    {
//        public readonly IdxC IdxsC;
//        public readonly XyC XyC;


//        //public IdxC Idx(in DirectTypes dir) => _aroundEs[(byte)dir - 1].IdxsC;
//        //public byte[] Idxs
//        //{
//        //    get
//        //    {
//        //        var idxs = new byte[_aroundEs.Length];
//        //        for (byte i = 0; i < idxs.Length; i++) idxs[i] = _aroundEs[i].IdxsC.Idx;
//        //        return idxs;
//        //    }
//        //}
//        //public DirectTC Direct(in byte idx)
//        //{
//        //    for (var i = 0; i < _aroundEs.Length; i++)
//        //    {
//        //        if (_aroundEs[i].IdxsC.Idx == idx) return new DirectTC((DirectTypes)i + 1);
//        //    }
//        //    throw new Exception();
//        //}
//        //public XyC XyAround(in DirectTypes dir) => _aroundEs[(byte)dir - 1].XyC;

//        internal CellAroundE(in byte idx, in byte[] xy)
//        {
//            IdxsC = new IdxC(idx);
//            XyC = new XyC(xy);
//        }
//    }
//}