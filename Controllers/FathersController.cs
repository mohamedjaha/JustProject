using FamilyDataCollector.Data.Models;
using FamilyDataCollector.DTO;
using FamilyDataCollector.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace FamilyDataCollector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FathersController : ControllerBase
    {
        protected IUnitOfWork _UnitOfWork { get; set; }
        public FathersController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetFathersById(int fatherId)
        {
            Father father = await _UnitOfWork.RepoFather.FindByIdAsync(fatherId);
            if (father == null)
            {
                return NotFound($"the father with id {fatherId} is not found");
            }
            return Ok(father);

        }
        [HttpGet("GetGroup/ids")]
        public async Task<IActionResult> GetGroupOfFathersById(string fathersId)
        {
            int[] ids;
            try
            {
                ids = fathersId.Split().Select(int.Parse).ToArray();
            }
            catch (FormatException ex)
            {
                return BadRequest();
            }
            List<Father> fathers = new List<Father>();
            foreach (var id in ids)
            {
                Father father = await _UnitOfWork.RepoFather.FindByIdAsync(id);
                if (father == null)
                {
                    return NotFound($"the father with id {id} is not found");
                }
                fathers.Add(father);

            }
            return Ok(fathers);

        }
        [HttpPost]
        public async Task<IActionResult> Add(FatherDTO father)
        {
            if (ModelState.IsValid)
            {
                Father NewFather = new Father() { Name = father.Name, Age = father.Age, Work = father.Work };
                _UnitOfWork.RepoFather.AddAsync(NewFather);
                _UnitOfWork.CommitChange();
                return Ok(NewFather);
            }
            return BadRequest();
        }
    }
}
