using System;
using System.Diagnostics.CodeAnalysis;

namespace OfficeLocationMicroservice.WebUi.SwaggerExtensions
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Method)]
    internal class SwaggerDefaultValueAttribute : Attribute
    {
        public SwaggerDefaultValueAttribute(string parameterName, Type examplesType)
        {
            ParameterName = parameterName;
            ExamplesType = examplesType;
        }

        public string ParameterName { get; private set; }

        public Type ExamplesType { get; set; }
    }
}