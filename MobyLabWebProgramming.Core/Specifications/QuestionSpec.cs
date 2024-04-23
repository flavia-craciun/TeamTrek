using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class QuestionSpec : BaseSpec<QuestionSpec, Question>
    {
        public QuestionSpec(Guid id) : base(id)
        {
        }
    }
}