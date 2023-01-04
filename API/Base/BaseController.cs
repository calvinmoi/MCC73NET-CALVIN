using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using API.Repository.Interface;

namespace API.Base
{
  [Route("api/[controller]")]
  [ApiController]
  public abstract class BaseController<Repository, TEntity, T> : ControllerBase where
  TEntity : class where
  Repository : IRepository<TEntity, T>
  {
    private Repository _repo;

    public BaseController(Repository repo)
    {
      _repo = repo;
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult<TEntity> GetAll()
    {
      try
      {
        var result = _repo.Get();
        return result.Count() == 0
        ? Ok(new { statusCode = 204, message = "Data Not Found!" })
        : Ok(new { statusCode = 201, message = "Success", data = result });
      }
      catch (Exception e)
      {
        return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
      }

    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult GetById(T key)
    {
      try
      {
        var result = _repo.Get(key);
        return result == null
        ? Ok(new { statusCode = 204, message = $"Data With Id {key} Not Found!" })
        : Ok(new { statusCode = 201, message = $"Id {key}", data = result });
      }
      catch (Exception e)
      {
        return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
      }
    }

    [HttpPost]
    public ActionResult Insert(TEntity entity)
    {
      try
      {
        var result = _repo.Insert(entity);
        return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
        Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
      }
      catch
      {
        return BadRequest(new { statusCode = 500, message = "" });
      }
    }

    [HttpPut]
    public ActionResult Update(TEntity entity)
    {
      try
      {
        var result = _repo.Update(entity);
        return result == 0 ?
        Ok(new { statusCode = 204, message = $"Id you search is not found!" })
      : Ok(new { statusCode = 201, message = "Update Succesfully!" });
      }
      catch
      {
        return BadRequest(new { statusCode = 500, message = "Something Wrong!" });
      }
    }

    [HttpDelete]
    public ActionResult<TEntity> Delete(T key)
    {
      try
      {
        var result = _repo.Delete(key);
        return result == 0 ? Ok(new { statusCode = 204, message = $"Id {key} Data Not Found" }) :
        Ok(new { statusCode = 201, message = "Data Delete Succesfully!" });
      }
      catch (Exception e)
      {
        return BadRequest(new { statusCode = 500, message = $"Something Wrong {e.Message}" });
      }
    }
  }
}
