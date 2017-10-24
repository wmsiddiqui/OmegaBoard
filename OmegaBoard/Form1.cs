using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmegaBoard
{
    public partial class Form1 : Form, Views.IView
    {
        private BoardManager boardManager;

        public Form1()
        {
            InitializeComponent();
            HomeBoard.WrapContents = false;
            HomeBoard.AutoScroll = true;
            boardManager = new BoardManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            boardManager.AddLane(HomeBoard, "test");
        }

        public void AddElement(string elementName)
        {
            boardManager.AddLane(HomeBoard, "test");
        }
        
    }
    class ControlWrapper
    {
        public Control Control { get; private set; }
        public ControlWrapper(Control control) { Control = control; }
    }
}
