import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login.component'
import { TableNewEntryPageComponent } from './pages/table-new-entry-page/table-new-entry-page.component';
import TablePageComponent  from './pages/table-page/table-page.component';
import { ResetPasswordPageComponent } from './pages/reset-password-page/reset-password-page.component';
import { NewTechnologyComponent } from './pages/new-technology/new-technology.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { CalendarComponent } from './pages/calendar/calendar.component';

const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'new-entry', component: TableNewEntryPageComponent },
  {path: 'table', component: TablePageComponent },
  {path: 'reset', component: ResetPasswordPageComponent },
  {path: 'new-technology', component: NewTechnologyComponent },
  {path: 'register', component: RegisterPageComponent },
  {path: 'calendar', component: CalendarComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
