//using Chessy.Game.Model.Entity;
//using Chessy.Game.Model.System;
//using Chessy.Game.Model.System.Master;
//using Chessy.Game.Values;
//using System.Collections.Generic;

//namespace Chessy.Game.Model.System
//{
//    internal sealed class CellSs
//    {
//        internal CellSs(in byte cell_0, in SystemsModelGame sMGame, in EntitiesModelGame eMGame)
//        {
//            var cellE_0 = eMGame.CellEs(cell_0);

//            var cellUnitE = cellE_0.UnitEs;


//            #region Unit


//            for (byte cell_1 = 0; cell_1 < StartValues.CELLS; cell_1++)
//            {
//                var cellE_to = eMGame.CellEs(cell_1);
//                var cellS_to = sMGame.CellSs(cell_1);

//                //_attack[cell_1] = new AttackUnit_M(eMGame, cellE_0, cellE_to, this, cellS_to);
//            }





//        }

//    }
//}