class Main{
   main():Int{1};
};
class X inherits A{
   c:A<-new SELF_TYPE;
};
class A
{
     
   sum(p1:Int,p2:Int):Int{ p1+p2+1};
};
class B inherits A
{

  b:String<-"caca";
  a:Int<-{2; 3;7+3;};
  sum(p1:Int,p2:Int):Object{ case new C of a:A=>new A 
	b:B=>new B c:X=>c d:D=>new D esac};

};
class C inherits B
{
   
};
class D{
};
