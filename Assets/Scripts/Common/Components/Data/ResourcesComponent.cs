using UnityEngine;

namespace Scripts.Common
{
    public struct ResourcesComponent
    {
        public static GameObject InMenuZoneGO { get; private set; }
        public static GameObject InGameZoneGO { get; private set; }

        public static PrefabData PrefabConfig { get; private set; }
        public static SoundData SoundConfig { get; private set; }
        public static SpritesData SpritesConfig { get; private set; }
        public static StartGameValuesConfig StartValuesGameConfig { get; private set; }
        public static VideoClipsData VideoClipsData { get; private set; }


        internal ResourcesComponent(bool isNeeded)
        {
            if (isNeeded)
            {
                PrefabConfig = Resources.Load<PrefabData>("PrefabData");

                InMenuZoneGO = PrefabConfig.Canvas.transform.Find("InMenuZone").gameObject;
                InGameZoneGO = PrefabConfig.Canvas.transform.Find("InGameZone").gameObject;

                SoundConfig = Resources.Load<SoundData>("SoundData");
                SpritesConfig = Resources.Load<SpritesData>("SpritesData");
                StartValuesGameConfig = Resources.Load<StartGameValuesConfig>("StartValues");
                VideoClipsData = Resources.Load<VideoClipsData>("VideoClipsData");
            }
        }
    }
}