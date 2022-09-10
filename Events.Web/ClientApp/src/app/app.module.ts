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
    CountryAbmComponent,
    CountrylistComponent,
    CityAbmComponent,
    StateAbmComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    AppMaterial,

  ],
  providers: [AppService, HttpClient,
                  { provide: MAT_DATE_LOCALE, useValue: 'en-ES' },
  ],
  bootstrap: [AppComponent],
  entryComponents: [CountrylistComponent]
  
})
export class AppModule { }
