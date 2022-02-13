using ECS;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitExtraToolWeaponE : CellEntityAbstract
    {
        ref ToolWeaponTC ToolWeaponTC => ref Ent.Get<ToolWeaponTC>();
        ref LevelTC LevelTC => ref Ent.Get<LevelTC>();
        ref AmountC ProtectionC => ref Ent.Get<AmountC>();


        public ToolWeaponTypes ToolWeapon
        {
            get => ToolWeaponTC.ToolWeapon;
            set => ToolWeaponTC.ToolWeapon = value;
        }
        public LevelTypes LevelT
        {
            get => LevelTC.Level;
            set => LevelTC.Level = value;
        }
        public int Protection
        {
            get => ProtectionC.Amount;
            set => ProtectionC.Amount = value;
        }


        public bool Is(params ToolWeaponTypes[] tws) => ToolWeaponTC.Is(tws);


        public bool HaveToolWeapon => ToolWeaponTC.HaveToolWeapon;
        public bool HaveProtection => Protection > 0;


        internal CellUnitExtraToolWeaponE(in byte idx, in EcsWorld gameW) : base(idx, gameW) { }

        public void SetNew(in ToolWeaponTypes tw, in LevelTypes level)
        {
            ToolWeaponTC.ToolWeapon = tw;
            LevelTC.Level = level;

            if (tw == ToolWeaponTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        ProtectionC.Amount = 1;
                        break;

                    case LevelTypes.Second:
                        ProtectionC.Amount = 3;
                        break;

                    default: throw new Exception();
                }
            }
        }
        public void SetNew(in (ToolWeaponTypes, LevelTypes) tw) => SetNew(tw.Item1, tw.Item2);
        public void BreakShield(in int taking = 1)
        {
            if (!Is(ToolWeaponTypes.Shield)) throw new Exception();
            if (!HaveProtection) throw new Exception();

            ProtectionC.Amount -= taking;

            if (!HaveProtection) ToolWeaponTC.ToolWeapon = ToolWeaponTypes.None;
        }
        public void Reset()
        {
            ToolWeaponTC.ToolWeapon = ToolWeaponTypes.None;
            LevelTC.Level = LevelTypes.None;
            ProtectionC.Amount = 0;
        }


        public void GiveTakeTW_Master(in ToolWeaponTypes tWForGive, in LevelTypes levelTW, in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var ownUnit_0 = e.UnitE(idx_0).Owner;

            if (e.UnitE(idx_0).Is(UnitTypes.Pawn))
            {
                if (e.MainTWE(idx_0).Is(ToolWeaponTypes.Axe))
                {
                    if (e.UnitE(idx_0).HaveSteps)
                    {
                        if (e.ExtraTWE(idx_0).HaveToolWeapon)
                        {
                            e.InventorToolWeaponEs.ToolWeapons(e.ExtraTWE(idx_0).ToolWeapon, e.ExtraTWE(idx_0).LevelT, ownUnit_0).Add();
                            e.UnitEs(idx_0).ExtraToolWeaponE.Reset();

                            e.UnitE(idx_0).TakeSteps(tWForGive);

                            e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (e.InventorToolWeaponEs.ToolWeapons(tWForGive, levelTW, ownUnit_0).HaveToolWeapon)
                        {
                            e.InventorToolWeaponEs.ToolWeapons(tWForGive, levelTW, ownUnit_0).Take();

                            e.UnitEs(idx_0).ExtraToolWeaponE.SetNew(tWForGive, levelTW);

                            e.UnitE(idx_0).TakeSteps(tWForGive);

                            e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else
                        {
                            if (e.InventorResourcesEs.CanBuyTW(tWForGive, levelTW, ownUnit_0, out var needRes))
                            {
                                e.InventorResourcesEs.BuyTW(tWForGive, levelTW, ownUnit_0);

                                e.UnitEs(idx_0).ExtraToolWeaponE.SetNew(tWForGive, levelTW);

                                e.UnitE(idx_0).TakeSteps(tWForGive);

                                e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }
                    }
                    else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}