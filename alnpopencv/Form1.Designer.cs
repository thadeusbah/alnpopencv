namespace OCRSharp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCtrl = new System.Windows.Forms.TabPage();
            this.areay = new System.Windows.Forms.TextBox();
            this.areax = new System.Windows.Forms.TextBox();
            this.R = new System.Windows.Forms.Button();
            this.Q = new System.Windows.Forms.Button();
            this.P = new System.Windows.Forms.Button();
            this.O = new System.Windows.Forms.Button();
            this.N = new System.Windows.Forms.Button();
            this.M = new System.Windows.Forms.Button();
            this.L = new System.Windows.Forms.Button();
            this.K = new System.Windows.Forms.Button();
            this.cmdJ = new System.Windows.Forms.Button();
            this.cmdI = new System.Windows.Forms.Button();
            this.H = new System.Windows.Forms.Button();
            this.G = new System.Windows.Forms.Button();
            this.F = new System.Windows.Forms.Button();
            this.E = new System.Windows.Forms.Button();
            this.D = new System.Windows.Forms.Button();
            this.C = new System.Windows.Forms.Button();
            this.B = new System.Windows.Forms.Button();
            this.A = new System.Windows.Forms.Button();
            this.cmbPos = new System.Windows.Forms.ComboBox();
            this.postext = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lptext = new System.Windows.Forms.TextBox();
            this.idcardtext = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.receivelptext = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbID = new System.Windows.Forms.ComboBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ReceiveCheck = new System.Windows.Forms.RadioButton();
            this.SendCheck = new System.Windows.Forms.RadioButton();
            this.indate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRecog = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabDb = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabCtrl.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabDb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCtrl);
            this.tabControl1.Controls.Add(this.tabDb);
            this.tabControl1.Location = new System.Drawing.Point(11, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(841, 524);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabCtrl
            // 
            this.tabCtrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabCtrl.Controls.Add(this.groupBox7);
            this.tabCtrl.Controls.Add(this.groupBox6);
            this.tabCtrl.Controls.Add(this.cmbPos);
            this.tabCtrl.Controls.Add(this.postext);
            this.tabCtrl.Controls.Add(this.groupBox5);
            this.tabCtrl.Controls.Add(this.groupBox4);
            this.tabCtrl.Controls.Add(this.cmbID);
            this.tabCtrl.Controls.Add(this.pictureBox4);
            this.tabCtrl.Controls.Add(this.groupBox3);
            this.tabCtrl.Controls.Add(this.indate);
            this.tabCtrl.Controls.Add(this.label4);
            this.tabCtrl.Controls.Add(this.groupBox2);
            this.tabCtrl.Controls.Add(this.groupBox1);
            this.tabCtrl.Location = new System.Drawing.Point(4, 22);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.tabCtrl.Size = new System.Drawing.Size(833, 498);
            this.tabCtrl.TabIndex = 0;
            this.tabCtrl.Text = "Control";
            this.tabCtrl.UseVisualStyleBackColor = true;
            // 
            // areay
            // 
            this.areay.Location = new System.Drawing.Point(7, 46);
            this.areay.Name = "areay";
            this.areay.Size = new System.Drawing.Size(48, 22);
            this.areay.TabIndex = 20;
            this.areay.Text = "3000";
            this.areay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // areax
            // 
            this.areax.Location = new System.Drawing.Point(6, 16);
            this.areax.Name = "areax";
            this.areax.Size = new System.Drawing.Size(49, 22);
            this.areax.TabIndex = 19;
            this.areax.Text = "600";
            this.areax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // R
            // 
            this.R.Location = new System.Drawing.Point(69, 273);
            this.R.Name = "R";
            this.R.Size = new System.Drawing.Size(30, 22);
            this.R.TabIndex = 18;
            this.R.Text = "R";
            this.R.UseVisualStyleBackColor = true;
            this.R.Click += new System.EventHandler(this.R_Click);
            // 
            // Q
            // 
            this.Q.Location = new System.Drawing.Point(18, 273);
            this.Q.Name = "Q";
            this.Q.Size = new System.Drawing.Size(30, 22);
            this.Q.TabIndex = 18;
            this.Q.Text = "Q";
            this.Q.UseVisualStyleBackColor = true;
            this.Q.Click += new System.EventHandler(this.Q_Click);
            // 
            // P
            // 
            this.P.Location = new System.Drawing.Point(69, 245);
            this.P.Name = "P";
            this.P.Size = new System.Drawing.Size(30, 22);
            this.P.TabIndex = 18;
            this.P.Text = "P";
            this.P.UseVisualStyleBackColor = true;
            this.P.Click += new System.EventHandler(this.P_Click);
            // 
            // O
            // 
            this.O.Location = new System.Drawing.Point(18, 245);
            this.O.Name = "O";
            this.O.Size = new System.Drawing.Size(30, 22);
            this.O.TabIndex = 18;
            this.O.Text = "O";
            this.O.UseVisualStyleBackColor = true;
            this.O.Click += new System.EventHandler(this.O_Click);
            // 
            // N
            // 
            this.N.Location = new System.Drawing.Point(69, 217);
            this.N.Name = "N";
            this.N.Size = new System.Drawing.Size(30, 22);
            this.N.TabIndex = 18;
            this.N.Text = "N";
            this.N.UseVisualStyleBackColor = true;
            this.N.Click += new System.EventHandler(this.N_Click);
            // 
            // M
            // 
            this.M.Location = new System.Drawing.Point(18, 217);
            this.M.Name = "M";
            this.M.Size = new System.Drawing.Size(30, 22);
            this.M.TabIndex = 18;
            this.M.Text = "M";
            this.M.UseVisualStyleBackColor = true;
            this.M.Click += new System.EventHandler(this.M_Click);
            // 
            // L
            // 
            this.L.Location = new System.Drawing.Point(69, 172);
            this.L.Name = "L";
            this.L.Size = new System.Drawing.Size(30, 22);
            this.L.TabIndex = 18;
            this.L.Text = "L";
            this.L.UseVisualStyleBackColor = true;
            this.L.Click += new System.EventHandler(this.L_Click);
            // 
            // K
            // 
            this.K.Location = new System.Drawing.Point(18, 172);
            this.K.Name = "K";
            this.K.Size = new System.Drawing.Size(30, 22);
            this.K.TabIndex = 18;
            this.K.Text = "K";
            this.K.UseVisualStyleBackColor = true;
            this.K.Click += new System.EventHandler(this.K_Click);
            // 
            // cmdJ
            // 
            this.cmdJ.Location = new System.Drawing.Point(69, 145);
            this.cmdJ.Name = "cmdJ";
            this.cmdJ.Size = new System.Drawing.Size(30, 22);
            this.cmdJ.TabIndex = 18;
            this.cmdJ.Text = "J";
            this.cmdJ.UseVisualStyleBackColor = true;
            this.cmdJ.Click += new System.EventHandler(this.cmdJ_Click);
            // 
            // cmdI
            // 
            this.cmdI.Location = new System.Drawing.Point(18, 145);
            this.cmdI.Name = "cmdI";
            this.cmdI.Size = new System.Drawing.Size(30, 22);
            this.cmdI.TabIndex = 18;
            this.cmdI.Text = "I";
            this.cmdI.UseVisualStyleBackColor = true;
            this.cmdI.Click += new System.EventHandler(this.cmdI_Click);
            // 
            // H
            // 
            this.H.Location = new System.Drawing.Point(69, 118);
            this.H.Name = "H";
            this.H.Size = new System.Drawing.Size(30, 22);
            this.H.TabIndex = 18;
            this.H.Text = "H";
            this.H.UseVisualStyleBackColor = true;
            this.H.Click += new System.EventHandler(this.H_Click);
            // 
            // G
            // 
            this.G.Location = new System.Drawing.Point(18, 118);
            this.G.Name = "G";
            this.G.Size = new System.Drawing.Size(30, 22);
            this.G.TabIndex = 18;
            this.G.Text = "G";
            this.G.UseVisualStyleBackColor = true;
            this.G.Click += new System.EventHandler(this.G_Click);
            // 
            // F
            // 
            this.F.Location = new System.Drawing.Point(69, 78);
            this.F.Name = "F";
            this.F.Size = new System.Drawing.Size(30, 22);
            this.F.TabIndex = 18;
            this.F.Text = "F";
            this.F.UseVisualStyleBackColor = true;
            this.F.Click += new System.EventHandler(this.F_Click);
            // 
            // E
            // 
            this.E.Location = new System.Drawing.Point(18, 77);
            this.E.Name = "E";
            this.E.Size = new System.Drawing.Size(30, 22);
            this.E.TabIndex = 18;
            this.E.Text = "E";
            this.E.UseVisualStyleBackColor = true;
            this.E.Click += new System.EventHandler(this.E_Click);
            // 
            // D
            // 
            this.D.Location = new System.Drawing.Point(69, 49);
            this.D.Name = "D";
            this.D.Size = new System.Drawing.Size(30, 22);
            this.D.TabIndex = 18;
            this.D.Text = "D";
            this.D.UseVisualStyleBackColor = true;
            this.D.Click += new System.EventHandler(this.D_Click);
            // 
            // C
            // 
            this.C.Location = new System.Drawing.Point(18, 49);
            this.C.Name = "C";
            this.C.Size = new System.Drawing.Size(30, 22);
            this.C.TabIndex = 18;
            this.C.Text = "C";
            this.C.UseVisualStyleBackColor = true;
            this.C.Click += new System.EventHandler(this.C_Click);
            // 
            // B
            // 
            this.B.Location = new System.Drawing.Point(69, 21);
            this.B.Name = "B";
            this.B.Size = new System.Drawing.Size(30, 22);
            this.B.TabIndex = 18;
            this.B.Text = "B";
            this.B.UseVisualStyleBackColor = true;
            this.B.Click += new System.EventHandler(this.B_Click);
            // 
            // A
            // 
            this.A.Location = new System.Drawing.Point(18, 21);
            this.A.Name = "A";
            this.A.Size = new System.Drawing.Size(30, 22);
            this.A.TabIndex = 18;
            this.A.Text = "A";
            this.A.UseVisualStyleBackColor = true;
            this.A.Click += new System.EventHandler(this.A_Click);
            // 
            // cmbPos
            // 
            this.cmbPos.FormattingEnabled = true;
            this.cmbPos.Location = new System.Drawing.Point(600, 416);
            this.cmbPos.Name = "cmbPos";
            this.cmbPos.Size = new System.Drawing.Size(89, 21);
            this.cmbPos.TabIndex = 17;
            // 
            // postext
            // 
            this.postext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.postext.Location = new System.Drawing.Point(318, 455);
            this.postext.Name = "postext";
            this.postext.Size = new System.Drawing.Size(28, 26);
            this.postext.TabIndex = 16;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox5.Controls.Add(this.listBox1);
            this.groupBox5.Controls.Add(this.pictureBox2);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(12, 322);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(149, 163);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "LP Proccessing";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(6, 129);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(126, 20);
            this.listBox1.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(6, 49);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(126, 40);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Isolated Template";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "OCR Result";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.MistyRose;
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.lptext);
            this.groupBox4.Controls.Add(this.idcardtext);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.receivelptext);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(167, 322);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(145, 165);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(3, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 16);
            this.label12.TabIndex = 13;
            this.label12.Text = "ID CARD";
            // 
            // lptext
            // 
            this.lptext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lptext.Location = new System.Drawing.Point(6, 25);
            this.lptext.Name = "lptext";
            this.lptext.Size = new System.Drawing.Size(133, 29);
            this.lptext.TabIndex = 4;
            // 
            // idcardtext
            // 
            this.idcardtext.AcceptsReturn = true;
            this.idcardtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idcardtext.Location = new System.Drawing.Point(6, 133);
            this.idcardtext.Name = "idcardtext";
            this.idcardtext.ReadOnly = true;
            this.idcardtext.Size = new System.Drawing.Size(133, 26);
            this.idcardtext.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "IN LP";
            // 
            // receivelptext
            // 
            this.receivelptext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receivelptext.Location = new System.Drawing.Point(6, 76);
            this.receivelptext.Name = "receivelptext";
            this.receivelptext.Size = new System.Drawing.Size(133, 26);
            this.receivelptext.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(3, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "OUT LP";
            // 
            // cmbID
            // 
            this.cmbID.FormattingEnabled = true;
            this.cmbID.Location = new System.Drawing.Point(600, 385);
            this.cmbID.Name = "cmbID";
            this.cmbID.Size = new System.Drawing.Size(90, 21);
            this.cmbID.TabIndex = 12;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox4.Location = new System.Drawing.Point(369, 322);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(219, 155);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.ReceiveCheck);
            this.groupBox3.Controls.Add(this.SendCheck);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(594, 320);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(97, 60);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control Mode";
            // 
            // ReceiveCheck
            // 
            this.ReceiveCheck.AutoSize = true;
            this.ReceiveCheck.Location = new System.Drawing.Point(4, 37);
            this.ReceiveCheck.Name = "ReceiveCheck";
            this.ReceiveCheck.Size = new System.Drawing.Size(67, 19);
            this.ReceiveCheck.TabIndex = 0;
            this.ReceiveCheck.TabStop = true;
            this.ReceiveCheck.Text = "Receive";
            this.ReceiveCheck.UseVisualStyleBackColor = true;
            this.ReceiveCheck.CheckedChanged += new System.EventHandler(this.ReceiveCheck_CheckedChanged);
            // 
            // SendCheck
            // 
            this.SendCheck.AutoSize = true;
            this.SendCheck.Location = new System.Drawing.Point(4, 19);
            this.SendCheck.Name = "SendCheck";
            this.SendCheck.Size = new System.Drawing.Size(52, 19);
            this.SendCheck.TabIndex = 0;
            this.SendCheck.TabStop = true;
            this.SendCheck.Text = "Send";
            this.SendCheck.UseVisualStyleBackColor = true;
            this.SendCheck.CheckedChanged += new System.EventHandler(this.SendCheck_CheckedChanged);
            // 
            // indate
            // 
            this.indate.AutoSize = true;
            this.indate.Location = new System.Drawing.Point(602, 463);
            this.indate.Name = "indate";
            this.indate.Size = new System.Drawing.Size(88, 13);
            this.indate.TabIndex = 6;
            this.indate.Text = "Thoi gian hien tai";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(597, 437);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Current Time";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.MistyRose;
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbStopBits);
            this.groupBox2.Controls.Add(this.cmbDataBits);
            this.groupBox2.Controls.Add(this.cmbParity);
            this.groupBox2.Controls.Add(this.cmbBaudRate);
            this.groupBox2.Controls.Add(this.cmbPortName);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(551, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(141, 303);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Communication";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 257);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 15);
            this.label10.TabIndex = 1;
            this.label10.Text = "Stop Bits";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "Data Bits";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "Parity";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "Baud Rate";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Com Port";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbStopBits.Location = new System.Drawing.Point(27, 273);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(80, 23);
            this.cmbStopBits.TabIndex = 0;
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
            this.cmbDataBits.Location = new System.Drawing.Point(27, 221);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(80, 23);
            this.cmbDataBits.TabIndex = 0;
            // 
            // cmbParity
            // 
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.cmbParity.Location = new System.Drawing.Point(27, 163);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(80, 23);
            this.cmbParity.TabIndex = 0;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "28800",
            "36000",
            "115000"});
            this.cmbBaudRate.Location = new System.Drawing.Point(27, 105);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(80, 23);
            this.cmbBaudRate.TabIndex = 0;
            // 
            // cmbPortName
            // 
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cmbPortName.Location = new System.Drawing.Point(27, 46);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(80, 23);
            this.cmbPortName.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.MistyRose;
            this.groupBox1.Controls.Add(this.btnCapture);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnRecog);
            this.groupBox1.Controls.Add(this.btnLoadImage);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(539, 303);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(441, 223);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(91, 51);
            this.btnCapture.TabIndex = 2;
            this.btnCapture.Text = "Capture From CAM";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(439, 165);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(94, 41);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(439, 118);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(94, 41);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Transfer";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(439, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 28);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnRecog
            // 
            this.btnRecog.Location = new System.Drawing.Point(439, 71);
            this.btnRecog.Name = "btnRecog";
            this.btnRecog.Size = new System.Drawing.Size(94, 41);
            this.btnRecog.TabIndex = 1;
            this.btnRecog.Text = "Recognize";
            this.btnRecog.UseVisualStyleBackColor = true;
            this.btnRecog.Click += new System.EventHandler(this.btnRecog_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(439, 24);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(94, 41);
            this.btnLoadImage.TabIndex = 1;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.LoadImage_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(6, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(420, 270);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabDb
            // 
            this.tabDb.Controls.Add(this.pictureBox3);
            this.tabDb.Controls.Add(this.label5);
            this.tabDb.Controls.Add(this.btnRefresh);
            this.tabDb.Controls.Add(this.btnSearch);
            this.tabDb.Controls.Add(this.txtSearch);
            this.tabDb.Controls.Add(this.dataGridView1);
            this.tabDb.Location = new System.Drawing.Point(4, 22);
            this.tabDb.Name = "tabDb";
            this.tabDb.Padding = new System.Windows.Forms.Padding(3);
            this.tabDb.Size = new System.Drawing.Size(833, 498);
            this.tabDb.TabIndex = 1;
            this.tabDb.Text = "Database";
            this.tabDb.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(361, 15);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(111, 82);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(18, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Find Car:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(193, 60);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(69, 29);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(105, 60);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(69, 29);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(105, 23);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(157, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 111);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(734, 305);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(61, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 16);
            this.label13.TabIndex = 21;
            this.label13.Text = "Height";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(61, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 16);
            this.label14.TabIndex = 21;
            this.label14.Text = "Width";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.groupBox6.Controls.Add(this.areax);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.areay);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(698, 328);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(119, 69);
            this.groupBox6.TabIndex = 22;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "License Size";
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.DarkOrange;
            this.groupBox7.Controls.Add(this.G);
            this.groupBox7.Controls.Add(this.A);
            this.groupBox7.Controls.Add(this.R);
            this.groupBox7.Controls.Add(this.B);
            this.groupBox7.Controls.Add(this.Q);
            this.groupBox7.Controls.Add(this.C);
            this.groupBox7.Controls.Add(this.P);
            this.groupBox7.Controls.Add(this.D);
            this.groupBox7.Controls.Add(this.O);
            this.groupBox7.Controls.Add(this.E);
            this.groupBox7.Controls.Add(this.N);
            this.groupBox7.Controls.Add(this.F);
            this.groupBox7.Controls.Add(this.M);
            this.groupBox7.Controls.Add(this.H);
            this.groupBox7.Controls.Add(this.L);
            this.groupBox7.Controls.Add(this.cmdI);
            this.groupBox7.Controls.Add(this.K);
            this.groupBox7.Controls.Add(this.cmdJ);
            this.groupBox7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(698, 11);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(119, 303);
            this.groupBox7.TabIndex = 23;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Position Car";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 548);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "LUAN VAN TOT NGHIEP";
            this.tabControl1.ResumeLayout(false);
            this.tabCtrl.ResumeLayout(false);
            this.tabCtrl.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabDb.ResumeLayout(false);
            this.tabDb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCtrl;
        private System.Windows.Forms.TabPage tabDb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnRecog;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lptext;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label indate;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton ReceiveCheck;
        private System.Windows.Forms.RadioButton SendCheck;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox receivelptext;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ComboBox cmbID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox idcardtext;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox postext;
        private System.Windows.Forms.ComboBox cmbPos;
        private System.Windows.Forms.Button A;
        private System.Windows.Forms.Button R;
        private System.Windows.Forms.Button Q;
        private System.Windows.Forms.Button P;
        private System.Windows.Forms.Button O;
        private System.Windows.Forms.Button N;
        private System.Windows.Forms.Button M;
        private System.Windows.Forms.Button L;
        private System.Windows.Forms.Button K;
        private System.Windows.Forms.Button cmdJ;
        private System.Windows.Forms.Button cmdI;
        private System.Windows.Forms.Button H;
        private System.Windows.Forms.Button G;
        private System.Windows.Forms.Button F;
        private System.Windows.Forms.Button E;
        private System.Windows.Forms.Button D;
        private System.Windows.Forms.Button C;
        private System.Windows.Forms.Button B;
        private System.Windows.Forms.TextBox areay;
        private System.Windows.Forms.TextBox areax;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
    }
}

