//using Chessy.Model;
//using Chessy.Model.Values;

//namespace Chessy.Model.View.System
//{
//    sealed class SyncPawnVS : SystemViewGameAbs
//    {
//        byte _curIdxPawn;

//        internal SyncPawnVS(in EntitiesViewGame eV, in EntitiesModelGame eMGame) : base(eV, eMGame)
//        {
//        }

//        internal void Sync()
//        {
//            _curIdxPawn = 0;

//            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
//            {
//                if (e.UnitTC(cell_0).Is(UnitTypes.Pawn))
//                {
//                    eV.PawnE(cell_0).ParenGOC.SetActive(true);
//                    //eV.PawnE(cell_0).SelectedSRC.
//                }
//                else
//                {
//                    eV.PawnE(cell_0).ParenGOC.SetActive(false);
//                }
//            }
//        }
//    }
//}