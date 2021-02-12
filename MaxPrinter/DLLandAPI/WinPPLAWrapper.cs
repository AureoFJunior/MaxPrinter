using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaxPrinter.DLLandAPI
{
    public class WinPPLAWrapper
    {

        public const uint IMAGE_BITMAP = 0;
        public const uint LR_LOADFROMFILE = 16;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType,
           int cxDesired, int cyDesired, uint fuLoad);
        [DllImport("Gdi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public  static extern int DeleteObject(IntPtr ho);

        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_Maxi(int x, int y, int primary, int secondary,
            int country, int service, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_Maxi_Ori(int x, int y, int ori, int primary,
            int secondary, int country, int service, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_PDF417(int x, int y, int narrow, int width, char normal,
            int security, int aspect, int row, int column, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_PDF417_Ori(int x, int y, int ori, int narrow, int width,
            char normal, int security, int aspect, int row, int column, char mode, int numeric,
            string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_DataMatrix(int x, int y, int rotation, int hor_mul,
            int ver_mul, int ECC, int data_format, int num_rows, int num_col, char mode,
            int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern void A_Clear_Memory();
        [DllImport("Winppla.dll")]
        public static extern void A_ClosePrn();
        [DllImport("Winppla.dll")]
        public static extern int A_CreatePrn(int selection, string filename);
        [DllImport("Winppla.dll")]
        public static extern int A_Del_Graphic(int mem_mode, string graphic);
        [DllImport("Winppla.dll")]
        public static extern int A_Draw_Box(char mode, int x, int y, int width, int height,
            int top, int side);
        [DllImport("Winppla.dll")]
        public static extern int A_Draw_Line(char mode, int x, int y, int width, int height);
        [DllImport("Winppla.dll")]
        public static extern void A_Feed_Label();
        [DllImport("Winppla.dll")]
        public static extern IntPtr A_Get_DLL_Version(int nShowMessage);
        [DllImport("Winppla.dll")]
        public static extern int A_Get_DLL_VersionA(int nShowMessage);
        [DllImport("Winppla.dll")]
        public static extern int A_Get_Graphic(int x, int y, int mem_mode, char format,
            string filename);
        [DllImport("Winppla.dll")]
        public static extern int A_Get_Graphic_ColorBMP(int x, int y, int mem_mode, char format,
            string filename);
        [DllImport("Winppla.dll")]
        public static extern int A_Get_Graphic_ColorBMPEx(int x, int y, int nWidth, int nHeight,
            int rotate, int mem_mode, char format, string id_name, string filename);
        [DllImport("Winppla.dll")]
        public static extern int A_Get_Graphic_ColorBMP_HBitmap(int x, int y, int nWidth, int nHeight,
           int rotate, int mem_mode, char format, string id_name, IntPtr hbm);
        [DllImport("Winppla.dll")]
        public static extern int A_Initial_Setting(int Type, string Source);
        [DllImport("Winppla.dll")]
        public static extern int A_WriteData(int IsImmediate, byte[] pbuf, int length);
        [DllImport("Winppla.dll")]
        public static extern int A_ReadData(byte[] pbuf, int length, int dwTimeoutms);
        [DllImport("Winppla.dll")]
        public static extern int A_Load_Graphic(int x, int y, string graphic_name);
        [DllImport("Winppla.dll")]
        public static extern int A_Open_ChineseFont(string path);
        [DllImport("Winppla.dll")]
        public static extern int A_Print_Form(int width, int height, int copies, int amount,
            string form_name);
        [DllImport("Winppla.dll")]
        public static extern int A_Print_Out(int width, int height, int copies, int amount);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Barcode(int x, int y, int ori, char type, int narrow,
            int width, int height, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text(int x, int y, int ori, int font, int type,
            int hor_factor, int ver_factor, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text_Chinese(int x, int y, int fonttype, string id_name,
            string data, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text_TrueType(int x, int y, int FSize, string FType,
            int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut, string id_name,
            string data, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text_TrueType_W(int x, int y, int FHeight, int FWidth,
            string FType, int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut,
            string id_name, string data, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Backfeed(int back);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_BMPSave(int nSave, string pstrBMPFName);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Cutting(int cutting);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Darkness(int heat);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_DebugDialog(int nEnable);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Feed(char rate);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Form(string formfile, string form_name, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Margin(int position, int margin);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Prncomport(int baud, int parity, int data, int stop);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Prncomport_PC(int nBaudRate, int nByteSize, int nParity,
            int nStopBits, int nDsr, int nCts, int nXonXoff);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Sensor_Mode(char type, int continuous);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Speed(char speed);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Syssetting(int transfer, int cut_peel, int length,
            int zero, int pause);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Unit(char unit);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Gap(int gap);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_Logic(int logic);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_ProcessDlg(int nShow);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_ErrorDlg(int nShow);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_LabelVer(int centiInch);
        [DllImport("Winppla.dll")]
        public static extern int A_GetUSBBufferLen();
        [DllImport("Winppla.dll")]
        public static extern int A_EnumUSB(byte[] buf);
        [DllImport("Winppla.dll")]
        public static extern int A_CreateUSBPort(int nPort);
        [DllImport("Winppla.dll")]
        public static extern int A_CreatePort(int nPortType, int nPort, string filename);
        [DllImport("Winppla.dll")]
        public static extern int A_Clear_MemoryEx(int nMode);
        [DllImport("Winppla.dll")]
        public static extern void A_Set_Mirror();
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_RSS(int x, int y, int ori, int ratio, int height,
            char rtype, int mult, int seg, string data1, string data2);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_QR_M(int x, int y, int ori, char mult, int value,
            int model, char error, int mask, char dinput, char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_Bar2d_QR_A(int x, int y, int ori, char mult, int value,
            char mode, int numeric, string data);
        [DllImport("Winppla.dll")]
        public static extern int A_GetNetPrinterBufferLen();
        [DllImport("Winppla.dll")]
        public static extern int A_EnumNetPrinter(byte[] buf);
        [DllImport("Winppla.dll")]
        public static extern int A_CreateNetPort(int nPort);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text_TrueType_Uni(int x, int y, int FSize, string FType,
            int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut, string id_name,
            byte[] data, int format, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_Prn_Text_TrueType_UniB(int x, int y, int FSize, string FType,
            int Fspin, int FWeight, int FItalic, int FUnline, int FStrikeOut, string id_name,
            byte[] data, int format, int mem_mode);
        [DllImport("Winppla.dll")]
        public static extern int A_GetUSBDeviceInfo(int nPort, byte[] pDeviceName,
            out int pDeviceNameLen, byte[] pDevicePath, out int pDevicePathLen);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_EncryptionKey(string encryptionKey);
        [DllImport("Winppla.dll")]
        public static extern int A_Check_EncryptionKey(string decodeKey, string encryptionKey,
            int dwTimeoutms);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_CommTimeout(int ReadTotalTimeoutConstant, int WriteTotalTimeoutConstant);
        [DllImport("Winppla.dll")]
        public static extern int A_Get_CommTimeout(out int ReadTotalTimeoutConstant, out int WriteTotalTimeoutConstant);
        [DllImport("Winppla.dll")]
        public static extern int A_Set_LabelForSmartPrint(int lablength, int gaplength);
    }
}
