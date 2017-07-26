using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Swashbuckle.Swagger;

namespace OfficeLocationMicroservice.WebUi.SwaggerExtensions
{
    [ExcludeFromCodeCoverage]
    internal class AddDefaultValuesOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                return;

            var actionParams = apiDescription.ActionDescriptor.GetParameters();
            var customAttributes =
                apiDescription.ActionDescriptor.GetCustomAttributes<SwaggerDefaultValueAttribute>();
            foreach (var param in operation.parameters)
            {
                var actionParam = actionParams.FirstOrDefault(p => p.ParameterName == param.name);
                if (actionParam != null)
                {
                    if (actionParam.DefaultValue != null)
                    {
                        param.@default = actionParam.DefaultValue;
                    }
                    else
                    {
                        var customAttribute =
                            customAttributes.FirstOrDefault(p => p.ParameterName == param.name);
                        if (customAttribute != null)
                        {
                            var provider = (IProvideExamples)Activator.CreateInstance(customAttribute.ExamplesType);
                            param.@default = FormatAsJson(provider);
                        }
                    }
                }
            }
        }

        private static object FormatAsJson(IProvideExamples provider)
        {
            return JsonConvert.SerializeObject(provider.GetExamples());            
        }        
    }
}