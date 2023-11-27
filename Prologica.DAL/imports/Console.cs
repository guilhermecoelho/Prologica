using System;
using System.Collections.Generic;

namespace Prologica.DAL.imports;

public partial class Console
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
