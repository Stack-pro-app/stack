import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TasksInfoComponent } from './tasks-info.component';

describe('TasksInfoComponent', () => {
  let component: TasksInfoComponent;
  let fixture: ComponentFixture<TasksInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TasksInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TasksInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
