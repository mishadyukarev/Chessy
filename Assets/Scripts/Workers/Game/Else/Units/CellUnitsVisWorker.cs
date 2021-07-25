using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.Else.Units
{
    internal sealed class CellUnitsVisWorker
    {
        private static CellUnitEntsContainer _cellUnitEntsContainer;

        private static SpritesData SpritesData => Main.Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;

        internal CellUnitsVisWorker(CellUnitEntsContainer cellUnitEntsContainer)
        {
            _cellUnitEntsContainer = cellUnitEntsContainer;
        }

        private static SpriteRenderer UnitSR(int[] xy) => _cellUnitEntsContainer.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer;

        internal static void SetSprite(UnitTypes unitType, params int[] xy)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _cellUnitEntsContainer.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.KingSprite;
                    break;

                case UnitTypes.Pawn:
                    _cellUnitEntsContainer.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.PawnSprite;
                    break;

                case UnitTypes.PawnSword:
                    _cellUnitEntsContainer.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.PawnSwordSprite;
                    break;

                case UnitTypes.Rook:
                    _cellUnitEntsContainer.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.RookSprite;
                    break;

                case UnitTypes.RookCrossbow:
                    _cellUnitEntsContainer.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.RookCrossbowSprite;
                    break;

                case UnitTypes.Bishop:
                    _cellUnitEntsContainer.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.BishopSprite;
                    break;

                case UnitTypes.BishopCrossbow:
                    _cellUnitEntsContainer.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.BishopCrossbowSprite;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void EnableUnitSR(bool isEnabled, int[] xy) => UnitSR(xy).enabled = isEnabled;
        internal static void SetColorSR(Color color, int[] xy) => UnitSR(xy).color = color;

        internal static void SetEnabledUnit(bool isEnabled, int[] xy) => UnitSR(xy).enabled = isEnabled;

        internal static void Flip(bool isActivated, XyTypes flipType, int[] xy)
        {
            switch (flipType)
            {
                case XyTypes.X:
                    UnitSR(xy).flipX = isActivated;
                    break;

                case XyTypes.Y:
                    UnitSR(xy).flipY = isActivated;
                    break;

                default:
                    break;
            }
        }
        internal void SetRotation(Vector3 rotation, int[] xy) => UnitSR(xy).transform.rotation = Quaternion.Euler(rotation);


        internal static void ActiveSelectorVisionUnit(bool isActive, UnitTypes unitType, int[] xy)
        {
            if (isActive)
            {
                EnableUnitSR(isActive, xy);
                SetSprite(unitType, xy);
            }

            else
            {
                EnableUnitSR(isActive, xy);
            }
        }
    }
}
