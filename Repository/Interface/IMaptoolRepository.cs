using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.ViewModel;

namespace TaoyuanBIMAPI.Repository.Interface
{
    public interface IMaptoolRepository
    {
        List<BookmarkViewModel> GetAllBookmark();
        ResponseViewModel AddBookmark(BookmarkParameter bookmarkParameter);
        ResponseViewModel DeleteBookmark(int bookmarkid);
        List<LayerViewModel> GetAllLayers();
        ResponseViewModel AddLayer(LayerParameter layerParameter);
        ResponseViewModel DeleteLayer(string layerid);
        ResponseViewModel AddLayerGroup(LayerGroupParameter layerGroupParameter);
        ResponseViewModel DeleteLayerGroup(string layerGroupid);

    }
}
