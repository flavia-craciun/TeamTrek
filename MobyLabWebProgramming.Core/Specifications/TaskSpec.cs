using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class TaskSpec : BaseSpec<TaskSpec, ProjectTask>
    {
        public TaskSpec(Guid id) : base(id)
        {
        }
    }
}