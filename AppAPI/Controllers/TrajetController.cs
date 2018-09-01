using App.Queries.Trajet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppAPI.Controllers
{
    [RoutePrefix("api/v1/trajet")]
    public class TrajetController : ApiController
    {
        #region Fields
        private readonly ITrajetQueries _trajetQueries;
        #endregion

        #region Constructors
        public TrajetController(ITrajetQueries trajetQueries)
        {
            _trajetQueries = trajetQueries;
        }
        #endregion

        [HttpGet]
        [Route("getalltrajet")]
        [ResponseType(typeof(TrajetDto))]
        public async Task<HttpResponseMessage> GetAllTrajetAsync()
        {
            var trajet = await _trajetQueries.GetAllTrajet();
            if (trajet == null) Request.CreateErrorResponse(HttpStatusCode.NotFound, "");
            return Request.CreateResponse(HttpStatusCode.OK, trajet);
        }
    }
}
