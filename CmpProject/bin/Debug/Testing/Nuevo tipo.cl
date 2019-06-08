class Main
{
   main():String{
     "Hola que hace"
   };
};
class X inherits A{
   c:B<-new B;
   d:Int<-b;
};
class A
{
   b:Int<-5;
   mult(p1:Int,p2:Int):Int{p1*p2};
   devuelveX():X{ new X};
};
class B inherits A
{
   mult(p1:Int,p2:Int):Int{p1};
};
class C inherits B
{
   
};
