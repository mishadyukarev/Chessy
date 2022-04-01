namespace Chessy
{
    public delegate void ActionMy<in T1, in T2, in T3>(T1 arg1, T2 arg2, params T3[] arg3);
}