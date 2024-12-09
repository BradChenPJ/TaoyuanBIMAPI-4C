using System;
using System.Collections.Generic;

namespace TaoyuanBIMAPI.Model;

public partial class LayersGroup
{
    public string GroupId { get; set; } = null!;

    public string? GroupName { get; set; }

    public int? GroupOrder { get; set; }

    public string? Dimention { get; set; }

    public virtual ICollection<Layer> Layers { get; set; } = new List<Layer>();
}
