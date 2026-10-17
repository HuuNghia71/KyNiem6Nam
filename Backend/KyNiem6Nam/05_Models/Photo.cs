using System;
using System.Collections.Generic;

namespace KyNiem6Nam.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public int UploaderId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string? Caption { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Uploader { get; set; } = null!;
}
