using Assets.Scripts;
using UnityEngine;

internal sealed class AnimationAttackUnitSystem : SystemGeneralReduction
{
    private bool _isInit = true;
    private bool _isOne = true;
    private Vector3 _direction;
    private Vector3 _heading;
    private float _speedAnimation = 3f;
    private float _distance;

    private int[] XyStartCell => _eGM.AnimationAttack_UnitComponent.XyStartCell;
    private int[] XyEndCell => _eGM.AnimationAttack_UnitComponent.XyEndCell;


    internal AnimationAttackUnitSystem() { }

    public override void Run()
    {
        base.Run();

        //if (_isInit)
        //{
        //    _isInit = false;
        //    _sGM.TryActiveRunSystem(false, this.ToString(), _sGM.RunUpdateSystems);
        //    return;
        //}

        //if (_isOne)
        //{
        //    _heading = _eGM.CellUnitEnt_CellUnitCom(XyEndCell).CurrentUnitTransform.position - _eGM.CellUnitEnt_CellUnitCom(XyStartCell).CurrentUnitTransform.position;
        //    _distance = _heading.magnitude;
        //    _direction = _heading / _distance;

        //    _eGM.CellUnitEnt_CellUnitCom(XyStartCell).CurrentUnitTransform.Translate(_direction * Time.deltaTime * _speedAnimation);

        //    if (_distance <= 0.1f)
        //    {
        //        _isOne = false;
        //    }

        //}
        //else
        //{
        //    _heading = _eGM.CellUnitEnt_CellUnitCom(XyStartCell).CurrentUnitTransform.position - _eGM.CellUnitEnt_CellUnitCom(XyStartCell).CurrentUnitTransform.position;
        //    _distance = _heading.magnitude;
        //    _direction = _heading / _distance;

        //    _eGM.CellUnitEnt_CellUnitCom(XyStartCell).CurrentUnitTransform.Translate(_direction * Time.deltaTime * _speedAnimation);

        //    if (_distance <= 0.01f)
        //    {
        //        _sGM.TryActiveRunSystem(false, this.ToString(), _sGM.RunUpdateSystems);
        //        _isOne = true;
        //    }
        //}

    }
}
