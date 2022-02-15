# Module 04 - Monad

## Functional Programming in C#
* **Functional Programming** Is possible in languages that allow you to hold a pointer to a function and pass the functional itself as value between variables and parameters
* **Java** Is an example of a programming language that does support functional programming at all. You can not hold a pointer to a function.
* **C#** is an **Object Oriented** language as well but it does support certain abilities from the world of **Functional Programming**. It does allow you to hold a function pointer
* Since in **C#** functions are always part of classes, a function pointer must also point to the instance of the object that the function is executed on. A mixture of the function and the object that it runs on (the **target**) is called in **C#** language: A `Delegate`
  * Delegates have 2 properties: 
    * Method: `MethodInfo` the function itself
    * Target: `Object` the target of invokation
* Through the evolution of C#, that delegate syntax became more and more convinient and friendly.
  * In .Net 1 you had to define your own delegate type, and then construct an instance of the delegate and suppoly the target and method.
  * In .Net 2 you could use Annonymous methods and ommit the `new` keyword
  * in .Net 3 entered **Lambda Expressions** So we could write annonimoums methods in a short and friendly syntax
  * in .Net 4 and 4.5 the types `Action` and `Func` were introduced and we no longer needed to define Delegate types and provide them with names
* Many features of modern .net originate from the **Functional Programming** Paradigm:
  * LINQ
  * TPL and Tasks
  * Reactive X and Observables
  * `Lazy<T>` and `Nullable<T>`
  * and more...


## Covariance and Contravariance
* When using Generic types, its important to remember that the inheritance rules apply on the outer type, not on type arguments.
* So, if type `B` is derived from type `A`, then `Generic<B>` does not derive from `Generic<A>`. And you can not use `Generic<B>` when a `Generic<A>` is expected.
* Sometimes, you want to be able to define interfaces that are friendly with derived types. For example, consider the following method: 
``` Csharp
void func(IEnumerable<Fruit> fruits) ...
```

Obviously, you can invoke this method using a sequence of Apples. 

``` CSharp
var apples = new List<Apple> {apple1, apple2, apple3};

func(apples);
```

It should be ok, because if `func` expects a sequence of fruits, then surly a sequence of apples meets the criteria.

* To allow such conversions, C# introduces a way to define generic type parameters as **Covariant**. And indeed the IEnumerable interface is defined as follows: 

``` Csharp
public interface IEnumerable<out T>
```

The `out` keyword means that `IEnumerable<T>` can be assigned to variables of type `IEnumerable<K>` if `T` is derived from `K`

* This feature is called `Covariance`
* Simillarily, we can define type argument as **Contravariant** using the `in` keyword. For example, in C# the interface `IComparer` is **Contravariant**

```CSharp
public interface IComparer<in T>
```

* The `in` keyword means that `IComparer<T>` can be assigned to cariables of type `IComparer<K>` if `K` is derived from `T`

* The reason the keywords are called `in` and `out` is because it makes it implies the usage
  * Interfaces with `in T` mean that the members of the interface can **consume** data of type `T` (take it as **in** - put), but cannot **produce** data of type `T` (send it as output).
  * Interfaces with `out T` mean that the members of the interface can **produce** data of type `T` (send if as **out** - put) but can not **consume** it. 

* With delegates, it becomes even clearer. Let's look at the definition of a type representing a function delegate that takes 2 parameters (input) and produces 1 output:

```CSharp
public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
```

* Using the `in` and `out` keywords is only allows on interfaces and delegates but can not be used on classes and structs.


## Monads
* In **Functional programming** We often use generic wrappers. For example: 
  * `Nullable<T>` is a wrapper of type T that may also hold null.
  * `Task<T>` is a wrapper of type T that contains logic that will calculate the T value in the backround and produce it in the future and push it to the consumer asynchronously
  * `Lazy<T>` contain the ability to calculate a value of T when needed, and then cache it.
  * `IEnumerable<T>` contain the ability to produce many values of type T lazily, when requirested, but not to cache them.
  * `IObservable<T>` wraps multiple values of type T that can be pushed to the consumer asynchronously.
  
* All of these examples are different but they have some characteristics in common
  * They are all wrappers of values of type T.
  * They all contain logic that has the ability to calculate T
* Many of the wrappers also allow us to compose logic in order to create more advanced wrappers of the same type.
  * `Task<T>` supports composition using the `ContinueWith` operator. so `Task<T>.ContinueWith(Func<T, K> cont)` producses a `Task<K>` that contains all the logic of the original task plus the provided `cont` function that is applied on the result of the original task.
  * `IEnumerable<T>` supports composition using the `Select` operator. So `IEnumerable<T>.Select(Func<T, K> func)` producses an `IEnumerable<K>` that contains all the logic of the original sequence with the `func` method applied to every item in the original sequence.
  * `IObservable<T>` supports the same `Select` method with the same behavior
  * With nullable, we have the `?.` operator. It does not look the same as the other operators because we do not call it using method syntax, but it is essentially the same thing. It creates a new nullable from the original nullable, by applying the method or property on the value contained inside the nullable.
* In functional programming, a **Monad** Is such a wrapper. In order to be called a **Monad**, the wrapper `M<T>` needs to support 2 actions:
  * The `Unit` rule: there should be a method that takes a value of type `T` and creates a wrapper for it. 
    * In functional programming lingo, we mark it as follows: `unit: T => M<T>`, so unit is a function that takes a value of type T and returns a wrapper of type M<T> that holds that value in it.
    * For `Task<T>` the unit method is `Task.FromResult`
    * For `IEnumerable<T>` you can use `Enumerable.Repeat(value, 1)`.
    * For `IObservable<T>` use `Observable.Return(T)`
    * For `Lazy` and `Nullable` simply use the constructor.
  * The `Bind` rule: there should be a method (usually called, "bind", "flatmap" or "SelectMany") that compose a function to produce a new wrapper.
    * It is marked as follows: `bind: (M<T>, T => M<R>) => M<R>`
    * For `IEnumerable` and `IObservable` this is `SelectMany`. 
    * For `Task` this is `ContinueWith` and `Unwrap`
    * For `Nullable` this is the `?.` operator

## The C# Query Syntax
* In C# you can use the query syntax instead of the Fluent API syntex.
  * `sequence.Select(x => x + 1)` can be replaced with 
  ```CSharp
  from x in sequence
  select x + 1
  ```
  * `sequence.SelectMany(item => item.subSequence)` can be replaced with 
  ```CSharp
  from item in sequence
  from x in item.subsequence
  select x
  ```
* In some cases, the query syntax is shorter and more friendly becuase it allows to use all the context variables in final select clause

  ```CSharp
  from item1 in sequence
  from item2 in item1.subsequence
  from item3 in item2.subsequence
  select item1 + item2 + item3
  ```
The last example can be quite verbose to write in the Fluent API.

* You can use the query syntax on the following built-in interfaces:
  * `IEnumerable`
  * `IQueryable`
* You can also use it on selected types of additional interfaces
  * `IParallelEnumerable`
  * `IObservable`
  * `IAsyncEnumerable`
* Oh... and you can use it on your own interfaces too...
  * `from...select` actually looks for an extension method called `Select` with the following signature

  ```CSharp
  M<R> Select<T, R>(this M<T> @this, Func<T, R> func);
  ```
  * If you use compoud `from` then you also need to implement the `SelectMany` method, with the following, very specific, signature
  
  ```CSharp
  M<R> SelectMany<T, S, R>(this M<T> @this, Func<T, M<R>> selector, Func<T, R, S> resultSelector);
  ```

  * Once you have implemented these 2 methods, you can start writing queries using your own monad interfaces.






