var decalage = 0;
var nbBout;	  
var timer;

function menu(nb)
{
 var angle = 360 / nb;		
 nbBout = nb;
 var posX;
 var posY;
 var rayon = 200;
 var centreX = document.getElementById("centreC").offsetLeft;
 var centreY = document.getElementById("centreC").offsetTop;
 var lItem = 20;
 var i = 1;
 var item;
 var b;
 
for(b = 0; b < 360; b += angle)
 {		  
  posX = centreX + 25 + rayon * Math.cos(b * Math.PI / 180);
  posY = centreY + 25 + rayon * Math.sin(b * Math.PI / 180);

  item = document.getElementById("itemC" + i);
  item.className = "itemC";	
  item.style.top = (posY - lItem / 2) + "px"; 
  item.style.left = (posX - lItem / 2) + "px";
  i++;
 }
}

function anim()
{		  
 decalage++;
 var angle = 360 / nbBout;		
 var posX;
 var posY;
 var rayon = 100;
 var centreX = document.getElementById("centreC").offsetLeft;
 var centreY = document.getElementById("centreC").offsetTop;
 var lCentre = 50;
 var lItem = 20;
 var i = 1;
 var item
 var b;

 for(b = 0; b < 360; b = b + angle)
 {		  
  posX = centreX + 25 + rayon * Math.cos((b + decalage) * Math.PI / 180);
  posY = centreY + 25 + rayon * Math.sin((b + decalage) * Math.PI / 180);

  item = document.getElementById("itemC" + i);
  item.style.top = (posY - lItem / 2) + "px"; 
  item.style.left = (posX - lItem / 2) + "px";
  i++;
 }	
}

function stopanim()
{
 clearInterval(timer);		   
}

function replayanim()
{
 timer = setInterval("anim()",50);
}