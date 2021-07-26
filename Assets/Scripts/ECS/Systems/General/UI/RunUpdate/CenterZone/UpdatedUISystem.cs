using Assets.Scripts;
using UnityEngine;

internal sealed class UpdatedUISystem : SystemGeneralReduction
{
    private float _timer;

    public override void Run()
    {

        base.Run();

        if (_eGGUIM.MotionEnt_ActivatedCom.IsActivated)
        {
            _eGGUIM.MotionEnt_TextMeshProUGUICom.TextMeshProUGUI.text = "Motion: " + _eGGUIM.MotionEnt_AmountCom.AmountMotions;
            _eGGUIM.MotionEnt_ParentCom.ParentGO.SetActive(true);

            _timer += Time.deltaTime;

            if (_timer >= 1)
            {
                _eGGUIM.MotionEnt_ParentCom.ParentGO.SetActive(false);
                _eGGUIM.MotionEnt_ActivatedCom.IsActivated = false;
                _timer = 0;
            }
        }
        else
        {
            _eGGUIM.MotionEnt_ParentCom.ParentGO.SetActive(false);
        }
    }
}
