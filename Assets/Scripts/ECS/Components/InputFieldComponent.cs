using TMPro;

namespace Assets.Scripts.ECS.Components
{
    internal struct InputFieldComponent
    {
        private TMP_InputField _tMP_InputField;

        internal string Text => _tMP_InputField.text;

        internal void StartFill(TMP_InputField tMP_InputField) => _tMP_InputField = tMP_InputField;
    }
}
