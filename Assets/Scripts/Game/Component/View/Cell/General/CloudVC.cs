﻿using UnityEngine;

namespace Chessy.Game
{
    public struct CloudVC
    {
        private SpriteRenderer _cloud_SR;

        public CloudVC(GameObject cell_GO)
        {
            _cloud_SR = cell_GO.transform.Find("Weather").Find("Cloud").GetComponent<SpriteRenderer>();
        }

        public void EnableCloud(bool enabled) => _cloud_SR.enabled = enabled;
    }
}