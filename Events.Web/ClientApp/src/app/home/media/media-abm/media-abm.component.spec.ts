import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaAbmComponent } from './media-abm.component';

describe('MediaAbmComponent', () => {
  let component: MediaAbmComponent;
  let fixture: ComponentFixture<MediaAbmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MediaAbmComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MediaAbmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
