﻿using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingLibrary.hjb.file;
using SXJLibrary;
using System.IO;
using BingLibrary.hjb;
using System.Data;
using samrtind;
using System.Collections.ObjectModel;

namespace HATrackScanUI.ViewModels
{
    class MainWindowViewModel : NotificationObject
    {
        #region 属性
        private string windowTitle;

        public string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                windowTitle = value;
                this.RaisePropertyChanged("WindowTitle");
            }
        }
        private string messageStr;

        public string MessageStr
        {
            get { return messageStr; }
            set
            {
                messageStr = value;
                this.RaisePropertyChanged("MessageStr");
            }
        }
        private string lOCATIONID;

        public string LOCATIONID
        {
            get { return lOCATIONID; }
            set
            {
                lOCATIONID = value;
                this.RaisePropertyChanged("LOCATIONID");
            }
        }

        private string fACTORYID;

        public string FACTORYID
        {
            get { return fACTORYID; }
            set
            {
                fACTORYID = value;
                this.RaisePropertyChanged("FACTORYID");
            }
        }
        private string bPEMPID;

        public string BPEMPID
        {
            get { return bPEMPID; }
            set
            {
                bPEMPID = value;
                this.RaisePropertyChanged("BPEMPID");
            }
        }
        private string bPWORKNO;

        public string BPWORKNO
        {
            get { return bPWORKNO; }
            set
            {
                bPWORKNO = value;
                this.RaisePropertyChanged("BPWORKNO");
            }
        }
        private string bPLINE;

        public string BPLINE
        {
            get { return bPLINE; }
            set
            {
                bPLINE = value;
                this.RaisePropertyChanged("BPLINE");
            }
        }
        private string bPIP;

        public string BPIP
        {
            get { return bPIP; }
            set
            {
                bPIP = value;
                this.RaisePropertyChanged("BPIP");
            }
        }
        private string bP01;

        public string BP01
        {
            get { return bP01; }
            set
            {
                bP01 = value;
                this.RaisePropertyChanged("BP01");
            }
        }
        private string saveButtonContent;

        public string SaveButtonContent
        {
            get { return saveButtonContent; }
            set
            {
                saveButtonContent = value;
                this.RaisePropertyChanged("SaveButtonContent");
            }
        }
        private bool textParmReadOnly;

        public bool TextParmReadOnly
        {
            get { return textParmReadOnly; }
            set
            {
                textParmReadOnly = value;
                this.RaisePropertyChanged("TextParmReadOnly");
            }
        }
        private string bP04;

        public string BP04
        {
            get { return bP04; }
            set
            {
                bP04 = value;
                this.RaisePropertyChanged("BP04");
            }
        }
        private string bP11;

        public string BP11
        {
            get { return bP11; }
            set
            {
                bP11 = value;
                this.RaisePropertyChanged("BP11");
            }
        }
        private string bPPNL;

        public string BPPNL
        {
            get { return bPPNL; }
            set
            {
                bPPNL = value;
                this.RaisePropertyChanged("BPPNL");
            }
        }
        private string homePageVisibility;

        public string HomePageVisibility
        {
            get { return homePageVisibility; }
            set
            {
                homePageVisibility = value;
                this.RaisePropertyChanged("HomePageVisibility");
            }
        }
        private string checkPageVisibility;

        public string CheckPageVisibility
        {
            get { return checkPageVisibility; }
            set
            {
                checkPageVisibility = value;
                this.RaisePropertyChanged("CheckPageVisibility");
            }
        }
        private string checkBarcode;

        public string CheckBarcode
        {
            get { return checkBarcode; }
            set
            {
                checkBarcode = value;
                this.RaisePropertyChanged("CheckBarcode");
            }
        }
        private DataTable checkItemsSource;

        public DataTable CheckItemsSource
        {
            get { return checkItemsSource; }
            set
            {
                checkItemsSource = value;
                this.RaisePropertyChanged("CheckItemsSource");
            }
        }
        private bool sys_status;

        public bool Sys_status
        {
            get { return sys_status; }
            set
            {
                sys_status = value;
                this.RaisePropertyChanged("Sys_status");
            }
        }
        private ObservableCollection<bool> ioInput;

        public ObservableCollection<bool> IoInput
        {
            get { return ioInput; }
            set
            {
                ioInput = value;
                this.RaisePropertyChanged("IoInput");
            }
        }
        private ObservableCollection<bool> ioOutput;

        public ObservableCollection<bool> IoOutput
        {
            get { return ioOutput; }
            set
            {
                ioOutput = value;
                this.RaisePropertyChanged("IoOutput");
            }
        }


        #endregion
        #region 方法绑定
        public DelegateCommand FuncTestCommand { get; set; }
        public DelegateCommand EditSaveCommand { get; set; }
        public DelegateCommand<object> ChoosePageCommand { get; set; }
        public DelegateCommand CheckCommand { get; set; }
        #endregion
        #region 变量
        string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        Scan Scan1 = new Scan(); IntPtr hMotion;
        #endregion
        #region 构造函数
        public MainWindowViewModel()
        {
            #region 方法绑定
            this.FuncTestCommand = new DelegateCommand(new Action(this.FuncTestCommandExecute));
            this.EditSaveCommand = new DelegateCommand(new Action(this.EditSaveCommandExecute));
            this.ChoosePageCommand = new DelegateCommand<object>(new Action<object>(this.ChoosePageCommandExecute));
            this.CheckCommand = new DelegateCommand(new Action(this.CheckCommandExecute));
            #endregion
            #region 界面元素初始化
            this.MessageStr = "";
            this.WindowTitle = "淮安鹏鼎轨道扫码软体";
            this.SaveButtonContent = "Edit";
            this.TextParmReadOnly = true;
            LOCATIONID = Inifile.INIGetStringValue(iniParameterPath, "System", "LOCATIONID", "HA");
            FACTORYID = Inifile.INIGetStringValue(iniParameterPath, "System", "FACTORYID", "A1-2F");
            BPEMPID = Inifile.INIGetStringValue(iniParameterPath, "System", "BPEMPID", "F7052804");
            BPWORKNO = Inifile.INIGetStringValue(iniParameterPath, "System", "BPWORKNO", "1000080146");
            BPLINE = Inifile.INIGetStringValue(iniParameterPath, "System", "BPLINE", "S14-302");
            BPIP = Inifile.INIGetStringValue(iniParameterPath, "System", "BPIP", "01");
            BP01 = Inifile.INIGetStringValue(iniParameterPath, "System", "BP01", "F0AP0194A0Q");
            BP04 = Inifile.INIGetStringValue(iniParameterPath, "System", "BP04", "FHAPGD6A7X1SBD180324121");
            BP11 = Inifile.INIGetStringValue(iniParameterPath, "System", "BP11", "FHAPGD6A7X1SBDC180324314");
            BPPNL = Inifile.INIGetStringValue(iniParameterPath, "System", "BPPNL", "A00110662A12210172");
            CheckBarcode = Inifile.INIGetStringValue(iniParameterPath, "System", "CheckBarcode", "A00110662A12210172");
            HomePageVisibility = "Visible";
            CheckPageVisibility = "Collapsed";
            #endregion
            #region 部件
            string COM = Inifile.INIGetStringValue(iniParameterPath, "Scan", "COM", "COM0");
            Scan1.ini(COM);
            #endregion
            #region 初始化IO卡
            IoInput = new ObservableCollection<bool>();
            for (int i = 0; i < 24; i++)
            {
                IoInput.Add(false);
            }
            IoOutput = new ObservableCollection<bool>();
            for (int i = 0; i < 16; i++)
            {
                IoOutput.Add(false);
            }
            IOCardRun();
            #endregion
            #region 更新本地时间
            try
            {
                SXJLibrary.Oracle oraDB = new SXJLibrary.Oracle("qddb04.eavarytech.com", "mesdb04", "ictdata", "ictdata*168");
                if (oraDB.isConnect())
                {
                    string oracleTime = oraDB.OraclDateTime();
                    AddMessage("更新数据库时间到本地" + oracleTime);
                }
                oraDB.disconnect();
            }
            catch (Exception ex)
            {
                AddMessage(ex.Message);
            }

            #endregion
        }
        #endregion
        #region 方法绑定函数
        private void FuncTestCommandExecute()
        {
            //Inifile.INIWriteValue(iniParameterPath, "Scan", "COM", "COM0");
            byte InputStatus = 0;
            si.co_motion_getDi(hMotion, 1, ref InputStatus);
            AddMessage("功能执行完成");
        }
        private void EditSaveCommandExecute()
        {
            if (TextParmReadOnly)
            {
                TextParmReadOnly = false;
                SaveButtonContent = "Save";
            }
            else
            {
                TextParmReadOnly = true;
                SaveButtonContent = "Edit";
                Inifile.INIWriteValue(iniParameterPath, "System", "LOCATIONID", LOCATIONID);
                Inifile.INIWriteValue(iniParameterPath, "System", "FACTORYID", FACTORYID);
                Inifile.INIWriteValue(iniParameterPath, "System", "BPEMPID", BPEMPID);
                Inifile.INIWriteValue(iniParameterPath, "System", "BPWORKNO", BPWORKNO);
                Inifile.INIWriteValue(iniParameterPath, "System", "BPLINE", BPLINE);
                Inifile.INIWriteValue(iniParameterPath, "System", "BPIP", BPIP);
                Inifile.INIWriteValue(iniParameterPath, "System", "BP01", BP01);
                AddMessage("参数保存完成");
            }
        }
        private void ChoosePageCommandExecute(object p)
        {
            switch (p.ToString())
            {
                case "0":
                    HomePageVisibility = "Visible";
                    CheckPageVisibility = "Collapsed";
                    break;
                case "1":
                    HomePageVisibility = "Collapsed";
                    CheckPageVisibility = "Visible";
                    break;
                default:
                    break;
            }

        }
        private void CheckCommandExecute()
        {
            try
            {
                if (CheckBarcode != "")
                {
                    SXJLibrary.Oracle oracle = new SXJLibrary.Oracle("qddb07.eavarytech.com", "mesdb07", "sfcabar", "sfcabar*168");
                    if (oracle.isConnect())
                    {
                        string stm = string.Format("SELECT * FROM SFCDATA.BARAUTPNL WHERE BPPNL = '{0}' ORDER BY BPDATE DESC,BPTIME DESC", CheckBarcode);
                        DataSet ds = oracle.executeQuery(stm);
                        Inifile.INIWriteValue(iniParameterPath, "System", "CheckBarcode", CheckBarcode);
                        CheckItemsSource = ds.Tables[0];
                    }
                    oracle.disconnect();
                }
            }
            catch (Exception ex)
            {
                AddMessage(ex.Message);
            }
        }
        #endregion
        #region 自定义函数
        private string GetBanci()
        {
            string rs = "";
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20)
            {
                rs += DateTime.Now.ToString("yyyyMMdd") + "_D";
            }
            else
            {
                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 8)
                {
                    rs += DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "_N";
                }
                else
                {
                    rs += DateTime.Now.ToString("yyyyMMdd") + "_N";
                }
            }
            return rs;
        }
        void AddMessage(string str)
        {
            string[] s = MessageStr.Split('\n');
            if (s.Length > 1000)
            {
                MessageStr = "";
            }
            if (MessageStr != "")
            {
                MessageStr += "\n";
            }
            MessageStr += System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " " + str;
        }
        void Scan1GetBarcodeCallback(string barcode)
        {
            AddMessage(barcode);
            if (barcode != "Error")
            {
                try
                {
                    //载具码、盖片码、产品码
                    string[] barcodes = barcode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    BP04 = barcodes[0];
                    Inifile.INIWriteValue(iniParameterPath, "System", "BP04", BP04);
                    BP11 = barcodes[1];
                    Inifile.INIWriteValue(iniParameterPath, "System", "BP11", BP11);
                    BPPNL = barcodes[2];
                    Inifile.INIWriteValue(iniParameterPath, "System", "BPPNL", BPPNL);
                    if (!Directory.Exists(Path.Combine(System.Environment.CurrentDirectory, GetBanci())))
                    {
                        Directory.CreateDirectory(Path.Combine(System.Environment.CurrentDirectory, GetBanci()));
                    }
                    string path = Path.Combine(System.Environment.CurrentDirectory, GetBanci(), "Barcode.csv");
                    Csvfile.savetocsv(path, new string[] { DateTime.Now.ToString(), BP04, BP11, BPPNL });
                    SXJLibrary.Oracle oracle = new SXJLibrary.Oracle("qddb07.eavarytech.com", "mesdb07", "sfcabar", "sfcabar*168");
                    if (oracle.isConnect())
                    {
                        string stm = string.Format("INSERT INTO SFCDATA.BARAUTPNL (LOCATIONID,FACTORYID,BPDATE,BPTIME,BPEMPID,BPWORKNO,BPLINE,BPIP,BPPNL,BP01,BP04,BP11) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')"
                            , LOCATIONID, FACTORYID, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"), BPEMPID, BPWORKNO, BPLINE, BPIP, BPPNL, BP01, BP04, BP11);
                        int rst = oracle.executeNonQuery(stm);
                        AddMessage("插入数据" + rst.ToString());
                    }
                    oracle.disconnect();
                }
                catch (Exception ex)
                {
                    AddMessage(ex.Message);
                }
            }
            else
            {
                //扫码失败
            }
        }
        private void IOCardRun()
        {
            Task.Run(() => {
                hMotion = si.co_motion_open("C:/Program Files/Smartind Inc/SmartSys/bin");          //这里可以输入通过SmartSysManager导出的文件夹也可以直接使用软件安装目录"C:/Program Files/Smartind Inc/SmartSys/bin"
                if (hMotion == (IntPtr)0)
                {
                    AddMessage("IO卡初始化失败");
                    return;
                }
                si.co_motion_setSyncPeriod(hMotion, 4000);                                          //设置总线控制周期为4毫秒
                si.co_motion_active(hMotion);                                                       //激活内核及总线上挂接的从站设备，并将设备切换到正常运行状态

                AddMessage("IO卡初始化完成");
                while (true)
                {
                    System.Threading.Thread.Sleep(10);
                    int status = 0;
                    int timeout = 2500;                                                                 //2500 *4 = 10000ms = 10s

                    while (true)
                    {
                        si.co_motion_getLinkStatus(hMotion, ref status);                              //获取总线网络连接状态，status = 0 表示网络连接正常，其他值表示网络连接异常，异常原因 1 初始化失败 2 总线配置与实际不对应

                        if (status == 0 || timeout == 0)                                                //status 1: link error 0: no error
                        {
                            Sys_status = true;
                            break;
                        }
                        System.Threading.Thread.Sleep(4);                                                                //sleep 4ms
                        timeout--;
                    }

                    if (timeout == 0)
                    {
                        Sys_status = false;
                        //AddMessage("SA1400总线网络连接超时!");                                       //获取总线网络连接超时
                    }
                    else
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            byte input = 0;
                            si.co_motion_getDi(hMotion, i, ref input);
                            IoInput[i] = input == 1;
                        }
                        for (int i = 0; i < 16; i++)
                        {
                            byte output = (byte)(IoOutput[i] ? 1 : 0);
                            si.co_motion_setDo(hMotion,i, output);
                        }
                    }
                }
            });
        }
        #endregion

    }
}
