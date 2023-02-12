namespace UnityHack
{
    public delegate TResult GUIRenderFunction<in T, out TResult>(T arg);
}
