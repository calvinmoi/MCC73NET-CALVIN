using Microsoft.EntityFrameworkCore;
using API.Contexts;
using API.Models;
using API.Repository.Interface;

namespace API.Repository.Data;

public class RoleRepositories : GeneralRepository<Role, int>
{
  public RoleRepositories(MyContext context) : base(context)
  {
  }
  /*private MyContext _context;
  private DbSet<Role> _roles;
  public RoleRepositories(MyContext context)
  {
    _context = context;
    _roles = context.Set<Role>();
  }
  public int Delete(int id)
  {
    var data = _roles.Find(id);
    if (data == null)
    {
      return 0;
    }

    _roles.Remove(data);
    var result = _context.SaveChanges();
    return result;

  }

  public IEnumerable<Role> Get()
  {

    return _roles.ToList();
  }

  public Role Get(int id)
  {
    return _roles.Find(id);
  }

  public int Insert(Role entity)
  {
    _roles.Add(entity);
    var result = _context.SaveChanges();
    return result;
  }

  public int Update(Role entity)
  {
    _roles.Entry(entity).State = EntityState.Modified;
    var result = _context.SaveChanges();
    return result;
  }*/
}