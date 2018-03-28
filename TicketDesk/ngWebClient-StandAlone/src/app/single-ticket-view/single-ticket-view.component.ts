import { Component, OnInit } from '@angular/core';
import { ActivityLogComponent } from '../activity-log/activity-log.component';
import { ContactInfoComponent } from '../contact-info/contact-info.component';
import { TicketActionBoxComponent } from '../ticket-action-box/ticket-action-box.component';
import { SingleTicketService } from '../services/single-ticket.service';
import { Ticket } from '../models/data';
import { Router, ActivatedRoute, Params } from '@angular/router';


@Component({
  selector: 'app-single-ticket-view',
  templateUrl: './single-ticket-view.component.html',
  styleUrls: ['./single-ticket-view.component.css']
})
export class SingleTicketViewComponent implements OnInit {

  ticket: Ticket = null;
  ticketId: number = null;
  ticketActionPermissions = 0;
  public isCollapsed = true;

  constructor(private singleTicketService: SingleTicketService,
    private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(params => {
      this.ticketId = Number(params['ticketID']);
    });
  }

  ngOnInit() {
    this.singleTicketService.getAvailableTicketActions(this.ticketId).subscribe(response => {
      console.warn("response", response['actionPermissions']);
      this.ticketActionPermissions = response['actionPermissions']
      console.warn("in the subscription function", this.ticketActionPermissions);
    });
    
    console.warn("just on init", this.ticketActionPermissions);
    this.ticket =
      this.singleTicketService.getTicketDetails(this.ticketId);
  }
}
