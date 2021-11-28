using Game.Common;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public sealed class CreateVCs
    {
        public CreateVCs(Quaternion main_rot)
        {

            //Main
            ToggleZoneVC.ReplaceZone(SceneTypes.Game);

            var genZone = new GameObject("GeneralZone");
            ToggleZoneVC.Attach(genZone.transform);


            new VideoClipsResC(true);
            new SpritesResC(true);

            SoundC.SavedVolume = SoundC.Volume;



            new GenerZoneVC(genZone);

            var backGroundGO = GameObject.Instantiate(PrefabResC.BackGroundCollider2D,
                MainGoVC.Pos + new Vector3(7, 5.5f, 2), main_rot);

            GenerZoneVC.Attach(backGroundGO.transform);

            var aSParent = new GameObject("AudioSource");
            GenerZoneVC.Attach(aSParent.transform);


            new SoundEffectVC(aSParent);
            new BackgroundVC(backGroundGO, PhotonNetwork.IsMasterClient);


            var rpc = new GameObject("RpcView");
            rpc.AddComponent<RpcSys>();
            GenerZoneVC.Attach(rpc.transform);
            new RpcVC(rpc);



            ///Canvas
            ///

            CanvasC.SetCurZone(SceneTypes.Game);

            var upZone_GO = CanvasC.FindUnderCurZone("UpZone");
            var centerZone_GO = CanvasC.FindUnderCurZone("CenterZone");
            var downZone_GO = CanvasC.FindUnderCurZone("DownZone");
            var leftZone_GO = CanvasC.FindUnderCurZone("LeftZone");
            var rightZone_go = CanvasC.FindUnderCurZone("RightZone");


            var uniqAbilZone_trans = rightZone_go.transform.Find("UniqueAbilitiesZone");


            ///Up
            new EconomyUIC(upZone_GO);
            new LeaveUIC(CanvasC.FindUnderCurZone<Button>("ButtonLeave"));
            new DirWindUIC(upZone_GO.transform);
            new AlphaUpUIC(upZone_GO.transform);

            ///Center
            new EndGameUIC(centerZone_GO);
            new ReadyUIC(centerZone_GO.transform.Find("ReadyZone").gameObject);
            new MotionsUIC(centerZone_GO);
            new MistakeUIC(centerZone_GO);
            new KingZoneUIC(centerZone_GO);
            new SelectorUIC(centerZone_GO);
            new FriendZoneUIC(centerZone_GO.transform);
            new HintViewUIC(centerZone_GO.transform, Common.HintC.IsOnHint);
            new PickUpgUIC(centerZone_GO.transform);
            new HeroesViewUIC(centerZone_GO.transform);

            ///Down
            new TwGiveTakeUIC(downZone_GO);
            new DonerUICom(downZone_GO);
            new UpgUnitUIC(downZone_GO.transform);

            var takeUnitZone = downZone_GO.transform.Find("TakeUnitZone");
            new GetPawnArcherUIC(takeUnitZone);
            new GetScoutUIC(takeUnitZone);
            new GetHeroDownUIC(takeUnitZone);

            ///Left
            new CutyLeftUIC(leftZone_GO);
            new EnvirUIC(leftZone_GO);

            ///Right
            new StatUIC(rightZone_go);
            new UniqButtonsUIC(uniqAbilZone_trans);
            new BuildAbilitUIC(rightZone_go.transform.Find("BuildingZone"));
            new ExtraTWZoneUIC(rightZone_go.transform);
            new EffectsUIC(rightZone_go.transform);
            new ProtectUIC(rightZone_go.transform.Find("ConditionZone"));
            new RelaxUIC(rightZone_go.transform.Find("ConditionZone"));
        }
    }
}