using Leopotam.Ecs;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class PickUpgMasSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var whoseMove = WhoseMoveC.WhoseMove;

            if (PickUpgC.HaveUpgrade(whoseMove))
            {
                switch (ForPickUpgMasC.UpgButType)
                {
                    case PickUpgradeTypes.None: throw new Exception();

                    case PickUpgradeTypes.King:
                        UnitPercUpgC.SetUpg(whoseMove, UnitTypes.King, UnitStatTypes.Damage, 0.2f);
                        break;

                    case PickUpgradeTypes.Pawn:
                        UnitPercUpgC.SetUpg(whoseMove, UnitTypes.Pawn, UnitStatTypes.Damage, 0.2f);
                        break;

                    case PickUpgradeTypes.Archer:
                        UnitPercUpgC.SetUpg(whoseMove, UnitTypes.Archer, UnitStatTypes.Damage, 0.2f);
                        break;

                    case PickUpgradeTypes.Scout:
                        UnitStepUpgC.SetStepUpg(whoseMove, UnitTypes.Scout, 3);
                        break;

                    case PickUpgradeTypes.Water:
                        for (var unit = (UnitTypes)1; unit < (UnitTypes)typeof(UnitTypes).GetEnumNames().Length; unit++)
                        {
                            UnitPercUpgC.SetUpg(whoseMove, unit, UnitStatTypes.Water, 0.2f);
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


                PickUpgC.SetHaveUpgrade(whoseMove, false);
                PickUpgC.SetHave_But(whoseMove, ForPickUpgMasC.UpgButType, false);

                RpcSys.SoundToGeneral(sender, ClipGameTypes.PickUpgrade);
            }
        }
    }
}