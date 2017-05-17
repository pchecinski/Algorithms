using System;

namespace StackTest {

    class Node {
        public string value;
        public Node next;
    }

    class Stack {
        Node head = null;
        int count = 0;

        public void push(string value) {
            if(head == null) {
                head = new Node();
                head.value = value;
            }
            else {
                Node tmp = head;
                head = new Node();
                head.value = value;
                head.next = tmp;
            }
            count++;
        }

        public string pop() {
            if(head == null) {
                return null;
            }
            string tmp = head.value;
            head = head.next;
            count--;
            return tmp;
        }

        public string seek() {
            return head == null ? null : head.value;
        }

        public void debug_print() {
            Console.WriteLine("Okay boi: {0}", count);
            Node tmp = head;
            while(tmp != null) {
                Console.WriteLine(tmp.value);
                tmp = tmp.next;
            }
        }
    }

    class Program {

        static void Main(string[] args) {

            //string input = "11+23+34+41";



            Stack s = new Stack();
             


            s.pop();
            s.seek();
            s.debug_print();


            s.push("112");

            s.debug_print();
            s.push("222");
            s.push("333");
            s.push("444");

            s.debug_print();

            s.pop();

            s.debug_print();

            Console.ReadKey();


        }
    }
}
