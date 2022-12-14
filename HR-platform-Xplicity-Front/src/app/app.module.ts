import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; 

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import TablePageComponent from './pages/table-page/table-page.component';
import { TableNewEntryPageComponent } from './pages/table-new-entry-page/table-new-entry-page.component';
import { ButtonModule } from "primeng/button";
import { CalendarModule } from 'primeng/calendar';
import { InputTextModule } from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MultiSelectModule } from 'primeng/multiselect';
import { ResetPasswordPageComponent } from './pages/reset-password-page/reset-password-page.component';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { FileUploadModule } from 'primeng/fileupload';
import { NewTechnologyComponent } from './pages/new-technology/new-technology.component';
import {DialogModule} from 'primeng/dialog';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import {MenubarModule} from 'primeng/menubar';
import { ToastModule } from 'primeng/toast';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import { TokenInterceptorService } from './services/token-interceptor.service';

export function tokenGetter() {
  return localStorage.getItem("Jwt")
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TablePageComponent,
    TableNewEntryPageComponent,
    ResetPasswordPageComponent,
    NewTechnologyComponent,
    RegisterPageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ButtonModule,
    InputTextModule,
    InputMaskModule,
    CalendarModule,
    BrowserAnimationsModule,
    CheckboxModule,
    InputTextareaModule,
    MultiSelectModule,
    TableModule,
    HttpClientModule,
    ToolbarModule,
    FileUploadModule,
    DialogModule,
    MenubarModule,
    ToastModule,
    ProgressSpinnerModule,

  ],
  providers: [
      {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
      }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
