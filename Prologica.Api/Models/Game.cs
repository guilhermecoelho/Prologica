using FluentValidation;

namespace Prologica.Api.Models
{
    public class Game
    {
        public int Id { get; set; }

        public int ConsoleId { get; set; }

        public string Name { get; set; } = null!;

        public double? Price { get; set; }
    }

    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.ConsoleId).NotNull().NotEmpty();
        }
    }
}
