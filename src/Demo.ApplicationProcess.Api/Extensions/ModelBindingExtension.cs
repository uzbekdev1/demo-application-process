using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo.ApplicationProcess.Api.Extensions
{
    public static class ModelBindingExtension
    {
        /// <summary>
        /// Model dictionary to message
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string ExtractErrorMessages(this ModelStateDictionary modelState)
        {

            var errors = modelState
                .Where(k => k.Value.Errors.Any())
                .Select(a => a.Value)
                .SelectMany(a => a.Errors)
                .Select(a => a.ErrorMessage);

            return string.Join("\r\n", errors);
        }

    }
}
