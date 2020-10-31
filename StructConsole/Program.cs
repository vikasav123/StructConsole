using System;
using System.Linq;

namespace StructConsole
{
    
    struct Entrant
    {
        public string Name { get; set; }
        public int IdNum { get; set; }
        public double CoursePoints { get; set; }
        public double AvgPoints { get; set; }
        public ZNO[] ZNOResults { get; set; }

        public Entrant(string name, int idNum, double coursePoints, double avgPoints, ZNO[] zNoResults)
        {
            Name = name;
            IdNum = idNum;
            CoursePoints = coursePoints;
            AvgPoints = avgPoints;
            ZNOResults = zNoResults;
        }

        public double GetCompMark()
        {
            double res = CoursePoints * 0.05+AvgPoints * 0.10 + ZNOResults[0].Points * 0.25 + ZNOResults[1].Points* 0.40 + ZNOResults[2].Points* 0.20;
            return res;
        }

        public string GetBestSubject(ZNO[] zNoResults)
        {
            if (zNoResults[0].Points >= zNoResults[1].Points && zNoResults[0].Points >= zNoResults[2].Points)
                return zNoResults[0].Subject;
            else if (zNoResults[1].Points >= zNoResults[0].Points && zNoResults[1].Points >= zNoResults[2].Points)
                return zNoResults[1].Subject;
            else return zNoResults[2].Subject;
        }
        public string GetWorstSybject(ZNO[] zNoResults)
        {
            if (zNoResults[0].Points <= zNoResults[1].Points && zNoResults[0].Points <= zNoResults[2].Points)
                return zNoResults[0].Subject;
            else if (zNoResults[1].Points <= zNoResults[0].Points && zNoResults[1].Points <= zNoResults[2].Points)
                return zNoResults[1].Subject;
            else return zNoResults[2].Subject;
        }

    };

    struct ZNO
    {
        public string Subject { get; set; }
        public int Points { get; set; }

        public ZNO(string subjects, int points)
        {
            Subject = subjects;
            Points = points;

        }
    };



    class Program
    {
        static Entrant[] ReadEntratntArray()
        {
            Console.WriteLine("Введіть кількість записів");
            int n = Convert.ToInt32(Console.ReadLine());
            Entrant[] mas = new Entrant[n];
                ZNO[] res = new ZNO[3];
            for (int i = 0; i < n; i++)
            {
                
                Console.WriteLine("Введіть ім'я:");
                mas[i].Name = Console.ReadLine();
                Console.WriteLine("Введіть id:");
                mas[i].IdNum = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введіть бали за підготовчі курси");
                mas[i].CoursePoints = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Введіть бали атестату");
                mas[i].AvgPoints = Convert.ToDouble(Console.ReadLine());
                for(int j = 0; j < 3; j++)
                {
                    Console.WriteLine("Введіть назву предметів ЗНО");
                    res[j].Subject = Console.ReadLine();
                    Console.WriteLine("Введіть отримані бали");
                    res[j].Points = Convert.ToInt32(Console.ReadLine());
                    mas[i].ZNOResults = res;
                }
               
            }
        
            return mas;
        }
        static void PrintEntrant(Entrant[] mas)
        {

            for (int i = 0; i < mas.Length; i++)
            {
                Console.WriteLine($"Iм'я: {mas[i].Name}");
                Console.WriteLine($"Id: {mas[i].IdNum}");
                Console.WriteLine($"Бали за підготовчі курси - {mas[i].CoursePoints}");
                Console.WriteLine($"Бали атестату - {mas[i].AvgPoints}");
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine($"Назву предметів ЗНО {mas[i].ZNOResults[j].Subject}");
                    Console.WriteLine($"Отримані бали {mas[i].ZNOResults[j].Points}");
                }

            }
        }

        public static void GetEntrantsInfo(Entrant[] mas, out double max, out double min)
        {
            max = min = mas[0].GetCompMark();
            foreach (Entrant en in mas)
            {
                max = max < en.GetCompMark() ? en.GetCompMark() : max;
                min = min > en.GetCompMark() ? en.GetCompMark() : min;
            }
        }
        protected static bool needToReOrder(string a, string b)
        {
            for (int i = 0; i < (a.Length > b.Length ? b.Length : a.Length); i++)
            {
                if (a.ToCharArray()[i] < b.ToCharArray()[i]) return false;
                if (a.ToCharArray()[i] > b.ToCharArray()[i]) return true;
            }
            return false;
        }


        static void SortEntrantsByName(Entrant[] mas)
        {
            string tmp;
            for (int i = 1; i > mas.Length; i++)
            {
                for (int j = 1; j < mas.Length - i; j++)
                {
                    if (needToReOrder(mas[j].Name, mas[j + 1].Name))
                    {
                        tmp = mas[j].Name;
                        mas[j].Name = mas[j + 1].Name;
                        mas[j + 1].Name = tmp;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Entrant[] entrant = new Entrant[2];
            double max, min;
            entrant = ReadEntratntArray();
            PrintEntrant(entrant);
            GetEntrantsInfo(entrant,out max,out min);
            Console.WriteLine($"max resalt - {max} min result - {min}");

        }
    }
}
    
