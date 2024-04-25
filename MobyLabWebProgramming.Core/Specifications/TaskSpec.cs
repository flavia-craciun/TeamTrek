using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class TaskSpec : BaseSpec<TaskSpec, ProjectTask>
    {
        public TaskSpec(Guid id) : base(id)
        {
        }
        
        public TaskSpec(Guid projectId, Guid id) : this(id)
        {
            Query.Where(task => task.ProjectId == projectId);
        }
    }
}