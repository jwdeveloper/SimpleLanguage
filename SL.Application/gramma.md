
## Grammer


# body
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


# variable declaration statement

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


# if statement

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

# for statement

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

# while statement

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