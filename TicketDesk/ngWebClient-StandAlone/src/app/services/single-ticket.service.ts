import { Injectable } from '@angular/core';
import { Ticket, Logs } from '../models/data';
import { tickets, logs } from './ticket_db'
@Injectable()
export class SingleTicketService {

  constructor() {
   };
  getTicketDetails(ticketId: number):Ticket{
    let get_ticket:Ticket = null; // 

    for(let ticket of tickets){ // "search" database here to match ticketId
      if(ticket.ticketId == ticketId){
        get_ticket = ticket;
        break;
      }
    };
    
	 return get_ticket; 
  }

  getTicketFiles(ticketId: number){

  }

  getTicketLog(ticketId: number):Log.entries{
    let get_log:Log.entries = null;

    for(let log of logs){
      if(log.ticketId == ticketId){
        get_log = log.entries;
        break;
      }
    }

    return get_log;

  };

  changeTicketSubscription(ticketID: number){

  }

}
