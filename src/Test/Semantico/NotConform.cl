class Main
{
   main():Int{
     {
       --En la declaracion de un entero no s puede asinar un String
       let a:Int<-"Str" in a;
       --No se puede asignar a un entero un bool
       let b:Int in b<-true;
       --El tipo de la expresion tiene que confromar el tipo estaico del duspatch
       (new X)@D.sum(2,2);
       --Los valores de cada parametro tienen que conformar a los tipos que recibe el metodo
       (new X).sum("a",2);
       0;
     }

   };
};
class X inherits A{
  a:Int<-0;
  algo():Object{
    sum(1,2)
  };
  --La expresion que devuelve el cuerpo del metodo no conforma al tipo de retorno
  sum(p1:Int,p2:Int):Int{ "Str"};
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
