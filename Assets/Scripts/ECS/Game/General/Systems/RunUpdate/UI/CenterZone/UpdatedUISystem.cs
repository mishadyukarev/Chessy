using Assets.Scripts;
using UnityEngine;

internal sealed class UpdatedUISystem : SystemGeneralReduction
{
    private float _timer;

    public override void Run()
    {

        base.Run();

        if (_eGM.MotionEnt_IsActivatedCom.IsActivated)
        {
            _eGM.MotionEnt_TextMeshProUGUICom.SetText("Motion: " + _eGM.MotionEnt_AmountCom.Amount);
            _eGM.MotionEnt_ParentCom.SetActive(true);

            _timer += Time.deltaTime;

            if (_timer >= 1)
            {
                _eGM.MotionEnt_ParentCom.SetActive(false);
                _eGM.MotionEnt_IsActivatedCom.IsActivated = false;
                _timer = 0;
            }
        }
        else
        {
            _eGM.MotionEnt_ParentCom.SetActive(false);
        }
    }
}
