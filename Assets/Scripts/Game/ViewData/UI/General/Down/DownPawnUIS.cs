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

            var amountPawnsInGame = E.UnitInfo(curPlayerI, LevelTypes.First, UnitTypes.Pawn).UnitsInGame
                + E.UnitInfo(curPlayerI, LevelTypes.Second, UnitTypes.Pawn).UnitsInGame;


            _pawnE.AmountTextC.TextUI.text = amountPawnsInGame.ToString() + "/" + Math.Round(E.PlayerE(curPlayerI).MaxAvailablePawns, 1);
            _pawnE.MaxPawnsTextC.TextUI.text = Math.Round(E.PlayerE(curPlayerI).PeopleInCity, 1).ToString();
        }
    }
}