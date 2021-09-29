using Assets.Scripts.Abstractions.Data;
using UnityEngine;

namespace Assets.Scripts
{
    internal struct ResourcesComponent
    {
        internal static GameObject InMenuZoneGO { get; private set; }
        internal static GameObject InGameZoneGO { get; private set; }

        internal static PrefabData PrefabConfig { get; private set; }
        internal static SoundData SoundConfig { get; private set; }
        internal static SpritesData SpritesConfig { get; private set; }
        internal static StartGameValuesConfig StartValuesGameConfig { get; private set; }
        internal static VideoClipsData VideoClipsData { get; private set; }


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