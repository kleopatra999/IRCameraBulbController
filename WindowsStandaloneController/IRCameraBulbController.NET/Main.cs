using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace IRCameraBulbController.NET
{
    public partial class Main : Form
    {

        private CameraController _camController;
        private DateTime _lastError = DateTime.MaxValue;


        public Main()
        {
            InitializeComponent();

            _camController = new CameraController(Properties.Settings.Default.BaudRate);

            LinkEvents();
        }


        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            _camController.PortName = "";
            Properties.Settings.Default.Save();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadDefaults();
            LoadPorts();

            TimerUI.Start();
        }

        private void ButtonPortRefresh_Click(object sender, EventArgs e)
        {
            LoadPorts();
        }

        private void CheckMirrorUp_CheckedChanged(object sender, EventArgs e)
        {
            _camController.MirrorUp = CheckMirrorUp.Checked;
            Properties.Settings.Default.MirrorUp = _camController.MirrorUp;
        }

        private void NumericQuantity_ValueChanged(object sender, EventArgs e)
        {
            _camController.Quantity = (int)NumericQuantity.Value;
            Properties.Settings.Default.Quantity = _camController.Quantity;
        }

        private void NumericGap_ValueChanged(object sender, EventArgs e)
        {
            _camController.Gap = (int)NumericGap.Value;
            Properties.Settings.Default.Gap = _camController.Gap;
        }

        private void NumericDuration_ValueChanged(object sender, EventArgs e)
        {
            _camController.Duration = (int)NumericDuration.Value;
            Properties.Settings.Default.Duration = _camController.Duration;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            _camController.Start();
        }

        private void ButtonAbort_Click(object sender, EventArgs e)
        {
            _camController.Abort();
        }

        private void ComboPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            _camController.PortName = ComboPorts.Text;
        }

        private void _camController_ErrorRecieved(object sender, ErrorRecievedEventArgs e)
        {
            _lastError = DateTime.Now;
            this.Invoke((MethodInvoker)delegate
                {
                    LabelStatus.Text = e.ErrorMessage;
                }
            );
        }

        private void TimerUI_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }


        private void LinkEvents()
        {
            this.Load += new EventHandler(Main_Load);
            this.ButtonPortRefresh.Click += new EventHandler(ButtonPortRefresh_Click);
            this.NumericDuration.ValueChanged += NumericDuration_ValueChanged;
            this.NumericGap.ValueChanged += NumericGap_ValueChanged;
            this.NumericQuantity.ValueChanged += NumericQuantity_ValueChanged;
            this.CheckMirrorUp.CheckedChanged += CheckMirrorUp_CheckedChanged;
            this.ButtonAbort.Click += ButtonAbort_Click;
            this.ButtonStart.Click += ButtonStart_Click;
            this.ComboPorts.SelectedIndexChanged += ComboPorts_SelectedIndexChanged;
            this._camController.ErrorRecieved += _camController_ErrorRecieved;
            this.TimerUI.Tick += TimerUI_Tick;
            Application.ApplicationExit += Application_ApplicationExit;
        }


        private void LoadDefaults()
        {
            NumericDuration.Value = Properties.Settings.Default.Duration;
            NumericGap.Value = Properties.Settings.Default.Gap;
            NumericQuantity.Value = Properties.Settings.Default.Quantity;
            CheckMirrorUp.Checked = Properties.Settings.Default.MirrorUp;
        }

        private void LoadPorts()
        {
            ComboPorts.DataSource = System.IO.Ports.SerialPort.GetPortNames();
        }

        private void UpdateUI()
        {
            EnableInputControls(!_camController.Busy);

            if ((DateTime.Now - _lastError).Seconds > 5)
            {
                _lastError = DateTime.MaxValue;
                LabelStatus.Text = "";
            }
        }

        private void EnableInputControls(bool enable)
        {
            ComboPorts.Enabled =
                ButtonStart.Enabled =
                ButtonPortRefresh.Enabled =
                CheckMirrorUp.Enabled =
                NumericDuration.Enabled =
                NumericGap.Enabled =
                NumericQuantity.Enabled = enable;

        }

    }
}
