namespace Housemate.Application.Attributes;

[AttributeUsage(validOn: AttributeTargets.Class, Inherited = false)]
public sealed class CachingDecorator : Attribute
{
}