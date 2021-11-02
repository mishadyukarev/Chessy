using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    public sealed class PickUpgMasSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var senderPlayer = sender.GetPlayerType();

            switch (ForPickUpgMasC.UpgButType)
            {
                case PickUpgradeTypes.None: throw new Exception();

                case PickUpgradeTypes.King:
                    UnitsUpgC.SetUpg(senderPlayer, UnitTypes.King, UnitStatTypes.Hp, 0.2f);
                    UnitsUpgC.SetUpg(senderPlayer, UnitTypes.King, UnitStatTypes.Damage, 0.2f);
                    UnitsUpgC.SetStepUpg(senderPlayer, UnitTypes.King, 1);
                    break;

                case PickUpgradeTypes.Pawn:
                    UnitsUpgC.SetUpg(senderPlayer, UnitTypes.Pawn, UnitStatTypes.Hp, 0.2f);
                    UnitsUpgC.SetUpg(senderPlayer, UnitTypes.Pawn, UnitStatTypes.Damage, 0.2f);
                    UnitsUpgC.SetStepUpg(senderPlayer, UnitTypes.Pawn, 1);
                    break;

                case PickUpgradeTypes.Archer:
                    UnitsUpgC.SetUpg(senderPlayer, UnitTypes.Rook, UnitStatTypes.Hp, 0.2f);
                    UnitsUpgC.SetUpg(senderPlayer, UnitTypes.Rook, UnitStatTypes.Damage, 0.2f);
                    UnitsUpgC.SetStepUpg(senderPlayer, UnitTypes.Rook, 1);

                    UnitsUpgC.SetUpg(senderPlayer, UnitTypes.Bishop, UnitStatTypes.Hp, 0.2f);
                    UnitsUpgC.SetUpg(senderPlayer, UnitTypes.Bishop, UnitStatTypes.Damage, 0.2f);
                    UnitsUpgC.SetStepUpg(senderPlayer, UnitTypes.Bishop, 1);
                    break;

                case PickUpgradeTypes.Scout:
                    UnitsUpgC.SetStepUpg(senderPlayer, UnitTypes.Scout, 3);
                    break;

                case PickUpgradeTypes.Water:
                    for (var unit = (UnitTypes)1; unit < (UnitTypes)typeof(UnitTypes).GetEnumNames().Length; unit++)
                    {
                        UnitsUpgC.SetUpg(senderPlayer, unit, UnitStatTypes.Water, 0.2f);
                    }
                    break;

                case PickUpgradeTypes.Farm:
                    BuildsUpgC.AddUpgrade(senderPlayer, BuildTypes.Farm);
                    break;

                case PickUpgradeTypes.Woodcutter:
                    BuildsUpgC.AddUpgrade(senderPlayer, BuildTypes.Woodcutter);
                    break;

                case PickUpgradeTypes.Mine:
                    BuildsUpgC.AddUpgrade(senderPlayer, BuildTypes.Mine);
                    break;

                default: throw new Exception();
            }

        }
    }
}