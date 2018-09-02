using App.Queries.Itineraire;
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
    [RoutePrefix("api/v1/itineraire")]
    public class ItineraireController : ApiController
    {
        #region Fields
        private readonly IItineraireQueries _itineraireQueries;
        #endregion

        #region Constructors
        public ItineraireController(IItineraireQueries itineraireQueries)
        {
            _itineraireQueries = itineraireQueries;
        }
        #endregion

        [HttpGet]
        [Route("getallitineraire")]
        [ResponseType(typeof(ItineraireDto))]
        public async Task<HttpResponseMessage> GetAllItineraireAsync()
        {
            var itineraire = await _itineraireQueries.GetAllItineraire();
            if (itineraire == null) Request.CreateErrorResponse(HttpStatusCode.NotFound, "");
            return Request.CreateResponse(HttpStatusCode.OK, itineraire);
        }
    }
}
