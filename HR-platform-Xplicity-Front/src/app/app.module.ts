import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { TablePageComponent } from './pages/table-page/table-page.component';
import { TableNewEntryPageComponent } from './pages/table-new-entry-page/table-new-entry-page.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TablePageComponent,
    TableNewEntryPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
