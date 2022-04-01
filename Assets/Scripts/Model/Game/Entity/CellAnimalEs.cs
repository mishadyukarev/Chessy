//using ECS;

//namespace Game.Game
//{
//    public struct CellAnimalEs
//    {
//        static Entity[] _animals;
//        public static ref AnimalTC AnimalTC(in byte idx) => ref _animals[idx].Get<AnimalTC>();


//        public CellAnimalEs(in EcsWorld gameW)
//        {
//            _animals = new Entity[CellStartValues.ALL_CELLS_AMOUNT];

//            for (byte idx = 0; idx < _animals.Length; idx++)
//            {
//                _animals[idx] = gameW.NewEntity()
//                    .Add(new AnimalTC());
//            }
//        }
//    }
//}