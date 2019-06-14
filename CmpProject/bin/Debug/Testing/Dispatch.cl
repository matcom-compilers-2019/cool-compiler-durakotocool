class Main inherits A{
     msg:String<-"Hello World";
     main():Int{a};

};
class Z inherits Y
{
  minus(a:Int):Int{a-3};
};
class X inherits A{
   c:B<-new B;
   minus(a:Int):Int{a-1};
};
class Y inherits X
{
  minus(a:Int):Int{a-2};
};
class A
{
   a:Int<-6;
   sum(p1:Int,p2:Int):Int{ p1+p2+1};
   mult(p1:Int,p2:Int):Int{ p1*p2 };
};
class B inherits A
{
  b:A<-new A;
  c:Int<-{2; 3;7+3;};
  sum(p1:Int,p2:Int):Int{ b.mult(p1,p1)+mult(p2,p2)};
  

};
class C inherits B
{
   sum(p1:Int,p2:Int):Int{ new C.sum(p1,p2)};
   pacata():Strint{type_name().concat("op")};
   tumba(a:A):Int{ b@A.sum(1,1) };
};
class U{

  algo():SELF_TYPE{ copy()};
};