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
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { EventlistComponent } from './home/event/eventlist/eventlist.component';
import { EventAbmComponent } from './home/event/event-abm/event-abm.component';
import { EventComponent } from './home/event/event.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EnumPipe } from './helpers/pipes/enum.pipe';
import { EventTypeComponent } from './home/eventType/eventtype.component';
import { EventtypeAbmComponent } from './home/eventType/eventtype-abm/eventtype-abm.component';
import { EventtypelistComponent } from './home/eventType/eventtypelist/eventtypelist.component';
import { MediaComponent } from './home/media/media.component';
import { MediaListComponent } from './home/media/media-list/media-list.component';
import { MediaAbmComponent } from './home/media/media-abm/media-abm.component';
import { LayoutComponent } from './home/layout/layout.component';
import { HeaderComponent } from './home/nav/header/header.component';
import { SideNavComponent } from './home/nav/side-nav/side-nav.component';
import { CustomDatePipe } from './helpers/pipes/customDate.pipe';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { FilterPeopleComponent } from './helpers/filters/people/filterPeople.component';
import { FilterEventTypeComponent } from './helpers/filters/eventType/filterEventType.component';
import { LocationComponent } from './home/location/location.component';
import { FilterCountriesComponent } from './helpers/filters/Location/Countries/filterCountries.component';
import { CountryAbmComponent } from './home/location/Country/Country-abm/country-abm.component';
import { CountrylistComponent } from './home/location/Country/CountryList/countrylist.component';
import { CityAbmComponent } from './home/location/Country/City-abm/city-abm.component';
import { StateAbmComponent } from './home/location/Country/State-abm/state-abm.component';
import { LocationAbmComponent } from './home/location/Country/location-abm/location-abm.component';
import { FilterCityComponent } from './helpers/filters/Location/Cities/filterCity.component';
import { FilterStatesComponent } from './helpers/filters/Location/States/filterStates.component';
import { SunburstComponent } from './helpers/visualization/sunburst/sunburst.component';
import { AppMediaService } from './server/app.media.service';
import { AppFileService } from './server/app.file.service';
import { FileUploadComponent } from './helpers/media/upload/FileUpload.component';
import { AuthInterceptor, ErrorInterceptor } from './helpers/interceptors';
import { environment } from '../environments/environment';
import { ServiceWorkerModule } from "@angular/service-worker";
import { ToastrModule } from 'ngx-toastr';
import { FilterEventComponent } from './helpers/filters/event/filterEvent.component';



@NgModule({
  declarations: [
    AppComponent,
    PeoplelistComponent,
    PeopleComponent,
    PeopleABMComponent,
    EventComponent,
    EventlistComponent,
    EventAbmComponent,
    EnumPipe,
    CustomDatePipe,
    EventTypeComponent,
    EventtypeAbmComponent,
    EventtypelistComponent,
    MediaComponent,
    MediaListComponent,
    MediaAbmComponent,
    LayoutComponent,
    HeaderComponent,
    SideNavComponent,
    FilterPeopleComponent,
    FilterEventTypeComponent,
    LocationComponent,
    FilterCountriesComponent,
    FilterStatesComponent,
    FilterCityComponent,
    CountryAbmComponent,
    CountrylistComponent,
    CityAbmComponent,
    StateAbmComponent,
    LocationAbmComponent,
    SunburstComponent,
    FilterEventComponent,
    //media
    FileUploadComponent
  ],
  imports: [
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
      registrationStrategy: 'registerImmediately',
    }),
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    AppMaterial,
    ToastrModule.forRoot({
      timeOut: 2000,
      positionClass: 'toast-top-right'
    }),
  ],
  providers: [AppService, AppMediaService, AppFileService, HttpClient,
    { provide: MAT_DATE_LOCALE, useValue: 'en-ES' },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
