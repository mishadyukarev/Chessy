using TMPro;

namespace Assets.Scripts.ECS.Components
{
    internal struct DropdownTMPComponent
    {
        internal TMP_Dropdown TMP_Dropdown { get; private set; }

        internal StepModeTypes StepModValue => (StepModeTypes)(TMP_Dropdown.value + 1);


        internal DropdownTMPComponent(TMP_Dropdown tMP_Dropdown) => TMP_Dropdown = tMP_Dropdown;
    }
}
