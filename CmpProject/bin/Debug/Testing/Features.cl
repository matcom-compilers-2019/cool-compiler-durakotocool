class X inherits A{
   c:B<-new B;
};
class A
{
   sum(p1:Int,p2:Int):Int{ p1+p2+1};
};
class B inherits A
{
  b:String<-(new A).type_name();
  c:Int<-{2; 3;7+3;};
  d:SELF_TYPE<-self;
  sum(p1:Int,p2:Int):Int{ p1+p2 };
  copy():Object{sum(1,2)};
  
};
class C inherits B
{
  sum(p1:Int,p2:Int):Int{ p1+p2};
};
