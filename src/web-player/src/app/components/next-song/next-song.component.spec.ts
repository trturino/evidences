import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextSongComponent } from './next-song.component';

describe('NextSongComponent', () => {
  let component: NextSongComponent;
  let fixture: ComponentFixture<NextSongComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextSongComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextSongComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
