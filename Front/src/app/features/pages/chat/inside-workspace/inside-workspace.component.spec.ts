import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsideWorkspaceComponent } from './inside-workspace.component';

describe('InsideWorkspaceComponent', () => {
  let component: InsideWorkspaceComponent;
  let fixture: ComponentFixture<InsideWorkspaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InsideWorkspaceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InsideWorkspaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
