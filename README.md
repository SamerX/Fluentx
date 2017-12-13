# Fluentx
Fluentx : A Special .NET Library

For donations please use the following paypal link: https://www.paypal.me/SamerX

<h2>What is Fluentx?</h2>

Fluentx is a .NET library I have created to help me in development in any project I work on, you can consider it as a utility libarary but its really bigger than that, it holds various utility methods, helper methods and design patterns that will help any developer using the .NET framework, I have also collected some extension methods from the web (some are optimized and some are left as is) which I have used previously in projects I have been working on, so if you think that you have a helper method, extension method a pattern or a class that will help others and you think it can fit in Fluentx then please feel free, the main limitation is dependency, because as much as possible I want fluentx to be portable and does'nt depend on web, windows or any other library or framework.

The aim is that Fluentx helps developers in their coding and make things just more and more simpler.

Fluentx covers all major C# control statements, and eliminates the limitations within them, and adds more features, the assembly holds 5 major categories: C# control statements, Helper Classes, Extension Methods, Specifications Pattern, Object to Object Mapper, the assembly will get bigger and bigger by time as we will add more and more to it to make it used and helps everybody out there.

It also has an implementation of Specification Pattern as a validation for any type of code, whether its a business validation or anything.

<h2>
Functionalities Categories
</h2>

So fluentx has so many functionalities, and up to the moment of writing this article I can divide them into five main categories:

- C# Control Statements

- Helper classes 

- Extension methods

- Patterns

- Fluentx Mapper (Object to Object Mapper)

To use Fluentx its straight forward and simple: you add a reference and then you can use the library, now for C# control statements and some Helper methods you need to start with the main class of fluentx which is Fx (Fx is also the representation of three mathematical expression F(x) => function of x).

Below you will find code snippets using Fluentx functionalities in all different categories

<h2>C# Control Statements</h2>

C# control statements such as if, switch, try catch, while, for, foreach etc ... are all translated into lambda expressions and can be used as functional statements, to use them start by stating Fx the main class then the control statement you want to use, below are some examples and you can find the rest also documented in the library it self.

<h3>Switch Statement</h3>

C# switch statement is a very helpful statement and provides more readability than if statement specially when the conditions become more and more, the main limitation of the switch statement is the that switch cases should hold a constant value, and you can't use it for any variable you want. A really good example of using it is with Types, when you want to do a switch statement over a type instance:
<pre>
Fx.Switch("Fluentx".GetType())
      .Case<int>().Execute(() =>
      {
          result = 1;
      })
      .Case<string>().Execute(() =>
      {
          result = 2;
      })
      .Default(() =>
      {
          result = 0;
      });
      
</pre>

<h3>ForEach/ForEvery with item and index</h3>

In many times I found my self wanting to use a foreach statement with an index of the current item. 

<pre>
IList<Custom> customers = new List<Customer>();
customers.Foreach((item, index)=>{
//item is the current item in the loop, and index of the current item in the loop
}); 

</pre>
of course foreach has a synonem in fluentx ForEvery which it exactly does the same thing as foreach, I added this one because some .NET libraries use ForEach and it might do a conflict which you can over come by using ForEvery
<h3>
WhileFalse, WhileTrue, WhileTrueFor and WhileFalseFor
</h3>

<pre>
int result = 0; Fx.WhileTrue(() => { ++result; return result != 6; });

int result = 0; Fx.WhileFalse(() => { ++result; return result == 6; });

Fx.WhileTrueFor(()=>IsProcessComplete(), 4);

Fx.WhileFalseFor(()=>IsProcessComplete(), 5);

</pre>
<h3>
Retry on Fail (Long Polling)
</h3>
<pre>
int result=0;
Fx.RetryOnFail(() =>
     {
        ++result;
        return result > 5;
     });

//Or

int result=0;
Fx.RetryOnFail(() =>
     {
        ++result;
        return result > 5;
     }, attemptSleepInMilliSeconds: 10);

</pre>

<h3>
Try - SwallowIf
</h3>
<pre>
int result = 0;
Fx.Try(() =>
{
     throw new NotImplementedException();
     ++result;
}).SwallowIf<NotImplementedException>();
//Swallows a certain exception and prevent throwing it.

</pre>

<h3>Using Statement</h3>

<pre>
Fx.Using(new PrivateDisposableTestEntity(), (instance) => { });
Fx.To(Value)

//All primitive types are supported to parse a string with an optional default value
var birthDate = Fx.ToDateTime("Some Text", DateTime.Now);
var age = Fx.ToInt32("Some Text", -1);
var age = Fx.ToInt32("Some Text");

</pre>
<h2>Helper Classes</h2>

<h3>Period Class</h3>

Period class represents a time period between two datetime instances, some times we have a different set of dates and we need to determine if there is an overlap occurs in between periods.

<pre>
var firstPeriod = new Period(date1, date2);

var secondPeriod = new Period(date3, date4);

bool isOverlap = firstPeriod.IsOverlap(secondPeriod);//Edges NOT considered in the overlap

bool isOverlap1 = firstPeriod.IsOverlap(secondPeriod, true); //Edges are considered in the overlap

</pre>

<h3>Guard Class</h3>

Use this class to validate for certain conditions and assertions but in a more neat way and without using the IF statement.

<pre>
Guard.Against<NotImplementedException>(myFlag);
</pre>

<h3>PredicateBuilder Class</h3>

Predicate Builder is where you build boolean logic using expressions, a good use for it is with linq queries as they miss the Or functionality, using this predicate builder you can do Or and AND operations easily, it also has a start point value of TRUE or FALSE, there are several implementations on the net for it, I think this one is good:

<pre>
var predicate = PredicateBuilder.True<Customer>();

foreach (var product in products)
{
    predicate = predicate.And(c => c.Products.Any(x => x.Id == productId));
}

</pre>

<h2>Extension Methods</h2>

<h3>Nullability Check</h3>
<pre>
var customer = GetCustomerById(id);

var flag= customer.IsNull(); 

var flag = customer.IsNotNull();

</pre>
<h3>IfTrue and IfFlase</h3>
<pre>
bool flag = GetBooleanValueMethod();

flag.IfTrue(()=>{ //Do Anything here });

flag.IfFlase(()=>{ //Do Anything here });
</pre>

<h3>Random</h3>
<pre>
IList<Customer> customers = GetCustomers();

var randomCustomer = customers.Random();
</pre>

<h3>Is</h3>

<pre>
var customer = GetCustomer();

var flag = customer.Is<Customer>();

<h3>Logical Operators</h3>

var flag = flag1.Or(flag2).Xor(flag3).And(flag4).And(()=> {return true;}).Not();
</pre>

<h3>Between</h3>
<pre>
var value = 7;
var flag = value.Between(5, 9);
</pre>

<h3>In/NotIn</h3>
<pre>
string parameter = null;

var result = parameter.In(new string[] { "one", null, "three" });

var result = parameter.NotIn(new string[] { "one", null, "three" });
</pre>

<h3>MinBy/MaxBy</h3>

Finds out the minimum/maximum item in an IEnumerable according to the specifed predicate.

<pre>
DateTime[] dates = new DateTime[] { DateTime.Now, DateTime.Now.AddYears(3) };

var min = dates.MinBy(x => x.Year);
</pre>
<h3>Random</h3>

Extension method to perform random return of an item within the specified list.
<pre>
DateTime[] dates = new DateTime[] { DateTime.Now, DateTime.Now.AddYears(3) };

var randomDate = dates.Random();
</pre>

<h3>To[PremitiveType]</h3>

ToInt, ToInt16, ToInt32, ToInt64, ToUInt, ToUInt16, ToUInt32, ToUInt64 ToDateTime, ToDouble, ToFloat, ToLong, ToULong, ToByte, ToSByte, ToBool, ToGuid, ToDateTime

<h3>ToCSV</h3>

Extension method to perform a comma separated string from the specified list
<pre>
DateTime[] dates = new DateTime[] { DateTime.Now, DateTime.Now.AddYears(3) };

var csvDates = dates.ToCSV();
</pre>

<h3>DateTime Extension Methods</h3>

Extension method to perform a comma separated string from the specified list
<pre>
DateTime date = 24.September(2014);
</pre>

<h3>Other Extension Methods</h3>

And many other extension methods that you can use.

<h2>Patterns</h2>

<h3>Specifications Pattern</h3>

Specifications pattern is to combine several business rules together using boolean logic. I have a good implementation of this pattern and I found a very useful and interesting basic implementation for it on the web written by Govindaraj Rangaraj and here is a link to the original article in codeproject:

 http://www.codeproject.com/Articles/670115/Specification-pattern-in-Csharp

I made a little bit of tweaking on it but eventually the main concept is still there

<pre>
ISpecification<int> rule1 = new ExpressionSpecification<int>(x => x == 1, "rule1 failed");
ISpecification<int> rule2 = new ExpressionSpecification<int>(x => x == 2, "rule2 failed");
ISpecification<int> rule3 = new ExpressionSpecification<int>(x => x == 3, "rule3 failed");
ISpecification<int> rule4 = rule1.Or(rule2).Or(rule3); 
var result = rule4.ValidateWithMessages(4);
</pre>

<h2>Fluentx Mapper (Object to Object Mapper)</h2>

Fluentx mapper is an object to object mapper, its very easy, simple and straight forward to use, it has the most common functionalities you would expect of a mapper to do the proper mapping you desire, it also provide you with the option to manually map properties and fields.

The following is an example of using the mapper with clarificaitons:

<pre>
var mapper = new Mapper<DTOCustomer, Customer>()
.UserMapper<DTOProfile, Profile>()
.Conditional(x => x.Description, src => src.Description != string.Empty)
.Ignore(dest => dest.BirthDate)
.IgnoreIf(dest => dest.Serial, src=> { return src.Serial == string.Empty;})
.For(dest => dest.Order, src => RepoAccount.LoadOrder(src.OrderId))
.ForIf(dest => dest.Number, src => src.Number, src => src.AutoGenerate)
.Resolve((src, dest)=>{ .... do whatever you want here ... }); 

var customer = mapper.Map(dtoCustomer);

</pre>
<h3>Basic Convention</h3>

First of all we need to know that Fluentx Mapper uses conventions to map properties, so it assumes and tries to map a src property to a destination property if the name of the properties match. so once you create:
<pre>
var mapper = new Mapper<DTOCustomer, Customer>();
</pre>
then you have the basic functionality turned on and kicking, and that is the default behaviour of an object to object mapper.

<h3>Nested Mapper</h3>

Now you can inject the use of another mapper to the mapper you have as:

<pre>
var mapper = new Mapper<DTOCustomer, Customer>()
.UseMapper<DTOProfile, Profile>();
</pre>

here we are instructing fluentx mapper that we are mapping DTOCustomer to Customer and we are using an internal mapper of DTOProfile to Profile when ever a property inside customer of type DTOProfile is found.

<h3>Conditional Mapping</h3>

Conditional mapping of a property is to override the basic mapping convention of NOT to map a specified property if a condition is NOT met, so a property will be mapped if the condition is true

<pre>
.Conditional(x => x.Description, src => src.Description != string.Empty)
</pre>

<h3>Ignore</h3>
Ignores the mapping of the specified property (dont map it)
<pre>
.Ignore(dest => dest.BirthDate)
</pre>

<h3>IgnoreIf</h3>
Ignores the mapping of the specified property if the condition evaluates to true.
<pre>
.IgnoreIf(dest => dest.Serial, src=> { return src.Serial == string.Empty;})
</pre>

<h3>For</h3>
Maps the specified property using the specified action which takes source as the parameter
<pre>
.For(dest => dest.Order, src => RepoAccount.LoadOrder(src.OrderId))
</pre>

<h3>ForIf</h3>
Maps the specified property using the specified action which takes source as the paramter if the evaluation of the specified action is true
<pre>
.ForIf(dest => dest.Number, src => src.Number, src => src.AutoGenerate)
</pre>

<h3>Resolve</h3>
A custom action to be executed on the mapper when all mapping is done, can be used to manually manipulate the mapping as it has the source and destination as parameters.
<pre>
.Resolve((src, dest)=>{ .... do whatever you want here ... }); 
</pre>

<h3>Centeralized Mapping</h3>
Sometimes you want to use the same mapping in different places in your code, in order to do that fluentx mapper can be used in such a way to make it centerazlied or reusable as follows using inheritance:
<pre>
public class CustomerMapper : Mapper<DTOCustomer, Customer>
{
    public CustomerMapper()
    {
        this.UseMapper<DTOProfile, Profile>();
        this.Conditional(x => x.Description, src => src.Description != string.Empty);
        this.Ignore(dest => dest.BirthDate);
        this.IgnoreIf(dest => dest.Serial, src=> { return src.Serial == string.Empty;});
        this.For(dest => dest.Order, src => RepoAccount.LoadOrder(src.OrderId));
        this.ForIf(dest => dest.Number, src => src.Number, src => src.AutoGenerate);
        this.Resolve((src, dest)=>{ .... do whatever you want here ... }); 
        
        //Or you can chain the methods after the first call 
    }
}

var mapper = new CustomerMapper(); 
var customer = mapper.Map(dtoCustomer);
</pre>
of course you can now cache the mapper and reuse it any where in your code in the way you desire.


<h2>
Sorting Algorithm
</h2>
Fluentx has a lot of sorting algorithms implemented against lists in c#, that includes : Quick Sort, Merge Sort, Heap Sort, Insertion Sort, Bubble Sort, Gnome Sort, Intro Sort, Cocktail Sort, Odd Even Sort and more.


<h2>
Text Search Algorithms
</h2>
Fluentx has an implementation for well know text search algoithms like: Boyer Moore Algorithm, Linear Search, Binary Search and many others.

<h2>
Donation and Support
</h2>
For donations please use the following link: https://www.paypal.me/SamerX
