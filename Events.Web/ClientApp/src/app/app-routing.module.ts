import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { EventComponent } from './home/event/event.component';
import { EventTypeComponent } from './home/eventType/eventtype.component';
import { MediaComponent } from './home/media/media.component';
import { PeopleComponent } from './home/people/people.component';

export const routes: Routes = [
  { path: 'home', component: AppComponent },
  { path: 'people', component: PeopleComponent},
  { path: 'event', component: EventComponent},
  { path: 'eventypes', component: EventTypeComponent},
  { path: 'media', component: MediaComponent},
 // { path: 'location', component: LocationComponent},
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
