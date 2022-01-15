//using System;
//using UnityEngine;

//namespace Game.Game
//{
//    public struct UnitMainVC
//    {
//        private SpriteRenderer _main_SR;

//        public UnitMainVC(GameObject cell_GO)
//        {
//            _main_SR = cell_GO.transform.Find("MainUnit_SR").GetComponent<SpriteRenderer>();
//        }


//        public void SetEnabled(bool enabled) => _main_SR.enabled = enabled;

//        public void SetSprite(UnitTypes unit, LevelTypes levUnit, bool isCornered)
//        {

//        }


//        public void SetFlipX(bool isActive) => _main_SR.flipX = isActive;
//        public void SetLocRot(Vector3 rotation) => _main_SR.transform.localEulerAngles = rotation;
//    }
//}

