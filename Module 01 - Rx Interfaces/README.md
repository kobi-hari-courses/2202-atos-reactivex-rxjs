# Module 01 - Rx Interfaces
## Stream
* We discussed the notion of **Stream<T>**, carrying events ("nexts") of type T
* We intruduced the **Marble Diagrams** as a visual tool that will help us to better understand the streams
* We talked about the 3 ways a stream can end
  * Not end... an infinite stream.  (Diagram: ------------------ )
  * Complete                        (Diagram: ---------------|   )
  * Error                           (Diagram: ---------------X   )

## `IObserver<T>`
* We understood that types that implement `IObserver<T>` are **passive** observers of a stream
* `IObserver<T>` contains 3 methods that a type needs to implement in order to be notified on the activity of a stream
  

| method  | role |
|-------- | ---- | 
| `next(T) => void` | Called on the next piece of data on the stream |
| `complete() => void` | Called when the stream completes succesfully |
| `error(Exception) => void` | Called when the stream terminates with an error |

* In typescript, the error method receives an error of type `any`. Any value can be used as error. In C#, only exceptions may be used here

## `IObservable<T>`
* We understood that in order to become an observable, an object needs to implement the `IObservable<T>` interface
* `Obserables` are the active source of data, `Observers` are passive consumers of data. 

| method  | role |
|-------- | ---- | 
| `subscribe(IObserver<T>) => Subscription` | Subscribes an observer to the observable. The observable will from now send notifications to the observer using the 3 observer methods |

* Notice that there is no `unsubscribe` method. Instead, the observable returns an object that may be used to cancel the subscription
  * In C#: `IDisposable` is used, so cancelling the subscription is done by calling the `.Dispose` method
  * In typescript: `Subscription` type is used, so cancellaing the subscription is done by calling `.unsubscribe`

## Demo in .net core
* We added the `System.Reactive` nuget package to a bare console application
* We create a simple observable using `Observable.Return<T>(T)` which wraps a single value inside an observable. 
* We create a new class `ConsoleObserver` that implements the `IObserver<T>` interface, so that the observer prints every notification to the console
* We create 2 instances of such observers with different id
* We subscribed the first observer to the observable and noticed that it recieves one `next` event and one `complete` event.
* We delayed for 2 seconds and then subscribed the second observer and notice the it also received the same events.
* We understood that a **Cold** Observable sends each observer its own private events and its own time of subscription.

## Intro to "Hot" and "Cold" Observables
* We understood that `Observable.Return<T>` creates a **cold** observable because each observer recieved the events in its own private time.
* A "Cold" Observable is like watching a movie on Netflix. The observable is the source of the movie, but it streams it to each observer privately when the observer clicks "play" (subscribe)
* A "Hot" Observable is lime watching a movie in the cinema. The movie is played regardless of how many observers are there, and all the observers share the same streaming. subscription is like entering the movie theatre but the movie runs regardless.






