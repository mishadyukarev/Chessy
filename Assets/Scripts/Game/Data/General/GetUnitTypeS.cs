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
                    var isMelee = true;

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
                                break;

                            case UnitTypes.Snowy:
                                isMelee = false;
                                break;

                            case UnitTypes.Undead:
                                break;

                            case UnitTypes.Hell:
                                break;

                            default:
                                break;
                        }
                    }

                    E.UnitMainE(idx_0).IsMelee = isMelee;
                }
            }
        }
    }
}