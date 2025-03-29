import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemDiaryComponent } from './system-diary.component';

describe('SystemDiaryComponent', () => {
  let component: SystemDiaryComponent;
  let fixture: ComponentFixture<SystemDiaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemDiaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SystemDiaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
