//using ECS;

//namespace Game.Game
//{
//    public readonly struct CellCloudE
//    {
//        readonly static Entity[] _clouds;

//        public static ref T Cloud<T>(in byte idx) where T : struct, ICloudCell => ref _clouds[idx].Get<T>();

//        static CellCloudE()
//        {
//            _clouds = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
//        }
//        public CellCloudE(in EcsWorld gameW)
//        {
//            for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
//            {
//                _clouds[idx] = gameW.NewEntity()
//                    .Add(new HaveEffectC());
//            }
//        }
//    }
//}