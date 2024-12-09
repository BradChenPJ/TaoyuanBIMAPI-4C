namespace TaoyuanBIMAPI.Parameter
{
    public class LayerParameter
    {
        public string LayerId { get; set; } = null!;

        public string? LayerName { get; set; }

        public string? LayerType { get; set; }

        public string? Url { get; set; }

        public string? UrlId { get; set; }

        public int? LayerOrder { get; set; }

        public string? GroupId { get; set; }

        public string? Note { get; set; }
    }
}
