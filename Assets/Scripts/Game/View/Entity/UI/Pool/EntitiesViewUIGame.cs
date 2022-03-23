using Chessy.Common.View.UI;
using UnityEngine.UI;

namespace Chessy.Game
{
    public sealed class EntitiesViewUIGame
    {
        public readonly LeftUIEs LeftEs;
        public readonly RightUIEs RightEs;
        public readonly CenterUIEs CenterEs;
        public readonly DownUIEs DownEs;
        public readonly UpUIEs UpEs;

        public LeftEnvironmentUIEs LeftEnvEs => LeftEs.EnvironmentEs;

        public EntitiesViewUIGame(in EntitiesViewUICommon eUIC)
        {
            var gameZone = eUIC.CanvasE.GameCanvasGOC.Transform;

            LeftEs = new LeftUIEs(gameZone.Find("Left+"));
            RightEs = new RightUIEs(gameZone.Find("RightZone"));
            CenterEs = new CenterUIEs(gameZone.Find("CenterZone"));
            DownEs = new DownUIEs(gameZone.Find("DownZone"));
            UpEs = new UpUIEs(gameZone.Find("Exit+").Find("Button+").GetComponent<Button>(), gameZone.Find("UpZone"));
        }
    }
}