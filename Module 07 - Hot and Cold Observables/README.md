# Module 07 - Temperature changing operators

### The `Publish` group
* We saw that `Publish === Multicast(new Subject)` - so the publish group of operators is a shortcut to "heat up" an observable while letting the operators create the subject by itself
* We saw that we receive an `IConnectableObservable` just like when using `Multicast`
* We saw other variations
  * `Publish(initialValue) === Multicast(new BehaviorSubject(initialValue))`
  * `Replay(bufferCount, windowLength) === Muilticast(new ReplaySubject(bufferCount, windowLength))`

### Important differences between `BehaviorSubject` and `ReplaySubject`
* We have taked about the similarity between `BehaviorSubject` and `ReplaySubject(1)`.
* We have seen that there are important differences in certain marginal scenarios
  * When you subscribe to the subject before it recieves the first "next"
    * Behavior subject yields next event with the initial value
    * Replay subject will not yield event until the first "next" arrives
  * When you subscribe to the subject after it recieves "complete"
    * Behavior subject will only yield "complete"
    * Replay subject will yield the last "next" value and then "complete"

### The `RefCount` operator
* The `RefCount` operator applies only on `ConnectableObservable`
* It subscribes to the source when the number of its own subscribes goes from 0 to 1
* It unsubscribes from the source when the number of its own subscribers goes back down to 0
* We saw several interesting scenarios when using `Publish` and `Refcount` on cold observables.

### The `share` group in javascript
* We saw that there is no `Share` operator in Rx.Net although it can be easily added
* We saw that in Javascript:
  
```js
obs$.pipe(share())
```

is equivalent to: 

```js
obs$.pipe(
  multicast(() => new Subject()), 
  refCount()
);
```

* There is also a **replay** version

```js
obs$.pipe(shareReplay(1))
```

is equivalent to: 
```js
obs$.pipe(
  multicast(() => new ReplaySubject(1)), 
  refCount()
);
```

