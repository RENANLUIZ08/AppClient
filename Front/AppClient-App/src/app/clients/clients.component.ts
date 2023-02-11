import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss'],
})
export class ClientsComponent implements OnInit {
  public clients: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getClients();
  }

  public getClients(): void {
    this.http.get('https://localhost:7173/api/client/GetAll').subscribe(
      (response) => (this.clients = response),
      (error) => console.log(error)
    );
  }
}
