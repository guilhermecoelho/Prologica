using System;
using System.Collections.Generic;

namespace Prologica.DAL.imports;

public partial class Game
{
    public int Id { get; set; }

    public int ConsoleId { get; set; }

    public string Name { get; set; } = null!;

    public double? Price { get; set; }

    public Console? Console { get; set; }
}
