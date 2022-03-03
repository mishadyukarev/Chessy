namespace Chessy.Game
{
    public sealed class GetUnitTypeS : SystemAbstract, IEcsRunSystem
    {
        internal GetUnitTypeS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_VALUES.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.UnitTC(idx_0).HaveUnit)
                {
                    E.UnitMainE(idx_0).IsAnimal = E.UnitTC(idx_0).Is(UnitTypes.Camel);

                    var isMelee = true;
                    var ishero = false;

                    if (E.UnitTC(idx_0).Is(UnitTypes.Pawn))
                    {
                        if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            isMelee = false;
                        }
                    }
                    else
                    {
                        switch (E.UnitTC(idx_0).Unit)
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

                    E.UnitMainE(idx_0).IsMelee = isMelee;
                    E.UnitMainE(idx_0).IsHero = ishero;
                }
            }
        }
    }
}