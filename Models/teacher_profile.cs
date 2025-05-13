using System;
using System.Collections.Generic;

namespace KSVA2._0_WPF.Models;

public partial class teacher_profile
{
    public int teacher_id { get; set; }

    public string expertise { get; set; } = null!;

    public string subject { get; set; } = null!;

    public decimal price { get; set; }

    public virtual ICollection<order> orders { get; set; } = new List<order>();

    public virtual ICollection<review> reviews { get; set; } = new List<review>();

    public virtual user teacher { get; set; } = null!;
}
