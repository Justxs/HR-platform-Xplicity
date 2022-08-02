import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component'
import { TableNewEntryPageComponent } from './pages/table-new-entry-page/table-new-entry-page.component';
import { TablePageComponent } from './pages/table-page/table-page.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'new-entry', component: TableNewEntryPageComponent},
  {path: 'table', component: TablePageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
