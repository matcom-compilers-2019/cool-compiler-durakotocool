class X inherits A{
   c:B<-new B;
};
class A
{
   b:Object<-(new B).sum(5,3);
  
   sum(p1:Int,p2:Int):Int{ p1+p2+1};
};
class B inherits A
{
  b:String<-"caca";
  a:Int<-{2; 3;7+3;};
  d:Int<-0;
  sum(p1:Int,p2:Int):Object{ {a<-0;while a<(p1+p2)  loop      {d<-d+1;a<-a+1;}pool;} };

};
class C inherits B
{
   
};
