using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmpProject.CIL;
using System.Numerics;
namespace CmpProject.Optimization
{
    public class Block
    {
        public string Name { get; set; }
        public List<IThreeDirIns> threeDirIns { get; set; }
        public bool IsBloqued { get; set; }
        public Block(List<IThreeDirIns> threeDirIns)
        {
            this.threeDirIns = threeDirIns;
        }
        public BigInteger[] Gen { get; set; }
        public BigInteger Gen_B { get; set; }
        public BigInteger[] Kill { get; set; }
        public BigInteger Kill_B { get; set; }
        public BigInteger[] Reaching_Definitions_func { get; set; }
        public BigInteger[] Use { get; set; }
        public BigInteger Use_B { get; set; }
        public BigInteger[] Def { get; set; }
        public BigInteger Def_B { get; set; }
        public int[] e_Gen { get; set; }
        public int[] e_Kill { get; set; }
        public Block()
        {
            threeDirIns = new List<IThreeDirIns>();
        }
        public Block(string Name):this()
        {
            this.Name = Name;
        }
        public Block(List<IThreeDirIns> threeDirIns, string Name):this(threeDirIns)
        {
            this.Name = Name;
        }
        public BigInteger[] Aplicate(Transfer_Function<BigInteger,IThreeDirIns> function,BigInteger init, int dir)
        {
            BigInteger[] result = new BigInteger[threeDirIns.Count];
            BigInteger after = init;
            for (int i = 0; i < result.Length; i++)
            {
                int pos = (dir > 0) ? i : result.Length - 1 - i;
                //Aplica la funcion de transferencia el estado anterior en la instruccion actual
                result[pos] = function(after,threeDirIns[i]);
                after = result[pos];
            }
            return result;
        }
        public BigInteger[] Aplicate2(Transfer_Function_Basic<BigInteger, IThreeDirIns> function, BigInteger init, int dir)
        {
            BigInteger[] result = new BigInteger[threeDirIns.Count];
            BigInteger after = init;
            for (int i = 0; i < result.Length; i++)
            {
                int pos = (dir > 0) ? i : result.Length - 1 - i;
                //Aplica la funcion de transferencia el estado anterior en la instruccion actual
                result[pos] = function(threeDirIns[i]);
                after = result[pos];
            }
            return result;
        }
        public Func<BigInteger,BigInteger>  Tranfer_Function;
        //Esto sirve para calcular Gen_B Uso_B
        public BigInteger TranferFunctionParam2(BigInteger[] seq1)
        {
            if (seq1.Length == 0)
                return 0;
            else if (seq1.Length == 1)
                return seq1[0];
            return seq1.Aggregate((x, y) => x | y);
        }
        public BigInteger TranferFunctionParam1(BigInteger[] seq1, BigInteger[] seq2)
        {
            BigInteger Result = 0;
            for (int i = 0; i < seq1.Length; i++)
            {
                var seq2Suf = seq2.Skip(seq2.Length - i).ToList();
                seq2Suf.Insert(0, seq1[seq1.Length - 1 - i]);
                if (seq2Suf.Count > 1)
                    Result |= ((seq2Suf.Aggregate((x, y) => x & ~y)));
                else
                    Result |= seq2Suf[0];
            }
            return Result;
        }
        public BigInteger Tranfer_Function_Reaching_Definitions(BigInteger x) => (Gen_B | (x & ~Kill_B));
        public BigInteger Tranfer_Function_Live_Variable(BigInteger x) => (Use_B | (x & ~Def_B));
        public void AddIns(IThreeDirIns ins)
        {
            threeDirIns.Add(ins);
        }
        public void CalculateKill(ZyroCil[] ins)
        {
            var function = new Transfer_Function_Basic<BigInteger, IThreeDirIns>((state) =>
            {
                //Conjunto de las demas definiciones de zyro
                if (state is ZyroCil zyro)
                    return ins.Select((v) => ((v.X.Equals(zyro.X))&&(v!=zyro))?1:0).Aggregate((x,y)=> ((x << 1) + y));
                else
                    return 0;
            }
            );
            Kill=Aplicate2(function, 0, 1);
            Kill_B = TranferFunctionParam2(Kill);
        }
        public void CalculateGen(ZyroCil[] ins)
        {
            var function = new Transfer_Function_Basic<BigInteger, IThreeDirIns>((state) =>
            {
                //Conjunto de las demas definiciones de zyro
                if (state is ZyroCil zyro)
                    return ins.Select((v) => (v == zyro)? 1 : 0).Aggregate((x, y) => ((x << 1) + y));
                else
                    return 0;
            }
            );
            Gen = Aplicate2(function, 0, 1);
            Gen_B = TranferFunctionParam1(Gen, Kill);
        }
        public void Calculate_Reaching_Definitions(ZyroCil[] Vars)
        {
            CalculateKill(Vars);
            CalculateGen(Vars);
        }
        public void Calculate_Live_Variable(ZyroCil[] Vars)
        {

        }
    }
    public class Data_Flow_Graph
    {
       public Block ENTRY { get; set; }
       public Block EXIT { get; set; }
       public Dictionary<Block, List<Block>> Sucessor { get; set;}
       public Dictionary<Block, List<Block>> Predecessor { get; set; }
       public List<Block> Blocks { get; set; }
       public List<IThreeDirIns> threeDirIns { get; set; }
       public Data_Flow_Graph(List<IThreeDirIns> threeDirIns)
       {
            this.threeDirIns = threeDirIns;
            var LastBlock = new Block();
            Sucessor = new Dictionary<Block, List<Block>>();
            Predecessor = new Dictionary<Block, List<Block>>();
            ENTRY = new Block();
            //Pongo este bloque como el bloque inicial
            Blocks = new List<Block>() {ENTRY };
            EXIT = new Block();
            foreach (var label in threeDirIns.OfType<Label>())
            {
                Blocks.Add(new Block(new List<IThreeDirIns>() { label},label.labelCil.Name));
            }
            foreach (var tds in threeDirIns)
            {
                if (tds is Label label)
                {
                    if (LastBlock.threeDirIns.Count > 0)
                        AddBlock(LastBlock);
                    LastBlock = GetBlockWhitName(label.labelCil.Name);
                    //Para mover esto a la ulima posicion asi aseguro que los bloque esten en orden
                    Blocks.Sort(new Comparison<Block>((a, b) => (a == LastBlock) ? 1 : ((b == LastBlock) ? -1:0)));
                }
                else if (tds is Goto @goto)
                {
                    LastBlock.AddIns(@goto);
                    //Si la ultima linea es un gotoCil este no puede haber flujo desde el hacia el siguiente bloque
                    if (@goto is GotoCil gotoCil)
                        LastBlock.IsBloqued = true;
                    AddBlock(LastBlock);
                    //Guarda gotoBlock como sucesor de LastBlock y de manera contraria
                    SetSucessor(LastBlock, GetBlockWhitName(@goto.LabelCil.Name));
                    LastBlock = new Block();
                }
                else
                    LastBlock.AddIns(tds);
            }
            AddBlock(LastBlock);
            //Pongo el bloque de salida
            Blocks.Add(EXIT);
            if (Blocks.Count>=2)
            {
                var after = Blocks[0];
                foreach (var b in Blocks.Skip(1))
                {
                    if (!after.IsBloqued)
                        SetSucessor(after, b);
                    after = b;
                }
            }
       }
        public void Calculate_Reaching_Definitions()
        {
            //Lo convierto primero en un set para que las varibles iguales se traten como una y despues en array
            var Vars =threeDirIns.OfType<ZyroCil>().ToArray();
            foreach (var b in Blocks)
                b.Calculate_Reaching_Definitions(Vars);
            var tranfer_function = new Transfer_Function<Semilattice<BigInteger>, Block>((x, state) => (new SemilatticeUnion(state.Tranfer_Function_Reaching_Definitions(x.Value))));
            var IN = new Dictionary<Block, Semilattice<BigInteger>>();
            var OUT = new Dictionary<Block, Semilattice<BigInteger>>();
            Semilattice<BigInteger>.Data_Flow_algorithm(this, IN,OUT , 1, new SemilatticeUnion(0), new SemilatticeUnion(0), tranfer_function);
        }
        public void Calculate_Live_Variable()
        {
            //Lo convierto primero en un set para que las varibles iguales se traten como una y despues en array
            var Vars = threeDirIns.OfType<ZyroCil>().ToArray();
            foreach (var b in Blocks)
                b.Calculate_Live_Variable(Vars);
            var tranfer_function = new Transfer_Function<Semilattice<BigInteger>, Block>((x, state) => (new SemilatticeUnion(state.Tranfer_Function_Live_Variable(x.Value))));
            var IN = new Dictionary<Block, Semilattice<BigInteger>>();
            var OUT = new Dictionary<Block, Semilattice<BigInteger>>();
            Semilattice<BigInteger>.Data_Flow_algorithm(this, IN, OUT, -1, new SemilatticeUnion(0), new SemilatticeUnion(0), tranfer_function);
        }
        public void SetLastBlock(ref Block LastBlock, Block block)
        {
            SetSucessor(LastBlock, block);
            SetPredecessor(block, LastBlock);
            LastBlock = block;
        }
        public void SetPredecessor(Block block, Block predecessor)
        {
            if (Predecessor.ContainsKey(block))
                Predecessor[block].Add(predecessor);
            else
                Predecessor[block] = new List<Block>() { predecessor };
        }
        public void SetSucessor(Block block, Block sucessor)
        {
            if (Sucessor.ContainsKey(block))
                Sucessor[block].Add(sucessor);
            else
                Sucessor[block] = new List<Block>() { sucessor };
            SetPredecessor(sucessor, block);

        }
        public Block GetBlockWhitName(string Name)
        {
            return Blocks.Single((a) => a.Name == Name);
        }
        public void AddBlock(Block block)
        {
            if (block.Name==null)
                Blocks.Add(block);
        }
    }
    public delegate Semilattice<V> Meet_Operator<V>(V x);
    public abstract class Semilattice<V>
    {
        //public Meet_Operator<V> meet_Operator{ get; set; }
        public V Value { get; set; } 
        public Semilattice(V Value)
        {
            this.Value = Value;
        }
        public abstract Semilattice<V> Meet_Operator(Semilattice<V> x);
        public static Semilattice<V> operator ^(Semilattice<V> x,Semilattice<V>y)
        {
            return x.Meet_Operator(y);
        }
        public static bool operator <=(Semilattice<V> x, Semilattice<V> y)
        {
            if ((x ^ y) == x)
                return true;
            return false;

        }
        public static bool operator >=(Semilattice<V> x, Semilattice<V> y)
        {
            return (x==y)||!(x <= y);
        }
        public override bool Equals(object obj)
        {
            if (obj is Semilattice<V> x)
            {
                if (x == null)
                    return false;
                return Value.Equals(x.Value);
            }
            return base.Equals(obj);
        }
        public static void Data_Flow_algorithm(Data_Flow_Graph data_Flow_Graph,Dictionary<Block,Semilattice<V>> IN, Dictionary<Block, Semilattice<V>> OUT,int D, Semilattice<V> init, Semilattice<V> T,Transfer_Function<Semilattice<V>,Block> transfer_Function)
        {
            if (D>0)
                Forward_Data_Flow_algorithm(data_Flow_Graph,IN,OUT,init,T,transfer_Function);
            else
                Backward_Data_Flow_algorithm(data_Flow_Graph, IN, OUT, init, T, transfer_Function);
        }
        public static void Forward_Data_Flow_algorithm(Data_Flow_Graph data_Flow_Graph, Dictionary<Block, Semilattice<V>> IN, Dictionary<Block, Semilattice<V>> OUT, Semilattice<V> init, Semilattice<V> T, Transfer_Function<Semilattice<V>, Block> transfer_Function)
        {
            OUT[data_Flow_Graph.ENTRY] = init;
            foreach (var B in data_Flow_Graph.Blocks.Where(b => b != data_Flow_Graph.ENTRY))
                OUT[B] = T;
            bool Change = false;
            do
            {
                Change = false;
                foreach (var B in data_Flow_Graph.Blocks.Where(b => b != data_Flow_Graph.ENTRY))
                {

                    var IN_B_old = (IN.ContainsKey(B)) ? IN[B] : null;
                    var OUT_B_old = OUT[B];
                    var PredecessorOUT = data_Flow_Graph.Predecessor[B].Select(c => OUT[c]).ToArray();
                    var PredecessorLenght = PredecessorOUT.Length;
                    IN[B] = (PredecessorLenght == 1) ? PredecessorOUT[0] : PredecessorOUT.Aggregate((x, y) => (x ^ y));
                    OUT[B] = transfer_Function(IN[B], B);
                    if (!OUT[B].Equals(OUT_B_old) || !IN[B].Equals(IN_B_old))
                        Change = true;
                }
            } while (Change);
        }
        public static void Backward_Data_Flow_algorithm(Data_Flow_Graph data_Flow_Graph, Dictionary<Block, Semilattice<V>> IN, Dictionary<Block, Semilattice<V>> OUT, Semilattice<V> init, Semilattice<V> T, Transfer_Function<Semilattice<V>, Block> transfer_Function)
        {
            IN[data_Flow_Graph.EXIT] = init;
            foreach (var B in data_Flow_Graph.Blocks.Where(b => b != data_Flow_Graph.EXIT))
                IN[B] = T;
            bool Change = false;
            do
            {
                Change = false;
                foreach (var B in data_Flow_Graph.Blocks.Where(b => b != data_Flow_Graph.EXIT))
                {
                    var OUT_B_old = (OUT.ContainsKey(B)) ? OUT[B] : null;
                    var IN_B_old = IN[B];
                    var SucessorIN = data_Flow_Graph.Sucessor[B].Select(c => IN[c]).ToArray();
                    var SucessorLenght = SucessorIN.Length;
                    OUT[B] = (SucessorLenght == 1) ? SucessorIN[0] : SucessorIN.Aggregate((x, y) => (x ^ y));
                    IN[B] = transfer_Function(OUT[B], B);
                    if (!OUT[B].Equals(OUT_B_old) || !IN[B].Equals(IN_B_old))
                        Change = true;
                }
            } while (Change);
        }


    }
    public delegate V Transfer_Function<V,T>(V x,T state);
    public delegate V Transfer_Function_Basic<V, T>(T x);
    //Uso biginteger pq el numero puede tener una gran cantidad de bit
    public class SemilatticeUnion : Semilattice<BigInteger>
    {
        public SemilatticeUnion(BigInteger Value) : base(Value){}
        public override Semilattice<BigInteger> Meet_Operator(Semilattice<BigInteger> x)
        {
           return new SemilatticeUnion (Value | x.Value);
        }
    }
    
}
