using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

using OpenTK;

namespace Swarm
{
    partial class SwarmVisualizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
         
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(284, 262);
            
            this.Name = "SwarmVisualizer";
            this.Text = "Swarm Visualizer";
            this.ResumeLayout(false);

        }

        #endregion

        public int _Width = 284;
        public int _Height = 262;
    }
}