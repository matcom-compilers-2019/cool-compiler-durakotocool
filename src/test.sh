
for file in `ls $1|grep .cl`;do
        echo "+++++++ Testing $file +++++"
	name=$(echo $file|cut -d '.' -f 1)
	dotnet Cool/bin/Debug/netcoreapp2.0/Cool.dll $1$name'.cl'
	echo "compilador"
	java -jar mars.jar $1$name'.mips'
	echo "------ End test $file ------"
	echo
	echo
	echo

done
