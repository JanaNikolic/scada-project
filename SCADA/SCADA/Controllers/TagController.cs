using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SCADA.DTOS;
using SCADA.Model;
using SCADA.Service.IService;

namespace SCADA.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("input")]
        public IActionResult GetInputs()
        {
            return Ok(_tagService.GetInputTags());
        }

        [HttpGet("output")]
        public IActionResult GetOutputs()
        {
            return Ok(_tagService.GetOutputTags());
        }

        [IgnoreAntiforgeryToken]
        [HttpPost("analoginput")]
        public IActionResult AddTag([FromBody] TagDTO input)
        {
            try
            {
                _tagService.AddTag(input);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [IgnoreAntiforgeryToken]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _tagService.RemoveTag(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IActionResult AddOutputValue([FromBody] OutputDTO outputDTO)
        {
            try
            {
                _tagService.AddOutputValue(outputDTO);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}

