using FluentValidation;
using UserMngt.Api.Resources;

namespace UserMngt.Api.Validations
{
    public class SaveUserResourceValidator : AbstractValidator<SaveUserResource>
    {
        public SaveUserResourceValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.Login)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(m => m.Pass)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}