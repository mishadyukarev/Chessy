using System;

namespace Chessy.Game
{
    sealed class DownPawnUIS : SystemAbstract, IEcsRunSystem
    {
        readonly DownPawnUIE _pawnE;

        internal DownPawnUIS(in DownPawnUIE pawnE, in EntitiesModel ents) : base(ents)
        {
            _pawnE = pawnE;
        }

        public void Run()
        {
            var curPlayerI = E.CurPlayerITC.Player;

            var amountPawnsInGame = E.UnitInfoE(curPlayerI, LevelTypes.First, UnitTypes.Pawn).UnitsInGame
                + E.UnitInfoE(curPlayerI, LevelTypes.Second, UnitTypes.Pawn).UnitsInGame;

            _pawnE.AmountTextC.TextUI.text = amountPawnsInGame.ToString() + "/" + E.PlayerE(curPlayerI).MaxAvailablePawns;
            _pawnE.MaxPawnsTextC.TextUI.text = Math.Truncate(E.PlayerE(curPlayerI).PeopleInCity).ToString();
        }
    }
}