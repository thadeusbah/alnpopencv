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
        private SerialPort comport = new SerialPort();
        private ControlMode CurrentControlMode = new ControlMode();
        OCR OCR = new OCR();
        int IDint;
        int i;
        Timer clock;
        bool mode;
        public Form1()
        {
            InitializeComponent();
            InitializeControlValues();
            
            // When data is recieved through the port, call this method
            comport.ReceivedBytesThreshold = 7;
            comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            //indate.Text = DateTime.Now.ToString();
            // iniciate DB connection
            string ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=data.mdb";
            try
            {
                database = new OleDbConnection(ConStr);
                database.Open();
                //SQL query to list data
                string queryString = "SELECT ID, NumberCar,DateIn, DateOut FROM data ";
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
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 180;

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

            string queryString = "SELECT ID, NumberCar,DateIn, DateOut FROM data ";

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
            string queryString = "SELECT ID, NumberCar,DateIn, DateOut FROM data ";
            loadDataGrid(queryString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mode = false;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select Image File";
            op.FileName = "*.jpg";
            if (op.ShowDialog() == DialogResult.OK)
            {
                if (OCR.LoadImage(op.FileName))
                {
                    pictureBox1.Image = OCR.src.ToBitmap();
                    btnRecog.Enabled = true;
                    //numericUpDown1.Enabled = false;
                    //numericUpDown1.Value = 1;
                    pictureBox2.Image = null;
                    listBox1.Items.Clear();
                }
            }
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
                        lptext.Text += k.ToString();
                    }
                }
            }

            pictureBox1.Image = OCR.src.ToBitmap();
            //numericUpDown1.Enabled = true;
            //numericUpDown1.Maximum = n;
            //numericUpDown1.Value = 1;
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
                cmbPortName.Items.Add(s);

            if (cmbPortName.Items.Contains(Settings.Default.PortName)) cmbPortName.Text = Settings.Default.PortName;
            else if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = 0;
            else
            {
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void OpenPort()
        {
            // If the port is open, close it.
            if (comport.IsOpen)
            {
                comport.Close();
                //Set the port's settings
                comport.BaudRate = int.Parse(cmbBaudRate.Text);
                comport.DataBits = int.Parse(cmbDataBits.Text);
                comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                comport.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                comport.PortName = cmbPortName.Text;

                // Open the port
                comport.Open();
                // If the port is open, send focus to the send data box
                if (comport.IsOpen)
                    lptext.Focus();
            }
            else
            {
                //Set the port's settings
                comport.BaudRate = int.Parse(cmbBaudRate.Text);
                comport.DataBits = int.Parse(cmbDataBits.Text);
                comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                comport.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                comport.PortName = cmbPortName.Text;

                // Open the port
                comport.Open();
                // If the port is open, send focus to the send data box
                if (comport.IsOpen)
                    lptext.Focus();
            }
        }
        private void SendData()
        {
            // Send the user's text straight out the port
            comport.Write(lptext.Text);
            
            //MessageBox.Show(String.Format("Application Started at {0}\n", DateTime.Now));
            if (CurrentControlMode == ControlMode.Send)
            {
                
                comport.Write("o");
            }
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // This method will be called when there is data waiting in the port's buffer
            // Read all the data waiting in the buffer
            string data = comport.ReadExisting();
            
            // Display the text to the user in the terminal
            lpreceived.Invoke(new EventHandler(delegate
            {
                lpreceived.SelectedText = string.Empty;
                lpreceived.SelectionFont = new Font(lpreceived.SelectionFont, FontStyle.Bold);
                lpreceived.SelectionColor = Color.Blue;
                lpreceived.AppendText(data);
                lpreceived.ScrollToCaret();
            }));
            CurrentControlMode = ControlMode.Receive;
            
            if (data==lptext.Text) MessageBox.Show("ok");
            
        }

       
        private void btnSend_Click(object sender, EventArgs e)
        {
            OpenPort();
            update();
            SendData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string soxe = lptext.Text.ToString();
            string giovao = indate.Text.ToString();
            //string giora = outdate.Text.ToString();
            i += 1; ;
            string SQLString = "";
            SQLString = "INSERT INTO data(NumberCar, DateIn) VALUES('" + soxe.Replace("'", "''") + "','" + giovao + "');";

            OleDbCommand SQLCommand = new OleDbCommand();
            SQLCommand.CommandText = SQLString;
            SQLCommand.Connection = database;
            int response = -1;
            try
            {
                response = SQLCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //if (response >= 1) MessageBox.Show("Number Car is added to database", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lptext.Clear();
        }
        private void update()
        {
            string soxe = lptext.Text.ToString();
            string giovao = indate.Text.ToString();
            //string giora = outdate.Text.ToString();

            string SQLString = "";
            SQLString = "INSERT INTO data(NumberCar, DateIn) VALUES('" + soxe.Replace("'", "''") + "','" + giovao + "');";

            OleDbCommand SQLCommand = new OleDbCommand();
            SQLCommand.CommandText = SQLString;
            SQLCommand.Connection = database;
            int response = -1;
            try
            {
                response = SQLCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //if (response >= 1) MessageBox.Show("Number Car is added to database", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //lptext.Clear();
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
            mode = true;
            using (CvCapture capture = CvCapture.FromFile("E:/LUAN VAN/CODE C#/APLR/ALPR/xemay/test.avi"))
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
                    Cv.WaitKey(10);

                }
            }
        }

    }
}
