class Main{
    main():Int{
        0
    };
    fact(i:Int):Int{
        if (i=0) then 1 else i*fact(i-1) fi
    };
    iterative(i:Int):Int{
        let f:Int<-1 in 
            {
                while(not(i=0)) loop
                    {
                        f<-f*i;
                        i<-i-1;
                    }   
                pool;
                f;
            }
    };
};