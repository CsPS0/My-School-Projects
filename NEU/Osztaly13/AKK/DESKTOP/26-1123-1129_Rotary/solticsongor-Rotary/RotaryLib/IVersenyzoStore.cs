namespace RotaryLib;

public interface IVersenyzoStore
{
    IEnumerable<Versenyzo> GetAll();
    Versenyzo? GetById(string rajtsz);
    bool AddNew(Versenyzo versenyzo);
}
