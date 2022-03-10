namespace Chessy.Game.System.Model
{
    public sealed class GetUnitTypeS : CellSystem, IEcsRunSystem
    {
        internal GetUnitTypeS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            if (E.UnitTC(Idx).HaveUnit)
            {
                var isMelee = true;

                if (E.UnitTC(Idx).Is(UnitTypes.Pawn))
                {
                    if (E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.BowCrossbow))
                    {
                        isMelee = false;
                    }
                }
                else
                {
                    switch (E.UnitTC(Idx).Unit)
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

                E.UnitMainE(Idx).IsMelee = isMelee;
            }
        }
    }
}