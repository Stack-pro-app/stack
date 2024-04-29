import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Schedule0Component } from './schedule0.component';

describe('Schedule0Component', () => {
  let component: Schedule0Component;
  let fixture: ComponentFixture<Schedule0Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Schedule0Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(Schedule0Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
