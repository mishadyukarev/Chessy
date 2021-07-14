using TMPro;

namespace Assets.Scripts.ECS.Components
{
    internal struct DropdownTMPComponent
    {
        private TMP_Dropdown _tMP_Dropdown;

        internal StepModeTypes StepModValue => (StepModeTypes)(_tMP_Dropdown.value + 1);

        internal void StartFill(TMP_Dropdown tMP_Dropdown) => _tMP_Dropdown = tMP_Dropdown;

        internal void SetActive(bool needActive) => _tMP_Dropdown.gameObject.SetActive(needActive);
    }
}
