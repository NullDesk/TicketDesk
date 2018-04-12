import { Injectable } from '@angular/core';
import { ListTicket } from '../models/list-ticket';
import { HttpClient, HttpParams, HttpHeaders, HttpRequest, HttpResponse } from '@angular/common/http';
import * as settings from '../app-settings';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class SearchService {

  constructor(
    private http: HttpClient
  ) {}

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };

  // FAKE RETURNS index list right now. NOT SEARCHING!
  search(
    term: string,
  ): Observable<ListTicket[]> {
    // const params = new HttpParams().set('listName', listName).set('page', String(page));
    const params = {page: 1, listName: term};
    const ticketList = this.http.post<ListTicket[]>( settings.getTicketsIndex, params, this.httpOptions);
    return ticketList.map(res => {
      console.warn('TicketList Responese', JSON.stringify(res));
      return res;
    });
  }
}
