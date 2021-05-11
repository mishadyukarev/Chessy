
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class StandartAbilityUISystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    private PhotonPunRPC _photonPunRPC = default;

    private Button _standartAbilityButton1;
    private Button _standartAbilityButton2;

    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;

    internal StandartAbilityUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;

        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;

        _standartAbilityButton1 = MainGame.InstanceGame.GameObjectPool.StandartAbilityButton1;
        _standartAbilityButton1.onClick.AddListener(delegate { StandartAbilityButton1(); });
        _standartAbilityButton2 = MainGame.InstanceGame.GameObjectPool.StandartAbilityButton2;
        _standartAbilityButton2.onClick.AddListener(delegate { StandartAbilityButton2(); });
    }

    public void Run()
    {
        if (CellUnitComponent(_xySelectedCell).IsMine)
        {
            switch (CellUnitComponent(_xySelectedCell).UnitType)
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
                if (CellUnitComponent(_xySelectedCell).IsProtected) _standartAbilityButton1.image.color = Color.yellow;
                else _standartAbilityButton1.image.color = Color.white;

                if (CellUnitComponent(_xySelectedCell).IsRelaxed) _standartAbilityButton2.image.color = Color.green;
                else _standartAbilityButton2.image.color = Color.white;
            }
        }
    }


    private void StandartAbilityButton1() => _photonPunRPC.ProtectUnitToMaster(!CellUnitComponent(_xySelectedCell).IsProtected, _xySelectedCell);
    private void StandartAbilityButton2() => _photonPunRPC.RelaxUnitToMaster(!CellUnitComponent(_xySelectedCell).IsRelaxed, _xySelectedCell);
}
