using System;
using System.Collections.Generic;

namespace KSVA2._0_WPF.Models;

public partial class order
{
    public int order_id { get; set; }

    public int student_id { get; set; }

    public int teacher_id { get; set; }

    public string subject { get; set; } = null!;

    public DateTime scheduled_date { get; set; }

    public string status { get; set; } = null!;

    public virtual ICollection<review> reviews { get; set; } = new List<review>();

    public virtual user student { get; set; } = null!;

    public virtual teacher_profile teacher { get; set; } = null!;
}
