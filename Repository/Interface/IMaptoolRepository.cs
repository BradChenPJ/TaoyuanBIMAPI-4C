using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.ViewModel;

namespace TaoyuanBIMAPI.Repository.Interface
{
    public interface IMaptoolRepository
    {
        List<BookmarkViewModel> GetAllBookmark(string userid);
        ResponseViewModel AddBookmark(BookmarkParameter bookmarkParameter);
        ResponseViewModel DeleteBookmark(int bookmarkid);
        List<LayerViewModel> GetAllLayers();
        ResponseViewModel AddLayer(LayerParameter layerParameter);
        ResponseViewModel DeleteLayer(string layerid);

    }
}
