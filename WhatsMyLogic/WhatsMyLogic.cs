using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
//using System.Threading.Tasks;
using System.IO;
namespace WhatsMyLogic
{
    /// <summary>
    /// I have ramdisk and I want my panels works fast
    /// </summary>
    public class LinksFolder
    {
        private string ramdisk;
        private string linksFolder;
        public LinksFolder(string disk, string folder)
        {
            ramdisk = disk;
            linksFolder = folder;
        }

    }

    public class Sorter
    {
        public int Recognise(object obj)
        {
            int result =0;
            if (obj.GetType().ToString() == "string") { result = 1; }
            return result;
        } 
    }
    public class StoreByExn
    {
        
        private string Folder;
        private IList<string> exs;
        private bool useDatas;
        [Flags]
        public enum Options { deleteExisted, deleteOlder, donotOverride }
        // store/04.07.2019/pdf/gg.pdf
        // версии 
        public StoreByExn(string folder) { Folder = folder; exs = new List<string>(); }
        public void PutDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                var files = Directory.EnumerateFiles(directory,"*.*",SearchOption.AllDirectories);
                foreach (string filename in files)
                {
                    PutFile(filename);
                }
            }
            
        }
        public void PutFile(string filename)
        {
            int pos = -1;
            string ex = string.Empty;
            //Path для операций с именем файла
            //FileInfo
            

            if (File.Exists(filename))
            {
                pos = filename.LastIndexOf('.');
                if (pos >0)
                {
                    ex = filename.Substring(pos + 1);
                    if (exs.IndexOf(ex) >= 0) { exs.Add(ex); }
                    if (Directory.Exists(Folder))
                    {
                        if (Directory.Exists(Folder + "\\" + ex) && !File.Exists(Folder + "\\" + ex + "\\" + filename.Substring(filename.LastIndexOf("\\") + 1)))
                        {
                            File.Copy(filename, Folder + "\\" + ex + "\\" + filename.Substring(filename.LastIndexOf("\\")+1));
                        }
                        else throw new Exception("Нет папки для файлов с таким расширением или файл с таким именем существует");
                    }
                    else throw new Exception("Storage directory not exists");
                }
            }
        }
    }
    /// <summary>
    /// Money cash box language
    /// </summary>
    public class MCBL
    {
        // Биль - скандинавская богиня времени
        /* Биль, девушка (Bil) — в северно-германской мифологии девушка, дочь Vidhtinnr,
         * которую Мани (Луна) вместе с ее братом Гиуки (Hiuki), 
         * когда они оба шли с водой от колодца Быргира и несли тяжелое ведро на плечах, 
         * взяла из жалости с земли на небо и поместила у себя, где, как гласит сага, 
         * их и теперь можно видеть с земли с их ведром и коромыслом. 
         * Происхождение этой саги, равно как о похитителе дров, взятом за кражу в воскресный день на луну,
         * вызвано лунными пятнами ("Mann in Mond"). 
         * Судя по одному месту в Эддах, Б. принадлежит к Асам (см. это сл.).*/
         //
        // Написать биль, былина
        // 
        public class Lexer { }
        public class Parser
        {
            private string filename;
            public Parser(string fileName)
            {
                filename = fileName;
            }
            //на выходе - дерево команд? какой формат у дерева? 
            public Token[] Tokenize()
            {
                string[] lines = File.ReadAllLines(filename);
                //new string[1];

                //FileStream stream = new FileStream(filename,FileMode.Open);
                //stream.re
                foreach (string line in lines)
                {
                    //what's the line, recognize and add in tree
                    //is it comment?
                    //multiline comments
                    line.Trim();
                    if (line[0] == '?') { }
                    if (line.Contains("stamp")) { }
                    if (line.Contains("счет")) { }
                    //по ключевым словам?  
                }
                Token[] result = new Token[1];
                return result;
            }
        }
        public class Token
        {
            // строка - шаблон - вывод
            // нейронную сеть применить?
            // это команда? она верна? какая это команда?
            // класс {описание}
            // переменная имя значение
            // операторы = * / - + 
            // функции
        }
        public DateTime stamp;


        public string keywords = "balance debit credit saldo operation currency";
        public string rwords = "счет счёт баланс расход доход план копилка резерв";
        //public string[] words = rwords.Split(' ');

        public void Operation() { }
        public class Currency
        {
            char symbol;
           // float amount;
        }
        public class Balance { Currency currency; float amount; }
        public class Account { string name; Balance balance; }
        public class Plan { IList<Account> accounts; }
        public class Command
        {
            // команда и ее аргументы
            // параметры командной строки
            // pro.exe -filename = go.pas -r -h
            // удалять пробелы? как анализировать?
            // оказывается это называется токен (лексема), токенизация, лексические анализаторы
            // синтаксический анализ, он же парсинг
            string toparse;
            string[] args;
            IDictionary<string, string> pairs;
            // do smf
            // do that=sample
            // write saldo 
            // get balance where date="20.10.2020"
            public string verb;
            public string noun;
            // получить список задач
            // получить список задач на завтра срочные
            public Command Interprete(string command)
            {
                Command result = new Command();
                string space = " ";
                string verb=string.Empty;
                string noun=string.Empty;
                int e;
                command.Trim(); // не портить входные данные
                if (command!=string.Empty)
                {
                    e = command.IndexOf(space);
                    this.verb = command.Substring(0, e);
                    command.Trim();command.Remove(0, e);
                    e = command.IndexOf(space);
                    this.noun  = command.Substring(0, e);
                }
                result.noun = noun;
                result.verb = verb;
                //get words
                string[] words = command.Split(new string[] {" ", ","},StringSplitOptions.RemoveEmptyEntries); 
                return result;
                // : , ; как класс?
                // машина: количествоКолес 4, цвет белый, модель внедорожник;
                // как быть с числами? разделитель точка или если нет пробелов и одни цифры - то это число
                // 333,34 верно 333, 34 не верное описание класса
                // перечислить типы, описания, классы, экземпляры
                // сопоставление, шаблоны
                // считываем символ, если очередная буква - добавляем слово, если разделитель - завершаем ввод слова
                // все слова, все цифры, порядок, дерево
                // конструкции языка
                // регулярные выражения https://metanit.com/sharp/tutorial/7.4.php

            }
        }

        //public class 
    }
    
    public class Neuron
    {
        float[] ins;
        float[] outs;
        double Sigma(float x) => 1 / (1 + Math.Exp(-x));
    }

    public class Layer
    {
        string name;
        Neuron[] neurons;

        public Layer(string name, Neuron[] neurons)
        {
            this.name = name;
            this.neurons = neurons;
        }
    }

    public class Weights
    {

    }

    public class NeuronNet
    {
        Neuron[,] neurons;

    }
    public class Point2D
    {
        public int X;
        public int Y;
    }
    public class Polygone
    {
        Point2D[] vertices;
        bool ThisIsShape;
        public bool IsConvex() { return false; }
        public bool IsCorrect() { return false; }
        public bool IsPath() { return !ThisIsShape; }
        public bool IsPointInShape()
        {
            bool toret = false;
            if (IsConvex() && IsCorrect()) { toret = true; }
            return toret;
        }
    }
    

    public class Point3D
    {
        public int X;
        public int Y;
        public int Z;
        public Point3D(Point2D point2D, int Z)
        {
            X = point2D.X;
            Y = point2D.Y;
            this.Z = Z;
        }
        public Point3D(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public void MoveX(int stepX) { this.X += stepX; }
        public void MoveY(int stepY) { this.Y += stepY; }
        public void MoveZ(int stepZ) { this.Z += stepZ; }
        public void MoveXY(int stepX, int stepY)
        {
            this.X += stepX;
            this.Y += stepY;
        }
    }
    public class Vector2D
    {
        public double X;
        public double Y;
        public Vector2D(double X, double Y)
        {
            this.X = X; this.Y = Y;
        }
    }
    public class Vector3D
    {

    }
    public static class Vectors
    {
        public static Vector2D Add2D (Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2D Scale (Vector2D a, double k)
        {
            return new Vector2D(a.X * k, a.Y * k);
        }
        //public static Vector2D 
    }
    public static class Physc
    {
        
    }
    [Serializable]
    public class Graph
    {
        [XmlElement(ElementName = "id")]
        public string id;
        //read https://msdn.microsoft.com/ru-ru/library/system.xml.serialization.xmlelementattribute(v=vs.110).aspx
        [XmlElement(ElementName = "Node")]
        public Node[] nodes;
        [XmlElement(ElementName = "Edge")]
        public Edge[] edges;

        [XmlElement(ElementName = "HyperEdge")]
        public HyperEdge[] hyperEdges;

        public Graph(string id, Node[] nodes, Edge[] edges)
        {
            this.id = id;
            this.nodes = nodes;
            this.edges = edges;
        }

        public Graph(Node[] nodes, Edge[] edges)
        {
            this.nodes = nodes;
            this.edges = edges;
        }
        public Graph() { }
        
    }
    public class GraphList
    {
        List<GraphItem> Items;// = new List<GraphItem>();

        public GraphList(List<GraphItem> items)
        {
            Items = items;
        }
    }
    public class GraphItem
    {
        public string name;
        public int edgesCount;

        public GraphItem(string name, int edgesCount)
        {
            this.name = name;
            this.edgesCount = edgesCount;
        }
    }
    [Serializable]
    public class Node
    {
        [XmlAttribute]
        public string id;
        [XmlAttribute]
        public string value;
        [XmlElement]
        public Data[] data;
        public Node(string id, string value)
        {
            this.id = id;
            this.value = value;
        }
        public Node() { }
    }
    [Serializable]
    public class Edge
    {
        [XmlAttribute]
        public string id;
        [XmlAttribute]
        public bool directed;
        [XmlAttribute]
        public string source;
        [XmlAttribute]
        public string target;
        public void Reverce() { var t = source; source = target; target = t; }

        [XmlElement]
        public Data[] data;

        public Edge(string id, bool directed, string source, string target)
        {
            this.id = id;
            this.directed = directed;
            this.source = source;
            this.target = target;
        }
        public Edge() { }
    }

    [Serializable]
    public class HyperEdge
    {
        //<hyperedge>
        //  <endpoint node = "n1"/>
        //  <endpoint node = "n3"/>
        //</hyperedge>
        [XmlAttribute]
        public string id;
        [XmlElement(ElementName ="endpoint")]
        public Endpoint[] endPoints;
        public HyperEdge() { }
    }
    [Serializable]
    public class Endpoint
    {
        [XmlAttribute(AttributeName = "node")]
        public string node;
    }
    [Serializable]
    public class Data
    {
        [XmlAttribute]
        public string key;
        public Data() { }
    }
    /// <summary>
    /// Everyday you must know "tomorrow" will never comes, but "now" flowing away.
    /// May the universe grow? Or our local time is exploded in 
    /// is time important? Do not straighten this spiral
    /// Our feelings are lying to us. We can not go through. But somebody wants.   
    /// </summary>
    public class TimeGraph :  Graph
    {
        //Why the time is linear and has one direction if our universe going away. 
        //required as data key in Edge and Node
        //as an idea to fix any time event in local history log
        //public DateTime TimeStamp;
        /// <summary>
        /// No action - no time
        /// </summary>
        /// <returns>Is it frozen?What if the time is starting after key is turned on, and flows out quickly</returns>
        public bool CheckThisTime()
        {
            if (edges == null) throw new Exception("No edges found");
            return base.edges[0].data.Length > 0;
            //https://music.yandex.ru/album/2363686/track/20720123
        }
    }
    /// <summary>
    /// План счетов и проводки. Списание и пополнение. Перенаправление. Актив. Пассив. Амортизация. Увеличение стоимости
    /// </summary>
    public class PlanOfAccounts
    {

        Node[] Accounts;
        Edge[] Operations; 
        double balance;
        DateTime now;
    }
    public class Debit: Data
    {
        const string keyName = "Debit";
        public double saldo;

    }
    public class Credit: Data
    {
        const string keyName = "Credit";
        public double saldo; 
    }
    public class Account: Node
    {
        int number;
        bool isSubAccount;
        int subNumber;
        APType type;  
        Debit debit;
        Credit credit;

        public Credit Credit { get => credit; set => credit = value; }
        public Debit Debit { get => debit; set => debit = value; }
        public int SubNumber { get => subNumber; set => subNumber = value; }
        public bool IsSubAccount { get => isSubAccount; set => isSubAccount = value; }
        public int Number { get => number; set => number = value; }
        public APType Type { get => type; set => type = value; }
        public Account() { }

        public Account(Credit credit, Debit debit, int subNumber, bool isSubAccount, int number, APType type)
        {
            Credit = credit;
            Debit = debit;
            SubNumber = subNumber;
            IsSubAccount = isSubAccount;
            Number = number;
            Type = type;
        }
        public double Saldo() { return credit.saldo - debit.saldo; throw new Exception("Not implemented"); }
    }
    public class Operation: Edge
    {
        public bool Do(Account source, Account target, double amount)
        {
            return false; 
        }
    }
    public enum APType { Active, Passive, ActivePassive };
    public class Matrix //;// where T: Int16, Int32, Int64, Double, Byte; 
    {
        public int Dx = 2;
        public int Dy = 1;
        public double[,] TheMatrix;
        public Matrix(int Dx, int Dy)
        {
            TheMatrix = new double[Dx, Dy];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public double[,] Multiply(double[,] A, double[,] B)
        {
            int l = A.GetUpperBound(0);
            int m = A.GetUpperBound(1);
            int n = B.GetUpperBound(1);
            if (m != B.GetUpperBound(0)) throw new Exception("Can not mp");
            double[,] toret = new double[A.GetUpperBound(0) + 1, B.GetUpperBound(1) + 1]; //filled by zeroes by default
            int i, j, r;
            for (i = 0; i <= m; i++)
            {
                for (j = 0; j <= n; j++)
                {
                    for (r = 0; r <= n; r++) { toret[i, j] += A[i, r] * B[r, j]; }
                    //toret[i, j] = 0;
                }
            }
            return toret;
        }
        /// <summary>
        /// Summing two matrixes with the same sizes
        /// </summary>
        /// <param name="A">Matrix A</param>
        /// <param name="B">Matrix B</param>
        /// <returns></returns>
        public double[,] Sum(double[,] A, double[,] B)
        {
            if (A.GetUpperBound(1) != B.GetUpperBound(0) || A.GetUpperBound(1) != B.GetUpperBound(1)) throw new Exception("Can not sum matrixes");
            int m = A.GetUpperBound(0);
            int n = B.GetUpperBound(1);
            double[,] toret = new double[A.GetUpperBound(0) + 1, B.GetUpperBound(1) + 1];
            int i, j;
            for (i = 0; i <= m; i++)
            {
                for (j = 0; j <= n; j++)
                {
                    toret[i, j] += A[i, j] + B[i, j]; }
                //toret[i, j] = 0;
            }
            return toret;
        }
        /// <summary>
        /// Scales each element of matrix by factor k
        /// </summary>
        /// <param name="A">Matrix to scale</param>
        /// <param name="k">Scale factor</param>
        /// <returns></returns>
        public double[,] Scale(double[,] A, double k)
        {
            
            int m = A.GetUpperBound(0);
            int n = A.GetUpperBound(1);
            double[,] toret = new double[A.GetUpperBound(0) + 1, A.GetUpperBound(1) + 1];
            int i, j;
            for (i = 0; i <= m; i++)
            {
                for (j = 0; j <= n; j++)
                {
                    toret[i, j] = A[i, j]*k;
                }
                //toret[i, j] = 0;
            }
            return toret;
        }
        public double[,] Transpose(double[,] A)
        {

            int m = A.GetUpperBound(0);
            int n = A.GetUpperBound(1);
            double[,] toret = new double[n, m];
            int i, j;
            for (i = 0; i <= n; i++)
            {
                for (j = 0; j <= m; j++)
                {
                    toret[i, j] = A[j, i] ;
                }
                //toret[i, j] = 0;
            }
            return toret;
        }
    }
    public class Board
    {
        int Size; //4x4, 5x5, 6x6 
        public int[,] Cells;
        public void Add2s()
        {
            Random dr = new Random();
            //dr.Next()
            //todo check if cell is busy
            Cells[ dr.Next(0, Size), dr.Next(0, Size)] = 2;

        }
        public void ShakeIt(Side touch)
        {
            for (int i = 0; i > 0; i++)
                for (int j = 0; j < Size; j++)
                {
                    if (Cells[i, j] != 0)
                        {
                            while (Cells[i,j+1] == 0) { }
                            Cells[i, j - 1] = Cells[i, j]; Cells[i, j] = 0;
                        }
                }
        }
        public bool Move(Side whereToMove)
        {
            switch (whereToMove)
            { 
                case Side.Bottom:
                    for (int j = Size-2; j> 0; j --)
                        for (int i = 0; i < Size; i++)
                        {
                            if (Cells[i,j] == 0) { Cells[i, j - 1] = Cells[i, j]; Cells[i, j] = 0; }
                        }
                    break;
                case Side.Left: break;
                case Side.Right: break;
                case Side.Top: break;
            }
            return false;
        }
        public enum Side { Left, Right, Top, Bottom };

        public Board(int size)
        {
            Size = size;
            Cells = new int[size, size];
        }
        public void Rotate(Side side)
        {
            //it's right rotation
            int[,] t = new int[Size, Size]; 
            for (int i = 0; i<Size; i++ )
                for (int j = 0; j < Size; j++)
                {
                    t[i, j] = Cells[Size - j-1,i];

                }
            Cells = t;
        }
        public int [] GetEmptyCells()
        {
            //2d->1d and back
            int[] result = new int[Size * Size];
            int k = 0;
            for (int i = 0; i<Size; i++)
                for (int j = 0; j< Size; j++)
                {
                    if (Cells[i,j] == 0  ) {  result[k] = 1; k += 1; }
                }
            return result;
        }
    }
    public abstract class Game
    {
        public Board board;
        public abstract void Init();
        public abstract void LoadContent();
        public abstract void Update();
        public abstract void Draw();
    } 
    public abstract class GA
    {
        // бинарное в фенотип
        // 00000001 - a
        // 00000010 - b
        public abstract class Gene
        {

        }
        public Gene[] Chromosome;
        
        public abstract void CrossOver();
        public abstract void Mutate();
        public abstract void Fit();
        public enum Options { UseElitism, CrossOverOnePoint, CrossOverTwoPoints}
    }
    
}
  