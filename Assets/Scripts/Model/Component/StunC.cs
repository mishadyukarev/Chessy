namespace Chessy.Model
{
    public struct StunC
    {
        public float Stun { get; internal set; }
        public bool IsStunned => Stun > 0;
    }
}