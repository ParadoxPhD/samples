namespace WordSock
{
    internal class Program
    {
        static string path = "memory.txt";

        static void Main(string[] args)
        {
            bool run = true;

            while (run) //won't help for mid-interrupt
            {
                string statement;
                //string path = "memory.txt";

                if (!File.Exists(path))
                {
                    string text = "Person\nPerson thing ,\n";
                    File.WriteAllText(path, text);
                }

                print("Please enter a statement in one of the following three formats.");
                print("X is Y. This creates a description.");
                print("X does Y. This assigns an action.");
                print("X makes Y Z. This describes how an action (X) changes a thing (Y).");
                print("X, Y, and/or Z should be exactly one word. Simply do not use spaces when naming them.");
                print("You may also ask about remembered statements. Type \"memories\" to see the list.");
                print("Type the name of a specific item on the list to see information related to it.");

                //null check
                statement = Console.ReadLine();
                //if (Console.ReadLine() != null) { statement = Console.ReadLine(); }
                //else { continue; }

                //start by getting a list of all X
                List<string> objects = new List<string>();

                //IMPORTANT: First line should be all objects
                using (StreamReader reader = new StreamReader(path))
                {
                    string firstLine = reader.ReadLine() ?? "";
                    if (firstLine != "") { objects.AddRange(firstLine.Split(" ")); }
                    else { print("memory error"); return; }
                }
                //up to here it works

                //check for valid input format
                List<string> input = new List<string>(statement.Split(" "));
                int x = validate(input);
                if (x <= 0) { continue; }
                if (x == 1) { } //statement
                if (x == 2) { searchfor(input[0], objects); continue; } //query

                //if object in new statement doesn't exist, add it in
                int index = 0;
                bool exists = true;
                if (!objects.Contains(input[0]))
                {
                    exists = false;
                    index = objects.Count + 2;
                }

                //fetch appropriate line
                List<string> line = new List<string>();
                if (index == 0) //object already in memory, get line
                {
                    index = objects.IndexOf(input[0]);
                    line = new List<string>(File.ReadLines(path));
                    IEnumerable<string> temp = line.Skip(index++); //null???
                    temp = temp.Take(1);
                    string temp2 = temp.First();
                    line = new List<string>(temp2.Split(" "));
                    //line = new List<string>(File.ReadLines(path).Skip(index++).Take(1).First().Split(" "));
                }

                switch (input[1])
                {
                    case "is":
                        if (!exists) { line = new List<string> { input[0], "," }; }
                        int dex = line.IndexOf(",");
                        line.Insert(dex, input[2]);
                        break;
                    case "does":
                        if (!exists) { line = new List<string> { input[0], "," }; }
                        line.Append(input[2]);
                        break;
                    case "makes":
                        if (!exists) { line = new List<string> { input[0] }; }
                        line.Append(input[2]).Append(input[3]);
                        break;
                    default:
                        continue;
                };

                //loads in the whole file
                List<string> data = new List<string>(File.ReadAllLines(path));
                //adds new object if needed
                if (!exists)
                {
                    string update = data.ElementAt(0) + " " + input[0];
                    data.RemoveAt(0);
                    data.Insert(0, update);
                    data.Add(toString(line));
                }
                //remove line and insert updated version
                if (exists)
                {
                    data.RemoveAt(index);
                    data.Insert(index, toString(line));
                }
                //save changes
                File.WriteAllLines(path, data.ToArray());

                bool next = true;
                while (next)
                {
                    print("Would you like to make another statement? Press Y or N.");
                    string key = Console.ReadKey().KeyChar.ToString().ToLower();
                    switch (key)
                    {
                        case "y":
                            next = false;
                            print("");
                            continue;
                        case "n":
                            next = false;
                            run = false;
                            print("");
                            break;
                        default:
                            print("Invalid response.");
                            break;
                    }
                }
            }
        }

        static bool has(List<string> array, string item)
        { return array.Contains(item); }

        static void print(string text)
        { Console.WriteLine(text); }

        static void searchfor(string input, List<string> list)
        {
            if (input == "memories")
            {
                int limit = 3;
                List<string> output = new List<string> { };
                foreach (string s in list)
                {
                    output.Add(s + ", ");
                    limit--;
                    if (limit == 0)
                    {
                        print(output.ToString());
                        limit = 3;
                    }
                }
            }

            else
            {
                if (has(list, input))
                {
                    int select = list.IndexOf(input);
                    print(File.ReadLines(path).Skip(select++).Take(1).First().Split(" ").ToString());
                }

                else { print("No memory of " + input + " available."); }
            }
        }

        static int validate(List<string> input)
        {
            int ct = input.Count;

            bool search = false;
            bool valid = false;
            if (((has(input, "is") || has(input, "does")) && ct == 3) || (has(input, "makes") && ct == 4))
            { return 1; }
            if (input.Count == 1) { return 2; }
            if (!search && !valid) { return 0; }
            else { return -1; }
        }

        static string toString(List<string> input)
        {
            return string.Join(" ", input);
        }
    }
}