using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    public sealed class PickUpgMasSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var whoseMove = WhoseMoveC.WhoseMove;

            switch (ForPickUpgMasC.UpgButType)
            {
                case PickUpgradeTypes.None: throw new Exception();

                case PickUpgradeTypes.King:
                    UnitsUpgC.SetUpg(whoseMove, UnitTypes.King, UnitStatTypes.Hp, 0.2f);
                    UnitsUpgC.SetUpg(whoseMove, UnitTypes.King, UnitStatTypes.Damage, 0.2f);
                    //UnitsUpgC.SetStepUpg(senderPlayer, UnitTypes.King, 1);
                    break;

                case PickUpgradeTypes.Pawn:
                    UnitsUpgC.SetUpg(whoseMove, UnitTypes.Pawn, UnitStatTypes.Hp, 0.1f);
                    UnitsUpgC.SetUpg(whoseMove, UnitTypes.Pawn, UnitStatTypes.Damage, 0.1f);
                    //UnitsUpgC.SetStepUpg(senderPlayer, UnitTypes.Pawn, 1);
                    break;

                case PickUpgradeTypes.Archer:
                    UnitsUpgC.SetUpg(whoseMove, UnitTypes.Rook, UnitStatTypes.Hp, 0.8f);
                    UnitsUpgC.SetUpg(whoseMove, UnitTypes.Rook, UnitStatTypes.Damage, 0.2f);
                    //UnitsUpgC.SetStepUpg(senderPlayer, UnitTypes.Rook, 1);

                    UnitsUpgC.SetUpg(whoseMove, UnitTypes.Bishop, UnitStatTypes.Hp, 0.8f);
                    UnitsUpgC.SetUpg(whoseMove, UnitTypes.Bishop, UnitStatTypes.Damage, 0.2f);
                    //UnitsUpgC.SetStepUpg(senderPlayer, UnitTypes.Bishop, 1);
                    break;

                case PickUpgradeTypes.Scout:
                    UnitsUpgC.SetStepUpg(whoseMove, UnitTypes.Scout, 3);
                    break;

                case PickUpgradeTypes.Water:
                    for (var unit = (UnitTypes)1; unit < (UnitTypes)typeof(UnitTypes).GetEnumNames().Length; unit++)
                    {
                        UnitsUpgC.SetUpg(whoseMove, unit, UnitStatTypes.Water, 0.2f);
                    }
                    break;

                case PickUpgradeTypes.Farm:
                    BuildsUpgC.AddUpgrade(whoseMove, BuildTypes.Farm);
                    break;

                case PickUpgradeTypes.Woodcutter:
                    BuildsUpgC.AddUpgrade(whoseMove, BuildTypes.Woodcutter);
                    break;

                case PickUpgradeTypes.Mine:
                    BuildsUpgC.AddUpgrade(whoseMove, BuildTypes.Mine);
                    break;

                default: throw new Exception();
            }

        }
    }
}