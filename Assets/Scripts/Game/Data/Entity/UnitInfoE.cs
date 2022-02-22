using System;

namespace Game.Game
{
    public struct UnitInfoE
    {
        public bool HaveInInventor;
        public int UnitsInGame;
        public CooldownC ScoutHeroCooldownC;
        public bool HaveCenterUpgrade;

        public float DamageStandart;
        public float MaxSteps;
        public float MaxWater;


        internal UnitInfoE(in LevelTypes levT, in UnitTypes unitT, bool haveInInventor) : this()
        {
            HaveInInventor = haveInInventor;

            HaveCenterUpgrade = true;


            var maxDamage = 0f;

            switch (levT)
            {
                case LevelTypes.First:
                    switch (unitT)
                    {
                        case UnitTypes.King:
                            maxDamage = UnitDamage_Values.KING;
                            break;

                        case UnitTypes.Pawn:
                            maxDamage = UnitDamage_Values.PAWN;
                            break;

                        case UnitTypes.Scout:
                            maxDamage = UnitDamage_Values.SCOUT;
                            break;

                        case UnitTypes.Elfemale:
                            maxDamage = UnitDamage_Values.ELFEMALE;
                            break;

                        case UnitTypes.Snowy:
                            maxDamage = UnitDamage_Values.SNOWY;
                            break;

                        case UnitTypes.Undead:
                            maxDamage = UnitDamage_Values.UNDEAD;
                            break;

                        case UnitTypes.Hell:
                            maxDamage = UnitDamage_Values.HELL;
                            break;

                        case UnitTypes.Skeleton:
                            maxDamage = UnitDamage_Values.SKELETON;
                            break;

                        case UnitTypes.Camel:
                            maxDamage = UnitDamage_Values.CAMEL;
                            break;

                        default: throw new Exception();
                    }
                    break;
            }

            DamageStandart = maxDamage;


            var maxSteps = 0f;

            switch (unitT)
            {
                case UnitTypes.King: maxSteps = 1; break;
                case UnitTypes.Pawn: maxSteps = 1; break;

                case UnitTypes.Scout: maxSteps = 2.5f; break;

                case UnitTypes.Elfemale: maxSteps = 1.5f; break;
                case UnitTypes.Snowy: maxSteps = 1.5f; break;
                case UnitTypes.Undead: maxSteps = 2; break;
                case UnitTypes.Hell: maxSteps = 1; break;

                case UnitTypes.Skeleton: maxSteps = 2; break;

                case UnitTypes.Camel: maxSteps = 2; break;

                default: throw new Exception();
            }

            MaxSteps = maxSteps;


            MaxWater = UnitWater_Values.MAX;
        }
    }
}