﻿using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Game
{
    public struct CellBuildViewComponent
    {
        private SpriteRenderer _build_SR;
        private SpriteRenderer _buildBack_SR;

        public CellBuildViewComponent(GameObject cell_GO)
        {
            var parentGO = cell_GO.transform.Find("Building").gameObject;

            _build_SR = parentGO.GetComponent<SpriteRenderer>();

            _buildBack_SR = parentGO.transform.Find("BackBuilding").GetComponent<SpriteRenderer>();
        }

        public void SetSpriteFront(BuildingTypes buildingType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    _build_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.City);
                    break;

                case BuildingTypes.Farm:
                    _build_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.Farm);
                    break;

                case BuildingTypes.Woodcutter:
                    _build_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.Woodcutter);
                    break;

                case BuildingTypes.Mine:
                    _build_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.Mine);
                    break;

                case BuildingTypes.Camp:
                    _build_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.Camp);
                    break;

                default:
                    throw new Exception();
            }
        }
        public void EnableFrontSR() => _build_SR.enabled = true;
        public void DisableFrontSR() => _build_SR.enabled = false;

        public void SetBackColor(PlayerTypes playerType)
        {
            switch (playerType)
            {
                case PlayerTypes.None: throw new Exception();
                case PlayerTypes.First: _buildBack_SR.color = Color.blue; return;
                case PlayerTypes.Second: _buildBack_SR.color = Color.red; return;
                default: throw new Exception();
            }
        }
        public void SetSpriteBack(BuildingTypes buildingType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    _buildBack_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.CityBack);
                    break;

                case BuildingTypes.Farm:
                    _buildBack_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.FarmBack);
                    break;

                case BuildingTypes.Woodcutter:
                    _buildBack_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.WoodcutterBack);
                    break;

                case BuildingTypes.Mine:
                    _buildBack_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.MineBack);
                    break;

                case BuildingTypes.Camp:
                    _buildBack_SR.sprite = SpritesResComC.Sprite(SpriteGameTypes.CampBack);
                    break;

                default:
                    throw new Exception();
            }
        }
        public void EnableBackSR() => _buildBack_SR.enabled = true;
        public void DisableBackSR() => _buildBack_SR.enabled = false;

        public void SetAlpha(bool isVisibled)
        {
            if (isVisibled)
            {
                _build_SR.color = new Color(_build_SR.color.r, _build_SR.color.g, _build_SR.color.b, 1);
                _buildBack_SR.color = new Color(_buildBack_SR.color.r, _buildBack_SR.color.g, _buildBack_SR.color.b, 1);
            }
            else
            {
                _build_SR.color = new Color(_build_SR.color.r, _build_SR.color.g, _build_SR.color.b, 0.8f);
                _buildBack_SR.color = new Color(_buildBack_SR.color.r, _buildBack_SR.color.g, _buildBack_SR.color.b, 0.8f);
            }
        }
    }
}
