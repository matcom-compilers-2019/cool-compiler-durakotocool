class Main
{
   main():Int{
     {
         --en esta caso no matchea con ninguna rama y debe dar un error en runtime
         case new A of
            a:X=>a;
            b:B=>b;
            c:D=>c;
         esac;
         0;
     }

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

class D inherits B
{
   f(a:Int):Int{0};
};
