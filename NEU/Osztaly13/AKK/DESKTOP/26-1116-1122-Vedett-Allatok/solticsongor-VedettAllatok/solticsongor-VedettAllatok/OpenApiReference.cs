using Microsoft.OpenApi;

internal class OpenApiReference
{
    public ReferenceType Type { get; set; }
    public string ID { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public DateOnly Year { get; set; }
    public string Category { get; set; }
}