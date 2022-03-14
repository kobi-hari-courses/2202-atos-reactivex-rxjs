import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map, withLatestFrom } from 'rxjs/operators';

interface Effect {
  name: string;
  callback: (t: string) => string
}


@Component({
  selector: 'app-example-one',
  templateUrl: './example-one.component.html',
  styleUrls: ['./example-one.component.css']
})
export class ExampleOneComponent implements OnInit {

  words = [
    'hello', 'world', 'how', 'are', 'you'
  ];

  effects: Effect[] = [
    {name: 'uppercase', callback: s => s.toUpperCase()},
    {name: 'lowercase', callback: s => s.toLowerCase()},
    {name: 'add !', callback: s => s + '!'}, 
    {name: 'reverse', callback: s => s.split('').reverse().join('')}    
  ];

  word$ = new BehaviorSubject<string>(this.words[0]);
  effect$ = new BehaviorSubject<Effect>(this.effects[0]);

  // result$ = combineLatest([this.word$, this.effect$]).pipe(
  //   map(([word, effect]) => effect.callback(word))
  // );

  result$ = this.word$.pipe(
    withLatestFrom(this.effect$), 
    map(([word, effect]) => effect.callback(word))
  )

  ngOnInit(): void {
      // this.a$.subscribe(val => console.log(val));
      // this.b$.subscribe(val => console.log(val));

      // merge(this.a$, this.b$).subscribe(val => console.log(val));


  }

}
