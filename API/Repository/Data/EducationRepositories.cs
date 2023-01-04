using API.Contexts;
using API.Models;
using API.ViewModels;

namespace API.Repository.Data;

public class EducationRepositories : GeneralRepository<MyContext, Education, int>
{
  private readonly MyContext _context;

  public EducationRepositories(MyContext context) : base(context)
  {
    _context = context;
  }


  public IEnumerable<MEducationVM> MasterEducation()
  {
    var result = _context.Educations.Join(_context.Universities, e => e.UniversityId, u => u.Id, (e, u) => new MEducationVM
    {
      Id = e.Id,
      Degree = e.Degree,
      GPA = e.GPA,
      UniversityName = u.Name
    });

    return result;
  }
  /*public int Delete(int id)
  {
    var data = _educations.Find(id);
    if (data == null)
    {
      return 0;
    }

    _educations.Remove(data);
    var result = _context.SaveChanges();
    return result;

  }

  public IEnumerable<Education> Get()
  {

    return _educations.ToList();
  }

  public Education Get(int id)
  {
    return _educations.Find(id);
  }

  public int Insert(Education entity)
  {
    _educations.Add(entity);
    var result = _context.SaveChanges();
    return result;
  }

  public int Update(Education entity)
  {
    _educations.Entry(entity).State = EntityState.Modified;
    var result = _context.SaveChanges();
    return result;
  }*/
}
