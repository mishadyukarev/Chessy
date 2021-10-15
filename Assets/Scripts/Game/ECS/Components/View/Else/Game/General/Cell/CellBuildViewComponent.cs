using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Game
{
    internal struct CellBuildViewComponent
    {
        private SpriteRenderer _cellBuildFront_SR;
        private SpriteRenderer _cellBuildBack_SR;

        internal CellBuildViewComponent(GameObject cell_GO)
        {
            var parentGO = cell_GO.transform.Find("Building").gameObject;

            _cellBuildFront_SR = parentGO.GetComponent<SpriteRenderer>();

            _cellBuildBack_SR = parentGO.transform.Find("BackBuilding").GetComponent<SpriteRenderer>();
        }

        internal void SetSpriteFront(BuildingTypes buildingType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    _cellBuildFront_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.City);
                    break;

                case BuildingTypes.Farm:
                    _cellBuildFront_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.Farm);
                    break;

                case BuildingTypes.Woodcutter:
                    _cellBuildFront_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.Woodcutter);
                    break;

                case BuildingTypes.Mine:
                    _cellBuildFront_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.Mine);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void EnableFrontSR() => _cellBuildFront_SR.enabled = true;
        internal void DisableFrontSR() => _cellBuildFront_SR.enabled = false;

        internal void SetBackColor(Color color) => _cellBuildBack_SR.color = color;
        internal void SetSpriteBack(BuildingTypes buildingType)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    _cellBuildBack_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.CityBack);
                    break;

                case BuildingTypes.Farm:
                    _cellBuildBack_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.FarmBack);
                    break;

                case BuildingTypes.Woodcutter:
                    _cellBuildBack_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.WoodcutterBack);
                    break;

                case BuildingTypes.Mine:
                    _cellBuildBack_SR.sprite = SpritesResCom.Sprite(SpriteGameTypes.MineBack);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void EnableBackSR() => _cellBuildBack_SR.enabled = true;
        internal void DisableBackSR() => _cellBuildBack_SR.enabled = false;
    }
}
