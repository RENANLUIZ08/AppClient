import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Client } from '../../models/Client';
import { ClientService } from '../../services/client.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss'],
})
export class ClientsComponent implements OnInit {
  modalRef?: BsModalRef;
  public clients: Client[] = [];
  private _filterList: string = '';

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
    private spinner: NgxSpinnerService
  ) {}

  public ngOnInit(): void {
    this.spinner.show();
    this.getClients();
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

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.toastr.success('Excluir cliente', 'Cliente foi excluido com exito!');
    this.modalRef?.hide();
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
