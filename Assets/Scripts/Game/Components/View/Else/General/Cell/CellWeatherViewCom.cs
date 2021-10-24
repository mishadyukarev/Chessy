using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    internal struct CellWeatherViewCom
    {
        private SpriteRenderer _cloud_SR;

        internal CellWeatherViewCom(GameObject cell_GO)
        {
            _cloud_SR = cell_GO.transform.Find("Weather").Find("Cloud").GetComponent<SpriteRenderer>();
        }

        internal void EnableCloud(bool enabled) => _cloud_SR.enabled = enabled;
    }
}