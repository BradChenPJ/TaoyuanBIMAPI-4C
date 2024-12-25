using AutoMapper;
using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.ViewModel;
using TaoyuanBIMAPI.Model.Data;

namespace TaoyuanBIMAPI.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile() 
        {
            this.CreateMap<BookmarkParameter, Bookmark>();
            this.CreateMap<Bookmark, BookmarkViewModel>();
            this.CreateMap<LayerParameter, Layer>();
            this.CreateMap<LayerGroupParameter, LayersGroup>();
        }
    }
}
