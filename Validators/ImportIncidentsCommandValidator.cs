using FluentValidation;
using Microsoft.AspNetCore.Http;
using Mobiroller.Commands.Models;

namespace Mobiroller.Validators
{
    public class ImportIncidentsCommandValidator : AbstractValidator<ImportIncidentsCommand>
    {
        public ImportIncidentsCommandValidator()
        {
            RuleFor(x => x.File)
                .NotNull()
                .SetValidator(new FormFileValidator());
        }
    }

    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        public FormFileValidator()
        {
            RuleFor(x => x.ContentType)
                .Must(x => x.Equals("application/json"))
                .WithMessage("Only json files can be imported.");
        }
    }
}