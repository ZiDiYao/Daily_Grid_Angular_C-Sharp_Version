import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PracticeCard } from './practice-card';

describe('PracticeCard', () => {
  let component: PracticeCard;
  let fixture: ComponentFixture<PracticeCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PracticeCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PracticeCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
