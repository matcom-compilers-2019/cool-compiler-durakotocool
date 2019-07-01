class Main inherits A{
     msg:String<-"Hello World";
     main():Int{a};

};
class X inherits A{
   c:B<-new B;
};
class A
{
   b:Bool<-true;
   a:Int<-{2; 3;7+3;};
  
   great(p1:Int,p2:Int):Bool{not(p1<p2)};
};
class B inherits A
{
  greatOrEqual(p1:Int,p2:Int):Bool{not(p1<=p2)};
};
class C inherits B
{
   
};
