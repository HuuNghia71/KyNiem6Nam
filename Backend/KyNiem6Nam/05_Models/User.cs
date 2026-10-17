using System;
using System.Collections.Generic;

namespace KyNiem6Nam.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Passcode { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public double? CurrentLat { get; set; }

    public double? CurrentLng { get; set; }

    public int? Pin { get; set; }

    public string? ThoiTiet { get; set; }

    public int? NhietDo { get; set; }

    public DateTime? LastLocationUpdate { get; set; }

    public virtual ICollection<Letter> Letters { get; set; } = new List<Letter>();

    public virtual ICollection<Music> Musics { get; set; } = new List<Music>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<SavedPlace> SavedPlaces { get; set; } = new List<SavedPlace>();
}
