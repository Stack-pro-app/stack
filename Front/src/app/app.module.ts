import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';


import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

import {FormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";


import {HttpClient, HttpClientModule} from "@angular/common/http";




@NgModule({
  declarations: [

  ],
  imports: [
    BrowserModule,

    BrowserAnimationsModule,

    FormsModule, MatFormFieldModule, MatInputModule,

    HttpClientModule,


  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync(),
    HttpClient

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
