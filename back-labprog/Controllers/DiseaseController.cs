using back_labprog.Business;
using back_labprog.Contracts.Frontend;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace back_labprog.Controllers;

[ApiController]
[Route("[controller]")]
public class DiseaseController : ControllerBase
{
    private readonly IDiseaseBO _diseaseBO;
    public DiseaseController(IDiseaseBO diseaseBO)
    {
        _diseaseBO = diseaseBO;
    }

    [EnableCors]
    [HttpPost("insert")]
    public ActionResult AddDisease([FromBody] Disease newDisease)
    {
        _diseaseBO.AddDisease(newDisease);
        return Ok();
    }
    
    [EnableCors]
    [HttpGet("list")]
    public IEnumerable<Disease> GetDiseases()
    {
        return _diseaseBO.GetDiseasesAsync();
    }

    [EnableCors]
    [HttpPost("delete")]
    public ActionResult DeleteDisease([FromBody] Disease disease)
    {
        var response = _diseaseBO.DeleteDisease(disease);
        if (!response) return NotFound();
        return Ok();
    }
}