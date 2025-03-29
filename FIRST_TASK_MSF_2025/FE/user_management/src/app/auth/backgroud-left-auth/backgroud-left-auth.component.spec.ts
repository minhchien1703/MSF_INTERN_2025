import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BackgroudLeftAuthComponent } from './backgroud-left-auth.component';

describe('BackgroudLeftAuthComponent', () => {
  let component: BackgroudLeftAuthComponent;
  let fixture: ComponentFixture<BackgroudLeftAuthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BackgroudLeftAuthComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BackgroudLeftAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
