﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Data;

namespace server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        [HttpGet]
        public List<Assignment> GetAllAssignments([FromServices] DataContext dataContext)
        {
            try
            {
                return dataContext.Assignments.ToList();
            }
            catch (Exception e) when (dataContext is null)
            {
                throw;
            }
        }


        [HttpPost]
        public Guid CreateNewAsssignment([FromBody] Assignment assignment, [FromServices] DataContext context)
        {
            try
            {
                context.Add(assignment);
                context.SaveChanges();
                return assignment.Id;
            }
            catch (Exception) when (context is null)
            {
                throw;
            }
        }
    }
}
