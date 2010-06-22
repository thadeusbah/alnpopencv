using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using ALPR.Properties;
using System.Data.OleDb;
using OpenCvSharp;
using OpenCvSharp.MachineLearning;
using OpenCvSharp.Blob;
using OpenCvSharp.UserInterface;

namespace OCRSharp
{
    public partial class Form1 : Form
    {
        #region Public Enumerations
        public enum ControlMode { Send, Receive }
        #endregion

        public OleDbConnection database;
        DataGridViewButtonColumn deleteButton;
        // The main control for communicating through the RS-232 port
        private SerialPort comport1 = new SerialPort();
        private SerialPort comport2 = new SerialPort();
        string RXStr;
        private ControlMode CurrentControlMode = new ControlMode();

        #region Load Image From Database
        private string imgName;
        private OleDbDataAdapter daImage;
        private DataSet dsImage;
        #endregion

        OCR OCR = new OCR();
        int IDint;
        private static int i=0;
        private static int j=0;
        private static int pos=0;
        Timer clock;
        bool mode;
        public Form1()
        {
            InitializeComponent();
            InitializeControlValues();
            
            // When data is recieved through the port, call this method
            comport1.ReceivedBytesThreshold = 8;
            comport1.DataReceived += new SerialDataReceivedEventHandler(port1_DataReceived);
            comport2.ReceivedBytesThreshold = 1;
            comport2.DataReceived += new SerialDataReceivedEventHandler(port2_DataReceived);
            //indate.Text = DateTime.Now.ToString();
            // iniciate DB connection
            string ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data.mdb";
            try
            {
                database = new OleDbConnection(ConStr);
                database.Open();
                //SQL query to list data
                string queryString = "SELECT ID, NumberCar,DateIn, DateOut, ImageData,Pos, IDcard FROM data ";
                loadDataGrid(queryString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            clock = new Timer();
            clock.Interval = 1000;
            clock.Start();
            clock.Tick += new EventHandler(timer1_Tick);
            FillCombo();
            BusyPos();
        }

        #region Load dataGrid
        public void loadDataGrid(string sqlQueryString)
        {
            OleDbCommand SQLQuery = new OleDbCommand();
            DataTable data = null;
            dataGridView1.DataSource = null;
            SQLQuery.Connection = null;
            OleDbDataAdapter dataAdapter = null;
            dataGridView1.Columns.Clear(); // <-- clear columns
            //---------------------------------
            SQLQuery.CommandText = sqlQueryString;
            SQLQuery.Connection = database;
            data = new DataTable();
            dataAdapter = new OleDbDataAdapter(SQLQuery);
            dataAdapter.Fill(data);
            dataGridView1.DataSource = data;
            dataGridView1.AllowUserToAddRows = false; // remove the null line
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].Width = 120;


            // insert delete button to datagridview
            deleteButton = new DataGridViewButtonColumn();
            deleteButton.HeaderText = "Delete";
            deleteButton.Text = "Delete";
            deleteButton.UseColumnTextForButtonValue = true;
            deleteButton.Width = 80;
            dataGridView1.Columns.Add(deleteButton);
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string queryString = "SELECT ID, NumberCar,DateIn, DateOut, Pos, IDcard FROM data ";

            int currentRow = int.Parse(e.RowIndex.ToString());
            try
            {
                string IDString = dataGridView1[0, currentRow].Value.ToString();
                IDint = int.Parse(IDString);
            }
            catch (Exception ex) { }
            
            // delete button
            if (dataGridView1.Columns[e.ColumnIndex] == deleteButton && currentRow >= 0)
            {
                // delete sql query
                string queryDeleteString = "DELETE FROM data where ID = "+IDint+"";
                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();
                loadDataGrid(queryString);
            }
             
         }
      

        private void tabControl1_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string queryString = "SELECT ID, NumberCar,DateIn, DateOut, Pos, IDcard FROM data ";
            loadDataGrid(queryString);
        }

        private void LoadImage_Click(object sender, EventArgs e)
        {
            mode = false;
            try
            {
                OpenFileDialog dlgImage = new OpenFileDialog();
                dlgImage.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                dlgImage.Title = "Select Image File";
                //dlgImage.FileName = "*.jpg";
                if (dlgImage.ShowDialog() == DialogResult.OK)
                {
                    imgName = dlgImage.FileName;
                    Bitmap newimg = new Bitmap(imgName);
                    pictureBox1.Image = (Image)newimg;
                    if (OCR.LoadImage(imgName))
                    {
                        pictureBox1.Image = OCR.src.ToBitmap();
                        btnRecog.Enabled = true;
                        pictureBox2.Image = null;
                        listBox1.Items.Clear();
                        Recog();
                    }
                }
                dlgImage = null;
            }
            catch (System.ArgumentException ea)
            {
                imgName = "";
                MessageBox.Show(ea.Message.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            

        }
        private void Recog()
        {
            OCR.PreProcess();
            int n = OCR.FindPlates();
            if (n == 0) return;
            OCR.ReadPlates();
            listBox1.Items.Clear();
            foreach (var s in OCR.plateList)
            {
                lptext.Clear();
                listBox1.Items.Add(s);
                char[] d = s.ToCharArray();
                TextBox text1 = new TextBox();
                int m = 0;
                foreach (char k in d)
                {

                    if ((k == '0') || (k == '1') || (k == '2') || (k == '3') || (k == '4') ||
                        (k == '5') || (k == '6') || (k == '7') || (k == '8') || (k == '9') ||
                        (k == 'A') || (k == 'B') || (k == 'C') || (k == 'D') || (k == 'E') || (k == 'F') ||
                        (k == 'G') || (k == 'H') || (k == 'I') || (k == 'J') || (k == 'K') || (k == 'L') ||
                        (k == 'M') || (k == 'N') || (k == 'O') || (k == 'P') || (k == 'Q') || (k == 'R') ||
                        (k == 'S') || (k == 'T') || (k == 'U') || (k == 'V') || (k == 'W') || (k == 'X') ||
                        (k == 'Y') || (k == 'Z'))
                    {
                        text1.Text += k.ToString();
                    }
                }
                char[] c = text1.Text.ToCharArray();
                char lp;
                foreach (char l in c)
                {
                    m++;
                    if (m == 1)
                    {
                        if (l == 'L')
                        {
                            lp = '-';
                            m = m - 1;
                        }
                        else
                            lp = l;
                    }
                    else if (m == 3)
                    {
                        if (l == '2') lp = 'Z';
                        else if (l == '0') lp = 'O';
                        else if (l == '1') lp = 'L';
                        else lp = l;
                    }
                    else
                    {
                        if (l == 'O') lp = '0';
                        else if (l == 'Z') lp = '2';
                        else if (l == 'L') lp = '1';
                        else if (l == 'D') lp = '0';
                        else if (l == 'T') lp = '1';
                        else lp = l;
                    }
                    if (lp != '-') lptext.Text += lp.ToString();
                }
            }

            pictureBox1.Image = OCR.src.ToBitmap();
            pictureBox2.Image = OCR.plate[0].ToBitmap();
        }

        private void btnRecog_Click(object sender, EventArgs e)
        {
            OCR.PreProcess();
            int n = OCR.FindPlates();
            if (n == 0) return;
            OCR.ReadPlates();
            listBox1.Items.Clear();
            foreach (var s in OCR.plateList)
            {
                lptext.Clear();
                listBox1.Items.Add(s);
                char[] d = s.ToCharArray();
                TextBox text1 = new TextBox();
                int m = 0;
                foreach (char k in d)
                {
                    
                    if ((k == '0') || (k == '1') || (k == '2') || (k == '3') || (k == '4') ||
                        (k == '5') || (k == '6') || (k == '7') || (k == '8') || (k == '9') ||
                        (k == 'A') || (k == 'B') || (k == 'C') || (k == 'D') || (k == 'E') || (k == 'F') ||
                        (k == 'G') || (k == 'H') || (k == 'I') || (k == 'J') || (k == 'K') || (k == 'L') ||
                        (k == 'M') || (k == 'N') || (k == 'O') || (k == 'P') || (k == 'Q') || (k == 'R') ||
                        (k == 'S') || (k == 'T') || (k == 'U') || (k == 'V') || (k == 'W') || (k == 'X') ||
                        (k == 'Y') || (k == 'Z'))
                    {
                        text1.Text+= k.ToString();                        
                    }
                }
                char[] c = text1.Text.ToCharArray();
                char lp;
                foreach (char l in c)
                {
                    m++;
                    if (m == 1)
                    {
                        if (l == 'L')
                        {
                            lp = '-';
                            m = m - 1;
                        }
                        else
                            lp = l;
                    }
                    else if (m == 3)
                    {
                        if (l == '2') lp = 'Z';
                        else if (l == '0') lp = 'O';
                        else if (l == '1') lp = 'L';
                        else lp = l;
                    }
                    else
                    {
                        if (l == 'O') lp = '0';
                        else if (l == 'Z') lp = '2';
                        else if (l == 'L') lp = '1';
                        else if (l == 'D') lp = '0';
                        else if (l == 'T') lp = '1';
                        else lp = l;
                    }
                    if(lp!='-') lptext.Text += lp.ToString();
                }
            }

            pictureBox1.Image = OCR.src.ToBitmap();
            pictureBox2.Image = OCR.plate[0].ToBitmap();
        }

        private void InitializeControlValues()
        {
            cmbParity.Items.Clear(); cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear(); cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            cmbParity.Text = Settings.Default.Parity.ToString();
            cmbStopBits.Text = Settings.Default.StopBits.ToString();
            cmbDataBits.Text = Settings.Default.DataBits.ToString();
            cmbParity.Text = Settings.Default.Parity.ToString();
            cmbBaudRate.Text = Settings.Default.BaudRate.ToString();

            cmbPortName.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                cmbPortName.Items.Add(s);
            }

            if (cmbPortName.Items.Contains(Settings.Default.PortName)) cmbPortName.Text = Settings.Default.PortName;
            else if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = 0;
            else
            {
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void OpenPort1()
        {
            // If the port is open, close it.
            if (comport1.IsOpen)
            {
                comport1.Close();
                //Set the port's settings
                comport1.BaudRate = int.Parse(cmbBaudRate.Text);
                comport1.DataBits = int.Parse(cmbDataBits.Text);
                comport1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                comport1.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                comport1.PortName = cmbPortName.Text;

                // Open the port
                comport1.Open();
                // If the port is open, send focus to the send data box
                if (comport1.IsOpen)
                    lptext.Focus();
            }
            else
            {
                //Set the port's settings
                comport1.BaudRate = int.Parse(cmbBaudRate.Text);
                comport1.DataBits = int.Parse(cmbDataBits.Text);
                comport1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                comport1.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                comport1.PortName = cmbPortName.Text;

                // Open the port
                comport1.Open();
                // If the port is open, send focus to the send data box
                if (comport1.IsOpen)
                    lptext.Focus();
            }
        }
        private void OpenPort2()
        {
            // If the port is open, close it.
            if (comport2.IsOpen)
            {
                comport2.Close();
                //Set the port's settings
                comport2.BaudRate = int.Parse(cmbBaudRate.Text);
                comport2.DataBits = int.Parse(cmbDataBits.Text);
                comport2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                comport2.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                comport2.PortName = "COM2";

                // Open the port
                comport2.Open();
            }
            else
            {
                //Set the port's settings
                comport2.BaudRate = int.Parse(cmbBaudRate.Text);
                comport2.DataBits = int.Parse(cmbDataBits.Text);
                comport2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                comport2.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                comport2.PortName = "COM2";

                // Open the port
                comport2.Open();
            }
        }

        private void SendData(string pos)
        {            
            //MessageBox.Show(String.Format("Application Started at {0}\n", DateTime.Now));               
            comport2.Write(pos);
        }

        private void DisplayText(object sender, EventArgs e)
        {
            idcardtext.Text=RXStr;
            
        }
        private void DisplayWarning(object sender, EventArgs e)
        {
            MessageBox.Show("Time is out of range");
        }
        private void port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            RXStr = comport1.ReadExisting();
            /*this.Invoke(new EventHandler(delegate
            {
                idcardtext.Text = RXStr;    
            }));*/
            this.Invoke(new EventHandler(DisplayText));
            
            if (CurrentControlMode == ControlMode.Send)
            {
                update();
                for (int j = 1; j <= 65535; j++) ;
                SendData(postext.Text.ToString());
            }
            
            if (CurrentControlMode == ControlMode.Receive)
            {
                receivecar();
                if (pictureBox4.Image != null)
                {
                    pictureBox4.Image.Dispose();
                }
                LoadImageFromDB();
            }          
            
        }
        private void port2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(DisplayWarning));
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            OpenPort1();
            OpenPort2();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();
        }

        private void update()
        {
         
            try
            {
                if (imgName != "")
                {
                    FileStream fs;
                    fs = new FileStream(imgName, FileMode.Open, FileAccess.Read);
                    byte[] picbyte = new byte[fs.Length];
                    fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();
                    if (CurrentControlMode == ControlMode.Send)
                    {
                        String strSQL;

                        strSQL = "INSERT INTO data(NumberCar,DateIn,ImageData,Pos, IDcard) values (@lp,@datein,@Img,@pos,@idcard)";

                        OleDbParameter imgParam1 = new OleDbParameter();
                        OleDbParameter imgParam2 = new OleDbParameter();
                        OleDbParameter imgParam3 = new OleDbParameter();
                        OleDbParameter imgParam4 = new OleDbParameter();
                        OleDbParameter imgParam5 = new OleDbParameter();

                        imgParam1.OleDbType = OleDbType.Char;
                        imgParam1.ParameterName = "lp";
                        imgParam1.Value = lptext.Text;

                        imgParam2.OleDbType = OleDbType.Date;
                        imgParam2.ParameterName = "datein";
                        imgParam2.Value = DateTime.Now.Date;// indate.Text;

                        imgParam3.OleDbType = OleDbType.Binary;
                        imgParam3.ParameterName = "Img";
                        imgParam3.Value = picbyte;

                        imgParam4.OleDbType = OleDbType.Char;
                        imgParam4.ParameterName = "pos";
                        imgParam4.Value = postext.Text;

                        imgParam5.OleDbType = OleDbType.Char;
                        imgParam5.ParameterName = "idcard";
                        imgParam5.Value = idcardtext.Text;

                        OleDbCommand cmd = new OleDbCommand(strSQL, database);

                        cmd.Parameters.Add(imgParam1);
                        cmd.Parameters.Add(imgParam2);
                        cmd.Parameters.Add(imgParam3);
                        cmd.Parameters.Add(imgParam4);
                        cmd.Parameters.Add(imgParam5);
                        int response = -1;
                        try
                        {
                            response = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //FillCombo();
        }

        
        private string gettime()
        {
            string timeinstring = "";
            int hour = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;
            //timeinstring = (hour < 10) ? "0" + hour.ToString() : hour.ToString();
            //timeinstring += ":" + ((min < 10) ? "0" + min.ToString() : min.ToString());
            //timeinstring += ":" + ((sec < 10) ? "0" + sec.ToString() : sec.ToString());
            timeinstring = DateTime.Now.ToString();
            return timeinstring;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(sender==clock)
            {
                indate.Text = DateTime.Now.ToString();
            }
        }

        private void SendCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (SendCheck.Checked) CurrentControlMode = ControlMode.Send;
        }

        private void ReceiveCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ReceiveCheck.Checked) CurrentControlMode = ControlMode.Receive;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text.ToString();
            if (name != "")
            {
                string queryString = "SELECT ID, NumberCar, DateIn,DateOut FROM data WHERE data.NumberCar LIKE '" + name + "%'";
                loadDataGrid(queryString);
            }
            else
            {
                MessageBox.Show("You must enter License Number Car", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            string queryString = "SELECT ID, NumberCar, DateIn,DateOut FROM data";
            loadDataGrid(queryString);
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            //LoadImageFromDB(); 
            mode = true;
            using (CvCapture capture = CvCapture.FromCamera(0))
            using (CvWindow w = new CvWindow("SampleCapture"))
            {
                capture.FrameHeight = 240;
                capture.FrameWidth = 320;

                while (mode)
                {
                    IplImage frame = capture.QueryFrame();
                    Cv.SaveImage("img.jpg", frame);
                    //IplImage img = Cv.LoadImage("img.jpg", LoadMode.Color);
                    if (OCR.LoadImage("img.jpg"))
                    {
                        pictureBox1.Image = OCR.src.ToBitmap();
                        btnRecog.Enabled = true;
                        pictureBox2.Image = null;
                        listBox1.Items.Clear();
                    }
                    //string str = string.Format("{0}[frame]", num);
                    //frame.PutText(str, new CvPoint(10, 20), font, new CvColor(0, 255, 100));
                    //vw.WriteFrame(frame);
                    //window.ShowImage(frame);
                    //w.Image = frame;
                    //pictureBox1.Image = frame.ToBitmap();
                    Cv.WaitKey(5);

                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            //update();
            AvailablePos();
            
            //SendData(postext.Text.ToString());
            //postext.Text = selectPos().ToString();
        }

        #region TUTAO
        private byte[] ImageToStream(Image photo)
        {

            Bitmap image = new Bitmap(photo);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            return stream.ToArray();
        }

        private void StoreData(byte[] content)
        {
            string soxe = lptext.Text.ToString();
            string giovao = indate.Text.ToString();

            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data.mdb");
            //conn.Open();
            if (database.State.Equals(ConnectionState.Closed))
                database.Open();

            try
            {
                //OleDbCommand insert = new OleDbCommand("INSERT INTO data(NumberCar,DateIn) VALUES('" + lptext.Text + "','" + indate.Text + "')", database);
                // OleDbParameter imageParameter = insert.Parameters.Add("@Image", OleDbType.Binary);
                //imageParameter.Value = content;
                //imageParameter.Size = content.Length;
                OleDbCommand insert = new OleDbCommand("INSERT INTO data(NumberCar,DateIn,ImageData) VALUES('" + lptext.Text + "','" + indate.Text + "',@ImageData)", database);
                OleDbParameter imageParameter = new OleDbParameter
                ("@ImageData", OleDbType.LongVarBinary, content.Length);
                imageParameter.Value = content;
                insert.Parameters.Add(imageParameter);
                insert.ExecuteNonQuery();
                MessageBox.Show("Data Stored successfully");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                MessageBox.Show(ex.StackTrace.ToString());
            }
            finally
            {
                database.Close();
            }
        }
        #endregion

        //////////////////////////////////////////////////////////////////////////
  
        private void FillCombo()
        {
            try
            {
                OleDbDataAdapter daImage = new OleDbDataAdapter();
                daImage.SelectCommand = new OleDbCommand("SELECT * FROM data ",database);
                dsImage = new DataSet("dsImage");
                daImage.Fill(dsImage);
                DataTable dtable;
                dtable = dsImage.Tables[0];
                cmbID.Items.Clear();
                foreach( DataRow drow in dtable.Rows)
                {
                    cmbID.Items.Add(drow[1].ToString());
                    cmbID.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void BusyPos()
        {
            try
            {
                OleDbDataAdapter daImage = new OleDbDataAdapter();
                daImage.SelectCommand = new OleDbCommand("SELECT * FROM data ", database);
                dsImage = new DataSet("dsImage");
                daImage.Fill(dsImage);
                DataTable dtable;
                dtable = dsImage.Tables[0];
                string POSITION;
                foreach (DataRow drow in dtable.Rows)
                {
                    POSITION = drow[5].ToString();
                    if (POSITION == "A") A.Enabled = false;
                    if (POSITION == "B") B.Enabled = false;
                    if (POSITION == "C") C.Enabled = false;
                    if (POSITION == "D") D.Enabled = false;
                    if (POSITION == "E") E.Enabled = false;
                    if (POSITION == "F") F.Enabled = false;
                    if (POSITION == "G") G.Enabled = false;
                    if (POSITION == "H") H.Enabled = false;
                    if (POSITION == "I") cmdI.Enabled = false;
                    if (POSITION == "J") cmdJ.Enabled = false;
                    if (POSITION == "K") K.Enabled = false;
                    if (POSITION == "L") L.Enabled = false;
                    if (POSITION == "M") M.Enabled = false;
                    if (POSITION == "N") N.Enabled = false;
                    if (POSITION == "O") O.Enabled = false;
                    if (POSITION == "P") P.Enabled = false;
                    if (POSITION == "Q") Q.Enabled = false;
                    if (POSITION == "R") R.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void AvailablePos()
        {
            if (postext.Text == "A") A.Enabled = true;
            if (postext.Text == "B") B.Enabled = true;
            if (postext.Text == "C") C.Enabled = true;
            if (postext.Text == "D") D.Enabled = true;
            if (postext.Text == "E") E.Enabled = true;
            if (postext.Text == "F") F.Enabled = true;
            if (postext.Text == "G") G.Enabled = true;
            if (postext.Text == "H") H.Enabled = true;
            if (postext.Text == "I") cmdI.Enabled = true;
            if (postext.Text == "J") cmdJ.Enabled = true;
            if (postext.Text == "K") K.Enabled = true;
            if (postext.Text == "L") L.Enabled = true;
            if (postext.Text == "M") M.Enabled = true;
            if (postext.Text == "N") N.Enabled = true;
            if (postext.Text == "O") O.Enabled = true;
            if (postext.Text == "P") P.Enabled = true;
            if (postext.Text == "Q") Q.Enabled = true;
            if (postext.Text == "R") R.Enabled = true;
        }

        private void LoadImageFromDB()
        {
            try
            {
                DataTable dataTable = null;
                dataTable = new DataTable();
                dataTable = dsImage.Tables[0];
                if (pictureBox4.Image != null)
                {
                    pictureBox4.Image.Dispose();
                }

                FileStream fsImage = new FileStream("image.jpg", FileMode.Create);
                for (int a = 0; a < 65339; a++) ;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (dataRow[6].ToString() == idcardtext.Text.ToString())
                    {
                        byte[] blob = (byte[])dataRow[4];//dsImage.Tables[0].Rows[0][4];
                        fsImage.Write(blob, 0, blob.Length);
                        fsImage.Close();
                        fsImage = null;
                        pictureBox4.Image = Image.FromFile("image.jpg");
                        pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                        /*string imgname = "image.jpg";
                        Bitmap newimg = new Bitmap(imgname);
                        pictureBox4.Image = (Image)newimg;
                        if (OCR.LoadImage(imgname))
                        {
                            pictureBox4.Image = OCR.src.ToBitmap();
                        }*/
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void receivecar()
        {
            string name = idcardtext.Text.ToString();
            try
            {    
                    string idcardStr = "SELECT * FROM data WHERE data.IDcard LIKE '" +name+ "%'";
                    OleDbDataAdapter da = new OleDbDataAdapter();
                    da.SelectCommand = new OleDbCommand(idcardStr, database);
                    dsImage= new DataSet("dsImage");
                    da.Fill(dsImage);
                    DataTable dtable;
                    dtable = dsImage.Tables[0];
                    
                    foreach (DataRow drow in dtable.Rows)
                    {
                        receivelptext.Invoke(new EventHandler(delegate
                        {
                            receivelptext.Text = drow[1].ToString();
                            postext.Text = drow[5].ToString();
                            AvailablePos();
                            
                        }));
                    }
                    for (int j = 1; j <= 65355; j++) ;
                    SendData(postext.Text.ToString());
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int selectPos()
        {
            for (j = 1; j <= 5; j++)
            {
                for (i = 1; i <= 5; i++)
                {
                    if (i != j) pos = j;

                }
            }
            return (pos);
        }

        private void A_Click(object sender, EventArgs e)
        {
            postext.Text = "A";
            A.Enabled = false;
        }
        private void B_Click(object sender, EventArgs e)
        {
            postext.Text = "B";
            B.Enabled = false;
        }
        private void C_Click(object sender, EventArgs e)
        {
            postext.Text = "C";
            C.Enabled = false;
        }
        private void D_Click(object sender, EventArgs e)
        {
            postext.Text = "D";
            D.Enabled = false;
        }
        private void E_Click(object sender, EventArgs e)
        {
            postext.Text = "E";
            E.Enabled = false;
        }
        private void F_Click(object sender, EventArgs e)
        {
            postext.Text = "F";
            F.Enabled = false;
        }
        private void G_Click(object sender, EventArgs e)
        {
            postext.Text = "G";
            G.Enabled = false;
        }
        private void H_Click(object sender, EventArgs e)
        {
            postext.Text = "H";
            H.Enabled = false;
        }
        private void cmdI_Click(object sender, EventArgs e)
        {
            postext.Text = "I";
            cmdI.Enabled = false;
        }
        private void cmdJ_Click(object sender, EventArgs e)
        {
            postext.Text = "J";
            cmdJ.Enabled = false;
        }
        private void K_Click(object sender, EventArgs e)
        {
            postext.Text = "K";
            K.Enabled = false;
        }
        private void L_Click(object sender, EventArgs e)
        {
            postext.Text = "L";
            L.Enabled = false;
        }
        private void M_Click(object sender, EventArgs e)
        {
            postext.Text = "M";
            M.Enabled = false;
        }
        private void N_Click(object sender, EventArgs e)
        {
            postext.Text = "N";
            N.Enabled = false;
        }
        private void O_Click(object sender, EventArgs e)
        {
            postext.Text = "O";
            O.Enabled = false;
        }
        private void P_Click(object sender, EventArgs e)
        {
            postext.Text = "P";
            P.Enabled = false;
        }
        private void Q_Click(object sender, EventArgs e)
        {
            postext.Text = "Q";
            Q.Enabled = false;
        }
        private void R_Click(object sender, EventArgs e)
        {
            postext.Text = "R";
            R.Enabled = false;
            MessageBox.Show("HE THONG DA DAY");
        }

      

    }
}
