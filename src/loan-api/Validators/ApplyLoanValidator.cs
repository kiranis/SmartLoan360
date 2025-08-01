using Features.ApplyLoan;
using FluentValidation;

namespace Validators
{
    public class ApplyLoanValidator : AbstractValidator<ApplyLoanCommand>
    {
        public ApplyLoanValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.TermMonths).InclusiveBetween(6, 120);
        }
    }
}