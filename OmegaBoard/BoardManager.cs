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
        Random rnd = new Random();
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


            //Add Create Button
            var createButton = new Button();
            createButton.Text = "Add Card";
            createButton.Name = "AddCardButton";
            createButton.Width = newFlow.Width - 10;
            createButton.Click += (s, e) =>
            {
                newFlow.Controls.Add(CreateDraggableButton(createButton.Width));
            };

            createButton.Click += (s, e) =>
            {
                MoveCreateButtonToBottomOfLane(newFlow, createButton);
            };

            newFlow.DragDrop += (s, e) =>
            {
                if (MouseHelper.DragThresholdMet(Control.MousePosition))
                {
                    Control userControl = GetControl(e);
                    newFlow.Controls.Add(userControl);
                    MoveCreateButtonToBottomOfLane(newFlow, createButton);
                }
                else
                {
                    //TODO: This is a click event for the button itself, e. Will need to invoke click event on e.
                }
            };

            //add controls to board
            newFlow.Controls.Add(createButton);
            newLane.Controls.Add(newFlow);
            homeBoard.Controls.Add(newLane);
        }

        private void MoveCreateButtonToBottomOfLane(FlowLayoutPanel newFlow, Button createButton)
        {
            newFlow.Controls.Remove(createButton);
            newFlow.Controls.Add(createButton);
        }

        private Control GetControl(DragEventArgs e)
        {
            ControlWrapper controlWrapper = e.Data.GetData(typeof(ControlWrapper)) as ControlWrapper;
            Control userControl = controlWrapper.Control as Control;
            return userControl;
        }

        private Button CreateDraggableButton(int width)
        {
            //create temporary button to drag
            var newButton = new Button();
            newButton.Width = width;
            newButton.Text = rnd.NextDouble().ToString();
            newButton.MouseDown += (s, e) =>
            {
                MouseHelper.MouseStartPosition = Control.MousePosition;
                newButton.DoDragDrop(new ControlWrapper(newButton), DragDropEffects.Move);
            };
            newButton.Click += (s, e) =>
            {

            };
            return newButton;
        }
    }
}
