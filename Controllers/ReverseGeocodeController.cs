using Microsoft.AspNetCore.Mvc;
using reverse_geocoding_api.ActionResults;
using reverse_geocoding_api.Models.ReverseGeocode;
using reverse_geocoding_api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace reverse_geocoding_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReverseGeocodeController : ControllerBase
    {
        private readonly IReverseGeocodeService _reverseGeocodeService;

        public ReverseGeocodeController(IReverseGeocodeService reverseGeocode)
        {
            _reverseGeocodeService = reverseGeocode;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(double latitude, double longitude)
        {
            var source = $"{Constants.General.SourcePrefixName}.api";
            try
            {
                ReverseGeocodeResult result = await _reverseGeocodeService
                     .GetAsync(latitude, longitude)
                     .ConfigureAwait(false);

                return WrapperResponse.SuccessResult(HttpStatusCode.OK, result, source);
            }
            catch (KeyNotFoundException)
            {
                return WrapperResponse.ErrorResult(
                    HttpStatusCode.NotFound,
                    "Could not reverse geocode coordinate",
                    source);
            }
            catch (Exception ex)
            {
                return WrapperResponse.ErrorResult(
                    HttpStatusCode.InternalServerError,
                    ex.Message,
                    source);
            }
        }
    }
}
