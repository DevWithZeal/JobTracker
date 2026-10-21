using JobTracker.Api.Data;
using JobTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly JobTrackerDbContext _context;

    public JobsController(JobTrackerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
    {
        return await _context.Jobs.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Job>> GetJob(int id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if (job is null)
        {
            return NotFound();
        }

        return job;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateJob(int id, Job updatedJob)
    {
        if (id != updatedJob.Id)
        {
            return BadRequest();
        }

        var existingJob = await _context.Jobs.FindAsync(id);

        if (existingJob is null)
        {
            return NotFound();
        }

        existingJob.CompanyName = updatedJob.CompanyName;
        existingJob.JobTitle = updatedJob.JobTitle;
        existingJob.Location = updatedJob.Location;
        existingJob.Status = updatedJob.Status;
        existingJob.AppliedDate = updatedJob.AppliedDate;
        existingJob.JobUrl = updatedJob.JobUrl;
        existingJob.Notes = updatedJob.Notes;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Job>> CreateJob(Job job)
    {
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return Created($"/api/jobs/{job.Id}", job);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteJob(int id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if (job is null)
        {
            return NotFound();
        }

        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}