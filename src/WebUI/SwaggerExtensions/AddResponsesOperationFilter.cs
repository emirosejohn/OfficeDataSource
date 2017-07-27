using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Swashbuckle.Swagger;

namespace OfficeLocationMicroservice.WebUi.SwaggerExtensions
{
    public interface IProvideExamples
    {
        object GetExamples();
    }

    [ExcludeFromCodeCoverage]
    internal class AddResponsesOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var responseAttributes =
                apiDescription.GetControllerAndActionAttributes<SwaggerResponseExamplesAttribute>();

            foreach (var attr in responseAttributes)
            {
                var schema = schemaRegistry.GetOrRegister(attr.ResponseType);

                var response =
                    operation.responses.FirstOrDefault(
                        x => x.Value.schema.type == schema.type && x.Value.schema.@ref == schema.@ref).Value;

                if (response != null)
                {
                    var provider = (IProvideExamples)Activator.CreateInstance(attr.ExamplesType);
                    response.examples = FormatAsJson(provider);
                }
            }
        }

        private static object FormatAsJson(IProvideExamples provider)
        {
            var examples = new Dictionary<string, object>
            {
                {
                    "application/json", provider.GetExamples()
                }
            };

            return ConvertToJson(examples);
        }

        private static object ConvertToJson(Dictionary<string, object> examples)
        {
            var jsonString = JsonConvert.SerializeObject(examples);
            return JsonConvert.DeserializeObject(jsonString);
        }
    }
}