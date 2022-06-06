using System.Text;
using back_labprog.Business;
using back_labprog.Contracts.Database;
using back_labprog.Contracts.Frontend;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace back_labprog.Controllers;

[ApiController]
[Route("[controller]")]
public class DiseaseController : ControllerBase
{
    private readonly IDiseaseBO _diseaseBO;
    private readonly IMapBO _mapBO;
    public DiseaseController(IDiseaseBO diseaseBO, IMapBO mapBO)
    {
        _diseaseBO = diseaseBO;
        _mapBO = mapBO;
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
    
    [EnableCors]
    [HttpPost("mapfilter")]
    public IEnumerable<DiseaseOccurrence> FilterOccurrences([FromBody] FilterRequest filter)
    {
        var occurrences = _mapBO.GetOccurrences(filter);
        return occurrences;
    }

    [EnableCors]
    [HttpPost("upload")]
    public ActionResult UploadCsv()
    {
        if (Request.ContentType != "text/tab-separated-values") return BadRequest("File type not supported");
        _mapBO.UploadCsv(Request.Body);
        return Ok();
    }
}