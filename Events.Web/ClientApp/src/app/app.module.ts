import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppMaterial } from './app.material';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';

import { PeoplelistComponent } from './home/people/peoplelist/peoplelist.component';
import { PeopleComponent } from './home/people/people.component';
import { PeopleABMComponent } from './home/people/people-abm/people-abm.component';
import { AppService } from './server/app.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';




@NgModule({
  declarations: [
    AppComponent,
    PeoplelistComponent,
    PeopleComponent,
    PeopleABMComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AppMaterial,
    BrowserAnimationsModule
  ],
  providers: [AppService,HttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
