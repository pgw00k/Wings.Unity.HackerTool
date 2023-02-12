
namespace UnityHack
{
    public interface IHackGUIFoldout
    {
        bool IsExpand { get; set; }

        void Expand();

        void Unexpand();
    }
}
