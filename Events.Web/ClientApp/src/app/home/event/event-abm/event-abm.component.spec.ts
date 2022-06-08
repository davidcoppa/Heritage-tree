import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventAbmComponent } from './event-abm.component';

describe('EventAbmComponent', () => {
  let component: EventAbmComponent;
  let fixture: ComponentFixture<EventAbmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventAbmComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventAbmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
