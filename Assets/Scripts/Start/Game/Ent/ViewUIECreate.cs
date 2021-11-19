using Game.Common;
using Leopotam.Ecs;
using UnityEngine.UI;

namespace Game.Game
{
    public sealed class ViewUIECreate : IEcsInitSystem
    {
        public void Init()
        {
            CanvasC.SetCurZone(SceneTypes.Game);

            var upZone_GO = CanvasC.FindUnderCurZone("UpZone");
            var centerZone_GO = CanvasC.FindUnderCurZone("CenterZone");
            var downZone_GO = CanvasC.FindUnderCurZone("DownZone");
            var leftZone_GO = CanvasC.FindUnderCurZone("LeftZone");
            var rightZone_go = CanvasC.FindUnderCurZone("RightZone");


            var uniqAbilZone_trans = rightZone_go.transform.Find("UniqueAbilitiesZone");


            ///Up
            new EconomyViewUIC(upZone_GO);
            new LeaveViewUIC(CanvasC.FindUnderCurZone<Button>("ButtonLeave"));
            new WindUIC(upZone_GO.transform);
            new AlphaUpUIC(upZone_GO.transform);

            ///Center
            new EndGameViewUIC(centerZone_GO);
            new ReadyViewUIC(centerZone_GO.transform.Find("ReadyZone").gameObject);
            new MotionsViewUIC(centerZone_GO);
            new MistakeViewUIC(centerZone_GO);
            new KingZoneViewUIC(centerZone_GO);
            new SelectorUIC(centerZone_GO);
            new FriendZoneViewUIC(centerZone_GO.transform);
            new HintViewUIC(centerZone_GO.transform, HintComC.IsOnHint);
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
            new CutyLeftZoneViewUIC(leftZone_GO);
            new EnvirZoneViewUICom(leftZone_GO);

            ///Right
            new StatUIC(rightZone_go);
            new UniqButtonsUIC(uniqAbilZone_trans);
            new BuildAbilitViewUIC(rightZone_go.transform.Find("BuildingZone"));
            new ExtraTWZoneUIC(rightZone_go.transform);
            new EffectsIUC(rightZone_go.transform);
            new ProtectUIC(rightZone_go.transform.Find("ConditionZone"));
            new RelaxUIC(rightZone_go.transform.Find("ConditionZone"));
        }
    }
}