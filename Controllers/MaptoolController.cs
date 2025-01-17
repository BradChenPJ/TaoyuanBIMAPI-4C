using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaoyuanBIMAPI.Parameter;
using TaoyuanBIMAPI.Repository.Implement;
using TaoyuanBIMAPI.Repository.Interface;

namespace TaoyuanBIMAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaptoolController : ControllerBase
    {
        private IMaptoolRepository _maptoolRepository;

        public MaptoolController(IMaptoolRepository maptoolRepository)
        {
            _maptoolRepository = maptoolRepository;
        }

        #region 書籤
        [Authorize]
        [HttpGet]
        [Route("GetAllBookmark")]
        public ActionResult GetAllBookmark()
        {
            return Ok(_maptoolRepository.GetAllBookmark());
        }
        [Authorize]
        [HttpPost]
        [Route("AddBookmark")]
        public ActionResult AddBookmark([FromBody] BookmarkParameter bookmarkParameter)
        {
            return Ok(_maptoolRepository.AddBookmark(bookmarkParameter));
        }

        [HttpDelete]
        [Route("DeleteBookmark/{bookmarkid}")]
        public ActionResult DeleteBookmark([FromRoute] int bookmarkid)
        {
            return Ok(_maptoolRepository.DeleteBookmark(bookmarkid));
        }
        #endregion

        #region 圖層
        [Authorize]
        [HttpGet]
        [Route("GetAllLayers")]
        public ActionResult GetAllLayers()
        {
            var username = User.Identity.Name;
            return Ok(_maptoolRepository.GetAllLayers());
        }

        [HttpPost]
        [Route("AddLayer")]
        public ActionResult AddLayer([FromBody] LayerParameter layerParameter)
        {
            return Ok(_maptoolRepository.AddLayer(layerParameter));
        }

        [HttpDelete]
        [Route("DeleteLayer")]
        public ActionResult DeleteLayer(string layerid)
        {
            return Ok(_maptoolRepository.DeleteLayer(layerid));
        }

        [HttpPost]
        [Route("AddLayerGroup")]
        public ActionResult AddLayerGroup([FromBody] LayerGroupParameter layerGroupParameter)
        {
            return Ok(_maptoolRepository.AddLayerGroup(layerGroupParameter));
        }

        [HttpDelete]
        [Route("DeleteLayerGroup")]
        public ActionResult DeleteLayerGroup(string layerGroupid)
        {
            return Ok(_maptoolRepository.DeleteLayerGroup(layerGroupid));
        }
        #endregion
    }
}
