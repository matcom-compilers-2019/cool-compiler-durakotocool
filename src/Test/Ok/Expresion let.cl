class Main{
  main():Object{
    (new B).sum(2,3)
  };
};
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
  a:Int<-{2; 3;7+3;};
  sum(p1:Int,p2:Int):Int{ (let a:Int<-(2+3),b:Int<-0 in (let a:Int<-5,c:Int<-(let a:Int<-0,b:Int<-1 in a+b) in a+b+c)+a)+a};

};
class C inherits B
{
   
};
