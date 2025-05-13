using System;
using System.Collections.Generic;

namespace KSVA2._0_WPF.Models;

public partial class user
{
    public int user_id { get; set; }

    public string name { get; set; } = null!;

    public string password { get; set; } = null!;

    public string phone { get; set; } = null!;

    public DateOnly date_of_birth { get; set; }

    public string role { get; set; } = null!;

    public virtual ICollection<order> orders { get; set; } = new List<order>();

    public virtual ICollection<review> reviews { get; set; } = new List<review>();

    public virtual teacher_profile? teacher_profile { get; set; }
}
