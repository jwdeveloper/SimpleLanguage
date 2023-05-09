function fibonacci(n)
{    
     var a, b = 1;
     for(var i=0; i<n; i+=1)
     {
            print(b," ");
            b = b + a;
            a = b-a; 
     }     
}
fibonacci(10);
