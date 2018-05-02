﻿using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using TicketDesk.Domain;
using TicketDesk.Domain.Model;
using TicketDesk.Search.Common;
using Newtonsoft.Json.Linq;
using ngWebClientAPI.Models;

namespace ngWebClientAPI.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/search")]
    public class SearchAPIController : ApiController
    {
        private TdDomainContext Context { get; set; }
        public SearchAPIController()
        {
            Context = new TdDomainContext();
        }

        [Route("")]
        [HttpPost]
        public async Task<List<ngWebClientAPI.Models.TicketCenterDTO>> Index(JObject data)
        {
            string term = data["term"].ToObject<string>();
            List<TicketCenterDTO> tkDTO = new List<TicketCenterDTO>();

            if (!string.IsNullOrEmpty(term))
            {
                var model = await TdSearchContext.Current.SearchAsync(Context.Tickets.Include(t => t.Project), term, 1);
                tkDTO = TicketCenterDTO.ticketsToTicketCenterDTO(model.ToList());
            }
            return tkDTO;
        }
    }
}
