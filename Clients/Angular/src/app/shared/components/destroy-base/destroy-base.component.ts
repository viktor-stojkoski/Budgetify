import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({ template: '' })
export abstract class DestroyBaseComponent implements OnDestroy {
  protected destroyed$: Subject<void> = new Subject<void>();

  public ngOnDestroy(): void {
    this.destroyed$.next();
    this.destroyed$.complete();
  }
}
