namespace Game.Game
{
    sealed class CenterUpgradeUnitS : SystemAbstract
    {
        internal CenterUpgradeUnitS(in EntitiesModel ents) : base(ents)
        {
            E.RpcPoolEs.UpgradeCenter = Upgrade;
        }

        public void Upgrade(UnitTypes unit)
        {
            var sender = E.RpcPoolEs.SenderC.Player;
            var whoseMove = E.WhoseMove.Player;

            if (unit == UnitTypes.Scout)
            {
                E.UnitInfo(whoseMove, LevelTypes.First, unit).MaxSteps += UnitStep_Values.CENTER_BONUS_SCOUT;
            }

            else
            {
                switch (unit)
                {
                    case UnitTypes.King:
                        E.UnitInfo(whoseMove, LevelTypes.First, unit).DamageStandart += UnitDamage_Values.CENTER_KING_BONUS;
                        E.UnitInfo(whoseMove, LevelTypes.First, unit).MaxSteps += UnitStep_Values.CENTER_KING_BONUS;
                        break;

                    case UnitTypes.Pawn:
                        E.UnitInfo(whoseMove, LevelTypes.First, unit).DamageStandart += UnitDamage_Values.CENTER_PAWN_BONUS;
                        E.UnitInfo(whoseMove, LevelTypes.First, unit).MaxSteps += UnitStep_Values.CENTER_PAWN_BONUS;
                        break;

                    default:
                        break;
                }
                
            }

            E.PlayerE(whoseMove).HaveCenterUpgrade = false;
            E.UnitInfo(whoseMove, LevelTypes.First, unit).HaveCenterUpgrade = false;

            E.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}