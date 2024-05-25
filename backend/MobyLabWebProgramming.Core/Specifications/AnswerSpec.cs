using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class AnswerSpec : Specification<Answer>
    {
        public AnswerSpec(Guid id)
        {
        }
	
        public AnswerSpec(Guid questionId, Guid id) : this(id)
        {
            Query.Where( a =>  a.QuestionId == questionId);
        }
    }
}