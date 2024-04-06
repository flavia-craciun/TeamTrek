using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class ProjectSpec : BaseSpec<ProjectSpec, Project>
    {
        public ProjectSpec(Guid id) : base(id)
        {
        }
    }
}