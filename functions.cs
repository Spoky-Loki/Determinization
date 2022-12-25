using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace determinization
{
    class functions
    {
        private static List<String> alp = new List<string>();

        public static State ContainsStateWithName(String name, List<State> statesList)
        {
            foreach (var state in statesList)
            {
                if (state.getName().Equals(name))
                    return state;
            }

            return null;
        }

        public static List<State> readFromFile(String path)
        {
            List<State> res = new List<State>();
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String[] alphabet = reader.ReadLine().Split(' ');
                    alp = alphabet.ToList();
                    String line;
                    State st;

                    while ((line = reader.ReadLine()) != null)
                    {
                        String[] arr = line.Split(' ');
                        st = ContainsStateWithName(arr[1], res);
                        if (st == null)
                        {
                            st = new State(arr[1], arr[0] == "+");
                            res.Add(st);
                        }
                        else if (arr[0] == "+")
                            st.setFinal();

                        for (int i = 2; i < arr.Length; i++)
                        {
                            String[] temp = arr[i].Split(':');
                            var tempSt = ContainsStateWithName(temp[1], res);
                            if (tempSt == null)
                            {
                                tempSt = new State(temp[1], false);
                                res.Add(tempSt);
                                st.addTransition(temp[0], tempSt);
                            }
                            else
                            {
                                st.addTransition(temp[0], tempSt);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            return res;
        }

        public static void createFileWithTable(String path, List<State> listStates)
        {
            try
            {
                if (listStates.Count == 0)
                    throw new Exception("Передан пустой список состояний!");

                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    var transitions = listStates[0].getTransitions();
                    foreach (var key in transitions.Keys)
                        writer.Write(key + " ");
                    writer.WriteLine();

                    foreach (var state in listStates)
                    {
                        writer.WriteLine(state.ToString());
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }
        }

        public static List<State> determinization(List<State> listStates)
        {
            List<State> result = new List<State>() { new State(listStates[0].getName(), 
                listStates[0].getFinal()) };

            SortedSet<String> setName = new SortedSet<string>();
            StringBuilder newStateName = new StringBuilder();
            List<State> currentStates = new List<State>();
            Boolean isFinal = false;

            int index = 0;
            while (index != result.Count())
            {
                var arr = result[index].getName().Split(',');
                foreach (var sym in alp)
                {
                    foreach (var n in arr)
                    {
                        var st = listStates.Find(s => s.getName() == n);
                        if (st != null)
                        {
                            var IsContains = st.getTransitions().TryGetValue(sym, out currentStates);
                            if (IsContains)
                            {
                                foreach (var s in currentStates)
                                {
                                    if (s.getFinal())
                                        isFinal = true;
                                    setName.Add(s.getName());
                                }
                            }
                        }
                        currentStates = new List<State>();
                    }

                    foreach (var x in setName)
                        newStateName.Append(x + ',');
                    newStateName.Remove(newStateName.Length - 1, 1);

                    var name = newStateName.ToString();
                    var newState = result.Find(st => st.getName() == name);
                    if (newState == null)
                    {
                        newState = new State(name, isFinal);
                        result.Add(newState);
                    }
                    result[index].addTransition(sym, newState);
                    newStateName = new StringBuilder();
                    setName.Clear();
                    isFinal = false;
                }
                index += 1;
            }
            return result;
        }
    }
}
