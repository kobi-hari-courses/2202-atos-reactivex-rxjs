# Module 05 - Higher Order Observables

## What does Higher order mean
* We talked about the concept of "Higher order wrappers"
  * `Array<Array<T>>`
  * `IEnumerable<IEnumerable<T>>`
  * `IObservable<IObservable<T>>`
* We said that delegates can also be in "higher order"
  * `Action<Action<T>>`
  * `Func<Func<T>>`
* We also said that some higher order wrappers are mixtures of 2 types
  * `IObservable<IEnumerable<T>>`
  * `IObservable<Task<T>>`

## How to increase order of observable
* We saw that the most direct way of creating a higher order observable is by using `Select` (`map` in js) so that each item is mapped to an entire observable

```javascript
  interval(1000).pipe(
    map(i => interval(1000))
  );
```

```csharp
  Observable.Range(1, 10)
    .Select(i => Observable.Return(i));
```

* We saw a visual representation of the higher order observables in the web site: [RxViz](https://rxviz.com/)
* We saw that we can also create a second order observable from a flat one using the `groupBy` operator which divides the original values into groups, where each group is an observable on its own
* We saw that `bufferCount` and `windowCount` divide a flat observable into buffers or windows. 
  * `bufferCount` returns `IObserable<IList<T>>`
  * `windowCount` returns `IObserable<IObservable<T>>`
* We saw that we can use `bufferCount` to create observables which are based on calculation done on new values compared to old values


## How to "flatten" observables
* We saw that `mergeAll` (`Merge` in C#) is an operator that takes an `IObservable<IObservable<T>>` and returns a single observable with all the values from all sub observables
* We saw that `switchAll` (`Switch` in C#) is an operator that takes an `IObservable<IObservable<T>>` and returns a single observable with values from the latest sub-observable at any point in time. It is always subscribed only to one sub observable
* We also saw how to use `concatAll` (`Concat` in C#) to take concurrent sub observables and run them sequentially. 
  * We emphasized how the temperature of the observables will affect the outcome. 
* Finally, we understood that `flatMap` (`SelectMany` in C#) is the ultimate flattening operator. It performs both upgrading and flattening at once
  * It takes a flat observable
  * It maps each item to a new wrapper (not necessarily an observable) thus creating a second order observable
  * It then flattens it like `mergeAll` does.
  * Note that `SelectMany` is particually strong because it also works with `Tasks`, `IEnumerables` and other higher order wrappers

## Homework
* A State of stock exchange market is represented by a dictionary mapping between stock code and its value
  * {'Appl': 50, 'Msft': 40, 'Googl': 60}
* Create a cold observable that runs a random simulation where stock values change every interval
* Using operators on the observable ytou have just created, create a sequence of notifications to single stocks that change by more than 20%.


