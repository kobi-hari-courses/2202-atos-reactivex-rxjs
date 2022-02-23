# Module 06 - Hot and Cold Observables

## Creating Hot Observable with `Observable.Create`
* We have seen how to use the **Immutable Collections** Package to create various types of collections that are immutable
  * We saw that calling state-mutating methods like `Add` actualy create new instances of the collection but do not change the state of the original instance
  * We saw how to use the `ImmutableList<T>` class
* We have create an observable that generates a sequence of immutable lists by running an asyncronous logic inside the subscribe callback
* We understood that in order to create a similar **Hot** Observable we need to run the same logic outside of the subscribe callback
* We saw that the implementation of a hot observable are quite complicated
  * State should be shared using global values
  * We should use locks and semaphores in order to protect the state from race conditions

## Subjects
* We understood that a subject is a class that is both an observable and an observer at the same time
* We saw that the subject is always a **HOT Observable**
* We saw that the subject may be used as a **HUB**
* We saw that when placed in front of a cold observable, the subject "heats it"
* We saw that there are several other types of subjects
  * The `BehaviorSubject` holds the latest "Next" as current value and sends the current value as a dedicated notification to every new subscriber upon subscription
  * The `ReplaySubject` accumulates the events it receives and replays them upon subscription to every new subscriber


## Temperature changing observables
* We saw that there are operators that act as a way to place a subject in front of an observable, thus multicating it as a **HOT Observable**
* The `Multicast` operator takes a subject as a parameter and returns an `IConnectableObservable`
  * It takes a subject as parameter, so you may send any type of subject. For example: `cold.Multicast(new BehaviorSubject<int>(12))`
  * The returned connectable observable may be connected to the source observable using the `Connect`. The method returns an `IDisposable` which may be used to disconnect from the source.




