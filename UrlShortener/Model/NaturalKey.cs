namespace UrlShortener.Model
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
#pragma warning disable CA1710
    public sealed class NaturalKey : Attribute
    {
        public override bool IsDefaultAttribute() => true;
    }
}
