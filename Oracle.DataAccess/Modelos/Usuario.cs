using System;
using System.Collections.Generic;

namespace Oracle.DataAccess.Modelos;

public partial class Usuario
{
    public decimal UserId { get; set; }

    public string Pnombre { get; set; } = null!;

    public string Snombre { get; set; } = null!;

    public string? Tnombre { get; set; }

    public string Papellido { get; set; } = null!;

    public string Sapellido { get; set; } = null!;
}
