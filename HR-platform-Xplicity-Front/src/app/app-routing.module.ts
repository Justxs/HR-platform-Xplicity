import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login.component'
import { TableNewEntryPageComponent } from './pages/table-new-entry-page/table-new-entry-page.component';
import TablePageComponent  from './pages/table-page/table-page.component';
import { ResetPasswordPageComponent } from './pages/reset-password-page/reset-password-page.component';
import { NewTechnologyComponent } from './pages/new-technology/new-technology.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { CalendarComponent } from './pages/calendar/calendar.component';
import { AuthGuard } from './services/auth.guard';


const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'new-entry', component: TableNewEntryPageComponent, canActivate: [AuthGuard]},
  {path: 'table', component: TablePageComponent, canActivate: [AuthGuard]},
  {path: 'reset', component: ResetPasswordPageComponent, canActivate: [AuthGuard] },
  {path: 'new-technology', component: NewTechnologyComponent, canActivate: [AuthGuard] },
  {path: 'register', component: RegisterPageComponent, canActivate: [AuthGuard] },
  {path: 'calendar', component: CalendarComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
