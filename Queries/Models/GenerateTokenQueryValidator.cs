using FluentValidation;

namespace Mobiroller.Queries.Models
{
    public class GenerateTokenQueryValidator:AbstractValidator<GenerateTokenQuery>
    {
        public GenerateTokenQueryValidator()
        {
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(4, 50);

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(4, 50);
        }
    }
}