using Demo.ApplicationProcess.Domain.Models;
using FluentValidation;

namespace Demo.ApplicationProcess.Api.Validations
{
    /// <summary>
    /// News validation
    /// </summary>
    public class NewsValidation : AbstractValidator<NewsModel>
    {
        public NewsValidation()
        {

            RuleFor(x => x.Title).NotNull().MinimumLength(5).MaximumLength(200);
            RuleFor(x => x.Description).NotNull().MinimumLength(5).MaximumLength(1000);

        }

    }
}
