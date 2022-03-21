using System;

namespace Chessy.Game
{
    sealed class DownPawnUIS : SystemAbstract, IEcsRunSystem
    {
        readonly DownPawnUIE _pawnE;

        internal DownPawnUIS(in DownPawnUIE pawnE, in Chessy.Game.Entity.Model.EntitiesModel ents) : base(ents)
        {
            _pawnE = pawnE;
        }

        public void Run()
        {
            var curPlayerI = E.CurPlayerITC.Player;

            var amountPawnsInGame = E.UnitInfoE(curPlayerI, LevelTypes.First).UnitsInGame(UnitTypes.Pawn)
                + E.UnitInfoE(curPlayerI, LevelTypes.Second).UnitsInGame(UnitTypes.Pawn);

            _pawnE.AmountTextC.TextUI.text = amountPawnsInGame.ToString() + "/" + E.PlayerInfoE(curPlayerI).MaxAvailablePawns;
            _pawnE.MaxPawnsTextC.TextUI.text = Math.Truncate(E.PlayerInfoE(curPlayerI).PeopleInCity).ToString();
        }
    }
}