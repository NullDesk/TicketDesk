﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using TicketDesk.Domain;
using TicketDesk.Domain.Model;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Net;
using ngWebClientAPI.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ngWebClientAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/ticket")]
    public class TicketAPIController : ApiController
    {
        private TicketController ticketController = new TicketController(new TdDomainContext());

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        public async Task<JObject> getAllTickets()
        {
            try
            {
                var model = await ticketController.GetTicketList(); //returns list of all tickets
                List<FrontEndTicket> TicketList = new List<FrontEndTicket>();
                foreach(var item in model)
                {
                    TicketList.Add(APITicketConversion.ConvertGETTicket(item));
                }
                JList lst = new JList();
                lst.list = TicketList;
                return JObject.FromObject(lst);
            }
            catch(Exception ex)
            {
                return JObject.FromObject(ex.Message);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{ticketId}")]
        public async Task<JObject> getSingleTicket(Int64 ticketId)
        {
            int convertedId = APITicketConversion.ConvertTicketId(ticketId);//for when we get semantic numbering to front end
            Ticket model = await ticketController.getTicket(convertedId);
            if (model == null)
            {
                return null;
            }
            try
            {
                FrontEndTicket retVal = APITicketConversion.ConvertGETTicket(model);
                return JObject.FromObject(retVal);
            }
            catch
            {
                return null;
            }
            
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("")]
        public async Task<HttpStatusCodeResult> createTicket([FromBody]JObject jsonData)
        {
            HttpStatusCodeResult result; 
            //convert data to comment and ID
            try
            {
                Ticket ticket = APITicketConversion.ConvertPOSTTicket(jsonData);
                bool status = await ticketController.CreateTicketAsync(ticket);
               result = new HttpStatusCodeResult(HttpStatusCode.OK, ticket.TicketId.ToString());
                
            }
            catch (Exception ex)
            {
                result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return result;
        }
    }
}