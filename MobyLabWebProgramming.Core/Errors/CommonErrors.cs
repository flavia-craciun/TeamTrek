using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
    public static ErrorMessage AnswerNotFound => new(HttpStatusCode.NotFound, "Answer doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage QuestionNotFound => new(HttpStatusCode.NotFound, "Question doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TeamNotFound => new(HttpStatusCode.NotFound, "Team doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage MembersNotFound => new(HttpStatusCode.NotFound, "This team doesn't have any members assigned!", ErrorCodes.EntityNotFound);
    public static ErrorMessage AnswersNotFound => new(HttpStatusCode.NotFound, "This question doesn't have any answers!", ErrorCodes.EntityNotFound);
    public static ErrorMessage AccessNotAllowed => new(HttpStatusCode.Forbidden, "User is not allowed to access or modify this resource!", ErrorCodes.UserNotAllowed);
}
