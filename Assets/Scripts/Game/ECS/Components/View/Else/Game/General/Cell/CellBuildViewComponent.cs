using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
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
                    _cellBuildFront_SR.sprite = ResourcesComponent.SpritesConfig.City_Sprite;
                    break;

                case BuildingTypes.Farm:
                    _cellBuildFront_SR.sprite = ResourcesComponent.SpritesConfig.Farm;
                    break;

                case BuildingTypes.Woodcutter:
                    _cellBuildFront_SR.sprite = ResourcesComponent.SpritesConfig.Woodcutter;
                    break;

                case BuildingTypes.Mine:
                    _cellBuildFront_SR.sprite = ResourcesComponent.SpritesConfig.Mine;
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
                    _cellBuildBack_SR.sprite = ResourcesComponent.SpritesConfig.CityBack;
                    break;

                case BuildingTypes.Farm:
                    _cellBuildBack_SR.sprite = ResourcesComponent.SpritesConfig.FarmBack;
                    break;

                case BuildingTypes.Woodcutter:
                    _cellBuildBack_SR.sprite = ResourcesComponent.SpritesConfig.WoodcutterBack;
                    break;

                case BuildingTypes.Mine:
                    _cellBuildBack_SR.sprite = ResourcesComponent.SpritesConfig.MineBack;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal void EnableBackSR() => _cellBuildBack_SR.enabled = true;
        internal void DisableBackSR() => _cellBuildBack_SR.enabled = false;
    }
}
