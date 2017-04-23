﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04Dijkstra {
    internal class Node {
        IList<NodeConnection> _connections;

        internal string Name { get; private set; }

        internal double DistanceFromStart { get; set; }

        internal IEnumerable<NodeConnection> Connections {
            get { return _connections; }
        }

        internal Node(string name) {
            Name = name;
            _connections = new List<NodeConnection>();
        }

        internal void AddConnection(Node targetNode, double distance, bool twoWay) {
            if(targetNode == null) throw new ArgumentNullException("targetNode");
            if(targetNode == this) throw new ArgumentException("Node may not connect to itself.");
            if(distance <= 0) throw new ArgumentException("Distance must be positive.");

            _connections.Add(new NodeConnection(targetNode, distance));
            if(twoWay) targetNode.AddConnection(this, distance, false);
        }
    }
}
