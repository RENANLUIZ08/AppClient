using AppClient.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppClient.Api.Controllers
{
    public abstract class ControllerBase<TEntity> : Controller where TEntity : class
    {
        public abstract IActionResult Get(int id);
        public abstract IActionResult Create([FromBody] TEntity entity);
        public abstract IActionResult Update(int id, [FromBody] TEntity entity);
        public abstract IActionResult Delete(int id);
        public abstract Task<IActionResult> GetAll(PageParams pageParams);
    }
}
