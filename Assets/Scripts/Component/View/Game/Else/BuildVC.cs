//using System;
//using UnityEngine;

//namespace Game.Game
//{
//    public struct BuildVC
//    {
//        SpriteRenderer _build_SR;


//        public BuildVC(GameObject build)
//        {
//            _build_SR = build.GetComponent<SpriteRenderer>();
//        }

//        public void Set(BuildingTypes build, bool isVisForMe, bool isVisForNext)
//        {
//            if (build != BuildingTypes.None)
//            {
//                if (isVisForMe)
//                {
//                    switch (build)
//                    {
//                        case BuildingTypes.None:
//                            throw new Exception();

//                        case BuildingTypes.City:
//                            _build_SR.sprite = ResourceSpriteVEs.Sprite(BuildingTypes.City).SpriteC.Sprite;
//                            break;

//                        case BuildingTypes.Farm:
//                            _build_SR.sprite = ResourceSpriteVEs.Sprite(BuildingTypes.Farm).SpriteC.Sprite;
//                            break;

//                        case BuildingTypes.Woodcutter:
//                            _build_SR.sprite = ResourceSpriteVEs.Sprite(BuildingTypes.Woodcutter).SpriteC.Sprite;
//                            break;

//                        case BuildingTypes.Mine:
//                            _build_SR.sprite = ResourceSpriteVEs.Sprite(BuildingTypes.Mine).SpriteC.Sprite;
//                            break;

//                        case BuildingTypes.Camp:
//                            _build_SR.sprite = ResourceSpriteVEs.Sprite(BuildingTypes.Camp).SpriteC.Sprite;
//                            break;

//                        default:
//                            throw new Exception();
//                    }
//                    _build_SR.enabled = true;


//                    var color = _build_SR.color;
//                    color.a = isVisForNext ? 1 : 0.7f;

//                    _build_SR.color = color;
//                }
//                else
//                {
//                    _build_SR.enabled = false;
//                }
//            }
//            else
//            {
//                _build_SR.enabled = false;
//            }
//        }
//    }
//}
