class Main
{
   main():String{
     "Hola qu hace"
   };
};
class X inherits A{
  a:Int<-0;
  algo():Object{
    sum(1,2)
  };
  sum(p1:Int,p2:Int):Int{ p1+p2+1};
};
class A
{
   
   sum(p1:Int,p2:Int):Int{ p1+p2+1};
};
class B inherits A
{
  b:String<-"caca";
  c:Int<-{2; 3;7+3;};
  sum(p1:Int,p2:Int):Int{ p1+p2 };


};
class F inherits E
{
  f():Int{9};
};
class C inherits D
{
   sum(p1:Int,p2:Int):Int{ p1+p2+9 };
};
class D inherits IO
{
   b:Int<-4;
   f():Int{0};
};
class E inherits C
{
  
};