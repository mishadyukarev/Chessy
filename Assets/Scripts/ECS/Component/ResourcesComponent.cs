﻿using UnityEngine;

namespace Assets.Scripts
{
    internal struct ResourcesComponent
    {
        private GameObject _inMenuZoneGO;
        private GameObject _inGameZoneGO;

        private PrefabData _prefabConfig;
        private SoundData _soundConfig;
        private SpritesData _spritesConfig;
        private StartGameValuesConfig _startValuesConfig;


        internal GameObject InMenuZoneGO => _inMenuZoneGO;
        internal GameObject InGameZoneGO => _inGameZoneGO;

        internal PrefabData PrefabConfig => _prefabConfig;
        internal SoundData SoundConfig => _soundConfig;
        internal SpritesData SpritesConfig => _spritesConfig;
        internal StartGameValuesConfig StartValuesGameConfig => _startValuesConfig;


        internal ResourcesComponent(bool isNeeded)
        {
            _prefabConfig = Resources.Load<PrefabData>("PrefabData");

            _inMenuZoneGO = _prefabConfig.Canvas.transform.Find("InMenuZone").gameObject;
            _inGameZoneGO = _prefabConfig.Canvas.transform.Find("InGameZone").gameObject;

            _soundConfig = Resources.Load<SoundData>("SoundData");
            _spritesConfig = Resources.Load<SpritesData>("SpritesData");
            _startValuesConfig = Resources.Load<StartGameValuesConfig>("StartValues");
        }
    }
}