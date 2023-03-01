using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_app.Controllers
{

    [ApiController]
    [Route("responsability")]
    public class ResponsabilityController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private IMapper mapper;

        public ResponsabilityController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpPost("add")]
        public async Task<ActionResult> Add(ResponsabilityNewDTO responNewDto)
        {


            var responsability = mapper.Map<Responsability>(responNewDto);

            context.Add(responsability);
            await context.SaveChangesAsync();
            var responsDTOres = mapper.Map<ResponsabilityRespDTO>(responsability);

            return Ok(responsDTOres);
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            var existe = await context.Responsabilities.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Responsability() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ResponsabilityNewDTO responsability, int id)
        {


            var existe = await context.Responsabilities.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var autorBD = mapper.Map<Responsability>(responsability);
            autorBD.Id = id;

            context.Update(autorBD);
            await context.SaveChangesAsync();
            return NoContent();
        }



    }
}
