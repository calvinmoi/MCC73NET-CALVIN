using API.Contexts;
using API.Models;
namespace API.Repository.Data;

public class UniversityRepositories : GeneralRepository<MyContext, University, int>
{
  public UniversityRepositories(MyContext context) : base(context)
  {
  }
  /*private MyContext _context;
  private DbSet<University> _universities;
  public UniversityRepositories(MyContext context)
  {
    _context = context;
    _universities = context.Set<University>();
  }
  public int Delete(int id)
  {
    var data = _universities.Find(id);
    if (data == null)
    {
      return 0;
    }

    _universities.Remove(data);
    var result = _context.SaveChanges();
    return result;

  }

  public IEnumerable<University> Get()
  {

    return _universities.ToList();
  }

  public University Get(int id)
  {
    return _universities.Find(id);
  }

  public int Insert(University entity)
  {
    _universities.Add(entity);
    var result = _context.SaveChanges();
    return result;
  }

  public int Update(University entity)
  {
    _universities.Entry(entity).State = EntityState.Modified;
    var result = _context.SaveChanges();
    return result;
  }*/
}