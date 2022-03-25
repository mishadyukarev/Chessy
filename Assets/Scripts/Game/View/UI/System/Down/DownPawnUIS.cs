using System;

namespace Chessy.Game
{
    sealed class DownPawnUIS : SystemModelGameAbs, IEcsRunSystem
    {
        readonly DownPawnUIE _pawnE;

        internal DownPawnUIS(in DownPawnUIE pawnE, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
            _pawnE = pawnE;
        }

        public void Run()
        {
            var curPlayerI = eMGame.CurPlayerITC.Player;

            var amountPawnsInGame = eMGame.UnitInfoE(curPlayerI, LevelTypes.First).UnitsInGame(UnitTypes.Pawn)
                + eMGame.UnitInfoE(curPlayerI, LevelTypes.Second).UnitsInGame(UnitTypes.Pawn);

            _pawnE.AmountTextC.TextUI.text = amountPawnsInGame.ToString() + "/" + eMGame.PlayerInfoE(curPlayerI).MaxAvailablePawns;
            _pawnE.MaxPawnsTextC.TextUI.text = Math.Truncate(eMGame.PlayerInfoE(curPlayerI).PeopleInCity).ToString();
        }
    }
}