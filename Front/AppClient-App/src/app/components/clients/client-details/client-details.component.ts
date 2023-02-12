import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/models/Client';
import { ClientService } from 'src/app/services/client.service';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrls: ['./client-details.component.scss'],
})
export class ClientDetailsComponent implements OnInit {
  form?: FormGroup;
  client = {} as Client;
  edit = false;

  get f(): any {
    return this.form?.controls;
  }

  constructor(
    private fb: FormBuilder,
    private activedRoute: ActivatedRoute,
    private clientService: ClientService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.loadClient();
    this.validation();
  }

  public loadClient(): void {
    const idClientParam = Number(this.activedRoute.snapshot.paramMap.get('id'));
    this.edit = idClientParam !== 0;

    if (this.edit) {
      this.clientService.getClientById(idClientParam).subscribe({
        next: (_client: Client) => {
          this.client = { ..._client };
          this.form?.patchValue(this.client);
        },
        error: () => {},
        complete: () => {},
      });
    }
  }

  saveClient(): void {
    if (this.form?.valid) {
      this.client = { ...this.form.value };
      this.spinner.show();

      const resultService = this.edit
        ? this.clientService.updateClient({
            id: this.client.id,
            ...this.form.value,
          })
        : this.clientService.createClient({ ...this.form.value });

      resultService.subscribe({
        next: () => {
          this.toastr.success('Cliente salvo com sucesso!', 'Sucesso');
          setTimeout(() => {
            this.router.navigate([`clients/details`]);
          }, 500);
          this.spinner.hide();
        },

        error: (error: any) => {
          console.log(error);
          this.toastr.error('Erro ao salvar o cliente !', 'Erro');
          this.spinner.hide();
        },
        complete: () => {
          setTimeout(() => {
            this.router.navigate([`clients/details`]);
          }, 500);
          this.spinner.hide();
        },
      });
    }
  }

  private validation(): void {
    this.form = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(20),
        ],
      ],
      address: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(60),
        ],
      ],
    });
  }

  resetForm(): void {
    this.form?.reset();
  }

  cssValidator(fieldForm: FormControl | AbstractControl): any {
    return { 'is-invalid': fieldForm.errors && fieldForm.touched };
  }
}
