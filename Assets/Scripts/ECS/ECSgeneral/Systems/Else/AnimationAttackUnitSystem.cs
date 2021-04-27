using Leopotam.Ecs;
using UnityEngine;

internal class AnimationAttackUnitSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<AnimationAttackUnitComponent> _animationAttackUnitComponentRef = default;

    private SystemsGeneralManager _systemsGeneralManager = default;

    private bool _isInit = true;
    private bool _isOne = true;
    private Vector3 _direction;
    private Vector3 _heading;
    private float _speedAnimation = 3f;
    private float _distance;

    private int[] _xyStartCell => _animationAttackUnitComponentRef.Unref().XYStartCell;
    private int[] _xyEndCell => _animationAttackUnitComponentRef.Unref().XYEndCell;


    internal AnimationAttackUnitSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _systemsGeneralManager = eCSmanager.SystemsGeneralManager;
        _animationAttackUnitComponentRef = eCSmanager.EntitiesGeneralManager.AnimationAttackUnitComponentRef;
    }

    public void Run()
    {
        if (_isInit)
        {
            _isInit = false;
            _systemsGeneralManager.ActiveRunSystem(false, SystemGeneralTypes.Update, this.ToString());
            return;
        }

        if (CellUnitComponent(_xyEndCell).CurrentUnitGO.activeSelf && CellUnitComponent(_xyStartCell).CurrentUnitGO.activeSelf)
        {
            if (_isOne)
            {
                _heading = CellUnitComponent(_xyEndCell).CurrentUnitGO.transform.position - CellUnitComponent(_xyStartCell).CurrentUnitGO.transform.position;
                _distance = _heading.magnitude;
                _direction = _heading / _distance;

                CellUnitComponent(_xyStartCell).CurrentUnitGO.transform.Translate(_direction * Time.deltaTime * _speedAnimation);

                if (_distance <= 0.1f)
                {
                    _isOne = false;
                }

            }
            else
            {
                _heading = CellComponent(_xyStartCell).TransformCell.position - CellUnitComponent(_xyStartCell).CurrentUnitGO.transform.position;
                _distance = _heading.magnitude;
                _direction = _heading / _distance;

                CellUnitComponent(_xyStartCell).CurrentUnitGO.transform.Translate(_direction * Time.deltaTime * _speedAnimation);

                if (_distance <= 0.01f)
                {
                    _systemsGeneralManager.ActiveRunSystem(false, SystemGeneralTypes.Update, this.ToString());
                    _isOne = true;
                }
            }
        }
    }
}
