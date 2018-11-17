import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilityPage } from './utility.page';

describe('UtilityPage', () => {
  let component: UtilityPage;
  let fixture: ComponentFixture<UtilityPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UtilityPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UtilityPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
