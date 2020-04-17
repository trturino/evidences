import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppPlayerComponent } from './app-player.component';

describe('AppPlayerComponent', () => {
  let component: AppPlayerComponent;
  let fixture: ComponentFixture<AppPlayerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppPlayerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppPlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
