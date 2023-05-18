namespace Housemate.Application.Attributes;

[AttributeUsage(validOn: AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class CachingDecorator : Attribute
{
}