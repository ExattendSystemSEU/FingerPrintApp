﻿using libzkfpcsharp;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ZKTecoFingerPrintScanner_Implementation;
using ZKTecoFingerPrintScanner_Implementation.Helpers;
using ZKTecoFingerPrintScanner_Implementation.Models;

namespace Dofe_Re_Entry.UserControls.DeviceController
{
    public partial class FingerPrintControl : UserControl
    {

        const string VerifyButtonDefault = "Verify";
        const string VerifyButtonToggle = "Stop Verification";
        const string Disconnected = "Disconnected";

        //1=student    .. 2=employee
        int userType = 0;

        bool CanRegisterId = false;


        Thread captureThread = null;

        public Master parentForm = null;

        #region -------- FIELDS --------

        const int REGISTER_FINGER_COUNT = 3;

        zkfp fpInstance = new zkfp();
        IntPtr FormHandle = IntPtr.Zero; // To hold the handle of the form
        bool bIsTimeToDie = false;
        bool IsRegister = false;
        bool bIdentify = true;
        bool bIdentify2 = true;
        byte[] FPBuffer;   // Image Buffer
        int RegisterCount = 0;

        byte[][] RegTmps = new byte[REGISTER_FINGER_COUNT][];

        byte[] RegTmp = new byte[2048];
        byte[] CapTmp = new byte[2048];
        int cbCapTmp = 2048;
        int regTempLen = 0;
        int iFid = 1;

        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;




        private int mfpWidth = 0;
        private int mfpHeight = 0;


        #endregion


        // [ CONSTRUCTOR ]
        public FingerPrintControl()
        {
            InitializeComponent();

            ReInitializeInstance();
            ComboBox_classesList.Enabled = false;
            ExAttDBRepo eR = new ExAttDBRepo();
            var allActivatedClasses = eR.GetAllActivatedClasses().ToList();
            foreach (var item in allActivatedClasses)
            {
                ComboBox_classesList.Items.Add(item.Exa_Course_CRN);
            }

        }


        private void bnInit_Click()
        {
            userType = 0;

            parentForm.statusBar.Visible = false;
            cmbIdx.Items.Clear();

            int initializeCallBackCode = fpInstance.Initialize();
            if (zkfp.ZKFP_ERR_OK == initializeCallBackCode)
            {
                int nCount = fpInstance.GetDeviceCount();
                if (nCount > 0)
                {
                    for (int i = 1; i <= nCount; i++) cmbIdx.Items.Add(i.ToString());

                    cmbIdx.SelectedIndex = 0;
                    btnInit.Enabled = false;
                    button1.Enabled = true;

                    if (!CanRegisterId)
                    {
                        Utilities.EnableControls(true, btnEnroll);
                    }
                    else
                    {
                        Utilities.EnableControls(false, btnEnroll);

                    }


                    DisplayMessage(MessageManager.msg_FP_InitComplete, true);
                }
                else
                {
                    int finalizeCount = fpInstance.Finalize();
                    DisplayMessage(MessageManager.msg_FP_NotConnected, false);
                }




                // CONNECT DEVICE

                #region -------- CONNECT DEVICE --------

                int openDeviceCallBackCode = fpInstance.OpenDevice(cmbIdx.SelectedIndex);
                if (zkfp.ZKFP_ERR_OK != openDeviceCallBackCode)
                {
                    DisplayMessage($"Uable to connect with the device! (Return Code: {openDeviceCallBackCode} )", false);
                    return;
                }

                Utilities.EnableControls(false, btnInit);
                Utilities.EnableControls(true, btnClose/*, btnEnroll*//*, btnVerify*/, btnIdentify, btnFree);

                ComboBox_classesList.Enabled = true;


                RegisterCount = 0;
                regTempLen = 0;
                iFid = 1;

                //for (int i = 0; i < 3; i++)
                for (int i = 0; i < REGISTER_FINGER_COUNT; i++)
                {
                    RegTmps[i] = new byte[2048];
                }

                byte[] paramValue = new byte[4];
                int size = 4;

                //fpInstance.GetParameters

                fpInstance.GetParameters(1, paramValue, ref size);
                zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

                size = 4;
                fpInstance.GetParameters(2, paramValue, ref size);
                zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

                FPBuffer = new byte[mfpWidth * mfpHeight];

                //FPBuffer = new byte[fpInstance.imageWidth * fpInstance.imageHeight];

                captureThread = new Thread(new ThreadStart(DoCapture));
                captureThread.IsBackground = true;
                captureThread.Start();


                bIsTimeToDie = false;

                string devSN = fpInstance.devSn;
                lblDeviceStatus.Text = "Connected \nDevice S.No: " + devSN;

                DisplayMessage("You are now connected to the device.", true);



                #endregion

            }
            else
                DisplayMessage("Unable to initialize the device. " + FingerPrintDeviceUtilities.DisplayDeviceErrorByCode(initializeCallBackCode) + " !! ", false);

        }


        // [ INITALIZE DEVICE ]
        private void bnInit_Click(object sender, EventArgs e)
        {
            userType = 0;

            parentForm.statusBar.Visible = false;
            cmbIdx.Items.Clear();

            int initializeCallBackCode = fpInstance.Initialize();
            if (zkfp.ZKFP_ERR_OK == initializeCallBackCode)
            {
                int nCount = fpInstance.GetDeviceCount();
                if (nCount > 0)
                {
                    for (int i = 1; i <= nCount; i++) cmbIdx.Items.Add(i.ToString());

                    cmbIdx.SelectedIndex = 0;
                    btnInit.Enabled = false;
                    button1.Enabled = true;

                    if (!CanRegisterId)
                    {
                        Utilities.EnableControls(true, btnEnroll);
                    }
                    else
                    {
                        Utilities.EnableControls(false, btnEnroll);

                    }


                    DisplayMessage(MessageManager.msg_FP_InitComplete, true);
                }
                else
                {
                    int finalizeCount = fpInstance.Finalize();
                    DisplayMessage(MessageManager.msg_FP_NotConnected, false);
                }




                // CONNECT DEVICE

                #region -------- CONNECT DEVICE --------

                int openDeviceCallBackCode = fpInstance.OpenDevice(cmbIdx.SelectedIndex);
                if (zkfp.ZKFP_ERR_OK != openDeviceCallBackCode)
                {
                    DisplayMessage($"Uable to connect with the device! (Return Code: {openDeviceCallBackCode} )", false);
                    return;
                }

                Utilities.EnableControls(false, btnInit);
                Utilities.EnableControls(true, btnClose/*, btnEnroll*//*, btnVerify*/, btnIdentify, btnFree);

                ComboBox_classesList.Enabled = true;


                RegisterCount = 0;
                regTempLen = 0;
                iFid = 1;

                //for (int i = 0; i < 3; i++)
                for (int i = 0; i < REGISTER_FINGER_COUNT; i++)
                {
                    RegTmps[i] = new byte[2048];
                }

                byte[] paramValue = new byte[4];
                int size = 4;

                //fpInstance.GetParameters

                fpInstance.GetParameters(1, paramValue, ref size);
                zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

                size = 4;
                fpInstance.GetParameters(2, paramValue, ref size);
                zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

                FPBuffer = new byte[mfpWidth * mfpHeight];

                //FPBuffer = new byte[fpInstance.imageWidth * fpInstance.imageHeight];

                captureThread = new Thread(new ThreadStart(DoCapture));
                captureThread.IsBackground = true;
                captureThread.Start();


                bIsTimeToDie = false;

                string devSN = fpInstance.devSn;
                lblDeviceStatus.Text = "Connected \nDevice S.No: " + devSN;

                DisplayMessage("You are now connected to the device.", true);



                #endregion

            }
            else
                DisplayMessage("Unable to initialize the device. " + FingerPrintDeviceUtilities.DisplayDeviceErrorByCode(initializeCallBackCode) + " !! ", false);

        }



        // [ CAPTURE FINGERPRINT ]
        private void DoCapture()
        {
            try
            {
                while (!bIsTimeToDie)
                {
                    cbCapTmp = 2048;

                    int ret = fpInstance.AcquireFingerprint(FPBuffer, CapTmp, ref cbCapTmp);

                    if (ret == zkfp.ZKFP_ERR_OK)
                    {
                        //if (RegisterCount == 0)
                        //    btnEnroll.Invoke((Action)delegate
                        //    {
                        //        btnEnroll.Enabled = true;
                        //    });
                        SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                    }
                    Thread.Sleep(100);
                }
            }
            catch { }

        }

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);


        private void bnIdentify_Click(object sender, EventArgs e)
        {
            if (!bIdentify)
            {
                bIdentify = true;
                DisplayMessage(MessageManager.msg_FP_PressForIdentification, true);
            }
        }

        private void bnVerify_Click(object sender, EventArgs e)
        {
            userType = 1;

            if (bIdentify)
            {
                bIdentify = false;
                btnVerify.Text = VerifyButtonToggle;
                DisplayMessage(MessageManager.msg_FP_PressForVerification, true);


                bIdentify2 = true;
                button1.Text = VerifyButtonDefault;

            }
            else
            {
                bIdentify = true;
                btnVerify.Text = VerifyButtonDefault;
            }
        }


        protected override void DefWndProc(ref Message m)
        {
            ExAttDBRepo exAttDBRepo = new ExAttDBRepo();
            switch (m.Msg)
            {
                case MESSAGE_CAPTURED_OK:
                    {
                        parentForm.statusBar.Visible = false;

                        if (!bIdentify2 || !bIdentify)
                        {
                            DisplayFingerPrintImage();
                        }

                        if (IsRegister)
                        {
                            #region -------- IF REGISTERED FINGERPRINT --------

                            int ret = zkfp.ZKFP_ERR_OK;
                            int fid = 0, score = 0;
                            ret = fpInstance.Identify(CapTmp, ref fid, ref score);
                            if (zkfp.ZKFP_ERR_OK == ret)
                            {
                                int deleteCode = fpInstance.DelRegTemplate(fid);   // <---- REMOVE FINGERPRINT
                                if (deleteCode != zkfp.ZKFP_ERR_OK)
                                {
                                    DisplayMessage(MessageManager.msg_FP_CurrentFingerAlreadyRegistered + fid, false);
                                    return;
                                }
                            }
                            if (RegisterCount > 0 && fpInstance.Match(CapTmp, RegTmps[RegisterCount - 1]) <= 0)
                            {
                                DisplayMessage("Please press the same finger " + REGISTER_FINGER_COUNT + " times for enrollment", true);

                                return;
                            }
                            Array.Copy(CapTmp, RegTmps[RegisterCount], cbCapTmp);


                            if (RegisterCount == 0) btnEnroll.Enabled = false;

                            RegisterCount++;
                            if (RegisterCount >= REGISTER_FINGER_COUNT)
                            {

                                RegisterCount = 0;
                                ret = GenerateRegisteredFingerPrint();   // <--- GENERATE FINGERPRINT TEMPLATE

                                if (zkfp.ZKFP_ERR_OK == ret)
                                {

                                    ret = AddTemplateToMemory();        //  <--- LOAD TEMPLATE TO MEMORY
                                    if (zkfp.ZKFP_ERR_OK == ret)         // <--- ENROLL SUCCESSFULL
                                    {
                                        string fingerPrintTemplate = string.Empty;
                                        zkfp.Blob2Base64String(RegTmp, regTempLen, ref fingerPrintTemplate);

                                        Utilities.EnableControls(true, button1, btnVerify, btnIdentify);
                                        Utilities.EnableControls(false, btnEnroll);

                                        MySqlConnection connection = null;
                                        try
                                        {
                                            var registerUSerId = "0";
                                            registerUSerId = textBox1_SocialID.Text;

                                            const String SERVER = "sql248.main-hosting.eu";
                                            const String DATABASE = "u842190477_Exattend";
                                            const String UID = "u842190477_Admin";
                                            const String PASSWORD = "Aa123456";
                                            MySqlConnection dbConn;
                                            String query = @"INSERT INTO u842190477_Exattend.fingerprint(Uni_ID, Fin_Code, FingerTemplate)VALUES(" + registerUSerId + ", " + 0 + ", '" + fingerPrintTemplate + "'); ";
                                            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                                            builder.Server = SERVER;
                                            builder.UserID = UID;
                                            builder.Password = PASSWORD;
                                            builder.Database = DATABASE;

                                            String connString = builder.ToString();

                                            builder = null;

                                            Console.WriteLine(connString);

                                            dbConn = new MySqlConnection(connString);

                                            MySqlCommand cmd = new MySqlCommand(query, dbConn);
                                            dbConn.Open();

                                            cmd.ExecuteNonQuery();

                                            cmd.Connection = connection;
                                            dbConn.Close();

                                        }
                                        finally
                                        {
                                            if (connection != null)
                                                connection.Close();
                                        }




                                        //var zz = exAttDBRepo.GetAllEmployees();
                                        //exAttDBRepo.inserFingerPrintToDB(textBox1_SocialID.Text, fingerPrintTemplate);
                                        // GET THE TEMPLATE HERE : fingerPrintTemplate


                                        DisplayMessage(MessageManager.msg_FP_EnrollSuccessfull, true);

                                        DisconnectFingerPrintCounter();
                                    }
                                    else
                                        DisplayMessage(MessageManager.msg_FP_FailedToAddTemplate, false);

                                }
                                else
                                    DisplayMessage(MessageManager.msg_FP_UnableToEnrollCurrentUser + ret, false);

                                IsRegister = false;
                                return;
                            }
                            else
                            {
                                int remainingCont = REGISTER_FINGER_COUNT - RegisterCount;
                                lblFingerPrintCount.Text = remainingCont.ToString();
                                string message = "Please provide your fingerprint " + remainingCont + " more time(s)";

                                DisplayMessage(message, true);

                            }
                            #endregion
                        }
                        else if (!bIdentify2 || !bIdentify)
                        {

                            #region ------- IF RANDOM FINGERPRINT -------
                            // If unidentified random fingerprint is applied

                            //if (regTempLen <= 0)
                            //{
                            //    DisplayMessage(MessageManager.msg_FP_UnidentifiedFingerPrint, false);
                            //    return;
                            //}


                            if ((bIdentify && userType == 1) || (bIdentify2 && userType == 2))
                            {
                                int ret = zkfp.ZKFP_ERR_OK;
                                int fid = 0, score = 0;
                                ret = fpInstance.Identify(CapTmp, ref fid, ref score);

                                //check if 
                                if (zkfp.ZKFP_ERR_OK == ret)
                                {
                                    DisplayMessage(MessageManager.msg_FP_IdentificationSuccess + ret, true);
                                    return;
                                }
                                else
                                {
                                    DisplayMessage(MessageManager.msg_FP_IdentificationFailed + ret, false);
                                    return;
                                }
                            }
                            else
                            {
                                ExAttDBRepo eR = new ExAttDBRepo();
                                ExUniDBRepo eU = new ExUniDBRepo();

                                var allFingers = eR.GetAllFingers();

                                if (userType == 1)
                                {
                                    var x = eU.GetAllStudents().Select(e => e.Stu_ID).ToList();
                                    allFingers = allFingers.Where(e => x.Contains(e.Uni_ID)).ToList();

                                }

                                else if (userType == 2)
                                {
                                    var x = eU.GetAllEmployees().Select(e => e.Emp_ID).ToList();
                                    allFingers = allFingers.Where(e => x.Contains(e.Uni_ID)).ToList();

                                }



                                Finger selectedStudent = new Finger();
                                foreach (var item in allFingers.ToList())
                                {
                                    var base46Registerd = zkfp.Base64String2Blob(item.FingerTemplate);
                                    var r = fpInstance.Match(CapTmp, base46Registerd);
                                    if (r > 0)
                                    {
                                        selectedStudent = item;
                                        bool checkIsSavedthroughOneHour = eR.CheckIsSavedthroughOneHour(selectedStudent.Uni_ID,userType);
                                        if (!checkIsSavedthroughOneHour)
                                        {
                                            eR.inserAttendanceLogToDB(selectedStudent.Uni_ID);
                                        }
                                        break;
                                    }
                                }
                                //if id for student match the his fingerprint then show message
                                if (selectedStudent.Uni_ID > 0)
                                {
                                    DisplayMessage(MessageManager.msg_FP_MatchSuccess + selectedStudent.Uni_ID, true);
                                    return;
                                }
                                //if the match is failed then show error message
                                else
                                {
                                    DisplayMessage(MessageManager.msg_FP_MatchFailed /*+ ret*/, false);
                                    return;
                                }
                            }
                            #endregion
                        }
                    }
                    break;

                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }



        /// <summary>
        /// FREE RESOURCES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnFree_Click(object sender, EventArgs e)
        {
            int result = fpInstance.Finalize();

            if (result == zkfp.ZKFP_ERR_OK)
            {
                DisconnectFingerPrintCounter();
                IsRegister = false;
                RegisterCount = 0;
                regTempLen = 0;
                ClearImage();
                cmbIdx.Items.Clear();
                Utilities.EnableControls(true, btnInit, button1);
                Utilities.EnableControls(false, btnFree, btnClose, btnEnroll, btnVerify, btnIdentify);

                DisplayMessage("Resources were successfully released from the memory !!", true);
            }
            else
                DisplayMessage("Failed to release the resources !!", false);
        }

        private void ClearImage()
        {
            picFPImg.Image = null;
            //pbxImage2.Image = null;
        }

        private void bnEnroll_Click(object sender, EventArgs e)
        {
            userType = 0;


            var registerUSerId = textBox1_SocialID.Text;
            ExAttDBRepo eA = new ExAttDBRepo();
            ExUniDBRepo eU = new ExUniDBRepo();

            var r = eA.IsRegisterdBefor(registerUSerId);
            if (r.Uni_ID > 0)
            {
                IsRegister = true;
                DisplayMessage("this Uni_ID Is registerd before", true);

                if (captureThread != null && captureThread.IsAlive)
                {
                    Utilities.EnableControls(false, btnEnroll);
                }

            }
            else
            {
                IsRegister = false;
            }
            var s = eU.GetStudent(registerUSerId);
            var em = eU.GetEmployee(registerUSerId);


            if (em.Emp_ID < 1 && s.Stu_ID < 1)
            {
                DisplayMessage("this Uni_ID Is not found", true);
                IsRegister = true;
            }


            //else if (s.Stu_ID < 1 )
            //{
            //    DisplayMessage("this Uni_ID Is not found", true);
            //    IsRegister = true;

            //}

            if (!IsRegister)
            {
                ClearImage();
                IsRegister = true;
                RegisterCount = 0;
                regTempLen = 0;
                Utilities.EnableControls(false, btnEnroll, button1, btnVerify, btnIdentify);
                DisplayMessage("Please press your finger " + REGISTER_FINGER_COUNT + " times to register", true);

                lblFingerPrintCount.Visible = true;
                lblFingerPrintCount.Text = REGISTER_FINGER_COUNT.ToString();
            }
            bnInit_Click();
        }




        public object PushToDevice(object args)
        {
            DisplayMessage("Pushed to fingerprint !", true);
            return null;
        }


        public void ReEnrollUser(bool enableEnroll, bool clearDeviceUser = true)
        {
            ClearImage();
            if (clearDeviceUser && !btnInit.Enabled) ClearDeviceUser();
            if (enableEnroll) btnEnroll.Enabled = true;
        }


        public void ClearDeviceUser()
        {
            try
            {
                int deleteCode = fpInstance.DelRegTemplate(iFid);   // <---- REMOVE FINGERPRINT
                if (deleteCode != zkfp.ZKFP_ERR_OK)
                {
                    DisplayMessage(MessageManager.msg_FP_UnableToDeleteFingerPrint + iFid, false);
                }
                iFid = 1;
            }
            catch { }

        }


        public bool ReleaseResources()
        {
            try
            {
                ReEnrollUser(true, true);
                bnClose_Click(null, null);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #region -------- CONNECT/DISCONNECT DEVICE --------



        /// <summary>
        /// DISCONNECT DEVICE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnClose_Click(object sender, EventArgs e)
        {
            OnDisconnect();
        }


        public void OnDisconnect()
        {
            bIsTimeToDie = true;
            RegisterCount = 0;
            DisconnectFingerPrintCounter();
            ClearImage();
            Thread.Sleep(1000);
            int result = fpInstance.CloseDevice();

            captureThread.Abort();
            if (result == zkfp.ZKFP_ERR_OK)
            {
                Utilities.EnableControls(false, btnInit, btnClose, btnEnroll, button1, btnVerify, btnIdentify);

                lblDeviceStatus.Text = Disconnected;

                Thread.Sleep(1000);
                result = fpInstance.Finalize();   // CLEAR RESOURCES

                if (result == zkfp.ZKFP_ERR_OK)
                {
                    regTempLen = 0;
                    IsRegister = false;
                    cmbIdx.Items.Clear();
                    Utilities.EnableControls(true, btnInit);
                    Utilities.EnableControls(false, btnClose, btnEnroll, button1, btnVerify, btnIdentify);

                    ReInitializeInstance();

                    DisplayMessage(MessageManager.msg_FP_Disconnected, true);
                }
                else
                    DisplayMessage(MessageManager.msg_FP_FailedToReleaseResources, false);


            }
            else
            {
                string errorMessage = FingerPrintDeviceUtilities.DisplayDeviceErrorByCode(result);
                DisplayMessage(errorMessage, false);
            }
        }


        #endregion



        #region ------- COMMON --------

        private void FingerPrintControl_Load(object sender, EventArgs e) { FormHandle = this.Handle; }

        private void ReInitializeInstance()
        {
            Utilities.EnableControls(true, btnInit);
            Utilities.EnableControls(false, btnClose, btnEnroll, button1, btnVerify, btnIdentify);
            DisconnectFingerPrintCounter();
            bIdentify = true;
            bIdentify2 = true;
            btnVerify.Text = VerifyButtonDefault;
        }

        private void DisconnectFingerPrintCounter()
        {
            lblFingerPrintCount.Text = REGISTER_FINGER_COUNT.ToString();
            lblFingerPrintCount.Visible = false;
        }

        #endregion


        #region -------- UTILITIES --------


        /// <summary>
        /// Combines Three Pre-Registered Fingerprint Templates as One Registered Fingerprint Template
        /// </summary>
        /// <returns></returns>
        private int GenerateRegisteredFingerPrint()
        {
            return fpInstance.GenerateRegTemplate(RegTmps[0], RegTmps[1], RegTmps[2], RegTmp, ref regTempLen);
        }

        /// <summary>
        /// Add A Registered Fingerprint Template To Memory | params: (FingerPrint ID, Registered Template)
        /// </summary>
        /// <returns></returns>
        private int AddTemplateToMemory()
        {
            return fpInstance.AddRegTemplate(iFid, RegTmp);
        }




        private void DisplayFingerPrintImage()
        {
            // NORMAL METHOD >>>

            //Bitmap fingerPrintImage = Utilities.GetImage(FPBuffer, fpInstance.imageWidth, fpInstance.imageHeight);
            //Rectangle cropRect = new Rectangle(0, 0, pbxImage2.Width / 2, pbxImage2.Height / 2);
            //Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
            //using (Graphics g = Graphics.FromImage(target))
            //{
            //    g.DrawImage(fingerPrintImage, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
            //}
            //this.pbxImage2.Image = target;



            // OPTIMIZED METHOD
            MemoryStream ms = new MemoryStream();
            BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
            Bitmap bmp = new Bitmap(ms);
            this.picFPImg.Image = bmp;

        }

        private void DisplayMessage(string message, bool normalMessage)
        {
            try
            {
                Utilities.ShowStatusBar(message, parentForm.statusBar, normalMessage);
            }
            catch (Exception ex)
            {
                Utilities.ShowStatusBar(ex.Message, parentForm.statusBar, false);
            }
        }



        #endregion

        private void button1_CheckSOcialID_Click(object sender, EventArgs e)
        {
            userType = 0;
            ExAttDBRepo eA = new ExAttDBRepo();

            long selectedStudentId = 0;
            if (textBox1_SocialID.Text.Length > 0)
            {
                selectedStudentId = long.Parse(textBox1_SocialID.Text);

                CanRegisterId = false;

                var u = new ExUniDBRepo();
                var a = new ExAttDBRepo();

                var allStudents = u.GetAllStudents();
                var allEmployees = u.GetAllEmployees();

                var selectedStudentModel = allStudents.Where(su => su.Stu_ID == selectedStudentId).FirstOrDefault();
                var selectedEmoloyeeModel = allEmployees.Where(em => em.Emp_ID == selectedStudentId).FirstOrDefault();



                var r = eA.IsRegisterdBefor(textBox1_SocialID.Text);



                if (selectedStudentModel != null && selectedStudentModel.Stu_ID > 0)
                {
                    label2_searchResult.Text = selectedStudentModel.Stu_ID + "," + selectedStudentModel.Stu_First_Name + " " + selectedStudentModel.Stu_Last_Name;

                    if (captureThread != null && captureThread.IsAlive && r.Uni_ID < 1)
                    {
                        CanRegisterId = true;
                        Utilities.EnableControls(true, btnEnroll);
                    }

                }
                else if (selectedEmoloyeeModel != null && selectedEmoloyeeModel.Emp_ID > 0)
                {
                    label2_searchResult.Text = selectedEmoloyeeModel.Emp_ID + "," + selectedEmoloyeeModel.Emp_First_Name + " " + selectedEmoloyeeModel.Emp_Last_Name;

                    if (captureThread != null && captureThread.IsAlive && r.Uni_ID < 1)
                    {
                        CanRegisterId = true;

                        Utilities.EnableControls(true, btnEnroll);
                    }
                }
                else
                {
                    label2_searchResult.Text = "Not Found";
                    Utilities.EnableControls(false, btnEnroll);

                }

                //MessageBox.Show("Regidsterd Befor");

            }
            else
            {
                label2_searchResult.Text = "Please fill out this field";
            }
        }



        private void textBox1_SocialID_TextChanged_1(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1_SocialID.Text, "[^0-9]") || textBox1_SocialID.Text.Length > 15)
            {
                MessageBox.Show("Please enter only numbers less than 15 digits.");
                textBox1_SocialID.Text = textBox1_SocialID.Text.Remove(textBox1_SocialID.Text.Length - 1);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = ComboBox_classesList.SelectedItem;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox_classesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utilities.EnableControls(true, btnVerify, button1);

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            userType = 2;

            if (bIdentify2)
            {
                bIdentify2 = false;
                button1.Text = VerifyButtonToggle;
                DisplayMessage(MessageManager.msg_FP_PressForVerification, true);


                bIdentify = true;
                btnVerify.Text = VerifyButtonDefault;
            }
            else
            {
                bIdentify2 = true;
                button1.Text = VerifyButtonDefault;
            }
        }
    }
}
