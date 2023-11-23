using FluentValidation;

namespace Prologica.Api.Models
{
    public class Console
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public class ConsoleValidator : AbstractValidator<Console>
    {
        public ConsoleValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
