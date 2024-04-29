import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Charts0Component } from './charts0.component';

describe('Charts0Component', () => {
  let component: Charts0Component;
  let fixture: ComponentFixture<Charts0Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Charts0Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(Charts0Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
