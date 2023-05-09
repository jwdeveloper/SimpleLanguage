function drawLine(number size)
{
   var line = " ";
  for(var i =0;i<size;i+=1)
  {
    line = line + "x";
    print(line);
    sleep(100);
  }
}

drawLine(3);
