namespace Restaurant
{
    partial class RoomSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.lbl_pageNumberDescription = new System.Windows.Forms.Label();
            this.lbl_currentPage = new System.Windows.Forms.Label();
            this.lbl_totalNumberOfPages = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_cash = new System.Windows.Forms.Button();
            this.btn_previous = new System.Windows.Forms.Button();
            this.panel_buttons = new System.Windows.Forms.Panel();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_logOut = new System.Windows.Forms.Button();
            this.nav_panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_buttons.SuspendLayout();
            this.nav_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_pageNumberDescription
            // 
            this.lbl_pageNumberDescription.AutoSize = true;
            this.lbl_pageNumberDescription.Location = new System.Drawing.Point(38, 3);
            this.lbl_pageNumberDescription.Name = "lbl_pageNumberDescription";
            this.lbl_pageNumberDescription.Size = new System.Drawing.Size(75, 13);
            this.lbl_pageNumberDescription.TabIndex = 4;
            this.lbl_pageNumberDescription.Text = "Page Number:";
            // 
            // lbl_currentPage
            // 
            this.lbl_currentPage.AutoSize = true;
            this.lbl_currentPage.Location = new System.Drawing.Point(119, 3);
            this.lbl_currentPage.Name = "lbl_currentPage";
            this.lbl_currentPage.Size = new System.Drawing.Size(12, 13);
            this.lbl_currentPage.TabIndex = 5;
            this.lbl_currentPage.Text = "x";
            // 
            // lbl_totalNumberOfPages
            // 
            this.lbl_totalNumberOfPages.AutoSize = true;
            this.lbl_totalNumberOfPages.Location = new System.Drawing.Point(154, 3);
            this.lbl_totalNumberOfPages.Name = "lbl_totalNumberOfPages";
            this.lbl_totalNumberOfPages.Size = new System.Drawing.Size(12, 13);
            this.lbl_totalNumberOfPages.TabIndex = 8;
            this.lbl_totalNumberOfPages.Text = "x";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "of";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Image = global::Restaurant.Properties.Resources.CompanyLogo;
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(664, 106);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // btn_next
            // 
            this.btn_next.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btn_next.Image = global::Restaurant.Properties.Resources.arrowNext;
            this.btn_next.Location = new System.Drawing.Point(105, 19);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(72, 72);
            this.btn_next.TabIndex = 2;
            this.btn_next.UseVisualStyleBackColor = false;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_cash
            // 
            this.btn_cash.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btn_cash.Image = global::Restaurant.Properties.Resources.cedi;
            this.btn_cash.Location = new System.Drawing.Point(852, 646);
            this.btn_cash.Name = "btn_cash";
            this.btn_cash.Size = new System.Drawing.Size(144, 72);
            this.btn_cash.TabIndex = 1;
            this.btn_cash.UseVisualStyleBackColor = false;
            this.btn_cash.Click += new System.EventHandler(this.btn_cash_Click);
            // 
            // btn_previous
            // 
            this.btn_previous.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btn_previous.Image = global::Restaurant.Properties.Resources.arrowPrevious;
            this.btn_previous.Location = new System.Drawing.Point(27, 19);
            this.btn_previous.Name = "btn_previous";
            this.btn_previous.Size = new System.Drawing.Size(72, 72);
            this.btn_previous.TabIndex = 0;
            this.btn_previous.UseVisualStyleBackColor = false;
            this.btn_previous.Click += new System.EventHandler(this.btn_previous_Click);
            // 
            // panel_buttons
            // 
            this.panel_buttons.BackColor = System.Drawing.Color.DarkGray;
            this.panel_buttons.Controls.Add(this.button17);
            this.panel_buttons.Controls.Add(this.button18);
            this.panel_buttons.Controls.Add(this.button19);
            this.panel_buttons.Controls.Add(this.button20);
            this.panel_buttons.Controls.Add(this.button21);
            this.panel_buttons.Controls.Add(this.button22);
            this.panel_buttons.Controls.Add(this.button23);
            this.panel_buttons.Controls.Add(this.button24);
            this.panel_buttons.Controls.Add(this.button9);
            this.panel_buttons.Controls.Add(this.button10);
            this.panel_buttons.Controls.Add(this.button11);
            this.panel_buttons.Controls.Add(this.button12);
            this.panel_buttons.Controls.Add(this.button13);
            this.panel_buttons.Controls.Add(this.button14);
            this.panel_buttons.Controls.Add(this.button15);
            this.panel_buttons.Controls.Add(this.button16);
            this.panel_buttons.Controls.Add(this.button8);
            this.panel_buttons.Controls.Add(this.button7);
            this.panel_buttons.Controls.Add(this.button6);
            this.panel_buttons.Controls.Add(this.button5);
            this.panel_buttons.Controls.Add(this.button4);
            this.panel_buttons.Controls.Add(this.button3);
            this.panel_buttons.Controls.Add(this.button2);
            this.panel_buttons.Controls.Add(this.button1);
            this.panel_buttons.Location = new System.Drawing.Point(12, 152);
            this.panel_buttons.Name = "panel_buttons";
            this.panel_buttons.Size = new System.Drawing.Size(984, 426);
            this.panel_buttons.TabIndex = 9;
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.SystemColors.Control;
            this.button17.Location = new System.Drawing.Point(64, 293);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(72, 72);
            this.button17.TabIndex = 23;
            this.button17.Tag = "17";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.SystemColors.Control;
            this.button18.Location = new System.Drawing.Point(178, 293);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(72, 72);
            this.button18.TabIndex = 22;
            this.button18.Tag = "18";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.SystemColors.Control;
            this.button19.Location = new System.Drawing.Point(290, 293);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(72, 72);
            this.button19.TabIndex = 21;
            this.button19.Tag = "19";
            this.button19.UseVisualStyleBackColor = false;
            this.button19.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.SystemColors.Control;
            this.button20.Location = new System.Drawing.Point(402, 293);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(72, 72);
            this.button20.TabIndex = 20;
            this.button20.Tag = "20";
            this.button20.UseVisualStyleBackColor = false;
            this.button20.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.SystemColors.Control;
            this.button21.Location = new System.Drawing.Point(513, 293);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(72, 72);
            this.button21.TabIndex = 19;
            this.button21.Tag = "21";
            this.button21.UseVisualStyleBackColor = false;
            this.button21.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button22
            // 
            this.button22.BackColor = System.Drawing.SystemColors.Control;
            this.button22.Location = new System.Drawing.Point(624, 293);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(72, 72);
            this.button22.TabIndex = 18;
            this.button22.Tag = "22";
            this.button22.UseVisualStyleBackColor = false;
            this.button22.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.SystemColors.Control;
            this.button23.Location = new System.Drawing.Point(737, 293);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(72, 72);
            this.button23.TabIndex = 17;
            this.button23.Tag = "23";
            this.button23.UseVisualStyleBackColor = false;
            this.button23.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.SystemColors.Control;
            this.button24.Location = new System.Drawing.Point(849, 293);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(72, 72);
            this.button24.TabIndex = 16;
            this.button24.Tag = "24";
            this.button24.UseVisualStyleBackColor = false;
            this.button24.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.Control;
            this.button9.Location = new System.Drawing.Point(64, 177);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(72, 72);
            this.button9.TabIndex = 15;
            this.button9.Tag = "9";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.SystemColors.Control;
            this.button10.Location = new System.Drawing.Point(178, 177);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(72, 72);
            this.button10.TabIndex = 14;
            this.button10.Tag = "10";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.SystemColors.Control;
            this.button11.Location = new System.Drawing.Point(290, 177);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(72, 72);
            this.button11.TabIndex = 13;
            this.button11.Tag = "11";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.SystemColors.Control;
            this.button12.Location = new System.Drawing.Point(402, 177);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(72, 72);
            this.button12.TabIndex = 12;
            this.button12.Tag = "12";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.SystemColors.Control;
            this.button13.Location = new System.Drawing.Point(513, 177);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(72, 72);
            this.button13.TabIndex = 11;
            this.button13.Tag = "13";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.SystemColors.Control;
            this.button14.Location = new System.Drawing.Point(624, 177);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(72, 72);
            this.button14.TabIndex = 10;
            this.button14.Tag = "14";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.SystemColors.Control;
            this.button15.Location = new System.Drawing.Point(737, 177);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(72, 72);
            this.button15.TabIndex = 9;
            this.button15.Tag = "15";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.SystemColors.Control;
            this.button16.Location = new System.Drawing.Point(849, 177);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(72, 72);
            this.button16.TabIndex = 8;
            this.button16.Tag = "16";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.Control;
            this.button8.Location = new System.Drawing.Point(849, 60);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(72, 72);
            this.button8.TabIndex = 7;
            this.button8.Tag = "8";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.Control;
            this.button7.Location = new System.Drawing.Point(737, 60);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(72, 72);
            this.button7.TabIndex = 6;
            this.button7.Tag = "7";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.Location = new System.Drawing.Point(624, 60);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(72, 72);
            this.button6.TabIndex = 5;
            this.button6.Tag = "6";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.Control;
            this.button5.Location = new System.Drawing.Point(513, 60);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(72, 72);
            this.button5.TabIndex = 4;
            this.button5.Tag = "5";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.Location = new System.Drawing.Point(402, 60);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 72);
            this.button4.TabIndex = 3;
            this.button4.Tag = "4";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Location = new System.Drawing.Point(290, 60);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 72);
            this.button3.TabIndex = 2;
            this.button3.Tag = "3";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(178, 60);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 72);
            this.button2.TabIndex = 1;
            this.button2.Tag = "2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(64, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 72);
            this.button1.TabIndex = 0;
            this.button1.Tag = "1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.dynamicBtn_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(12, 646);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(325, 23);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "SELECT A ROOM OR CASH";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(908, 630);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Cash";
            // 
            // btn_logOut
            // 
            this.btn_logOut.Location = new System.Drawing.Point(924, 12);
            this.btn_logOut.Name = "btn_logOut";
            this.btn_logOut.Size = new System.Drawing.Size(72, 72);
            this.btn_logOut.TabIndex = 17;
            this.btn_logOut.Text = "Log Out";
            this.btn_logOut.UseVisualStyleBackColor = true;
            this.btn_logOut.Click += new System.EventHandler(this.btn_logOut_Click);
            // 
            // nav_panel
            // 
            this.nav_panel.Controls.Add(this.btn_next);
            this.nav_panel.Controls.Add(this.btn_previous);
            this.nav_panel.Controls.Add(this.lbl_pageNumberDescription);
            this.nav_panel.Controls.Add(this.lbl_currentPage);
            this.nav_panel.Controls.Add(this.label1);
            this.nav_panel.Controls.Add(this.lbl_totalNumberOfPages);
            this.nav_panel.Location = new System.Drawing.Point(636, 627);
            this.nav_panel.Name = "nav_panel";
            this.nav_panel.Size = new System.Drawing.Size(200, 100);
            this.nav_panel.TabIndex = 18;
            // 
            // RoomSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.nav_panel);
            this.Controls.Add(this.btn_logOut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.panel_buttons);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_cash);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "RoomSelectionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Room Selection";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_buttons.ResumeLayout(false);
            this.nav_panel.ResumeLayout(false);
            this.nav_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_previous;
        private System.Windows.Forms.Button btn_cash;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Label lbl_pageNumberDescription;
        private System.Windows.Forms.Label lbl_currentPage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_totalNumberOfPages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_buttons;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_logOut;
        private System.Windows.Forms.Panel nav_panel;
    }
}

