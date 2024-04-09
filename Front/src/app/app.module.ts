import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';


import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

import {FormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";


import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ButtonModule} from "primeng/button";
import {MatNativeDateModule, provideNativeDateAdapter} from "@angular/material/core";
import {MatDatepickerModule} from "@angular/material/datepicker";




@NgModule({
  declarations: [

  ],
  imports: [
    BrowserModule,

    BrowserAnimationsModule,

    FormsModule, MatFormFieldModule, MatInputModule,

    HttpClientModule,
    ButtonModule,



  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync(),
    HttpClient,


  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
