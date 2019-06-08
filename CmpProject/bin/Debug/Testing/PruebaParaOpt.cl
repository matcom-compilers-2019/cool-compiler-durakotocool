class Main{
    main():Int{
        0
    };
    cosa(a:Int,n:Int,m:Int):Object{
        let i:Int<-m,j:Int<-n in
        {
            a<-0;
            while i<a loop
            {
                i<-i+1;
                j<-j-i;
                if i<j then a<-1 else 0 fi;
                i<-2;
            }
            pool;
        }
    };
};