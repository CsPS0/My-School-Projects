namespace VedettLib;

public interface IVedettAllatStore
{
    IEnumerable<VedettAllat> GetAll();
    VedettAllat? GetById(int id);
    IEnumerable<VedettAllat> GetByYear(int year);
    IEnumerable<VedettAllat> GetByCategory(string category);
    IEnumerable<string> GetCategories();
    int AddNew(VedettAllat allat);
    bool Update(int id, VedettAllat newAllat);
    bool Delete(int id);
}
