using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class AnswerSpec : BaseSpec<AnswerSpec, Question>
    {
        public AnswerSpec(Guid id) : base(id)
        {
        }
    }
}