using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bsp
{

    public class BSPNode {
        public int indexofotr; // индекс отрезка (для 3d - индекс полигона)
        public BSPNode left;
        public BSPNode right;
        public BSPTreePolygon Divider; // разделитель

        public BSPNode() {
            this.indexofotr = 0;
            this.left = null;
            this.right = null;
        }

    }
    public class BSPTree {
        BSPNode root; // Корень
        BSPTreePolygon Divider;
        BSPTreePolygon[] PolygonSet;
        BSPTree RightChild;
        BSPTree LeftChild;
        BSPTreePolygon[] RightSet; // Набор отрезков справа
        BSPTreePolygon[] Leftset; // Набор отрезков слева

        public static double[] geturav(Point point1, Point point2)
        { // получаем коэффициенты общего уравнения
            double[] urav = new double[3]; // A, B, C
            urav[0] = point1.Y - point2.Y; // A = y1 - y2
            urav[1] = point2.X - point1.X; // B = x2 - x1
            urav[2] = point1.X * point2.Y - point2.X * point1.Y; // C = x1 * y2 - x2 * y1
            return urav;
        }
       /* public BSPTreePolygon[] AddToSet(BSPTreePolygon[] Parent, BSPTreePolygon[] Dobav) // Parent - то, куда добавляем. Dobav - то, что добавляем
        {

            
        } */
        public BSPTree(BSPNode root, BSPTreePolygon Div, int n) { // Div - выбранная разделяющая прямая, n - число линий

             double[] divline = geturav(Div.Point1, Div.Point2); // получаем коэффициенты уравнения прямой (по двум её точкам)
             for (int i = 0; i < n; i++)
             {
                 double Temp1 = divline[0] * PolygonSet[i].Point1.X + divline[1] * PolygonSet[i].Point1.Y + divline[2]; // A*x+B*y+C временная переменная для удобства, для точки 1 отрезка
                 double Temp2 = divline[0] * PolygonSet[i].Point2.X + divline[1] * PolygonSet[i].Point2.Y + divline[2]; // аналогично, но для второй точки отрезка
                 if ((Temp1 >= 0) && (Temp2 >= 0)) // Если A*x+B*y+C>=0 для обоих точек т.е. отрезок сверху от разделяющей прямой 
                 {
                     // Добавляем этот отрезок в RightChild (front)
                     //PolygonSet.RightSet = RightSet + PolygonSet[i];
                 }
                 if ((Temp1 < 0) && (Temp2 < 0))
                 {
                     // Добавляем этот отрезок в LeftChild (back)
                 }
                 if ((Temp1 >= 0) && (Temp2 < 0)) // Отрезок пересекается разделяющей прямой
                 {
                    // double[] otrezline = geturav(PolygonSet[i].Point1, PolygonSet[i].Point2); // Прямая, задающая отрезок (который пересекает разделяющая прямая)
                     Point DivPoint = Intersection(Div.Point1, Div.Point2, PolygonSet[i].Point1, PolygonSet[i].Point2); // Получаем пресечение отрезка и разделяющей прямой(заданной двумя точками)
                     // Добавляем отрезок (PolygonSet[i].Point1, Divpoint) в RightChild (front)
                     // Добавляем отрезок (Divpoint, PolygonSet[i].Point2) в LeftChild (back)                      
                 }  
                 if ((Temp1 < 0) && (Temp2 >= 0)) // Отрезок пересекается разделяющей прямой
                 {
                     Point DivPoint = Intersection(Div.Point1, Div.Point2, PolygonSet[i].Point1, PolygonSet[i].Point2); // Получаем пресечение отрезка и разделяющей прямой(заданной двумя точками)
                     // Добавляем отрезок (PolygonSet[i].Point1, Divpoint) в LeftChild (back)
                     // Добавляем отрезок (Divpoint, PolygonSet[i].Point2) в RightChild (front) 
                 }

             } 

        } // Построение дерева 
        void Draw() { } // Алгоритм художника
        //http://www.cyberforum.ru/csharp-beginners/thread1127196.html
          static public Point Intersection(Point A, Point B, Point C, Point D) // ищем точку пересечения двух прямых (их задают 2 точки)
          {
              double xo = A.X, yo = A.Y;
              double p = B.X - A.X, q = B.Y - A.Y;

              double x1 = C.X, y1 = C.Y;
              double p1 = D.X - C.X, q1 = D.Y - C.Y;

              double x = (xo * q * p1 - x1 * q1 * p - yo * p * p1 + y1 * p * p1) /
                  (q * p1 - q1 * p);
              double y = (yo * p * q1 - y1 * p1 * q - xo * q * q1 + x1 * q * q1) /
                  (p * q1 - p1 * q);
             
              return new Point(x, y);
          } 
    }  
    
    public  class BSPTreePolygon { // Отрезок

        public Point Point1;       // Вершина 1 в отрезке.
        public Point Point2;       // Вершина 2 в отрезке.       
       // public double testt;
        public  BSPTreePolygon() {
            Point1 = new Point();
            Point2 = new Point();           
        }

        }
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point() { }
        public Point(double x, double y)
        {
            X = x;
            Y = y;

        }
    }
    
    class Program
    {

        static string VVodFileName() { // Ввод адреса файла
            Console.WriteLine("Введите адрес файла с координатами");
            string FileName = Console.ReadLine();
            
            if (System.IO.File.Exists(FileName) == false)
            { // Это тест
                Console.WriteLine("Файла не существует");
                VVodFileName();
            }
            return FileName;
        }

        static string[] VVodCoordPOV() { // Ввод координат наблюдателя
            Console.WriteLine("Введите координаты точки обзора наблюдателя");
            string coords = Console.ReadLine();
            string[] coordpov;
            coordpov = coords.Split(new char[] { ' ' });
            for (int i = 0; i < 3; i++)  // Пока оставил для 3х мерного случая
            {
                try { Convert.ToDouble(coordpov[i]); }
                catch
                {
                    try
                    {
                        if (coordpov[i] == null)
                            Console.WriteLine("");
                    }
                    //if (coordpov[i] == null) 
                    catch
                    {
                        Console.WriteLine("Не хватает данных");
                        VVodCoordPOV(); // Обратно к вводу координат наблюдателя
                    }

                    //else
                    Console.WriteLine("Это не цифры"); // Тоже тест
                    VVodCoordPOV(); // Обратно к вводу координат наблюдателя
                }

            }
            try
            {
                if (coordpov[3] != null)
                {
                    Console.WriteLine("Нужно ввести только 3 числа");
                    VVodCoordPOV(); // Обратно к вводу координат наблюдателя
                }
            }
            catch
            {
                // Console.WriteLine("Нужно ввести только 3 числа");
                // goto Start1; // Обратно к вводу координат наблюдателя
            }
            return coordpov;
        }
        static void Main(string[] args) {
            string FileName = VVodFileName();
            string[] coordpov = VVodCoordPOV();
            
            //BSPTreePoly[1]
            BSPTreePolygon[] BSPTreePolygon = getcoords(FileName);


        }
        // потом перенести geturav
        public static int[] geturav(int[] ycoords, int[] xcoords) { // получаем коэффициенты общего уравнения
            int[] urav = new int[3]; // A, B, C
            urav[0] = ycoords[0] - ycoords[1]; // A = y1 - y2
            urav[1] = xcoords[1] - xcoords[0]; // B = x2 - x1
            urav[2] = xcoords[0] * ycoords[1] - xcoords[1] * ycoords[0]; // C = x1 * y2 - x2 * y1
            return urav;        
        }


        public static BSPTreePolygon[] getcoords(string FileName)
        {

            string[] lines = File.ReadAllLines(FileName);
            string[] coord;
            int i = 0; // Счетчик для полигонов
            // int k = 0; // Счетчик для координат
            int count = System.IO.File.ReadAllLines(FileName).Length; // Количество строк в файле
            BSPTreePolygon[] BSPTreePoly = new BSPTreePolygon[count]; // Массив полигонов
            foreach (string s in lines) {

                coord = s.Split(new char[] { ' ' });

                BSPTreePoly[i] = new BSPTreePolygon();
                //double teee = Convert.ToDouble(coord[0]); 
                //BSPTreePoly[i].testt = Convert.ToDouble(coord[0]);   
                BSPTreePoly[i].Point1.X = Convert.ToDouble(coord[0]);                
                BSPTreePoly[i].Point1.Y = Convert.ToDouble(coord[1]);               
                // BSPTreePoly[i].Point1.Z = Convert.ToDouble(coord[2]); // Заполнили координаты первой точки отрезка

                BSPTreePoly[i].Point2.X = Convert.ToDouble(coord[3]);
                BSPTreePoly[i].Point2.Y = Convert.ToDouble(coord[4]);
                //BSPTreePoly[i].Point2.Z = Convert.ToDouble(coord[5]); // Заполнили координаты второй точки отрезка

                //BSPTreePoly[i].Point3.X = Convert.ToDouble(coord[6]);
                //BSPTreePoly[i].Point3.Y = Convert.ToDouble(coord[7]);
                //BSPTreePoly[i].Point3.Z = Convert.ToDouble(coord[8]); // Заполнили координаты третьей точки полигона 

                Console.WriteLine(i);
                i++;
            }
            return BSPTreePoly;
            Console.ReadLine();
       }
}
}
