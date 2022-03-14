import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { scan, startWith, withLatestFrom } from 'rxjs/operators';

type Position = [number, number];

interface Direction {
  name: string;
  index: 0 | 1;
  multiplier: 1 | -1;
}

interface Step {
  name: string;
  amount: number;
}

@Component({
  selector: 'app-example-two',
  templateUrl: './example-two.component.html',
  styleUrls: ['./example-two.component.css']
})
export class ExampleTwoComponent implements OnInit {

  directions: Direction[] = [
    { name: 'North', index: 1, multiplier: -1}, 
    { name: 'South', index: 1, multiplier: 1},
    { name: 'West', index: 0, multiplier: -1}, 
    { name: 'East', index: 0, multiplier: 1},
  ];

  steps: Step[] = [
    { name: 'Small Forward', amount: 1 }, 
    { name: 'Small Backwards', amount: -1}, 
    { name: 'Large Forward', amount: 3}, 
    { name: 'Large Backwords', amount: -3}
  ];

  direction$ = new BehaviorSubject<Direction>(this.directions[0]);
  step$ = new Subject<Step>();


  advance$ = this.step$.pipe(
    withLatestFrom(this.direction$)
  );

  position$: Observable<Position> = this.advance$.pipe(
    scan((pos, [step, direction]) => this.advance(pos, step, direction), 
         <Position>[0, 0]), 
    startWith([0, 0])
  );


  constructor() { }

  ngOnInit(): void {
  }

  advance(position: Position, step: Step, direction: Direction): Position {
    const result: Position = [...position];

    result[direction.index] += step.amount * direction.multiplier;

    return result;
  }

}
