
# Grammar


## Variable declaration statement

```
   (type) (name) [[optional] = (value) ] ;
```

Example

```
   var name; 
   text markName = "mark";
   number x, y, z = 0;
   bool isToday = false;
```

## Function declaration statement
```
  function [optional](type) (name) ( (arguments) )
    (body)
```

example

```
  function sayHello()
  {
    print("hello");
  }
  
  funtion getAge(age)
  {
    return age + 1;
  }

  funtion number getNumber()
  {
    return 23;
  }

```



## Expression
```
  (expression) (operator) (expression) 
```
example

```
   var x =  2 + 2;
   var y =  2 - 2;
   var z =  2 * 2;
   vat t =  2 ^ 2;

   bool a = x == y;
   bool b = c != d;
   
   bool d = a and b;
   bool e = a or b;
```

## Assigment expression

```
  (variable) (operator) (expression)
```

example

```
  var i = 0;
  i += 1;
  i = i + 1;
  
  i -= 2
  i = i - 2;
  
  i *= 3;
  i = i * 3;
  
  i /= 4;
  i = i / 4;
  
  i ^= 5;
  i = i ^ 5;
```

## Function call expression
```
    (functionName) ( (agruments) )
```

example

```
   funtion getAge(age)
   {
     return age + 1;
   }

   var age = getAge(34);
```

## Body
``` 
    {
      (statements) 
    } 
 ```
Example

```
  {
   var i =0; 
   print(i);
  }
```





## If statement

```
   if(expression) 
      (body)
   [optional] else [optional] if(expression)
      (body)
```

 Example
```
  var i =0;
  if(i == 5)
  {
     print(i, "is equal 5")
  }
  else if(i ==6)
  {
       print(i, "is not equal 6")
  }
  else
  {
     print(i, "is not equal 5 and 5")
  }
```

## For statement

```
   for( [optional]variable_declaration ; [optional]expression ; [optional] expression )
     (body)
```

Example
```
   for( var i =0 ; i < 10 ; i += 1 ) 
   {
     print(i)
   }
```


## Foreach statement

```
   for( (variable_declaration) in (list))
    (body)
```

example

```
   for(var i in range(0,10))
   {
     print(i);
   } 
```

## While statement

```
   while (expression) 
    (body)
```
or 
```
   do
    (body) 
   while (expression) 
```

Example
```
   var i =0;
   while(i < 10) 
   {
     print(i)
     i +=1;
   }
```

```
   var i =0;
   
   do
   {
     print(i)
     i +=1;
   }
   while(i < 10) 
```