﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04AVLTrees {

    class AVL {

        Node root;
        public AVL() {
        }
        public void Add(int data) {
            Node newItem = new Node(data);
            if(root == null) {
                root = newItem;
            } else {
                root = RecursiveInsert(root, newItem);
            }
        }
        private Node RecursiveInsert(Node current, Node n) {
            if(current == null) {
                current = n;
                return current;
            } else if(n.data < current.data) {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            } else if(n.data > current.data) {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        private Node balance_tree(Node current) {
            int b_factor = balance_factor(current);
            if(b_factor > 1) {
                if(balance_factor(current.left) > 0) {
                    current = RotateLL(current);
                } else {
                    current = RotateLR(current);
                }
            } else if(b_factor < -1) {
                if(balance_factor(current.right) > 0) {
                    current = RotateRL(current);
                } else {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        public void Delete(int target) {//and here
            root = Delete(root, target);
        }
        private Node Delete(Node current, int target) {
            Node parent;
            if(current == null) { return null; } else {
                //left subtree
                if(target < current.data) {
                    current.left = Delete(current.left, target);
                    if(balance_factor(current) == -2)//here
                    {
                        if(balance_factor(current.right) <= 0) {
                            current = RotateRR(current);
                        } else {
                            current = RotateRL(current);
                        }
                    }
                }
                //right subtree
                else if(target > current.data) {
                    current.right = Delete(current.right, target);
                    if(balance_factor(current) == 2) {
                        if(balance_factor(current.left) >= 0) {
                            current = RotateLL(current);
                        } else {
                            current = RotateLR(current);
                        }
                    }
                }
                //if target is found
                else {
                    if(current.right != null) {
                        //delete its inorder successor
                        parent = current.right;
                        while(parent.left != null) {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        if(balance_factor(current) == 2)//rebalancing
                        {
                            if(balance_factor(current.left) >= 0) {
                                current = RotateLL(current);
                            } else { current = RotateLR(current); }
                        }
                    } else {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }
        public void Find(int key) {
            if(Find(key, root).data == key) {
                Console.WriteLine("{0} was found!", key);
            } else {
                Console.WriteLine("Nothing found!");
            }
        }
        private Node Find(int target, Node current) {

            if(target < current.data) {
                if(target == current.data) {
                    return current;
                } else
                    return Find(target, current.left);
            } else {
                if(target == current.data) {
                    return current;
                } else
                    return Find(target, current.right);
            }

        }
        public void DisplayTree() {
            if(root == null) {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }

        public void PrettyPrintTree() {
            _PrettyPrintTree(root, "");
        }

        private void _PrettyPrintTree(Node root, String space) {
            if(root.left == null && root.left == null)
                Console.WriteLine("{0}{1}", space, root.data);
            else {
                if(root.left != null)
                    _PrettyPrintTree(root.left, space + "  ");
                Console.WriteLine("{0}{1}", space, root.data);
                if(root.right != null)
                    _PrettyPrintTree(root.right, space + "  ");
            }
        }
        private void InOrderDisplayTree(Node current) {
            if(current != null) {
                InOrderDisplayTree(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplayTree(current.right);
            }
        }
        private void PreOrderDisplayTree(Node current) {
            if(current != null) {
                Console.Write("({0}) ", current.data);
                PreOrderDisplayTree(current.left);
                PreOrderDisplayTree(current.right);
            }
        }
        private void PostOrderDisplayTree(Node current) {
            if(current != null) {
                PostOrderDisplayTree(current.left);
                PostOrderDisplayTree(current.right);
                Console.Write("({0}) ", current.data);
            }
        }
        private int max(int l, int r) {
            return l > r ? l : r;
        }
        private int getHeight(Node current) {
            int height = 0;
            if(current != null) {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node current) {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent) {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent) {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent) {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent) {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}
