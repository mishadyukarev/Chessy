using Chessy.Common;
using Leopotam.Ecs;
using Photon.Pun;

namespace Chessy.Game
{
    public sealed class CenterEventUIS : IEcsInitSystem
    {
        public void Init()
        {
            ReadyViewUIC.AddListenerToReadyButton(Ready);
            LeaveViewUIC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
            FriendZoneViewUIC.AddListenerReady(FriendReady);
            HintViewUIC.AddListHint_But(Hint);

            for (var unit = UnitTypes.First; unit < UnitTypes.Scout; unit++)
            {
                
            }

            PickUpgUIC.AddList(UnitTypes.King, delegate { UpgradeUnit(UnitTypes.King); });
            PickUpgUIC.AddList(UnitTypes.Pawn, delegate { UpgradeUnit(UnitTypes.Pawn); });
            PickUpgUIC.AddList(UnitTypes.Archer, delegate { UpgradeUnit(UnitTypes.Archer); });
            PickUpgUIC.AddList(UnitTypes.Scout, delegate { UpgradeUnit(UnitTypes.Scout); });


            HeroesViewUIC.AddListElf(Elf);
            HeroesViewUIC.AddListPremium(OpenShop);
        }

        private void Ready() => RpcSys.ReadyToMaster();
        private void FriendReady()
        {
            FriendC.IsActiveFriendZone = false;
        }
        private void Hint()
        {
            HintC.CurStartNumber++;

            if (HintC.CurStartNumber <= (int)VideoClipTypes.Start5)
            {
                HintC.SetWasActived((VideoClipTypes)HintC.CurStartNumber, true);
                HintViewUIC.SetVideoClip((VideoClipTypes)HintC.CurStartNumber);
            }
            else
            {
                HintViewUIC.SetActiveHintZone(false);
            }
        }

        private void UpgradeUnit(UnitTypes unit)
        {
            if (WhoseMoveC.IsMyMove)
            {
                RpcSys.PickUpgUnitToMas(unit);

                HeroesViewUIC.SetActiveZone(true);
            }
            else SoundEffectC.Play(ClipTypes.Mistake);
        }

        private void Elf()
        {
            if (WhoseMoveC.IsMyMove)
            {
                RpcSys.GetHero(UnitTypes.Elfemale);
            }
            else SoundEffectC.Play(ClipTypes.Mistake);
        }

        private void OpenShop()
        {
            ShopUIC.EnableZone();
        }
    }
}