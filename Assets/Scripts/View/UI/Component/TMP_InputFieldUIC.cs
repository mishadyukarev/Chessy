using TMPro;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    public struct TMP_InputFieldUIC
    {
        public readonly TMP_InputField InputField;

        public TMP_InputFieldUIC(in TMP_InputField inputField) => InputField = inputField;
    }
}