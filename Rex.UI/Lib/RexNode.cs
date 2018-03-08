using Rex.Common;
using System;
using System.Windows.Forms;

namespace Rex.UI.Lib
{
    public abstract class RexNode : TreeNode
    {
        public RexNode(String text, KeySet keys)
            : base(text)
        {
            this.keys = keys;
        }

        public KeySet keys { get; private set; }

        public String Table { get; protected set; }
    }
}
