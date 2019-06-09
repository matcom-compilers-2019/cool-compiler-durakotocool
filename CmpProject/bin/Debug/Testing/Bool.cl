class Main
{
   main():String{
     "Hola que hace"
   };
};
class X inherits A{
   c:B<-new B;
};
class A
{
   b:Bool<-true;
  
   or(p1:Bool,p2:Bool):Bool{ if p1 then true else( if p2 then true else false fi) fi };
};
class B inherits A
{
  a:Int<-{2; 3;7+3;};
  and(p1:Bool,p2:Bool):Bool{ if not p1 then false else (if not p2 then false else true fi) fi};

};
class C inherits B
{
   select(p:Bool,a:Int,b:Int):Int{if p then a else b fi}
};
