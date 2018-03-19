﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketDesk.Domain.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace ngWebClientAPI.Models
{
    public class APITicketConversion
    {
        public static Ticket ConvertPOSTTicket(JObject jsonData)
        {
            FrontEndTicket data;
            try
            {
                data = jsonData.ToObject<FrontEndTicket>();
            }
            catch
            {
                return null;
            }
            Ticket ticket = new Ticket(); //made new ticket object
            ticket.TicketId = default(int); //inserting to DB will assign backend ticketID
            ticket.ProjectId = data.projectId; //assuming front end will pass back project ID as int
            ticket.Details = data.comment; //assuming coming from comment field
            ticket.Priority = null; //we don't know priority yet
            ticket.TicketType = data.ticketType;
            ticket.Category = data.category;
            ticket.SubCategory = data.subcategory; //no subcategory thing in TD currently, might add?
            ticket.Owner = data.owner; //might have to use auth data to get owner/created by info
            ticket.AssignedTo = null; //probably will be null since we don't want users to assign their own tickets
            ticket.TicketStatus = TicketStatus.Active; //assuming ticket is open
            ticket.TagList = data.tagList;
            ticket.CreatedDate = DateTime.Now; //we get the datetime ourselves when new ticket
            ticket.Title = data.title;
            ticket.CreatedBy = data.owner; //might have to use the auth stuff
            ticket.IsHtml = false;
            ticket.CurrentStatusDate = ticket.CreatedDate;
            ticket.CurrentStatusSetBy = ticket.CreatedBy;
            ticket.LastUpdateBy = ticket.CreatedBy;
            ticket.LastUpdateDate = ticket.CreatedDate;
            ticket.AffectsCustomer = true;
            ticket.SemanticId = ticket.CreatedDate.ToString("yyMMddHHmm"); //formatting for semantic numbering
            return ticket;
        }
        public static JObject ConvertGETTicket(Ticket ticket)
        {
            FrontEndTicket FETicket = new FrontEndTicket();
            string ticketID = ticket.SemanticId + ticket.TicketId.ToString();
            FETicket.ticketId = int.Parse(ticketID); //gross conversion to string back to int to get around bit shifting
            FETicket.projectId = ticket.ProjectId;
            FETicket.comment = ticket.Details;
            FETicket.priority = ticket.Priority;
            FETicket.ticketType = ticket.TicketType;
            FETicket.category = ticket.Category;
            FETicket.subcategory = ticket.SubCategory; //no subcategory in TicketDesk db, might want to add?
            FETicket.owner = ticket.Owner;
            FETicket.assignedTo = ticket.AssignedTo;
            FETicket.status = ticket.TicketStatus;
            FETicket.tagList = ticket.TagList;
            FETicket.createdDate = ticket.CreatedDate.ToString();
            FETicket.title = ticket.Title;
            FETicket.subcategory = ticket.SubCategory;

            return JObject.FromObject(FETicket);
        }

        public static int ConverTicketId(int id)
        {
            string sId = id.ToString();
            //yymmddhhmm
            return int.Parse(sId.Substring(10, sId.Length-10));
        }
    }

    public class FrontEndTicket
    {
        public int ticketId { get; set; }
        public int projectId { get; set; }
        public string comment { get; set; }
        public string details { get; set; }
        public string priority { get; set; }
        public string ticketType { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string owner { get; set; }
        public string assignedTo { get; set; }
        public TicketStatus status { get; set; }
        public string tagList { get; set; }
        public string createdDate { get; set; }
        public string title { get; set; }
    }
}