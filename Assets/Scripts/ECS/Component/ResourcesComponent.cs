using UnityEngine;

namespace Assets.Scripts
{
    internal struct ResourcesComponent
    {
        internal GameObject InMenuZoneGO { get; private set; }
        internal GameObject InGameZoneGO { get; private set; }

        internal PrefabData PrefabConfig { get; private set; }
        internal SoundData SoundConfig { get; private set; }
        internal SpritesData SpritesConfig { get; private set; }
        internal StartGameValuesConfig StartValuesGameConfig { get; private set; }


        internal ResourcesComponent(bool isNeeded)
        {
            PrefabConfig = Resources.Load<PrefabData>("PrefabData");

            InMenuZoneGO = PrefabConfig.Canvas.transform.Find("InMenuZone").gameObject;
            InGameZoneGO = PrefabConfig.Canvas.transform.Find("InGameZone").gameObject;

            SoundConfig = Resources.Load<SoundData>("SoundData");
            SpritesConfig = Resources.Load<SpritesData>("SpritesData");
            StartValuesGameConfig = Resources.Load<StartGameValuesConfig>("StartValues");
        }
    }
}