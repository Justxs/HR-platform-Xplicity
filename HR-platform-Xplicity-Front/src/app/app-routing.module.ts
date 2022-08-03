import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
<<<<<<< Updated upstream
import { LoginComponent } from './pages/login/login.component'
import { TableNewEntryPageComponent } from './pages/table-new-entry-page/table-new-entry-page.component';
import { TablePageComponent } from './pages/table-page/table-page.component';
import { ResetPasswordPageComponent } from './pages/reset-password-page/reset-password-page.component';
=======
import { loginComponent } from '.pages/loginComponent';

>>>>>>> Stashed changes

const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'new-entry', component: TableNewEntryPageComponent},
  {path: 'table', component: TablePageComponent },
  {path: 'reset', component: ResetPasswordPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
