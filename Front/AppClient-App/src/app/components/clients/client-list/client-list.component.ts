import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { Client } from 'src/app/models/Client';
import { Pagination, PaginationResult } from 'src/app/models/Pagination';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.scss'],
})
export class ClientListComponent implements OnInit {
  modalRef?: BsModalRef;
  public clients: Client[] = [];
  public idClient?: number;
  public pagination = {} as Pagination;

  nameClientChange: Subject<string> = new Subject<string>();

  constructor(
    private clientService: ClientService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPPage: 3,
      totalItems: 1,
    } as Pagination;
    this.getClients();
  }

  detailClient(id: number): void {
    this.router.navigate([`clients/details/${id}`]);
  }

  public filterClients(evt: any): void {
    if (!this.nameClientChange.observers.length) {
      this.nameClientChange.pipe(debounceTime(800)).subscribe((filterDigit) => {
        this.clientService
          .getClients(
            this.pagination.currentPage,
            this.pagination.itemsPPage,
            filterDigit
          )
          .subscribe({
            next: (_resultPagination: PaginationResult<Client[]>) => {
              this.clients = _resultPagination.result;
              this.pagination = _resultPagination.pagination;
            },
            error: (error) => {
              this.spinner.hide();
              this.toastr.error('Erro ao carregar os clientes', 'Erro');
            },
            complete: () => this.spinner.hide(),
          });
      });
    } else {
      this.nameClientChange.next(evt.value);
    }
  }

  public getClients(): void {
    this.spinner.show();
    this.clientService
      .getClients(this.pagination?.currentPage, this.pagination.itemsPPage)
      .subscribe({
        next: (_resultPagination: PaginationResult<Client[]>) => {
          this.clients = _resultPagination.result;
          this.pagination = _resultPagination.pagination;
        },
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

  pageChanged($event: any): void {
    this.pagination.currentPage = $event.page;
    this.getClients();
  }
}
