using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct EntitiesViewUI
    {
        public readonly LeftUIEs LeftEs;
        public readonly RightUIEs RightEs;
        public readonly CenterUIEs CenterEs;
        public readonly DownUIEs DownEs;
        public readonly UpUIEs UpEs;

        public LeftEnvironmentUIEs LeftEnvEs => LeftEs.EnvironmentEs;

        public EntitiesViewUI(in Chessy.Common.Entity.View.EntitiesView eVC, in Entity.Model.EntitiesModel ents)
        {
            eVC.MenuGOC.SetActive(false);
            eVC.GameGOC.SetActive(true);

            var gameZone = eVC.GameGOC.Transform;

            LeftEs = new LeftUIEs(gameZone.Find("Left+"));
            RightEs = new RightUIEs(gameZone.Find("RightZone"));
            CenterEs = new CenterUIEs(gameZone.Find("CenterZone"));
            DownEs = new DownUIEs(gameZone.Find("DownZone"));
            UpEs = new UpUIEs(gameZone.Find("ButtonLeave").GetComponent<Button>(), gameZone.Find("UpZone"));
        }
    }
}