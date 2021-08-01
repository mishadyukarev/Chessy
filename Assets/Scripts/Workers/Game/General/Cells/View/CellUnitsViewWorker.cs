using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Entities.Game.General.Base.View.Containers.Cell;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.Else.Units
{
    internal sealed class CellUnitsViewWorker
    {
        private static CellUnitsViewContainerEnts _cellUnitsViewContainerEnts;

        private static SpritesData SpritesData => Main.Instance.ECSmanager.EntCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;

        internal CellUnitsViewWorker(CellUnitsViewContainerEnts cellUnitsViewContainerEnts)
        {
            _cellUnitsViewContainerEnts = cellUnitsViewContainerEnts;
        }

        private static SpriteRenderer UnitSR(int[] xy) => _cellUnitsViewContainerEnts.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer;

        internal static void SetSprite(UnitTypes unitType, params int[] xy)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    _cellUnitsViewContainerEnts.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.KingSprite;
                    break;

                case UnitTypes.Pawn:
                    _cellUnitsViewContainerEnts.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.PawnSprite;
                    break;

                case UnitTypes.PawnSword:
                    _cellUnitsViewContainerEnts.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.PawnSwordSprite;
                    break;

                case UnitTypes.Rook:
                    _cellUnitsViewContainerEnts.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.RookSprite;
                    break;

                case UnitTypes.RookCrossbow:
                    _cellUnitsViewContainerEnts.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.RookCrossbowSprite;
                    break;

                case UnitTypes.Bishop:
                    _cellUnitsViewContainerEnts.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.BishopSprite;
                    break;

                case UnitTypes.BishopCrossbow:
                    _cellUnitsViewContainerEnts.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.BishopCrossbowSprite;
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
