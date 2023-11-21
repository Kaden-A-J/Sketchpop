using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchpop
{

    public class Operation_Manager
    {
        private Dictionary<int, DoubleStack> stack_maps;
        private Stack<int> redo_stack;
        private Stack<int> undo_stack;
        public bool undo_stack_empty { get { return undo_stack.Count == 1; } }
        public bool redo_stack_empty { get { return redo_stack.Count == 0; } }
        //constructor
        public Operation_Manager()
        {
            stack_maps = new Dictionary<int, DoubleStack>();
            undo_stack = new Stack<int>();
            redo_stack = new Stack<int>();
        }

        // when canvas is changed, add a new snapshot into the stack
        public void save_snapshot_into_stack(int layer_index, float opacity, byte[] data)
        {
            undo_stack.Push(layer_index);
            add_operation((layer_index, opacity, data, "canvas"));
        }

        // when an add layer operation is performed, we need to add a new double stack
        // we need to assign a layer label to the new layer
        public void setup_new_layer_into_stack(int layer_index, float opacity, byte[] data)
        {
            // hold the whole stack
            undo_stack.Push(layer_index);
            // hold the corresponding double stack
            add_operation((layer_index, opacity, data, "add"));
        }

        public void delete_layer(int layer_index)
        {
            // hold the whole stack
            undo_stack.Push(layer_index);
            // hold the corresponding double stack
            add_operation((layer_index, 0, null, "delete"));
        }

        // add a new snapshot into the correponding stack
        private void add_operation((int, float, byte[], string) info)
        {
            int layer_index = info.Item1;
            if (stack_maps.ContainsKey(layer_index))
            {
                stack_maps[layer_index].undo_push(info);
            }
            else
            {
                stack_maps.Add(layer_index, new DoubleStack());
                stack_maps[layer_index].undo_push(info);
            }
            print_count("add operation");
        }

        public void print_count(string method)
        {
            Console.WriteLine(method);
            Console.WriteLine("sole redo count: " + redo_stack.Count);
            Console.WriteLine("sole undo count: " + undo_stack.Count);
            Console.WriteLine("all redo count: " + get_all_redo_count());
            Console.WriteLine("all undo count: " + get_all_undo_count());
        }

        public (int, float, byte[], string) undo_operation()
        {


            if (undo_stack.Count > 0)
            {
                var temp = undo_stack.Pop();
                redo_stack.Push(temp);
                int layer_index = temp;
                var final = stack_maps[layer_index].undo();
                return final;
            }
            else
            {
                return (-2, 0, null, null);
            }
        }

        public (int, float, byte[], string) redo_operation()
        {
            if (redo_stack.Count > 0)
            {
                var temp = redo_stack.Pop();
                undo_stack.Push(temp);
                int layer_index = temp;
                var final = stack_maps[layer_index].redo();
                return final;
            }
            else
            {
                return (-2, 0, null, null);
            }
        }

        public void clear_redo()
        {
            redo_stack.Clear();
        }

        public void clear_undo()
        {
            undo_stack.Clear();
        }

        public int peek_undo_index()
        {
            return undo_stack.Peek();

        }

        public int get_all_redo_count()
        {
            int count = 0;
            foreach (var pair in stack_maps)
            {
                count += pair.Value.get_redo_count();

            }
            return count;
        }

        public int get_all_undo_count()
        {
            int count = 0;
            foreach (var pair in stack_maps)
            {
                count += pair.Value.get_undo_count();

            }
            return count;
        }
        // clear redo stack when a new operation is performed
        public void clear_redo_stack()
        {
            foreach (var item in redo_stack)
            {
                int label = item;
                if (!stack_maps[label].redo_empty())
                    stack_maps[label].redo_clear();
            }
            redo_stack.Clear();
        }

        private class DoubleStack
        {
            private Stack<(int, float, byte[], string)> undo_stack;
            private Stack<(int, float, byte[], string)> redo_stack;

            public DoubleStack()
            {
                undo_stack = new Stack<(int, float, byte[], string)>();
                redo_stack = new Stack<(int, float, byte[], string)>();
            }

            public (int, float, byte[], string) undo()
            {
                if (undo_stack.Count > 0)
                {
                    var temp = undo_stack.Pop();
                    Console.WriteLine("undo count: " + undo_stack.Count);
                    redo_stack.Push(temp);
                    Console.WriteLine("undo operation: " + temp.Item4);
                    if (temp.Item4.Equals("delete"))
                    {
                        return (undo_stack.Peek().Item1, undo_stack.Peek().Item2, undo_stack.Peek().Item3, "delete");
                    }
                    else if (undo_stack.Count > 0 && undo_stack.Peek().Item4.Equals("canvas"))
                        return undo_stack.Peek();
                    else if (undo_stack.Count > 0 && undo_stack.Peek().Item4.Equals("add"))
                    {
                        (int, float, byte[], string) temp2 = (temp.Item1, 1, null, "empty");
                        return temp2;
                    }
                    else
                        return temp;
                }
                else
                {
                    Console.WriteLine("empty undo_stack");
                    return (-2, 0, null, null);
                }
            }

            public (int, float, byte[], string) redo()
            {
                if (redo_stack.Count > 0)
                {
                    var temp = redo_stack.Pop();
                    undo_stack.Push(temp);
                    return temp;
                }
                else return (-2, 0, null, null);
            }

            public void undo_push((int, float, byte[], string) item)
            {
                undo_stack.Push(item);
            }

            public (int, float, byte[], string) undo_pop()
            {
                if (undo_stack.Count == 0) return (-2, 0, null, null);
                else return undo_stack.Pop();
            }

            public (int, float, byte[], string) undo_peek()
            {
                if (undo_stack.Count == 0) return (-2, 0, null, null);
                else return undo_stack.Peek();
            }

            public void undo_clear()
            {
                undo_stack.Clear();
            }

            public bool undo_empty()
            {
                return undo_stack.Count == 1;
            }

            public void redo_push((int, float, byte[], string) item)
            {
                redo_stack.Push(item);
            }

            public (int, float, byte[], string) redo_pop()
            {
                if (redo_stack.Count == 0) return (-2, 0, null, null);
                else return redo_stack.Pop();
            }

            public (int, float, byte[], string) redo_peek()
            {
                if ((redo_stack.Count == 0)) return (-2, 0, null, null);
                else return redo_stack.Peek();
            }

            public void redo_clear()
            {
                redo_stack.Clear();
            }

            public bool redo_empty()
            {
                return redo_stack.Count == 0;
            }


            public int get_undo_count()
            {
                return undo_stack.Count;
            }

            public int get_redo_count()
            {
                return redo_stack.Count;
            }
        }
    }
}

