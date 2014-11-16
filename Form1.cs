﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework.Forms;
using WindowsFormsApplication1.Classes;

namespace WindowsFormsApplication1
{
    public enum selected_tool {
        SQUARE,
        ELIPSE,
        GROUP,
        CAPTION,
        SELECT
    };

    public partial class Form1 : Form
    {
        private int X;
        private int Y;
        private Shape _selected = null;
        private selected_tool _cur_tool;
        private drawBoxHandler _draw_handler;
        private drawBoxHandler _draw_handler_hidden;

        public Form1()
        {
            InitializeComponent();
            init_draw_box();
            set_tool(selected_tool.SQUARE);

            draw_box_hidden.BackColor = Color.Transparent;
            draw_box_hidden.Parent = draw_box;
            draw_box_hidden.Location = new Point(0, 0);

            _draw_handler = new drawBoxHandler(this, this.draw_box, this.object_count_text);
            _draw_handler_hidden = new drawBoxHandler(this, this.draw_box_hidden, null, Color.Transparent, Pens.Aqua);
        }

        private void init_draw_box()
        {
            draw_box.Image = new Bitmap(draw_box.Width, draw_box.Height);
            draw_box_hidden.Image = new Bitmap(draw_box.Width, draw_box.Height);
        }

        private void draw_box_MouseDown(object sender, MouseEventArgs e)
        {
            this.X = e.X;
            this.Y = e.Y;
        }

        private void draw_box_MouseUp(object sender, MouseEventArgs e)
        {
            if (_selected == null)
                _draw_handler.viewClicked(X, Y, e.X, e.Y, _cur_tool);
            else
                _draw_handler_hidden.viewClicked(X, Y, e.X, e.Y, _selected);
        }

        private void set_tool(selected_tool new_tool)
        {
            applyToolStripMenuItem.Enabled = false;
            _cur_tool = new_tool;
            this.selected_tool_text.Text = "Selected tool: " + _cur_tool.ToString().ToLower();
        }

        public void item_selected(Shape item)
        {
            applyToolStripMenuItem.Enabled = true;
            _selected = item;

            toolsToolStripMenuItem.Enabled = false;

            _draw_handler_hidden.viewClicked(0, 0, 0, 0, _selected);
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.SQUARE); }
        private void elipseToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.ELIPSE); }
        private void groupToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.GROUP); }
        private void captionToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.CAPTION); }
        private void selectToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.SELECT); }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void applyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selected = null;
            _draw_handler_hidden.Clear();
            _draw_handler.Redraw();
            applyToolStripMenuItem.Enabled = true;
            toolsToolStripMenuItem.Enabled = true;
        }
    }
}