using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmpProject
{
    public class MIPS
    {
        public MIPS() {
            Data = new List<String>();
            Text = new List<String>();
            Functions = new List<String>();
        }
        public MIPS(List<string> data, List<string> text, List<string> functions){ Data = data; Text = text; Functions = functions; }
        public List<string> Data { get; set; }
        public List<string> Text { get; set; }  //Donde va el codigo
        public List<string> Functions { get; set; } //
    }
}

