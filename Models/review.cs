using System;
using System.Collections.Generic;

namespace KSVA2._0_WPF.Models;

public partial class review
{
    public int review_id { get; set; }

    public int student_id { get; set; }

    public int teacher_id { get; set; }

    public int order_id { get; set; }

    public sbyte rating { get; set; }

    public string? review_text { get; set; }

    public DateTime date { get; set; }

    public virtual order order { get; set; } = null!;

    public virtual user student { get; set; } = null!;

    public virtual teacher_profile teacher { get; set; } = null!;
}
