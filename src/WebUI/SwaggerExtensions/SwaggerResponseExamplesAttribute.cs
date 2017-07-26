using System;
using System.Diagnostics.CodeAnalysis;

namespace OfficeLocationMicroservice.WebUi.SwaggerExtensions
{
    [ExcludeFromCodeCoverage]
    public class SwaggerResponseExamplesAttribute : Attribute
    {
        public SwaggerResponseExamplesAttribute(Type responseType, Type examplesType)
        {
            ResponseType = responseType;
            ExamplesType = examplesType;
        }

        public Type ResponseType { get; set; }
        public Type ExamplesType { get; set; }
    }
}