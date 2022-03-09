import { Injectable } from '@angular/core';
import { interval, Observable, ReplaySubject, Subject, timer } from 'rxjs';
import { mapTo, multicast, publish, publishBehavior, publishReplay, refCount, share, shareReplay, take, tap, windowTime } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private dataSource$: Observable<number>;

  constructor() {
    const cold$ = timer(5000).pipe(mapTo(42));

    this.dataSource$ = cold$.pipe(
      tap({
        next: val => console.log('next ' + val), 
        complete: () => console.log('complete'), 
        error: err => console.log('error ' + err)
      }),
      shareReplay({
        windowTime: 4000, 
        bufferSize: 1, 
        refCount: true
      })
    );
  }

  /*

  shareReplay - uses refCount to connect, but not to disconnect

  */

  getData(): Observable<number> {
    return this.dataSource$;
  }
}
