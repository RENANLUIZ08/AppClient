import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Client } from '../models/Client';
import { PaginationResult } from '../models/Pagination';
import { take, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  baseURL = `${environment.apiURL}api/client`;
  constructor(private http: HttpClient) {}

  public getClients(
    page?: Number,
    itemsPPage?: Number,
    name?: string
  ): Observable<PaginationResult<Client[]>> {
    const paginationResult: PaginationResult<Client[]> = new PaginationResult<
      Client[]
    >();

    let params = new HttpParams();
    if (page != null && itemsPPage != null) {
      params = params.append('pageNumber', page?.toString());
      params = params.append('pageSize', itemsPPage?.toString());
    }

    if (name != null && name != '') {
      params = params.append('name', name?.toString());
    }

    return this.http
      .get<Client[]>(`${this.baseURL}/GetAll`, { observe: 'response', params })
      .pipe(
        take(1),
        map((response: any) => {
          paginationResult.result = response.body;
          if (response.headers.has('Pagination')) {
            paginationResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginationResult;
        })
      );
  }

  public getClientById(id: number): Observable<Client> {
    return this.http.get<Client>(`${this.baseURL}/Get?id=${id}`);
  }

  public createClient(client: Client): Observable<Client> {
    return this.http.post<Client>(`${this.baseURL}/create`, client);
  }

  public updateClient(client: Client): Observable<Client> {
    return this.http.put<Client>(
      `${this.baseURL}/update?id=${client.id}`,
      client
    );
  }

  public deleteClient(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/delete?id=${id}`);
  }
}
