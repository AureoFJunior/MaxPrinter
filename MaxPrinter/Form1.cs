using Apolíneo;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MaxPrinter.DLLandAPI.WinPPLAWrapper;

namespace MaxPrinter
{
    public partial class Form1 : Form
    {
        int mov, x, y;
        private const String COM = "COM2";
        private static String pesoBruto = "";
        private static String pesoTarado = "";
        private SerialPort mPortaSerial;

        private Double mSegundosOK = 2.0;
        private Double mUltimoPeso = 0.0;
        private Double mUltimoPesoImpresso = 0.0;
        private DateTime mUltimaLeitura = DateTime.MinValue;
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //obterPesoBruto("PB:   40,5kg PL:    0,0kg T:    0,0kg");
            openSerial();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            mPortaSerial?.Close();
        }

        protected override void WndProc(ref Message m) //another way to move windows form
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            x = e.X;
            y = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            x = e.X;
            y = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //obterPesoBruto("PB:+ 35.66kgPB:+ 35.66kgPB:+ 35.66kg "); 
            doThis();

        }

        private void doThis()
        {
            var prod = getBd(getInformations());
            if (prod != null)
            {
                var aux = getInformations();

                openTheDoor();
                //Imprimir(prod.codDesc, DateTime.Now, prod.codBar, aux.pesoTara, pesoBruto, pesoTarado);
                Imprimir(prod.codDesc, DateTime.Now, prod.codBar, aux.pesoTara, pesoBruto, pesoTarado);

                closeTheDoor();

            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y);
            }
        }


        public Infos getInformations()
        {
            var objInf = new Infos();

            if (!String.IsNullOrEmpty(txtCod.Text))
            {
                objInf.codProd = txtCod.Text;

            }
            else
            {
                MessageBox.Show("Campo código do produto obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            if (!String.IsNullOrEmpty(txtTara.Text))
            {
                if (Convert.ToDouble(txtTara.Text) > 0)
                {
                    objInf.pesoTara = Convert.ToDouble(txtTara.Text);
                }
            }

            else
            {
                objInf.pesoTara = 0;
            }


            if (Convert.ToDouble(txtQtd.Value) > 0)
            {

                objInf.qtdeCaixa = Convert.ToInt32(txtQtd.Value);
                objInf.pesoTara = objInf.pesoTara * objInf.qtdeCaixa;



            }
            else
            {
                objInf.qtdeCaixa = 0;
            }


            return objInf;
        }

        public Prod getBd(Infos obj)
        {
            if (obj == null) return null;
            var item = new Prod();

            String sql = $"select prod_descrpdv, prod_codbarras from produtos where prod_codigo = {obj.codProd} ";

            NpgsqlConnection connBd = new NpgsqlConnection("Server = 192.168.1.100; Port = 5432; " +
                                                           "Database = interativo; User Id = postgres;" +
                                                           " Password = 123456");

            var cmd = new NpgsqlCommand(sql, connBd);

            try
            {
                connBd.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erro: {e}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var dr = cmd.ExecuteReader();



            if (dr.Read())
            {
                item.codDesc = dr.GetSafeValue<String>("prod_descrpdv");
                item.codBar = dr.GetSafeValue<String>("prod_codbarras");

                return item;
            }

            else
            {
                return item;
            }





        }

        public void openTheDoor()
        {

            //Test code start
            // open port.
            int nLen, ret, sw;
            byte[] pbuf = new byte[128];
            string strmsg;
            IntPtr ver;
            System.Text.Encoding encAscII = System.Text.Encoding.ASCII;
            System.Text.Encoding encUnicode = System.Text.Encoding.Unicode;

            // dll version.
            ver = A_Get_DLL_Version(0);

            // search port.
            nLen = A_GetUSBBufferLen() + 1;
            strmsg = "DLL ";
            strmsg += Marshal.PtrToStringAnsi(ver);
            strmsg += "\r\n";
            if (nLen > 1)
            {
                byte[] buf1, buf2;
                int len1 = 128, len2 = 128;
                buf1 = new byte[len1];
                buf2 = new byte[len2];
                A_EnumUSB(pbuf);
                A_GetUSBDeviceInfo(1, buf1, out len1, buf2, out len2);
                sw = 1;
                if (1 == sw)
                {
                    ret = A_CreatePrn(12, encAscII.GetString(buf2, 0, len2));// open usb.
                }
                else
                {
                    ret = A_CreateUSBPort(1);// must call A_GetUSBBufferLen() function fisrt.
                }
                if (0 != ret)
                {
                    strmsg += "Open USB fail!";
                }
                else
                {
                    strmsg += "Open USB:\r\nDevice name: ";
                    strmsg += encAscII.GetString(buf1, 0, len1);
                    strmsg += "\r\nDevice path: ";
                    strmsg += encAscII.GetString(buf2, 0, len2);
                    //sw = 2;
                    if (2 == sw)
                    {
                        //get printer status.
                        pbuf[0] = 0x01;
                        pbuf[1] = 0x46;
                        pbuf[2] = 0x0D;
                        pbuf[3] = 0x0A;
                        A_WriteData(1, pbuf, 4);//<SOH>F
                        ret = A_ReadData(pbuf, 2, 1000);
                    }
                }
            }
            else
            {
                throw new Exception("A Impressora não está conectada");
                
            }
            if (0 != ret)
            {
                throw new Exception("Erro:" + strmsg);
            }

        }

        public static void closeTheDoor()
        {
            // close port.
            A_ClosePrn();
        }

        private void txtCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCod.FindForm().SelectNextControl(txtCod, true, true, true, false);
                e.SuppressKeyPress = true;
            }
        }

        private void txtTara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTara.FindForm().SelectNextControl(txtTara, true, true, true, false);
                e.SuppressKeyPress = true;
            }
        }

        private void txtQtd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQtd.FindForm().SelectNextControl(txtQtd, true, true, true, false);
                e.SuppressKeyPress = true;
            }
        }

        public static void Imprimir(String nomeProd, DateTime data, String codProd, Double pesoTara, String peel, String peel2)
            //Imprimir(String nomeProd, DateTime data, String codProd, Double pesoTara, String peel, String peel2)
        {
            var pesoAux = peel.Replace('.', ',');
            var pesoBruto = Convert.ToDouble(pesoAux);

            var pesoAux2 = peel2.Replace('.', ',');
            var pesoTaradoF = Convert.ToDouble(pesoAux2);



            // sample setting.
            A_Set_DebugDialog(1);
            A_Set_Unit('n');
            A_Set_Syssetting(2, 0, 0, 0, 0);
            A_Set_Darkness(10);
            A_Del_Graphic(1, "*");// delete all picture.
            A_Clear_Memory();// clear memory.

            



            A_Draw_Box('A', 45, 0, 330, 200, 1, 1);
            A_Prn_Text_TrueType(65, 170, 32, "Arial", 1, 700, 0, 0, 0, "AA1", "AGRO IND FRIOSUL LTDA", 1);



            A_Prn_Text_TrueType(55, 160, 25, "Arial", 1, 750, 0, 0, 0, "AA2", "Produto", 1);
            A_Prn_Text_TrueType(55, 145, 30, "Arial", 1, 50, 0, 0, 0, "AA4", nomeProd.Trim(), 1);
            A_Prn_Text_TrueType(55, 130, 25, "Arial", 1, 750, 0, 0, 0, "AA8", "Peso Líquido:", 1);
            A_Prn_Text_TrueType(200, 130, 25, "Arial", 1, 750, 0, 0, 0, "AA9", "Peso Tara:", 1);


            if (pesoTara >= 0)
            {
                if (pesoBruto < pesoTara)
                {
                    MessageBox.Show("Peso de Tara deve ser\nmenor do que o peso bruto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var pesoLiq = pesoBruto - pesoTara - pesoTaradoF;
                    A_Prn_Text_TrueType(125, 130, 30, "Arial", 1, 50, 0, 0, 0, "AA5", pesoLiq.ToString("0.00").Trim() + " KG", 1);
                    //A_Prn_Text_TrueType(250, 130, 30, "Arial", 1, 50, 0, 0, 0, "AA5", pesoTaradoF.ToString("0.00").Trim() + " KG", 1); 
                    A_Prn_Text_TrueType(265, 130, 30, "Arial", 1, 50, 0, 0, 0, "AA10", pesoTara.ToString("0.00").Trim() + " KG", 1);

                    A_Draw_Line('A', 45, 120, 280, 1);
                    A_Prn_Text_TrueType(55, 85, 30, "Arial", 1, 50, 0, 0, 0, "AA6", data.ToString(), 1);

                    var cod = codProd.Substring(codProd.Length - 5);
                    var liq = (pesoLiq * 100).ToString("0");
                    A_Prn_Barcode(55, 20, 1, 'F', 0, 0, 50, 'B', 1, $"2{cod}{liq.PadLeft(6, '0')}");

                    A_Print_Out(1, 1, 1, 1);
                }
            }
            else
            {
                A_Prn_Text_TrueType(150, 130, 30, "Arial", 1, 50, 0, 0, 0, "AA5", pesoBruto.ToString().Trim() + " KG", 1);

                A_Print_Out(1, 1, 1, 1);
            }


        }


        public void openSerial()
        {
            mPortaSerial = new SerialPort(COM);

            mPortaSerial.BaudRate = 9600;
            mPortaSerial.Parity = Parity.None;
            mPortaSerial.StopBits = StopBits.One;
            mPortaSerial.DataBits = 8;
            mPortaSerial.Handshake = Handshake.None;
            mPortaSerial.RtsEnable = true;

            mPortaSerial.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            mPortaSerial.Open();

        }



        private void DataReceivedHandler(
                       object sender,
                       SerialDataReceivedEventArgs e)
        {

            SerialPort sp = (SerialPort)sender;
            var aux = sp.ReadLine();

            var lista = aux.Split("\n");
            var linhaOK = lista.Where(t => t.StartsWith("PB")).ToList().LastOrDefault();

            if (String.IsNullOrEmpty(linhaOK)) return;

            //Arrumar bytes!!
            if (linhaOK.Length < 35) return;

            //System.Windows.Forms.MessageBox.Show(linhaOK);
            var peso = obterPesoBruto(linhaOK);
            var taraPeso = obterTaraOriginal(linhaOK);
            var pesoMostrado = peso - taraPeso;

            //Log($"{linhaOK} | {peso.ToString("0.00")} | {taraPeso.ToString("0.00")}");
            //var peso = obterPesoBruto("PB:   40,5kg PL:    0,0kg T:    0,0kg");

            var invoke = (MethodInvoker)(() => lblPeso.Text = pesoMostrado.ToString("0.00") + " KG");
            lblPeso.Invoke(invoke);

            //Log("Primeiro IF");
            if (mUltimaLeitura != DateTime.MinValue)
            {
                //Log("Segundo IF");
                if (mUltimoPeso == pesoMostrado && pesoMostrado != mUltimoPesoImpresso) 
                {
                    //Log("Terceiro IF");
                    TimeSpan ts = (DateTime.Now - mUltimaLeitura);
                    if (Math.Abs(ts.TotalSeconds) >= mSegundosOK)
                    {
                        //Log($"----{ts.TotalSeconds} e {mSegundosOK}------");
                        mUltimoPesoImpresso = mUltimoPeso;
                        //MessageBox.Show("Impressão");
                        //Log("Msgbox");//imprimir
                        //btnPrint_Click(btnPrint, new EventArgs());
                        doThis();
                        //Log("Feito!");
                    }
                }
            }

            if (mUltimoPeso != pesoMostrado)
                mUltimaLeitura = DateTime.Now;

            mUltimoPeso = pesoMostrado;
            

        }

        private static Double obterPesoBruto(String pesoLido)
        {
            Double convertido = -1.0;
            //Double convertido2 = -1.0;
            //String tmp = pesoLido.Substring(4, pesoLido.Length - 29 - 5);
            if (pesoLido.Length >= 35)
            {
                try
                {
                    String tmp = pesoLido.Substring(3, 7);
                    //String tmp2 = pesoLido.Substring(16, 7);

                    //MessageBox.Show("tmp: " + tmp + "\ntmp2: " + tmp2, "aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //todo Atualizar os valores em tempo real.

                    //PB:   40,5kg PL:    0,0kg T:    0,0kg

                    //System.Windows.Forms.MessageBox.Show(tmp.Trim());
                    convertido = Convert.ToDouble(tmp.Trim());
                    //convertido2 = Convert.ToDouble(tmp2.Trim());

                    if (pesoLido != null)
                    {
                        //if (convertido2 == 0)
                            pesoBruto = tmp;
                        //else
                        //{
                        //    pesoBruto = tmp2;
                        //}
                    }
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show($"Peso lido {pesoLido} \n {e.Message.ToString()} \n Favor, verificar conexão com a balança");
                }

            }

            //if (convertido2 == 0)
                return convertido;
            //else
            //{
            //    return convertido2;
            //}
        }

        private static Double obterTaraOriginal(String pesoLido)
        {
            Double convertido = -1.0;
            //String tmp = pesoLido.Substring(4, pesoLido.Length - 29 - 5);
            if (pesoLido.Length >= 35)
            {
                try
                {
                    String tmp = pesoLido.Substring(28 , 7);

                    //MessageBox.Show("tmp: " + tmp + "\ntmp2: " + tmp, "aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //todo Atualizar os valores em tempo real.

                    //PB:   40,5kg PL:    0,0kg T:    0,0kg

                    //System.Windows.Forms.MessageBox.Show(tmp.Trim());
                    convertido = Convert.ToDouble(tmp.Trim());
                    //pesoLido.Substring(0, pesoLido.Length - 3);

                    if (pesoLido != null)
                    {
                        pesoTarado = tmp;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Peso lido  {pesoLido} \n {e.Message.ToString()} \n Favor, verificar conexão com a balança" );
                }

            }

            return convertido;

        }

        private static void Log(String msg)
        {
            var sr = System.IO.File.AppendText("log.txt");
            sr.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}]-{msg} {Environment.NewLine}");
            sr.Close();


        }
    }
}
