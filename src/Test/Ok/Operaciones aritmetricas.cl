class X inherits A{
   c:B<-new B;
};
class A
{
   b:Int<-(new B).sum(5,3);
  
   sum(p1:Int,p2:Int):Int{ p1+p2+1};
};
class B inherits A
{
  b:String<-"caca";
  a:Int<-{2; 3;7+3;};
  sum(p1:Int,p2:Int):Int{ {a<-p1;a+p1+p2;} };

};
class C inherits B
{
   
};
