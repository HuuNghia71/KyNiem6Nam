using System;
using System.Collections.Generic;

namespace KyNiem6Nam.Models;

public partial class Music
{
    public int MusicId { get; set; }

    public int AddedById { get; set; }

    public string SongTitle { get; set; } = null!;

    public string AudioUrl { get; set; } = null!;

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User AddedBy { get; set; } = null!;
}
