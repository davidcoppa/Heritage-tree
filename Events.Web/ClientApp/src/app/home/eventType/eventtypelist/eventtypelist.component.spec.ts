import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventtypelistComponent } from './eventtypelist.component';

describe('EventtypelistComponent', () => {
  let component: EventtypelistComponent;
  let fixture: ComponentFixture<EventtypelistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventtypelistComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventtypelistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
