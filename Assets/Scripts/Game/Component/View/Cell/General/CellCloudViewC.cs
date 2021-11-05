using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    public struct CellCloudViewC
    {
        private SpriteRenderer _cloud_SR;

        public CellCloudViewC(GameObject cell_GO)
        {
            _cloud_SR = cell_GO.transform.Find("Weather").Find("Cloud").GetComponent<SpriteRenderer>();
        }

        public void EnableCloud(bool enabled) => _cloud_SR.enabled = enabled;
    }
}