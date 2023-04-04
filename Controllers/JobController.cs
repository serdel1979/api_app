using api_app.DTO;
using api_app.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_app.Controllers
{
    [ApiController]
    [Route("job")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [Authorize(Policy = "EsAdmin")]
    public class JobController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public JobController(ApplicationDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }



        [HttpPost("add")]
        public async Task<ActionResult> Add(JobNewDTO jobNewDto)
        {


            var job = mapper.Map<Job>(jobNewDto);

            context.Add(job);
            await context.SaveChangesAsync();
            var jobResDTO = mapper.Map<JobRespDTO>(job);

            return Ok(jobResDTO);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            var existe = await context.Jobs.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Job() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(JobNewDTO job, int id)
        {

            var existe = await context.Jobs.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            var jobBD = mapper.Map<Job>(job);
            jobBD.Id = id;

            context.Update(jobBD);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
