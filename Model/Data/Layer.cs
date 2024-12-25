using System;
using System.Collections.Generic;

namespace TaoyuanBIMAPI.Model.Data;

public partial class Layer
{
    public string LayerId { get; set; } = null!;

    public string? LayerName { get; set; }

    public string? LayerType { get; set; }

    public string? Url { get; set; }

    public string? UrlId { get; set; }

    public int? LayerOrder { get; set; }

    public string? GroupId { get; set; }

    public string? Note { get; set; }

    public virtual LayersGroup? Group { get; set; }
}
