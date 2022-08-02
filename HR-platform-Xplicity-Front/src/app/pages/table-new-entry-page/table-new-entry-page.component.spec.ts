import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableNewEntryPageComponent } from './table-new-entry-page.component';

describe('TableNewEntryPageComponent', () => {
  let component: TableNewEntryPageComponent;
  let fixture: ComponentFixture<TableNewEntryPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TableNewEntryPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TableNewEntryPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
