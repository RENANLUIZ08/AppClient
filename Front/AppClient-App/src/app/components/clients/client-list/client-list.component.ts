import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/models/Client';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.scss'],
})
export class ClientListComponent implements OnInit {
  modalRef?: BsModalRef;
  public clients: Client[] = [];
  private _filterList: string = '';
  idClient?: number;

  public get filterList(): string {
    return this._filterList;
  }

  public set filterList(value: string) {
    this._filterList = value;
    if (this._filterList) {
      this.clients = this.filterClients(this._filterList);
    } else {
      this.getClients();
    }
  }

  public filterClients(value: string): Client[] {
    value = value.toLocaleLowerCase();
    return this.clients.filter(
      (client: Client) => client.name.toLocaleLowerCase().indexOf(value) !== -1
    );
  }

  constructor(
    private clientService: ClientService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.spinner.show();
    this.getClients();
  }

  detailClient(id: number): void {
    this.router.navigate([`clients/details/${id}`]);
  }

  public getClients(): void {
    this.clientService.getClients().subscribe({
      next: (_clients: Client[]) => (this.clients = _clients),
      error: (error) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar os clientes', 'Erro');
      },
      complete: () => this.spinner.hide(),
    });
  }

  openModal(event: any, template: TemplateRef<any>, id: number) {
    event.stopPropagation();
    this.idClient = id;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.clientService.deleteClient(Number(this.idClient)).subscribe({
      next: () => {
        this.toastr.success(
          'Cliente foi excluido com exito!',
          'Excluir Cliente'
        );
        this.spinner.hide();
        this.getClients();
      },
      error: () => {
        this.toastr.error(
          `Ocorreu um erro durante a exclusão do cliente código ::${this.idClient}::`,
          'Erro ao Excluir cliente'
        );
        this.spinner.hide();
      },
      complete: () => {
        this.spinner.hide();
      },
    });
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
