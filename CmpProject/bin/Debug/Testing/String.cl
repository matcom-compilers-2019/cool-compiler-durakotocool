class Main
{
    main():Int{0};
};
class X inherits A{
   c:String<-"Praa";
};
class A
{
   b:String<-"Raaaa";
  
   devuelveX():X{ new X};
};
class B inherits A
{
  devuelveStr():String{ "Rooo"};
  func():String{b.concat("aaa")};
  func2():Int{b.lenght()};
  func3():String{ b.substr(2,3)};
};
class C inherits B
{
   
};
