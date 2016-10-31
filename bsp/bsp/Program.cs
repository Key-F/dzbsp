﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bsp
{

    public class BSPTree {
        BSPTreePolygon root;
        BSPTreePolygon Divider;
        BSPTreePolygon[] PolygonSet;
        BSPTree RightChild;
        BSPTree LeftChild;
        public BSPTree() { } // Построение дерева 
        void Draw() { } // Алгоритм художника
    }
    public  class BSPTreePolygon {

        public double[] Point1;       // Вершина 1 в полигоне.
        public double[] Point2;       // Вершина 2 в полигоне.
        public double[] Point3;       // Вершина 3 в полигоне.
        public double testt;
        public  BSPTreePolygon() {
            Point1 = new double[3];
            Point2 = new double[3];
            Point3 = new double[3];
        }

        }
    class Program
    {
        static void Main(string[] args) {
Start:  // Ввод адреса файла
            Console.WriteLine("Введите адрес файла с координатами");
            string FileName = Console.ReadLine();
            string[] coordpov;
            if (System.IO.File.Exists(FileName) == false) { // Это тест
                Console.WriteLine("Файла не существует");
                goto Start;
            }
Start1:  // Ввод координат наблюдателя
            Console.WriteLine("Введите координаты точки обзора наблюдателя");
            string coords = Console.ReadLine();          
            coordpov = coords.Split(new char[] { ' ' });
            for (int i = 0; i < 3; i++ ) {
                try { Convert.ToDouble(coordpov[i]); }
                catch {
                    try { 
                        if (coordpov[i] == null) 
                        Console.WriteLine(""); }
                    //if (coordpov[i] == null) 
                    catch{
                        Console.WriteLine("Не хватает данных");
                        goto Start1; // Обратно к вводу координат наблюдателя
                    }
                        
                    //else
                    Console.WriteLine("Это не цифры"); // Тоже тест
                    goto Start1; // Обратно к вводу координат наблюдателя
                }
                
            }
            try {
                if (coordpov[3] != null) {
                    Console.WriteLine("Нужно ввести только 3 числа");
                    goto Start1; // Обратно к вводу координат наблюдателя
                }
            } 
            catch{
               // Console.WriteLine("Нужно ввести только 3 числа");
               // goto Start1; // Обратно к вводу координат наблюдателя
            }
            getcoords(FileName);

        }
        public static void getcoords(string FileName) {

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
                BSPTreePoly[i].Point1[0] = Convert.ToDouble(coord[0]);                
                BSPTreePoly[i].Point1[1] = Convert.ToDouble(coord[1]);               
                BSPTreePoly[i].Point1[2] = Convert.ToDouble(coord[2]); // Заполнили координаты первой точки полигона 

                BSPTreePoly[i].Point2[0] = Convert.ToDouble(coord[3]);
                BSPTreePoly[i].Point2[1] = Convert.ToDouble(coord[4]);
                BSPTreePoly[i].Point2[2] = Convert.ToDouble(coord[5]); // Заполнили координаты второй точки полигона 

                BSPTreePoly[i].Point3[0] = Convert.ToDouble(coord[6]);
                BSPTreePoly[i].Point3[1] = Convert.ToDouble(coord[7]);
                BSPTreePoly[i].Point3[2] = Convert.ToDouble(coord[8]); // Заполнили координаты третьей точки полигона 

                Console.WriteLine(i);
                i++;
            }
            Console.ReadLine();
       }
}
}