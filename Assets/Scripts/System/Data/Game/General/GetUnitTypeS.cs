using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class GetUnitTypeS : SystemAbstract, IEcsRunSystem
    {
        internal GetUnitTypeS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (Es.UnitTC(idx_0).HaveUnit)
                {
                    Es.UnitEs(idx_0).IsAnimal = Es.UnitTC(idx_0).Is(UnitTypes.Camel);

                    var isMelee = true;
                    var ishero = false;

                    if (Es.UnitTC(idx_0).Is(UnitTypes.Pawn))
                    {
                        if (Es.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            Es.UnitEs(idx_0).IsMelee = false;
                        }
                    }
                    else
                    {
                        switch (Es.UnitTC(idx_0).Unit)
                        {
                            case UnitTypes.Elfemale:
                                isMelee = false;
                                ishero = true;
                                break;

                            case UnitTypes.Snowy:
                                isMelee = false;
                                ishero = true;
                                break;

                            case UnitTypes.Undead:
                                ishero = true;
                                break;

                            case UnitTypes.Hell:
                                ishero = true;
                                break;

                            default:
                                break;
                        }
                    }

                    Es.UnitEs(idx_0).IsMelee = isMelee;
                    Es.UnitEs(idx_0).IsHero = ishero;
                }
                
            }
        }
    }
}