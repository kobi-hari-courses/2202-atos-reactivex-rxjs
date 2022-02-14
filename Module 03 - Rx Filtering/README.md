# Module 03 - Rx Filters

## Filters by value
* We talked about how filters work, and that they create a new observable from an existing observable
* We said that filters are operators that **preserve temparature** so that that result observable is as hot or cold as the source one.
  * The result obeservable only subscribes to the source one when it is subscribed to. 
  * In fact, it subscribes to the source once per own subscription
* The `Where` operator passes values from source observable by condition
* The `Distinct` operator passes values from source only if they have not occured before
* The `DistinctUntilChanged` operator passes a value only if it is not equal to the previous one
* The `IgnoreElments` operator does not pass any value, and passes only `Complete` and `Error` events
  
## Filter by position - Take and Skip
* The `Take` and `Skip` filter all values from a position (Take) or until a position (Skip). 
* The `Take(count)` operator passes {`count`} values and then complets
* The `Skip(count)` operator ignores the first {`count`} values and then passes all the rest of the values.
* The `TakeWhile(condition)` operator passes all values until the first one the fails the condition, and then completes.
* The `SkipWhile(condition)` operator ignores all values until the first one that fails the confition, and then passes all the rest of the values.
* The `TakeLast(count)` passes only the last {`count`} values.
  * Since it can not guess ahead of time which are the last values, it only passes the values once the source have completed.
  * Note that if the source is an infinite observable, the result is an `never` observable, which means that it never ends and never emits any value.
* The `SkipLast(count)` skips passes all except the last {`count`} values. 
  * Since it cannot know ahead of time which are the last items, it accumulates a buffer of values. 
  * Whenever more than {`count`} items were seen, it can deduct that the first of them is not one of the {`count`} items and therfore emits it.
  * So the result is a "delayed" version of the source observable without the last values.
* The `TakeUntil` and `SkipUntil` combine 2 observables together.
  * The first is the source observable
  * The second is the control observable
  * `TakeUntil` emits all values from the source ovservable until the control observable emits the first value, and then completes.
  * `SkipUntil` skips all values from the source observable until the control observable emits a value, and then emits all the values it receives.
* Some interesting conclusions
  * The `Take` observables are finite and complete as soon as there are no more values to take. 
  * You can use the `Take` operators to turn an infinite observable into a finite one or to shorten another observable lifespan
  * The `Skip` observable is always as long (in time) as the source observable. It always completes only when the source observable completes.
