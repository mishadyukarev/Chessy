using ECS;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public sealed class CellUnitExtraToolWeaponE : CellEntityAbstract
    {
        ref ToolWeaponTC ToolWeaponTCRef => ref Ent.Get<ToolWeaponTC>();
        ref LevelTC LevelTCRef => ref Ent.Get<LevelTC>();
        ref AmountC ProtectionRef => ref Ent.Get<AmountC>();

        public ToolWeaponTC ToolWeaponTC => Ent.Get<ToolWeaponTC>();
        public LevelTC LevelTC => Ent.Get<LevelTC>();
        public AmountC ProtectionC => Ent.Get<AmountC>();


        public ToolWeaponTypes ToolWeaponT
        {
            get => ToolWeaponTC.ToolWeapon;
            internal set => ToolWeaponTCRef.ToolWeapon = value;
        }
        public LevelTypes LevelT
        {
            get => LevelTCRef.Level;
            internal set => LevelTCRef.Level = value;
        }
        public int Protection
        {
            get => ProtectionRef.Amount;
            internal set => ProtectionRef.Amount = value;
        }
        public bool HaveProtection => ProtectionC.Amount > 0;
        public bool Is(params ToolWeaponTypes[] tws) => ToolWeaponTC.Is(tws);

        internal CellUnitExtraToolWeaponE(in byte idx, in EcsWorld gameW) : base(idx, gameW) { }

        internal void Set(in CellUnitExtraToolWeaponE twE)
        {
            ToolWeaponTCRef = twE.ToolWeaponTC;
            LevelTCRef = twE.LevelTC;
            ProtectionRef = twE.ProtectionC;
        }
        public void SetNew(in ToolWeaponTypes tw, in LevelTypes level)
        {
            ToolWeaponTCRef.ToolWeapon = tw;
            LevelTCRef.Level = level;

            if (tw == ToolWeaponTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        ProtectionRef.Amount = 1;
                        break;

                    case LevelTypes.Second:
                        ProtectionRef.Amount = 3;
                        break;

                    default: throw new Exception();
                }
            }
        }
        public void SetNew(in (ToolWeaponTypes, LevelTypes) tw) => SetNew(tw.Item1, tw.Item2);

        public void BreakShield(in int taking = 1)
        {
            if (!ToolWeaponTC.IsShield) throw new Exception();
            if (!HaveProtection) throw new Exception();

            ProtectionRef.Amount -= taking;

            if (!HaveProtection) ToolWeaponTCRef.ToolWeapon = ToolWeaponTypes.None;
        }

        public void Reset()
        {
            ToolWeaponTCRef.ToolWeapon = ToolWeaponTypes.None;
            LevelTCRef.Level = LevelTypes.None;
            ProtectionRef.Amount = 0;
        }


        public void GiveTakeTW_Master(in ToolWeaponTypes tWForGive, in LevelTypes levelTW, in Player sender, in Entities e)
        {
            var idx_0 = Idx;


            if (idx_0 != default)
            {
                var unit_0 = e.UnitEs(idx_0).UnitE.UnitTC;

                var ownUnit_0 = e.UnitE(idx_0).OwnerC;

                var tw_0 = e.UnitEs(idx_0).ExtraToolWeaponE.ToolWeaponTC;
                var twLevel_0 = e.UnitEs(idx_0).ExtraToolWeaponE.LevelTC;


                if (unit_0.Is(UnitTypes.Pawn))
                {
                    if (e.UnitE(idx_0).HaveSteps)
                    {

                        if (tw_0.HaveTW)
                        {
                            e.InventorToolWeaponEs.ToolWeapons(tw_0.ToolWeapon, twLevel_0.Level, ownUnit_0.Player).ToolWeapons.Amount++;
                            e.UnitEs(idx_0).ExtraToolWeaponE.Reset();

                            e.UnitE(idx_0).Take(tWForGive);

                            e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }


                        else if (e.InventorToolWeaponEs.ToolWeapons(tWForGive, levelTW, ownUnit_0.Player).HaveTW)
                        {
                            e.InventorToolWeaponEs.ToolWeapons(tWForGive, levelTW, ownUnit_0.Player).ToolWeapons.Amount--;

                            e.UnitEs(idx_0).ExtraToolWeaponE.SetNew(tWForGive, levelTW);

                            e.UnitE(idx_0).Take(tWForGive);

                            e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }

                        else if (tWForGive == ToolWeaponTypes.Pick)
                        {
                            if (e.InventorResourcesEs.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                e.InventorResourcesEs.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                e.UnitEs(idx_0).ExtraToolWeaponE.SetNew(tWForGive, levelTW);

                                e.UnitE(idx_0).Take(tWForGive);

                                e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.Sword)
                        {
                            if (e.InventorResourcesEs.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                e.InventorResourcesEs.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                e.UnitEs(idx_0).ExtraToolWeaponE.SetNew(tWForGive, levelTW);

                                e.UnitE(idx_0).Take(tWForGive);

                                e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.BowCrossbow)
                        {
                            if (e.InventorResourcesEs.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                e.InventorResourcesEs.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                e.UnitEs(idx_0).ExtraToolWeaponE.SetNew(tWForGive, levelTW);

                                e.UnitE(idx_0).Take(tWForGive);

                                e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.Axe)
                        {
                            if (e.InventorResourcesEs.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                e.InventorResourcesEs.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                e.UnitEs(idx_0).ExtraToolWeaponE.SetNew(tWForGive, levelTW);

                                e.UnitE(idx_0).Take(tWForGive);

                                e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                e.RpcE.MistakeEconomyToGeneral(sender, needRes);
                            }
                        }

                        else if (tWForGive == ToolWeaponTypes.Shield)
                        {
                            if (e.InventorResourcesEs.CanBuyTW(ownUnit_0.Player, tWForGive, levelTW, out var needRes))
                            {
                                e.InventorResourcesEs.BuyTW(ownUnit_0.Player, tWForGive, levelTW);

                                e.UnitEs(idx_0).ExtraToolWeaponE.SetNew(tWForGive, levelTW);

                                e.UnitE(idx_0).Take(tWForGive);

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