
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class StandartAbilityUISystem : RPCGeneralReduction, IEcsRunSystem
{
    private Button _standartAbilityButton1;
    private Button _standartAbilityButton2;

    private int[] _xySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;

    internal StandartAbilityUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _standartAbilityButton1 = MainGame.Instance.ObjectPool.StandartAbilityButton1;
        _standartAbilityButton1.onClick.AddListener(delegate { StandartAbilityButton1(); });
        _standartAbilityButton2 = MainGame.Instance.ObjectPool.StandartAbilityButton2;
        _standartAbilityButton2.onClick.AddListener(delegate { StandartAbilityButton2(); });
    }

    public void Run()
    {
        if (_eGM.CellEnt_CellUnitCom(_xySelectedCell).IsMine)
        {
            switch (_eGM.CellEnt_CellUnitCom(_xySelectedCell).UnitType)
            {
                case UnitTypes.None:
                    ActiveStandartAbilities(false);
                    break;

                case UnitTypes.King:
                    ActiveStandartAbilities(true);
                    break;

                case UnitTypes.Pawn:
                    ActiveStandartAbilities(true);
                    break;

                case UnitTypes.Rook:
                    ActiveStandartAbilities(true);
                    break;

                case UnitTypes.Bishop:
                    ActiveStandartAbilities(true);
                    break;

                default:
                    break;
            }
        }
        else
        {
            ActiveStandartAbilities(false);
        }

        void ActiveStandartAbilities(bool isActive)
        {
            _standartAbilityButton1.gameObject.SetActive(isActive);
            _standartAbilityButton2.gameObject.SetActive(isActive);

            if (isActive)
            {
                if (_eGM.CellEnt_CellUnitCom(_xySelectedCell).IsProtected) _standartAbilityButton1.image.color = Color.yellow;
                else _standartAbilityButton1.image.color = Color.white;

                if (_eGM.CellEnt_CellUnitCom(_xySelectedCell).IsRelaxed) _standartAbilityButton2.image.color = Color.green;
                else _standartAbilityButton2.image.color = Color.white;
            }
        }
    }


    private void StandartAbilityButton1() => _photonPunRPC.ProtectUnitToMaster(!_eGM.CellEnt_CellUnitCom(_xySelectedCell).IsProtected, _xySelectedCell);
    private void StandartAbilityButton2() => _photonPunRPC.RelaxUnitToMaster(!_eGM.CellEnt_CellUnitCom(_xySelectedCell).IsRelaxed, _xySelectedCell);
}
