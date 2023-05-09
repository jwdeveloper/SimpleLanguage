function drawLine(width, symbol)
{
  var line = " ";
  for(var i in range(width))
  { 
    line = line + symbol;
  }
  return line;
}

function drawGrid(height)
{
  for(var x in range(height))
  {
    var line = drawLine(10,"  X  ");
    print(line);
    sleep(100);
  }
}

drawGrid(10);
