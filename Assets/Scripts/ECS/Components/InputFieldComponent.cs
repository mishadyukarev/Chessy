using TMPro;

namespace Assets.Scripts.ECS.Components
{
    internal struct InputFieldComponent
    {
        internal TMP_InputField TMP_InputField { get; private set; }


        internal InputFieldComponent(TMP_InputField tMP_InputField) => TMP_InputField = tMP_InputField;
    }
}
