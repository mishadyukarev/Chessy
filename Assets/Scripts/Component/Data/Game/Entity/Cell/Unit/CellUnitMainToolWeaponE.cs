using ECS;
using Photon.Realtime;

namespace Game.Game
{
    public sealed class CellUnitMainToolWeaponE : CellEntityAbstract
    {
        ref ToolWeaponTC ToolWeaponTC => ref Ent.Get<ToolWeaponTC>();
        ref LevelTC LevelTC => ref Ent.Get<LevelTC>();


        public ToolWeaponTypes ToolWeapon
        {
            get => ToolWeaponTC.ToolWeapon;
            set => ToolWeaponTC.ToolWeapon = value;
        }
        public LevelTypes Level
        {
            get => LevelTC.Level;
            set => LevelTC.Level = value;
        }

        public bool Is(params ToolWeaponTypes[] tws) => ToolWeaponTC.Is(tws);
        public bool Is(params LevelTypes[] levels) => LevelTC.Is(levels);


        public bool HaveToolWeapon => ToolWeaponTC.HaveToolWeapon;


        internal CellUnitMainToolWeaponE(in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
        }

        public void Set(in ToolWeaponTypes twT, in LevelTypes levelT)
        {
            ToolWeapon = twT;
            Level = levelT;
        }
        public void TakeToInventor(in PlayerTypes player, in InventorToolWeaponEs invTWEs)
        {
            invTWEs.ToolWeapons(this, player).Add();
            Set(ToolWeaponTypes.Axe, LevelTypes.First);
        }
        public void SetFromInventor(in ToolWeaponTypes twT, in LevelTypes levT, in PlayerTypes player, in InventorToolWeaponEs invTWEs)
        {
            invTWEs.ToolWeapons(twT, levT, player).Take();
            Set(twT, levT);
        }

        public void GiveTake_Master(in ToolWeaponTypes twT, in LevelTypes levT, in Player sender, in Entities e)
        {
            var idx_0 = Idx;

            var whoseMove = e.WhoseMoveE.CurPlayerI;

            if (e.UnitE(idx_0).Is(UnitTypes.Pawn))
            {
                if (e.UnitE(idx_0).Steps >= 0.5f)
                {
                    if (Is(ToolWeaponTypes.Axe))
                    {
                        if (Is(LevelTypes.First))
                        {
                            if (e.InventorToolWeaponEs.ToolWeapons(twT, levT, whoseMove).HaveToolWeapon)
                            {
                                SetFromInventor(twT, levT, whoseMove, e.InventorToolWeaponEs);
                                e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                            }
                            else
                            {
                                if (e.InventorResourcesEs.CanBuyTW(twT, levT, whoseMove, out var needs))
                                {
                                    e.InventorResourcesEs.BuyTW(twT, levT, whoseMove);
                                    Set(twT, levT);
                                    e.UnitE(idx_0).TakeSteps(0.5f);
                                    e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }
                                else
                                {
                                    e.RpcE.MistakeEconomyToGeneral(sender, needs);
                                }
                            }
                        }
                        else
                        {
                            TakeToInventor(whoseMove, e.InventorToolWeaponEs);
                        }
                    }
                    else
                    {
                        TakeToInventor(whoseMove, e.InventorToolWeaponEs);

                        e.RpcE.SoundToGeneral(sender, ClipTypes.PickMelee);
                    }
                }
                else e.RpcE.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}