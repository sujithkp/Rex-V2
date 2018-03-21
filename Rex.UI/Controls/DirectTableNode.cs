using Rex.UI.Lib;

namespace Rex.UI.Controls
{
    public class DirectTableNode : RexNode
    {
        public DirectTableNode(string text) : base(text, null)
        {
            this.Text = "~" + text;
        }
    }
}
