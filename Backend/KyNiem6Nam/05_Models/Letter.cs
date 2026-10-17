using System;
using System.Collections.Generic;

namespace KyNiem6Nam.Models;

public partial class Letter
{
    public int LetterId { get; set; }

    public int SenderId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool? IsLiked { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Sender { get; set; } = null!;
}
