class Main
{
   main():Int{
     {
         --El tipo U no existe
         (new B)@U.sum(1,1);
         --El tipo L no existe
         new L;
         --El tipo Y no existe
         let a:A<-new A,b:Y in
         case a of
            a:X=>a;
            b:B=>b;
            --El tipo Z no existe
            c:Z=>c;
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
