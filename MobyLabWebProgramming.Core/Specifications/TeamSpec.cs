using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class TeamSpec : BaseSpec<TeamSpec, Team>
    {
        public TeamSpec(Guid id) : base(id)
        {
        }
    }
}