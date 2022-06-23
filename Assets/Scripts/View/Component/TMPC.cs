using TMPro;

namespace Chessy.Model
{
    public readonly struct TMPC
    {
        public readonly TextMeshPro TextMeshPro;

        public TMPC(in TextMeshPro tmp)
        {
            TextMeshPro = tmp;
        }
    }
}