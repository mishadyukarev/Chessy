using Chessy.Game.Entity.Model;
using Chessy.Game.Entity.Model.Cell.Unit;
using Chessy.Game.System.Model;
using System;

namespace Chessy.Game.Model.System
{
    sealed class KillUnitS : SystemModelGameAbs
    {
        readonly UnitMainE _unitMainE;
        readonly CellSs _unitSs;

        internal KillUnitS(in UnitMainE unitMainE, in CellSs unitSs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _unitMainE = unitMainE;
            _unitSs = unitSs;
        }

        public void Kill(in PlayerTypes whoKiller)
        {
            if (whoKiller != PlayerTypes.None)
            {
                if (_unitMainE.UnitTC.Is(UnitTypes.King)) e.WinnerC.Player = whoKiller;
            }
            
            if (_unitMainE.UnitTC.IsGod)
            {
                var cooldown = 0f;

                switch (_unitMainE.UnitTC.Unit)
                {
                    case UnitTypes.Elfemale:
                        cooldown = HeroCooldownValues.Elfemale;
                        break;

                    case UnitTypes.Snowy:
                        cooldown = HeroCooldownValues.Snowy;
                        break;

                    case UnitTypes.Undead:
                        cooldown = HeroCooldownValues.Undead;
                        break;

                    case UnitTypes.Hell:
                        cooldown = HeroCooldownValues.Hell;
                        break;

                    default: throw new Exception();
                }

                e.PlayerInfoE(_unitMainE.PlayerTC.Player).HeroCooldownC.Cooldown = cooldown;
                e.PlayerInfoE(_unitMainE.PlayerTC.Player).HaveHeroInInventor = true;
            }

            if (_unitMainE.UnitTC.Is(UnitTypes.Tree)) e.HaveTreeUnit = false;


            _unitSs.SetLastDiedS.Set();
            e.UnitInfo(_unitMainE).Take(_unitMainE.UnitTC.Unit, 1);


            _unitSs.ClearUnitS.Clear();
        }
    }
}