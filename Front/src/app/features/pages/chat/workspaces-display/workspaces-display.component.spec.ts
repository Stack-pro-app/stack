import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspacesDisplayComponent } from './workspaces-display.component';

describe('WorkspacesDisplayComponent', () => {
  let component: WorkspacesDisplayComponent;
  let fixture: ComponentFixture<WorkspacesDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkspacesDisplayComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WorkspacesDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
