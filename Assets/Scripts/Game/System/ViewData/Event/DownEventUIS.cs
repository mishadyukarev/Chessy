using Leopotam.Ecs;
using Game.Common;

namespace Game.Game
{
    public sealed class DownEventUIS : IEcsInitSystem
    {
        public void Init()
        {
            GetScoutUIC.AddListScout(ExecuteScout);
            GetHeroDownUIC.AddList(Hero);

            DonerUICom.AddListener(Done);

            GetPawnArcherUIC.AddListenerToCreateUnit(UnitTypes.Pawn, delegate { CreateUnit(UnitTypes.Pawn); });
            GetPawnArcherUIC.AddListenerToCreateUnit(UnitTypes.Archer, delegate { CreateUnit(UnitTypes.Archer); });

            KingZoneUIC.AddListenerToSetKing_Button(delegate { GetUnit(UnitTypes.King); });
            GetPawnArcherUIC.AddListener(UnitTypes.Pawn, delegate { GetUnit(UnitTypes.Pawn); });
            GetPawnArcherUIC.AddListener(UnitTypes.Archer, delegate { GetUnit(UnitTypes.Archer); });

            UpgUnitUIC.AddList(ToggleUpgradeUnit);
            TwGiveTakeUIC.AddList_Button(TWTypes.Pick, delegate { ToggleToolWeapon(TWTypes.Pick); });
            TwGiveTakeUIC.AddList_Button(TWTypes.Sword, delegate { ToggleToolWeapon(TWTypes.Sword); });
            TwGiveTakeUIC.AddList_Button(TWTypes.Shield, delegate { ToggleToolWeapon(TWTypes.Shield); });
        }

        private void ExecuteScout()
        {
            SelIdx.Reset();

            TryOnHint(VideoClipTypes.CreatingScout);

            if (WhoseMoveC.IsMyMove)
            {
                if (!ScoutHeroCooldownC.HaveCooldown(WhoseMoveC.CurPlayerI, UnitTypes.Scout))
                {
                    if (WhoseMoveC.IsMyMove)
                    {
                        CellClickC.Set(CellClickTypes.GiveScout);
                    }
                }
                else
                {
                    SoundEffectVC.Play(ClipTypes.Mistake);
                }
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }
        private void Hero()
        {
            SelIdx.Reset();
            TryOnHint(VideoClipTypes.CreatingHero);

            if (WhoseMoveC.IsMyMove)
            {
                if (!ScoutHeroCooldownC.HaveCooldown(WhoseMoveC.CurPlayerI, InvUnitsC.MyHero))
                {
                    CellClickC.Set(CellClickTypes.GiveHero);
                }
                else
                {
                    SoundEffectVC.Play(ClipTypes.Mistake);
                }
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }


        private void Done()
        {
            if (!InvUnitsC.Have(WhoseMoveC.CurPlayerI, UnitTypes.King, LevelTypes.First))
            {
                RpcSys.DoneToMaster();
            }
            else
            {
                SoundEffectVC.Play(ClipTypes.Mistake);
            }
        }

        private void CreateUnit(UnitTypes unitType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                GetterUnitsC.ResetCurTimer(unitType);

                RpcSys.CreateUnitToMaster(unitType);
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void GetUnit(UnitTypes unitType)
        {
            CellClickC.Set(CellClickTypes.Firstlick);
            SelIdx.Reset();

            GetterUnitsC.ResetCurTimer(unitType);

            if (WhoseMoveC.IsMyMove)
            {
                if (InvUnitsC.Have(WhoseMoveC.CurPlayerI, unitType, LevelTypes.Second))
                {
                    CellClickC.Set(CellClickTypes.SetUnit);
                    SelUnitC.SetSelUnit(unitType, LevelTypes.Second);
                }
                else if (InvUnitsC.Have(WhoseMoveC.CurPlayerI, unitType, LevelTypes.First))
                {
                    CellClickC.Set(CellClickTypes.SetUnit);
                    SelUnitC.SetSelUnit(unitType, LevelTypes.First);
                }
                else
                {
                    GetterUnitsC.ActiveNeedCreateButton(unitType, true);
                }
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void ToggleToolWeapon(TWTypes tWType)
        {
            SelIdx.Reset();

            if (WhoseMoveC.IsMyMove)
            {
                if (tWType == TWTypes.Pick)
                {
                    TryOnHint(VideoClipTypes.Pick);
                }
                else
                {
                    TryOnHint(VideoClipTypes.UpgToolWeapon);
                }

                if (CellClickC.Is(CellClickTypes.GiveTakeTW))
                {
                    if (tWType == TWTypes.Shield)
                    {
                        if (TwGiveTakeC.TWTypeForGive == tWType)
                        {
                            if (TwGiveTakeC.Level(tWType) == LevelTypes.First) TwGiveTakeC.SetInDown(tWType, LevelTypes.Second);
                            else TwGiveTakeC.SetInDown(tWType, LevelTypes.First);
                        }
                        else
                        {
                            TwGiveTakeC.Set(tWType);
                            TwGiveTakeC.SetInDown(tWType, LevelTypes.First);
                        }
                    }
                    else
                    {
                        TwGiveTakeC.Set(tWType);
                        TwGiveTakeC.SetInDown(tWType, LevelTypes.Second);
                    }
                }
                else
                {
                    CellClickC.Set(CellClickTypes.GiveTakeTW);
                    TwGiveTakeC.Set(tWType);

                    if (tWType == TWTypes.Shield)
                    {
                        //if(SelectorC.LevelTWType == LevelTWTypes.Iron)
                        //SelectorC.LevelTWType = LevelTWTypes.Wood;
                    }
                    else TwGiveTakeC.SetInDown(tWType, LevelTypes.Second);
                }
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }

        private void ToggleUpgradeUnit()
        {
            SelIdx.Reset();

            if (WhoseMoveC.IsMyMove)
            {
                TryOnHint(VideoClipTypes.UpgToolWeapon);
                CellClickC.Set(CellClickTypes.UpgradeUnit);
            }
            else SoundEffectVC.Play(ClipTypes.Mistake);
        }


        private void TryOnHint(VideoClipTypes videoClip)
        {
            if (HintComC.IsOnHint)
            {
                if (!HintC.WasActived(videoClip))
                {
                    HintViewUIC.SetActiveHintZone(true);
                    HintViewUIC.SetVideoClip(videoClip);
                    HintC.SetWasActived(videoClip, true);
                }
            }
        }
    }
}