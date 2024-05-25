using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProjectMemberNotFound => new(HttpStatusCode.NotFound, "This member is not associated with the project!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TaskNotFound => new(HttpStatusCode.NotFound, "Task doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage AnswerNotFound => new(HttpStatusCode.NotFound, "Answer doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProjectNotFound => new(HttpStatusCode.NotFound, "Project doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage QuestionNotFound => new(HttpStatusCode.NotFound, "Question doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TeamNotFound => new(HttpStatusCode.NotFound, "Team doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage MembersNotFound => new(HttpStatusCode.NotFound, "This team doesn't have any members assigned!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TasksNotFound => new(HttpStatusCode.NotFound, "This projecr doesn't have any tasks assigned!", ErrorCodes.EntityNotFound);
    public static ErrorMessage AnswersNotFound => new(HttpStatusCode.NotFound, "This question doesn't have any answers!", ErrorCodes.EntityNotFound);
    public static ErrorMessage AccessNotAllowed => new(HttpStatusCode.Forbidden, "User is not allowed to access or modify this resource!", ErrorCodes.UserNotAllowed);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);

}
