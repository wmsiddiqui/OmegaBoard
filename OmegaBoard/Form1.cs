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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HomeBoard.WrapContents = false;
            HomeBoard.AutoScroll = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //first get total lanes
            var existingLanes = HomeBoard.Controls;
            var existingNumberofLanes = existingLanes.Count;
            //add new lane
            var newLane = new GroupBox();
            newLane.Name = "Lane" + (existingNumberofLanes + 1);
            newLane.Text = "test" + (existingNumberofLanes + 1);
            newLane.Width = 200;
            newLane.Height = HomeBoard.Height- 30;
            var newFlow = new FlowLayoutPanel();
            newFlow.Name =  "Flow" + (existingNumberofLanes + 1);
            
            newFlow.BackColor = Color.Aqua;
            newFlow.Dock = DockStyle.Fill;
            newFlow.DragEnter += (s, ee) =>
            {
                ee.Effect = DragDropEffects.Move;
            };
            newFlow.AllowDrop = true;
            newFlow.DragDrop += (s, ee) =>
            {
                Control userControl = GetControl(ee);
                newFlow.Controls.Add(userControl);
            };
            //create temporary button to drag
            var newButton = new Button();
            newButton.Width = newFlow.Width-10;
            newButton.MouseDown += (s, ee) =>
            {
                newButton.DoDragDrop(new ControlWrapper(newButton), DragDropEffects.Move);
            };

            //add controls to board
            newFlow.Controls.Add(newButton);
            newLane.Controls.Add(newFlow);
            HomeBoard.Controls.Add(newLane);
        }
        private static Control GetControl(DragEventArgs e)
        {
            ControlWrapper controlWrapper = e.Data.GetData(typeof(ControlWrapper)) as ControlWrapper;
            Control userControl = controlWrapper.Control as Control;
            return userControl;
        }
    }
    class ControlWrapper
    {
        public Control Control { get; private set; }
        public ControlWrapper(Control control) { Control = control; }
    }
}
