using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;
using System;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class CenterEventUISys : IEcsInitSystem
    {
        public void Init()
        {
            ReadyViewUIC.AddListenerToReadyButton(Ready);
            LeaveViewUIC.AddListener(delegate { PhotonNetwork.LeaveRoom(); });
            FriendZoneViewUIC.AddListenerReady(FriendReady);
            HintViewUIC.AddListHint_But(Hint);

            PickUpgUIC.AddList(UnitTypes.Pawn, delegate { UpgradeUnit(UnitTypes.Pawn); });

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