import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostProComponent } from './post-pro.component';

describe('PostProComponent', () => {
  let component: PostProComponent;
  let fixture: ComponentFixture<PostProComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PostProComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PostProComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
