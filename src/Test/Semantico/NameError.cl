class Main
{
   main():Int{
     {
       --La variable b no exsite e este contexto
       let a:Int<-b in a;
       --La variable c no exsite en este contexto
       let a:Int<-2,b:Int in c<-b;
       0;
     }

   };
};
