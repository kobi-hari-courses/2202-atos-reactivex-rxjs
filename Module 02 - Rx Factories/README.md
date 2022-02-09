# Module 02 - Rx Factories
## Custom Observables
* We have created our own class that is derived from `IObservable<int>`
* We have created an errornous situation where the observer receives notifications after it has unsubscribed
* We have also made the observable send notifications after sending **Complete**
* We noticed that such errors are possible because it is the responsibility of the observable to behave according to the contract and there are not external objects involved in the process to "guard" it.
* We saw that we can use `Observable.Create` as an alternative, where we supply the subscription logic as callback instead of creating our own class.
* We saw that when using the `Observable.Create` factory, it protects us from making such mistakes. 

## Atomic factories
* We saw how to create any logic we want using the `Observable.Create` factory. We also noticed that if the subscription logic is a pure function, the observable will be cold.
* `Observable.Return` creates a single value observable that pushes the single value and then completes.
  * `Observable.Return(42)` ==> --42|
* `Observable.Empty` create an observable that completes upon subscription
  * `Observable.Empty<T>()` ==> -|
* `Observable.Never` creates an observable that does not push any notifications and never ends
  * `Observable.Never<T>()` ==> ------....
* `Observable.Throw` creates an observable that terminates with an error
  * `Observable.Throw<T>(err)` ==> -X(err)
  
## Synchronous factories
* `Observable.Range` pushes a range of values and then completes
  * `Observable.Range(2, 3)` ==> -2-3-4|
* `Observable.Generate` is a way to create a while loop by providing an initial state, stop condition, iteration logic and result selector
  * `Observable.Generate(1, i => i<4, i => i+1, i => i * i)` ==> -1-4-9|

## Asynchronous factories
* All the observables we have seen so far were synchoronous in the sense that they ended at time 0. All notifications were pushed sequentially one after the other.
* `Observable.Interval` creates a **COLD** observable that pushes an incrementing integer (long) value in constant intervals
  * `Observable.Interval(TimeSpan.FromSeconds(1))` ==> 0--1--2--3--...
* `Observable.Timer` has 2 signatures that create very different observables, but both of them use timers.
  * `Observable.Timer(2 seconds)` => ----0|
  * `Observable.Timer(2 seconds, 1 second)` => ----0--1--2--3--4...

## Converters
* Some factory methods create observables from other constructs, thus converting them to observables
* `Observable.Start(Func<T> or Action)` creates an observable the wraps a call to the function or action. Note that this is a **HOT** observable so the action or function will be called as soon as the observable is created even before the first subscription. But the result will be pushed to all subscribers.
* There is also a `StartAsync` version which takes an `async` method and wraps it with an observable that ends when the execution of the function ends. Again - The observable is **HOT** in the sense that the function runs regardless of subscriptions, but it is **WARM** in the sense that it pushes the result to late subscribers.
* `Observable.FromEvent` and `Observable.FromEventPattern` convert a normal .net event into a hot observable. All subscribers share the same event handler, but the handler is connected to the event lazily by ref count. So only when there is a at least one subsciber, the observable handles the event.
* `Task.ToObservable` creates a **HOT** observable that pushes the result of the task and completes when the task is completed
* `IEnumerable.ToObservable` creates a **COLD** observable that enumrates over the enumerable once per observer

## Exercise
* Implement a `ToObservable` operator for `IAsyncEnumerable`. (solution in the code)



  