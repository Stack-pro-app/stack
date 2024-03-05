import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateWorkSpaceComponent } from './create-work-space.component';

describe('CreateWorkSpaceComponent', () => {
  let component: CreateWorkSpaceComponent;
  let fixture: ComponentFixture<CreateWorkSpaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateWorkSpaceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateWorkSpaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
