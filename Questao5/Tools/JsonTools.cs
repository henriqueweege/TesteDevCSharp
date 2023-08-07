using NJsonSchema;

namespace Tools;

public static class JsonTools
{
    public static string Generate(Type type)
        => JsonSchema.FromType(type).ToSampleJson().ToString();
}
