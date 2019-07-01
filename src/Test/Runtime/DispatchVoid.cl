class Main
{
    main():Int{
       {
          let a:A in {
             --Esto se debe dar un runtime error ya que a se inicilaliza en void
             a.func(10);
          };
          0;
       }
    };
};
class A
{
  b:Int<-10;
  func(a:Int):Int{ a+b};
};