using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmegaBoard
{
    public class BoardManager
    {
        public void AddLane(FlowLayoutPanel homeBoard, string laneName)
        {
            //first get total lanes
            var existingLanes = homeBoard.Controls;
            var existingNumberofLanes = existingLanes.Count;
            //add new lane
            var newLane = new GroupBox();
            newLane.Name = "Lane" + (existingNumberofLanes + 1);
            newLane.Text = laneName;
            newLane.Width = 200;
            newLane.Height = homeBoard.Height - 30;
            var newFlow = new FlowLayoutPanel();
            newFlow.Name = "Flow" + (existingNumberofLanes + 1);

            newFlow.BackColor = System.Drawing.Color.Aqua;
            newFlow.Dock = DockStyle.Fill;
            newFlow.DragEnter += (s, e) =>
            {
                e.Effect = DragDropEffects.Move;
            };
            newFlow.AllowDrop = true;
            newFlow.DragDrop += (s, e) =>
            {
                Control userControl = GetControl(e);
                newFlow.Controls.Add(userControl);
            };
            //create temporary button to drag
            var newButton = new Button();
            newButton.Width = newFlow.Width - 10;
            newButton.MouseDown += (s, e) =>
            {
                newButton.DoDragDrop(new ControlWrapper(newButton), DragDropEffects.Move);
            };

            //add controls to board
            newFlow.Controls.Add(newButton);
            newLane.Controls.Add(newFlow);
            homeBoard.Controls.Add(newLane);
        }

        private Control GetControl(DragEventArgs e)
        {
            ControlWrapper controlWrapper = e.Data.GetData(typeof(ControlWrapper)) as ControlWrapper;
            Control userControl = controlWrapper.Control as Control;
            return userControl;
        }
    }
}
