using System;
using System.Numerics;
using System.Collections.Generic;
namespace Graph{
    
    class Vertex
    {
        public object Label { get; set; }

        public Vertex(object label)
        {
            Label = label;
        }
    }
}