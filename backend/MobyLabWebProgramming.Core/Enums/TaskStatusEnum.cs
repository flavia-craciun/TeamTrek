using System.Text.Json.Serialization;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;

namespace MobyLabWebProgramming.Core.Enums;

[JsonConverter(typeof(SmartEnumNameConverter<TaskStatusEnum, string>))]
public sealed class TaskStatusEnum : SmartEnum<TaskStatusEnum, string>
{
    public static readonly TaskStatusEnum Open = new(nameof(Open), "Open");
    public static readonly TaskStatusEnum InProgress = new(nameof(InProgress), "InProgress");
    public static readonly TaskStatusEnum Review = new(nameof(Review), "Review");
    public static readonly TaskStatusEnum Done = new(nameof(Done), "Done");

    private TaskStatusEnum(string name, string value) : base(name, value)
    {
    }
}
