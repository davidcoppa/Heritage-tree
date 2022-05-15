import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PeopleABMComponent } from './people-abm.component';

describe('PeopleABMComponent', () => {
  let component: PeopleABMComponent;
  let fixture: ComponentFixture<PeopleABMComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PeopleABMComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PeopleABMComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
