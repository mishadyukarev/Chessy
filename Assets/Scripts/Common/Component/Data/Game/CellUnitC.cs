//using System;

//namespace Game.Game
//{
//    public sealed class CellUnitC : UnitTC
//    {
//        public bool IsHero
//        {
//            get
//            {
//                switch (Unit)
//                {
//                    case UnitTypes.King: return false;
//                    case UnitTypes.Pawn: return false;

//                    case UnitTypes.Scout: return false;

//                    case UnitTypes.Elfemale: return true;
//                    case UnitTypes.Snowy: return true;
//                    case UnitTypes.Undead: return true;
//                    case UnitTypes.Hell: return true;

//                    case UnitTypes.Skeleton: return false;

//                    case UnitTypes.Camel: return false;
//                    default: throw new Exception();
//                }
//            }
//        }
//        public bool IsAnimal
//        {
//            get
//            {
//                switch (Unit)
//                {
//                    case UnitTypes.King: return false;
//                    case UnitTypes.Pawn: return false;

//                    case UnitTypes.Scout: return false;

//                    case UnitTypes.Elfemale: return false;
//                    case UnitTypes.Snowy: return false;
//                    case UnitTypes.Undead: return false;
//                    case UnitTypes.Hell: return false;

//                    case UnitTypes.Skeleton: return false;

//                    case UnitTypes.Camel: return true;
//                    default: throw new Exception();
//                }
//            }
//        }

//        public bool IsMelee(in ToolWeaponTC mainTC) 
//        {
//            return !Is(UnitTypes.Elfemale, UnitTypes.Snowy) && !mainTC.Is(ToolWeaponTypes.BowCrossbow);
//        }
//        public bool TrySetCooldownBeforeKilling(in CooldownC cooldown, in AmountC units, in int needCooldown)
//        {
//            if (Is(UnitTypes.Scout) || IsHero)
//            {
//                cooldown.Amount = needCooldown;
//                units.Add(1);
//                return true;
//            }
//            return false;
//        }
//        public void KillUnit(in PlayerTC playerTC, in WinnerPlayerTC winnerTC)
//        {
//            if (Is(UnitTypes.King)) winnerTC.Player = playerTC.Player;
//            Unit = UnitTypes.None;
//        }
//    }
//}