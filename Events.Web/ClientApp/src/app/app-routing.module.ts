import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { EventComponent } from './home/event/event.component';
import { EventTypeComponent } from './home/eventType/eventtype.component';
import { LocationComponent } from './home/location/location.component';
import { MediaComponent } from './home/media/media.component';
import { PeopleABMComponent } from './home/people/people-abm/people-abm.component';
import { PeopleComponent } from './home/people/people.component';

export const routes: Routes = [
  { path: 'home', redirectTo: '', pathMatch: 'full' },
  { path: 'people', component: PeopleComponent},
  { path: 'people/action', component: PeopleABMComponent},
  { path: 'event', component: EventComponent},
  { path: 'eventypes', component: EventTypeComponent},
  { path: 'media', component: MediaComponent},
  { path: 'location', component: LocationComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
