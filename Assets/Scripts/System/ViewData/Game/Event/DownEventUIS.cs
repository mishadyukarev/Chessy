using static Game.Game.DownToolWeaponUIEs;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class DownEventUIS : SystemUIAbstract
    {
        internal DownEventUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            DownScoutUIEs.Scout<ButtonUIC>().AddListener(ExecuteScout);
            DownHeroUIE.ButtonC.AddListener(Hero);

            UIEntDownDoner.Doner<ButtonUIC>().AddListener(Done);


            DownPawnUIE.ButtonUIC.AddListener(delegate { GetPawn(); });

            Button<ButtonUIC>(ToolWeaponTypes.Pick).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            Button<ButtonUIC>(ToolWeaponTypes.Sword).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            Button<ButtonUIC>(ToolWeaponTypes.Shield).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });
            Button<ButtonUIC>(ToolWeaponTypes.BowCrossbow).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.BowCrossbow); });
            Button<ButtonUIC>(ToolWeaponTypes.Axe).AddListener(delegate { ToggleToolWeapon(ToolWeaponTypes.Axe); });
        }

        void ExecuteScout()
        {
            Es.SelectedIdxC.Reset();

            TryOnHint(VideoClipTypes.CreatingScout);

            if (Es.WhoseMovePlayerTC.IsMyMove)
            {
                if (Es.ScoutHeroCooldownE(UnitTypes.Scout, Es.WhoseMovePlayerTC.CurPlayerI).CooldownC.Amount !> 0)
                {
                    if (Es.WhoseMovePlayerTC.IsMyMove)
                    {
                        Es.SelUnitTC.Unit = UnitTypes.Scout;
                        Es.SelUnitLevelTC.Level = LevelTypes.First;
                        Es.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                }
                else
                {
                    SoundV(ClipTypes.Mistake).Play();
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }
        void Hero()
        {
            Es.SelectedIdxC.Reset();
            TryOnHint(VideoClipTypes.CreatingHero);

            if (Es.WhoseMovePlayerTC.IsMyMove)
            {
                if (Es.HaveHeroInInventor(Es.WhoseMovePlayerTC.CurPlayerI, out var myHero))
                {
                    if (Es.ScoutHeroCooldownE(myHero, Es.WhoseMovePlayerTC.CurPlayerI).CooldownC.Amount !> 0)
                    {
                        Es.SelUnitTC.Unit = myHero;
                        Es.SelUnitLevelTC.Level = LevelTypes.First;
                        Es.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                    else
                    {
                        SoundV(ClipTypes.Mistake).Play();
                    }
                }
            }
            else SoundV(ClipTypes.Mistake).Play();
        }
        void Done()
        {
            if (!Es.Units(UnitTypes.King, LevelTypes.First, Es.WhoseMovePlayerTC.CurPlayerI).HaveUnits)
            {
                Es.RpcE.DoneToMaster();
            }
            else
            {
                SoundV(ClipTypes.Mistake).Play();
            }
        }
        void GetPawn()
        {
            Es.SelectedIdxC.Reset();

            if (Es.WhoseMovePlayerTC.IsMyMove)
            {
                var curPlayerI = Es.WhoseMovePlayerTC.CurPlayerI;

                if (Es.PeopleInCityE(curPlayerI).AmountC.HaveAny)
                {
                    if (Es.MaxAvailablePawnsE(curPlayerI).CanGetPawn(Es.WhereWorker))
                    {
                        Es.SelUnitTC.Unit = UnitTypes.Pawn;
                        Es.SelUnitLevelTC.Level = LevelTypes.First;
                        Es.CellClickTC.Click = CellClickTypes.SetUnit;
                    }
                }
                else
                {
                    Es.MistakeC.Mistake = MistakeTypes.NeedMorePeopleInCity;
                    Es.Sound(ClipTypes.Mistake).ActionC.Action.Invoke();
                }


            }
            else SoundV(ClipTypes.Mistake).Play();
        }
        void ToggleToolWeapon(in ToolWeaponTypes tw)
        {
            Es.SelectedIdxC.Reset();

            if (Es.WhoseMovePlayerTC.IsMyMove)
            {
                if (tw == ToolWeaponTypes.Pick)
                {
                    TryOnHint(VideoClipTypes.Pick);
                }
                else
                {
                    TryOnHint(VideoClipTypes.UpgToolWeapon);
                }


                var levT = LevelTypes.First;

                if (tw == ToolWeaponTypes.Shield || tw == ToolWeaponTypes.BowCrossbow)
                {
                    if (Es.CellClickTC.Is(CellClickTypes.GiveTakeTW))
                    {
                        if (tw == ToolWeaponTypes.Shield || tw == ToolWeaponTypes.BowCrossbow)
                        {
                            if (Es.SelectedTWLevelTC.Is(LevelTypes.First)) levT = LevelTypes.Second;
                        }
                        else if (tw != ToolWeaponTypes.BowCrossbow) levT = LevelTypes.Second;
                    }   
                    else
                    {
                        levT = Es.SelectedTWLevelTC.Level;
                    }
                }
                else if (tw == ToolWeaponTypes.Axe || tw == ToolWeaponTypes.Sword)
                {
                    levT = LevelTypes.Second;
                }

                Es.SelectedTWTC.ToolWeapon = tw;
                Es.SelectedTWLevelTC.Level = levT;




                Es.CellClickTC.Click = CellClickTypes.GiveTakeTW;
            }
            else SoundV(ClipTypes.Mistake).Play();
        }
        void TryOnHint(VideoClipTypes videoClip)
        {
            if (Common.HintC.IsOnHint)
            {
                //if (!HintC.WasActived(videoClip))
                //{
                //    //EntityCenterHintUIPool.SetActiveHintZone(true);
                //    //EntityCenterHintUIPool.SetVideoClip(videoClip);
                //    HintC.SetWasActived(videoClip, true);
                //}
            }
        }
    }
}