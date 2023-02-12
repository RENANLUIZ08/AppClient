import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Client } from '../models/Client';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  baseURL = 'https://localhost:7173/api/client';
  constructor(private http: HttpClient) {}

  public getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(`${this.baseURL}/GetAll`);
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
