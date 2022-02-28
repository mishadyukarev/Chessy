using Chessy.Common;
using Photon.Pun;
using UnityEngine;
using static Chessy.Game.CenterHintUIE;
using static Chessy.Game.CenterUpgradeUIE;
using static Chessy.Game.UpEconomyUIE;
using static Chessy.Game.EntityVPool;

namespace Chessy.Game
{
    sealed class CenterEventUIS : SystemUIAbstract
    {
        internal CenterEventUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            UIEs.CenterEs.ReadyButtonC.AddListener(Ready);
            UIEs.CenterEs.JoinDiscordButtonC.AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });

            UIEs.CenterEs.KingE.Button.AddListener(delegate { GetKing(); });

            UIEs.CenterEs.FriendE.ButtonC.AddListener(FriendReady);
            //UIEs.CenterEs.AddListener(Hint);

            //UIEs.CenterEs.Units(UnitTypes.King).AddListener(delegate { UpgradeUnit(UnitTypes.King); });
            //Units(UnitTypes.Pawn).AddListener(delegate { UpgradeUnit(UnitTypes.Pawn); });
            //Units(UnitTypes.Scout).AddListener(delegate { UpgradeUnit(UnitTypes.Scout); });

            //Builds(BuildingTypes.Farm).AddListener(delegate { UpgradeBuild(BuildingTypes.Farm); });
            //Builds(BuildingTypes.Woodcutter).AddListener(delegate { UpgradeBuild(BuildingTypes.Woodcutter); });

            //Water.AddListener(UpgradeWater);


            UIEs.CenterEs.HeroE(UnitTypes.Elfemale).ButtonC.AddListener(delegate { GetHero(UnitTypes.Elfemale); });
            UIEs.CenterEs.HeroE(UnitTypes.Snowy).ButtonC.AddListener(delegate { GetHero(UnitTypes.Snowy); });
            UIEs.CenterEs.HeroE(UnitTypes.Undead).ButtonC.AddListener(delegate { GetHero(UnitTypes.Undead); });
            UIEs.CenterEs.HeroE(UnitTypes.Hell).ButtonC.AddListener(delegate { GetHero(UnitTypes.Hell); });
            //UIEs.CenterEs.CenterHeroUIE(UnitTypes.Elfemale).AddListener(OpenShop);
        }

        void Ready() => E.RpcPoolEs.ReadyToMaster();
        void FriendReady()
        {
            E.FriendIsActive = false;
        }
        void GetKing()
        {
            E.SelectedIdxC.Idx = 0;


            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                if (E.UnitInfo(E.CurPlayerITC.Player, LevelTypes.First, UnitTypes.King).HaveInInventor)
                {
                    E.SelectedUnitE.Set(UnitTypes.King, LevelTypes.First);
                    E.CellClickTC.Click = CellClickTypes.SetUnit;
                }
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }
        void Hint()
        {
            //HintC.CurStartNumber++;

            //if (HintC.CurStartNumber <= (int)VideoClipTypes.Start5)
            //{
            //    HintC.SetWasActived((VideoClipTypes)HintC.CurStartNumber, true);
            //    //EntityCenterHintUIPool.SetVideoClip((VideoClipTypes)HintC.CurStartNumber);
            //}
            //else
            //{
            //    //EntityCenterHintUIPool.SetActiveHintZone(false);
            //}
        }

        void UpgradeUnit(UnitTypes unit)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                E.RpcPoolEs.PickUpgUnitToMas(unit);

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        void UpgradeBuild(BuildingTypes build)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                E.RpcPoolEs.PickUpgBuildToMas(build);

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        void UpgradeWater()
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                E.RpcPoolEs.UpgWater();

                UIEs.CenterEs.HeroE(UnitTypes.Elfemale).Parent.SetActive(true);
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        void GetHero(in UnitTypes unit)
        {
            if (E.CurPlayerITC.Is(E.WhoseMove.Player))
            {
                E.RpcPoolEs.GetHeroToMaster(unit);
            }
            else E.Sound(ClipTypes.Mistake).Action.Invoke();
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}