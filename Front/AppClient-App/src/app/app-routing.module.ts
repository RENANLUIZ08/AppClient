import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientDetailsComponent } from './components/clients/client-details/client-details.component';
import { ClientListComponent } from './components/clients/client-list/client-list.component';
import { ClientsComponent } from './components/clients/clients.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: 'clients',
    component: ClientsComponent,
    children: [
      { path: 'details/:id', component: ClientDetailsComponent },
      { path: 'create', component: ClientDetailsComponent },
      { path: 'list', component: ClientListComponent },
    ],
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
  },
  { path: '', redirectTo: 'clients/list', pathMatch: 'full' },
  { path: '**', redirectTo: 'clients/list', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
