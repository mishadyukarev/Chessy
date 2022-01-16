//using System;
//using UnityEngine;

//namespace Game.Game
//{
//    public struct BuildBackVC : IBuildCellV
//    {
//        SpriteRenderer _buildBack_SR;

//        public BuildBackVC(GameObject build)
//        {
//            _buildBack_SR = build.transform.Find("BackBuilding").GetComponent<SpriteRenderer>();
//        }

//        public void Set(BuildTypes build, PlayerTypes owner, bool isVisForMe, bool isVisForNext)
//        {
//            if (build != BuildTypes.None)
//            {
//                if (isVisForMe)
//                {
//                    _buildBack_SR.enabled = true;

//                    switch (build)
//                    {
//                        case BuildTypes.None:
//                            throw new Exception();

//                        case BuildTypes.City:
//                            _buildBack_SR.sprite = SpritesResC.Sprite(SpriteTypes.CityBack);
//                            break;

//                        case BuildTypes.Farm:
//                            _buildBack_SR.sprite = SpritesResC.Sprite(SpriteTypes.FarmBack);
//                            break;

//                        case BuildTypes.Woodcutter:
//                            _buildBack_SR.sprite = SpritesResC.Sprite(SpriteTypes.WoodcutterBack);
//                            break;

//                        case BuildTypes.Mine:
//                            _buildBack_SR.sprite = SpritesResC.Sprite(SpriteTypes.MineBack);
//                            break;

//                        case BuildTypes.Camp:
//                            _buildBack_SR.sprite = SpritesResC.Sprite(SpriteTypes.CampBack);
//                            break;

//                        default:
//                            throw new Exception();
//                    }


//                    var color = _buildBack_SR.color;
//                    color.a = isVisForNext ? 1 : 0.7f;

//                    _buildBack_SR.color = color;

//                    switch (owner)
//                    {
//                        case PlayerTypes.None: throw new Exception();
//                        case PlayerTypes.First: _buildBack_SR.color = Color.blue; return;
//                        case PlayerTypes.Second: _buildBack_SR.color = Color.red; return;
//                        default: throw new Exception();
//                    }
//                }
//                else
//                {
//                    _buildBack_SR.enabled = false;
//                }
//            }
//            else
//            {
//                _buildBack_SR.enabled = false;
//            }
//        }
//    }
//}