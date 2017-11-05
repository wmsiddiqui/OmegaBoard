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

        private Random rnd = new Random();
        private FlowLayoutPanel _homeBoard;


        public BoardManager(FlowLayoutPanel homeBoard)
        {
            _homeBoard = homeBoard;
        }
        public void AddLane(string laneName)
        {
            //first get total lanes
            var existingLanes = _homeBoard.Controls;
            var existingNumberofLanes = existingLanes.Count;
            //add new lane
            var newLane = new GroupBox();
            newLane.Name = "Lane" + (existingNumberofLanes + 1);
            newLane.Text = laneName;
            newLane.Width = 200;
            newLane.Height = _homeBoard.Height - 30;
            var newFlow = new FlowLayoutPanel();
            newFlow.Name = "Flow" + (existingNumberofLanes + 1);
            newFlow.AutoScroll = false;
            newFlow.HorizontalScroll.Enabled = false;
            newFlow.HorizontalScroll.Visible = false;
            newFlow.HorizontalScroll.Maximum = 0;
            newFlow.AutoScroll = true;
            newFlow.FlowDirection = FlowDirection.TopDown;
            newFlow.WrapContents = false;
            newFlow.BackColor = System.Drawing.Color.Aqua;
            newFlow.Dock = DockStyle.Fill;
            newFlow.ClientSizeChanged += (s, e) =>
            {
                if (newFlow.VerticalScroll.Visible)
                {
                    foreach (var control in newFlow.Controls)
                    {
                        var controlButton = (Control)control;
                        controlButton.Width = newFlow.Width - 25;
                    }
                }
                else
                {
                    foreach (var control in newFlow.Controls)
                    {
                        var controlButton = (Control)control;
                        controlButton.Width = newFlow.Width - 15;
                    }
                }
            };
            newFlow.DragEnter += (s, e) =>
            {
                e.Effect = DragDropEffects.Move;
            };
            newFlow.AllowDrop = true;


            //Add Create Button
            var createButton = new Button();
            createButton.Text = "Add Card";
            createButton.Name = "AddCardButton";
            createButton.Width = newFlow.Width - 15;
            createButton.Click += (s, e) =>
            {
                newFlow.Controls.Add(CreateDraggableButton(createButton.Width));
                MoveCreateButtonToBottomOfLane(newFlow, createButton);
            };

            newFlow.DragDrop += (s, e) =>
            {
                if (MouseHelper.DragThresholdMet(Control.MousePosition))
                {
                    DragDropMoveCard(e, newFlow, createButton);
                }
                else
                {
                    //TODO: This is a click event for the button itself, e. Will need to invoke click event on e.
                }
            };

            //add controls to board
            newFlow.Controls.Add(createButton);
            newLane.Controls.Add(newFlow);
            _homeBoard.Controls.Add(newLane);
        }

        private void DragDropMoveCard(DragEventArgs e, FlowLayoutPanel newFlow, Button createButton)
        {
            Control userControl = GetControl(e);
            newFlow.Controls.Add(userControl);
            var point = newFlow.PointToClient(new System.Drawing.Point(e.X, e.Y));
            Control item = GetClosestChildItem(newFlow, point);
            if (item != null)
            {
                var index = newFlow.Controls.GetChildIndex(item);
                newFlow.Controls.SetChildIndex(userControl, index);
            }
            userControl.Width = newFlow.VerticalScroll.Visible ? newFlow.Width - 25 : newFlow.Width - 15;
            MoveCreateButtonToBottomOfLane(newFlow, createButton);
        }

        private Control GetClosestChildItem(FlowLayoutPanel newFlow, System.Drawing.Point point)
        {
            var childItem = newFlow.GetChildAtPoint(point);
            if(childItem == null)
            {
                var tempPoint = new System.Drawing.Point(point.X, point.Y + 5);
                childItem = newFlow.GetChildAtPoint(tempPoint);
            }

            return childItem;
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
