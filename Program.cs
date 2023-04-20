namespace CheckTuring
{
    internal class Program
    {
        static void Check()
        {
            string alphabet = "";
            var states = new Dictionary<(string, string), string[]>();

            // Проверка входных данных.
            Console.Write("Введите количество строк с описанием Машины Тьюринга: ");
            int m = int.Parse(Console.ReadLine());

            Console.Write("Введите разделитель для ленты(не пробел): ");
            char del = Console.ReadLine().First();


            Console.WriteLine($"Введите {m} строк вида: текущее состояние, входной символ -> новое состояние, выходной символ, смещение " +
                $"(q1 a -> q2 b +1)");

            for(var i = 0; i < m; i++)
            {
                var s = Console.ReadLine();
                var ss = s.Split(' ');
                
                try
                {
                    if (!alphabet.Contains(ss[1])) alphabet += ss[1];
                    if (!alphabet.Contains(ss[4])) alphabet += ss[4];


                    states.Add((ss[0], ss[1].ToString()), new string[] { ss[3], ss[4], ss[5]});
                }
                catch (IndexOutOfRangeException) 
                { 
                    Console.WriteLine("Некорректно введённая строка. Попробуйте снова.");
                    return;
                }
                

            }

            Console.Write("Введите длину ленты: ");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите данные на ленте: ");
            string data = Console.ReadLine();

            if (data.Length > n)
               throw new Exception("Данные на ленте не могут превышать её длину.");

            foreach (var c in data)
                if (!alphabet.Contains(c.ToString()))
                    throw new Exception("Алфавит не содержит данных, указанных в строке.");

            data += new string(del, n - data.Length);
            var arr_data = data.Select(x => x.ToString()).ToArray();

            Console.Write("Введите начальное состояние: ");
            string start_st = Console.ReadLine();
            if (!states.Keys.Select(x => x.Item1).Contains(start_st))
                throw new Exception("Начадьное состояние не было указано в описании программы.");

            Console.Write("Введите конечное состояние: ");
            string last_st = Console.ReadLine();

            // Проверка входных данных окончена.

            int idx = 0;
            int iteration = 0;
            var max_iters = n * states.Keys.Select(x => x.Item1).Distinct().Count() * Math.Pow(alphabet.Length, n);

            Console.WriteLine();

            while (true)
            {
                if (start_st == last_st)
                {
                    Console.WriteLine("OK");
                    return;
                }    
                    

                if (states.TryGetValue((start_st, arr_data[idx]), out string[] val))
                    (arr_data[idx], start_st, idx) = (val[1], val[0], int.Parse(val[2]));
                else
                {
                    Console.WriteLine($"Вы не указали вариант, когда состояние {start_st} встречает символ {arr_data[idx]}.\nПопробуйте снова.");
                    return;
                }

                if (idx < 0) idx = arr_data.Length - 1;
                if (idx == arr_data.Length) idx = 0;

                iteration++;

                if (iteration == max_iters)
                {
                    Console.WriteLine("Not OK");
                    return;
                }
            }
        }

        static void Main()
        {
            Check();
        }
    }
}