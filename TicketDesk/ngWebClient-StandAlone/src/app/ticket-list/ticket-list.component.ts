import { Component, OnInit, Input, Output,
         EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { TicketStub, columnHeadings } from '../models/ticket-stub';
import { FormsModule } from '@angular/forms';
import { getTicketStatusText } from '../models/ticket';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.css']
})

export class TicketListComponent implements OnInit, OnChanges {
  // imported into the class, so can be used in HTML
  private colHeadings = columnHeadings;
  private getStatusText = getTicketStatusText;
  // Adds a vairable to add keep track of checkbox
  private displayList: {ticket: TicketStub, checked: boolean}[];
  @Input() ticketList:  TicketStub[];
  @Input() pagination: {current: number, max: number } = null;
  @Output() pageChange = new EventEmitter<number>();
  @Output() sortTrigger = new EventEmitter<string>();

  ngOnInit() {
    this.makeDisplayList(this.ticketList);
  }

  makeDisplayList(ticketList: TicketStub[]) {
    // filter removes objects not of type ticket or null/undefined
    this.displayList = this.ticketList
          .filter( x => x)
          .map(ticket => ({ticket: ticket, checked: false}));
  }

  isAllChecked() {
    return this.displayList.every(x => x.checked);
  }

  selectAll(ev) {
    this.displayList.forEach(x => {x.checked = ev.target.checked; });
  }

  getSelected() {
    return this.displayList.filter( x => x.checked);
  }

  onPageChange(page: number) {
    this.pageChange.emit(page);
  }

  ngOnChanges(changes: SimpleChanges) {
    for (const propName of Object.keys(changes)) {
      const change = changes[propName];
      console.log('ngChange triggered new value: ', JSON.stringify(change.currentValue));
      if (propName === 'ticketList') {
        this.makeDisplayList(change.currentValue);
      }
      if (propName === 'pagination') {
        this.pagination = change.currentValue;
      }
    }
  }

  headerSort(header: string) {
    const colName = header.replace(/\s/g, '');
    console.log(colName, 'has been clicked');
    this.sortTrigger.emit(colName);
  }
}
