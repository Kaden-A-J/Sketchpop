using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    /// <summary>
    /// Object that holds both a coordinate point and an enum value to help us know what operation should take place
    /// </summary>
    internal class Point_Operation
    {
        public Point point { get; private set; }

        public OperationType operationType { get; private set; }

        public enum OperationType
        {
            jump,
            line_to
        }
        
        public Point_Operation(Point point, OperationType operationType)
        {
            this.point = point;
            this.operationType = operationType;
        }
    }
}
