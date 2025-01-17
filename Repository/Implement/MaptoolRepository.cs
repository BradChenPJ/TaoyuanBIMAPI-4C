using AutoMapper;
using Microsoft.OpenApi.Validations.Rules;
using TaoyuanBIMAPI.Model.Data;
using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.Repository.Interface;
using TaoyuanBIMAPI.ViewModel;

namespace TaoyuanBIMAPI.Repository.Implement
{
    public class MaptoolRepository : IMaptoolRepository
    {
        private readonly TaoyuanBimContext _taoyuanBimContext;
        private readonly IMapper _mapper;
        private readonly ResponseViewModel _responseViewModel;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MaptoolRepository(TaoyuanBimContext taoyuanBimContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _taoyuanBimContext = taoyuanBimContext;
            _mapper = mapper;
            _responseViewModel = new ResponseViewModel();
            _httpContextAccessor = httpContextAccessor;
        }

        #region 書籤
        public List<BookmarkViewModel> GetAllBookmark()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string userId = httpContext.User.Identity.Name;
            List<Bookmark> bookmarkList = _taoyuanBimContext.Bookmarks.Where(x => x.UserId == userId).OrderBy(x => x.CreateTime).ToList();
            List<BookmarkViewModel> allBookmarkList = _mapper.Map<List<Bookmark>, List<BookmarkViewModel>>(bookmarkList);
            return allBookmarkList;
        }
        public ResponseViewModel AddBookmark(BookmarkParameter bookmarkParameter)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string userId = httpContext.User.Identity.Name;
            bookmarkParameter.UserId = userId;
            Bookmark bookmarkData = _mapper.Map<BookmarkParameter, Bookmark>(bookmarkParameter);
            try
            {
                _taoyuanBimContext.Bookmarks.Add(bookmarkData);
                _taoyuanBimContext.SaveChanges();
                _responseViewModel.Status = true;
                _responseViewModel.Message = $"書籤:{bookmarkData.BookmarkName} 新增成功";
            }
            catch (Exception ex)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = $"書籤:{bookmarkData.BookmarkName} 新增失敗，原因: {ex.Message}";
            }
            return _responseViewModel;
        }
        public ResponseViewModel DeleteBookmark(int bookmarkid)
        {
            //找到這個id的資料
            var deleteBookmarkData = _taoyuanBimContext.Bookmarks.Find(bookmarkid);
            try
            {
                if (deleteBookmarkData != null)
                {
                    //刪除
                    _taoyuanBimContext.Bookmarks.Remove(deleteBookmarkData);
                    _taoyuanBimContext.SaveChanges();
                    _responseViewModel.Status = true;
                    _responseViewModel.Message = $"書籤:{deleteBookmarkData.BookmarkName} 刪除成功";
                }
                else
                {
                    _responseViewModel.Status = false;
                    _responseViewModel.Message = $"書籤:ID {bookmarkid.ToString()} 刪除失敗，原因: 無此書籤";
                }
            }
            catch (Exception ex)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = $"書籤:ID {bookmarkid.ToString()} 刪除失敗，原因: {ex.Message}";
            }
            return _responseViewModel;
            
        }
        #endregion

        #region 圖層
        public List<LayerViewModel> GetAllLayers()
        {
            List<LayerViewModel> allLayers = _taoyuanBimContext.LayersGroups.Select(group => new LayerViewModel
            {
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                GroupOrder = group.GroupOrder,
                Dimention = group.Dimention,
                Layers = _taoyuanBimContext.Layers.Where(x =>x.GroupId == group.GroupId).OrderBy(layer=>layer.LayerOrder).ToList()
            }).OrderBy(group => group.GroupOrder).ToList();

            return allLayers;
        }
        public ResponseViewModel AddLayer(LayerParameter layerParameter)
        {
            Layer layerData = _mapper.Map<LayerParameter, Layer>(layerParameter);
            try
            {
                //判斷LayerID有沒有重複
                bool exist = _taoyuanBimContext.Layers.Any(x => x.LayerId == layerParameter.LayerId);
                if (exist)
                {
                    _responseViewModel.Status = false;
                    _responseViewModel.Message = $"圖層ID重複";
                }
                else 
                {
                    _taoyuanBimContext.Layers.Add(layerData);
                    _taoyuanBimContext.SaveChanges();
                    _responseViewModel.Status = true;
                    _responseViewModel.Message = $"圖層:{layerData.LayerName} 新增成功";
                }
                
            }
            catch (Exception ex)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = $"圖層:{layerData.LayerName} 新增失敗，原因: {ex.Message}";
            }
            return _responseViewModel;

        }
        public ResponseViewModel DeleteLayer(string layerid)
        {
            //找到這個id的資料
            var deleteLayerData = _taoyuanBimContext.Layers.Find(layerid);
            try
            {
                if (deleteLayerData != null)
                {
                    //刪除
                    _taoyuanBimContext.Layers.Remove(deleteLayerData);
                    _taoyuanBimContext.SaveChanges();
                    _responseViewModel.Status = true;
                    _responseViewModel.Message = $"圖層:{deleteLayerData.LayerName} 刪除成功";
                }
                else
                {
                    _responseViewModel.Status = false;
                    _responseViewModel.Message = $"圖層:ID {layerid} 刪除失敗，原因: 無此圖層";
                }
            }
            catch (Exception ex)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = $"圖層:ID {layerid} 刪除失敗，原因: {ex.Message}";
            }
            return _responseViewModel;
        }
        public ResponseViewModel AddLayerGroup(LayerGroupParameter layerGroupParameter)
        {
            LayersGroup layerGroupData = _mapper.Map<LayerGroupParameter, LayersGroup>(layerGroupParameter);
            try
            {
                //判斷GroupID有沒有重複
                bool exist = _taoyuanBimContext.LayersGroups.Any(x => x.GroupId == layerGroupParameter.GroupId);
                if (exist)
                {
                    _responseViewModel.Status = false;
                    _responseViewModel.Message = $"圖層群組ID重複";
                }
                else
                {
                    _taoyuanBimContext.LayersGroups.Add(layerGroupData);
                    _taoyuanBimContext.SaveChanges();
                    _responseViewModel.Status = true;
                    _responseViewModel.Message = $"圖層群組:{layerGroupData.GroupName} 新增成功";
                }

            }
            catch (Exception ex)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = $"圖層群組:{layerGroupData.GroupName} 新增失敗，原因: {ex.Message}";
            }
            return _responseViewModel;

        }
        public ResponseViewModel DeleteLayerGroup(string layerGroupid)
        {
            //找到這個id的資料
            var deleteLayerGroupData = _taoyuanBimContext.LayersGroups.Find(layerGroupid);
            try
            {
                if (deleteLayerGroupData != null)
                {
                    //刪除
                    _taoyuanBimContext.LayersGroups.Remove(deleteLayerGroupData);
                    _taoyuanBimContext.SaveChanges();
                    _responseViewModel.Status = true;
                    _responseViewModel.Message = $"圖層群組:{deleteLayerGroupData.GroupName} 與圖層刪除成功";
                }
                else
                {
                    _responseViewModel.Status = false;
                    _responseViewModel.Message = $"圖層群組:ID {layerGroupid} 刪除失敗，原因: 無此圖層";
                }
            }
            catch (Exception ex)
            {
                _responseViewModel.Status = false;
                _responseViewModel.Message = $"圖層群組:ID {layerGroupid} 刪除失敗，原因: {ex.Message}";
            }
            return _responseViewModel;
        }
        #endregion
    }
}
