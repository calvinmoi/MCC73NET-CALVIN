using API.Contexts;
using API.Models;
using API.ViewModels;
using API.Handlres;

namespace API.Repository.Data;

public class AccountRepositories : GeneralRepository<MyContext, Account, string>
{
  private readonly MyContext _context;

  public AccountRepositories(MyContext context) : base(context)
  {
    _context = context;
  }

  public int Register(RegisterVM register)
  {
    string nik = GenerateNIK();
    if (!CheckEmailPhone(register.Email, register.Phone))
    {
      return 0; // Email atau Password sudah terdaftar
    }

    if (!IsPasswordValid(register.Password))
    {
      return 3; // Password Must Contain blabla
    }

    Employee employee = new Employee()
    {
      NIK = nik,
      Phone = register.Phone,
      Gender = register.Gender,
      BirthDate = register.BirthDate,
      Salary = register.Salary,
      Email = register.Email,
    };
    int index = register.FullName.IndexOf(" ");
    if (index <= 0)
    {
      employee.FirstName = register.FullName;
      employee.LastName = register.FullName;
    }
    else
    {
      employee.FirstName = register.FullName.Substring(0, register.FullName.IndexOf(" "));
      employee.LastName = register.FullName.Substring(register.FullName.IndexOf(" ") + 1);
    }

    _context.Employees.Add(employee);
    _context.SaveChanges();

    Account account = new Account()
    {
      NIK = nik,
      Password = Hashing.HashPassword(register.Password),
    };
    _context.Accounts.Add(account);
    _context.SaveChanges();

    University university = new University()
    {
      Name = register.UniversityName
    };

    var getuId = _context.Universities.SingleOrDefault(u => u.Name == register.UniversityName).Id;
    if (university.Name == register.UniversityName)
    {
      university.Id = getuId;
    }

    _context.Universities.Add(university);
    _context.SaveChanges();

    Education education = new Education()
    {
      GPA = register.GPA,
      Degree = register.Degree,
    };
    education.UniversityId = university.Id;
    _context.Educations.Add(education);
    _context.SaveChanges();

    Profiling profiling = new Profiling();
    profiling.NIK = nik;
    profiling.EducationId = education.Id;
    _context.Profilings.Add(profiling);
    _context.SaveChanges();

    AccountRole accountRole = new AccountRole()
    {
      AccountNIK = nik,
      RoleId = 1006
    };
    _context.AccountRoles.Add(accountRole);
    _context.SaveChanges();

    return 1;
  }

  public int Login(LoginVM login)
  {
    var result = _context.Accounts.Join(_context.Employees, a => a.NIK, e => e.NIK, (a, e) => new LoginVM
    {
      Email = e.Email,
      Password = a.Password
    }).SingleOrDefault(c => c.Email == login.Email);

    if (result == null)
    {
      return 0; // Email not found
    }
    else if (!Hashing.ValidatePassword(login.Password, result.Password))
    {
      return 1; // Wrong Password
    }
    return 2; // Email & Pass true
  }

  public bool IsPasswordValid(string password)
  {
    return password.Length >= 8 &&
           password.Length <= 15 &&
           password.Any(char.IsDigit) &&
           password.Any(char.IsLetter) &&
           (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation));
  }

  private bool CheckEmailPhone(string email, string phone)
  {
    var duplicate = _context.Employees.Where(s => s.Email == email || s.Phone == phone).ToList();

    if (duplicate.Any())
    {
      return false; // Email atau Password sudah ada
    }
    return true; // Email dan Password belum terdaftar
  }

  private string GenerateNIK()
  {
    var empCount = _context.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();

    if (empCount == null)
    {
      return "x1111";
    }
    string NIK = empCount.NIK.Substring(1, 4);
    return Convert.ToString("x" + (Convert.ToInt32(NIK) + 1));
  }

  public List<string> UserRoles(string email)
  {
    var getNIK = _context.Employees.SingleOrDefault(e => e.Email == email).NIK;
    var getRoles = _context.AccountRoles.Where(ar => ar.AccountNIK == getNIK)
                                            .Join(_context.Roles, ar => ar.RoleId, r => r.Id, (ar, r) => r.Name)
                                            .ToList();
    return getRoles;

    /*var result = (from e in _context.Employees
                 join a in _context.AccountRoles on e.NIK equals a.AccountNIK
                 join r in _context.Roles on a.RoleId equals r.Id
                 where e.Email == email
                 select r.Name).ToList();*/
  }
}


/*public int Delete(string id)
{
  var data = _accounts.Find(id);
  if (data == null)
  {
    return 0;
  }
  _accounts.Remove(data);
  var result = _context.SaveChanges();
  return result;
}


public IEnumerable<Account> Get()
{

  return _accounts.ToList();
}

public Account Get(string id)
{
  return _accounts.Find(id);
}

public int Insert(Account entity)
{
  _accounts.Add(entity);
  var result = _context.SaveChanges();
  return result;
}

public int Update(Account entity)
{
  _accounts.Entry(entity).State = EntityState.Modified;
  var result = _context.SaveChanges();
  return result;
} */


/*catch (Exception x)
{
  var reg = _context.Employees.FirstOrDefault(e => e.NIK != register.NIK);
  if (reg.Phone == register.Phone)
  {
    result = "Phone is already taken";
  }
  else if (reg.Email == register.Email)
{
  result = "Email is already taken";
}
else if (x.Message.Contains("An error occurred"))
{
  result = "Email / Phone is already taken / NIK cant have more than 5 characters";
}
}*/
