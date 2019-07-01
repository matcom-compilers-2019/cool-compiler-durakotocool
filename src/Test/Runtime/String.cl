
class Main
{
    main():Int{
       {
          let a:A<-new A in {
             --Esto se debe dar un runtime error 
             a.errorSubStr();
          };
          0;
       }
    };
};

class A
{
  b:String<-"nabuconodozor";
  errorSubStr():String{ b.substr(2,12)};
};
