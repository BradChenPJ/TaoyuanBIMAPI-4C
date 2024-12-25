using TaoyuanBIMAPI.Model.Data;

namespace TaoyuanBIMAPI.ViewModel
{
    public class LayerViewModel
    {
        public string GroupId { get; set; } = null!;

        public string? GroupName { get; set; }

        public int? GroupOrder { get; set; }

        public string? Dimention { get; set; }
        public List<Layer>? Layers { get; set; }

    }
}
