using System;
using System.Collections.Generic;

namespace KyNiem6Nam.Models;

public partial class SavedPlace
{
    public int PlaceId { get; set; }

    public int AddedById { get; set; }

    public string PlaceName { get; set; } = null!;

    public string? Description { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User AddedBy { get; set; } = null!;
}
