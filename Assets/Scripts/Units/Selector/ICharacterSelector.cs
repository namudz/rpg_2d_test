public interface ICharacterSelector
{
    bool IsSelectable {get;}
    bool Select();
    void Reset();
}