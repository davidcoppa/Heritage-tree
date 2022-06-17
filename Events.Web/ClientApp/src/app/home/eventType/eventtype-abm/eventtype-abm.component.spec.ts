import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventtypeAbmComponent } from './eventtype-abm.component';

describe('EventtypeAbmComponent', () => {
  let component: EventtypeAbmComponent;
  let fixture: ComponentFixture<EventtypeAbmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventtypeAbmComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventtypeAbmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
