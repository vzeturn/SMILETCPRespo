﻿/*
 * Created by SharpDevelop.
 * User: Jayan Nair
 * Date: 7/9/2004
 * Time: 2:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace DefaultNamespace
{
	/// <summary>
	/// Description of SocketClient.	
	/// </summary>
	public class SocketClient : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonDisconnect;
		private System.Windows.Forms.TextBox textBoxIP;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.RichTextBox richTextRxMessage;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxConnectStatus;
		private System.Windows.Forms.RichTextBox richTextTxMessage;
		private System.Windows.Forms.Button buttonSendMessage;
		private System.Windows.Forms.Button buttonClose;
		
		byte[] m_dataBuffer = new byte [10];
		IAsyncResult m_result;
		public AsyncCallback m_pfnCallBack ;
		private System.Windows.Forms.Button btnClear;
        private TextBox tbTotalChar;
        public Socket m_clientSocket;
		
		public SocketClient()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            //textBoxIP.Text = GetIP();
            textBoxIP.Text= "52.78.211.88";

        }
        private const byte stx = 0x02;
        private const byte etx = 0x03;
        private byte[] WrapString(string send)
        {
            int length = send.Length;
            byte[] data = new byte[length + 2];
            data[0] = stx;
            data[length + 1] = etx;
            Array.Copy(System.Text.Encoding.ASCII.GetBytes(send), 0, data, 1, length);
            return data;
        }
        [STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new SocketClient());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSendMessage = new System.Windows.Forms.Button();
            this.richTextTxMessage = new System.Windows.Forms.RichTextBox();
            this.textBoxConnectStatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextRxMessage = new System.Windows.Forms.RichTextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbTotalChar = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(440, 216);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(64, 24);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Location = new System.Drawing.Point(8, 184);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(240, 24);
            this.buttonSendMessage.TabIndex = 14;
            this.buttonSendMessage.Text = "Send Message";
            this.buttonSendMessage.Click += new System.EventHandler(this.ButtonSendMessageClick);
            // 
            // richTextTxMessage
            // 
            this.richTextTxMessage.Location = new System.Drawing.Point(8, 80);
            this.richTextTxMessage.Name = "richTextTxMessage";
            this.richTextTxMessage.Size = new System.Drawing.Size(240, 96);
            this.richTextTxMessage.TabIndex = 2;
            this.richTextTxMessage.Text = "";
            // 
            // textBoxConnectStatus
            // 
            this.textBoxConnectStatus.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxConnectStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxConnectStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textBoxConnectStatus.Location = new System.Drawing.Point(128, 224);
            this.textBoxConnectStatus.Name = "textBoxConnectStatus";
            this.textBoxConnectStatus.ReadOnly = true;
            this.textBoxConnectStatus.Size = new System.Drawing.Size(240, 13);
            this.textBoxConnectStatus.TabIndex = 10;
            this.textBoxConnectStatus.Text = "Not Connected";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Message To Server";
            // 
            // richTextRxMessage
            // 
            this.richTextRxMessage.BackColor = System.Drawing.Color.LightGreen;
            this.richTextRxMessage.Location = new System.Drawing.Point(256, 80);
            this.richTextRxMessage.Name = "richTextRxMessage";
            this.richTextRxMessage.ReadOnly = true;
            this.richTextRxMessage.Size = new System.Drawing.Size(562, 128);
            this.richTextRxMessage.TabIndex = 1;
            this.richTextRxMessage.Text = "";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(112, 31);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(48, 20);
            this.textBoxPort.TabIndex = 6;
            this.textBoxPort.Text = "30001";
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.SystemColors.HotTrack;
            this.buttonConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.ForeColor = System.Drawing.Color.Yellow;
            this.buttonConnect.Location = new System.Drawing.Point(344, 8);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(72, 48);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "Connect To Server";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnectClick);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Connection Status";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(112, 8);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(152, 20);
            this.textBoxIP.TabIndex = 3;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.BackColor = System.Drawing.Color.Red;
            this.buttonDisconnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDisconnect.ForeColor = System.Drawing.Color.Yellow;
            this.buttonDisconnect.Location = new System.Drawing.Point(432, 8);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(72, 48);
            this.buttonDisconnect.TabIndex = 15;
            this.buttonDisconnect.Text = "Disconnet From Server";
            this.buttonDisconnect.UseVisualStyleBackColor = false;
            this.buttonDisconnect.Click += new System.EventHandler(this.ButtonDisconnectClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server IP Address";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server Port";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(256, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Message From Server";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(376, 216);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 24);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbTotalChar
            // 
            this.tbTotalChar.Enabled = false;
            this.tbTotalChar.Location = new System.Drawing.Point(521, 219);
            this.tbTotalChar.Name = "tbTotalChar";
            this.tbTotalChar.Size = new System.Drawing.Size(100, 20);
            this.tbTotalChar.TabIndex = 17;
            // 
            // SocketClient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(830, 244);
            this.Controls.Add(this.tbTotalChar);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonSendMessage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxConnectStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.richTextTxMessage);
            this.Controls.Add(this.richTextRxMessage);
            this.Name = "SocketClient";
            this.Text = "Socket Client";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		void ButtonCloseClick(object sender, System.EventArgs e)
		{
			if ( m_clientSocket != null )
			{
				m_clientSocket.Close ();
				m_clientSocket = null;
			}		
			Close();
		}
		
		void ButtonConnectClick(object sender, System.EventArgs e)
		{
			// See if we have text on the IP and Port text fields
			if(textBoxIP.Text == "" || textBoxPort.Text == ""){
				MessageBox.Show("IP Address and Port Number are required to connect to the Server\n");
				return;
			}
			try
			{
				UpdateControls(false);
				// Create the socket instance
				m_clientSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
				
				// Cet the remote IP address
				IPAddress ip = IPAddress.Parse (textBoxIP.Text);
				int iPortNo = System.Convert.ToInt16 ( textBoxPort.Text);
				// Create the end point 
				IPEndPoint ipEnd = new IPEndPoint (ip,iPortNo);
				// Connect to the remote host
				m_clientSocket.Connect ( ipEnd );
				if(m_clientSocket.Connected) {
					
					UpdateControls(true);
					//Wait for data asynchronously 
					WaitForData();
				}
			}
			catch(SocketException se)
			{
				string str;
				str = "\nConnection failed, is the server running?\n" + se.Message;
				MessageBox.Show (str);
				UpdateControls(false);
			}		
		}			
        void SendMessage(string smsg)
        {
            MessageBox.Show("Messaage: "+smsg);
            byte[] byteArray = WrapString(smsg);
            if (m_clientSocket != null)
            {
                m_clientSocket.Send(byteArray);
            }
        }

        void ButtonSendMessageClick(object sender, System.EventArgs e)
		{
			try
			{
                //string msg = richTextTxMessage.Text;
                //            byte[] byteArray = WrapString(msg);
                //            string result = System.Text.Encoding.UTF8.GetString(byteArray);
                //            // New code to send strings
                //            NetworkStream networkStream = new NetworkStream(m_clientSocket);
                //System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
                //streamWriter.WriteLine(result);
                //streamWriter.Flush();

                //Use the following code to send bytes

                //byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(richTextTxMessage.Text);
                byte[] byteArray = WrapString(richTextTxMessage.Text);
                if (m_clientSocket != null)
                {
                    m_clientSocket.Send(byteArray);
                }

            }
            catch (SocketException se)
			{
				MessageBox.Show (se.Message );
			}	
		}
		public void WaitForData()
		{
			try
			{
				if  ( m_pfnCallBack == null ) 
				{
					m_pfnCallBack = new AsyncCallback (OnDataReceived);
				}
				SocketPacket theSocPkt = new SocketPacket ();
				theSocPkt.thisSocket = m_clientSocket;
				// Start listening to the data asynchronously
				m_result = m_clientSocket.BeginReceive (theSocPkt.dataBuffer,
				                                        0, theSocPkt.dataBuffer.Length,
				                                        SocketFlags.None, 
				                                        m_pfnCallBack, 
				                                        theSocPkt);
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}

		}
		public class SocketPacket
		{
			public System.Net.Sockets.Socket thisSocket;
			public byte[] dataBuffer = new byte[1024];
		}

		public  void OnDataReceived(IAsyncResult asyn)
		{
			try
			{
				SocketPacket theSockId = (SocketPacket)asyn.AsyncState ;
				int iRx  = theSockId.thisSocket.EndReceive (asyn);
				char[] chars = new char[iRx +  1];
				System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
				int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
				System.String szData = new System.String(chars);

                CheckMessage(szData);


                richTextRxMessage.AppendText(szData);
                richTextRxMessage.AppendText("\r\n");

                richTextRxMessage.SelectionStart = richTextRxMessage.Text.Length;
                richTextRxMessage.Focus();
                tbTotalChar.Text = "" + richTextRxMessage.Text.Length;

                WaitForData();
			}
			catch (ObjectDisposedException )
			{
				System.Diagnostics.Debugger.Log(0,"1","\nOnDataReceived: Socket has been closed\n");
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
		}
        public IEnumerable<int> FindAllIndexes(string str, string pattern)
        {
            int prevIndex = -pattern.Length; // so we start at index 0
            int index;
            while ((index = str.IndexOf(pattern, prevIndex + pattern.Length)) != -1)
            {
                prevIndex = index;
                yield return index;
            }
        }
        private void Respo(string msg)
        {
            char cSplit = '|';
            string[] msgRes = msg.Split(cSplit);
            switch(msgRes[0])
            {
                case "RS": MessageBox.Show("RS"); break;
                case "CS": MessageBox.Show("CS");  SendMessage("RS|" + msgRes[1] + "|RESUCCESS|"); break;
                default:break;
            }

        }
        private void CheckMessage(string msg)
        {

            List<int> istx = FindAllIndexes(msg, "\u0002").ToList();
            List<int> ietx = FindAllIndexes(msg, "\u0003").ToList();
            int iCount = istx.Count();
            string[] sData = new string[iCount];
            for(int i=0;i<iCount; i++)
            {
                int istart = istx[i];
                int iend = ietx[i];
                sData[i]= msg.Substring(istart+1, iend - istart-2);
                MessageBox.Show(sData[i] + "-" + istart + "-" + iend);
                Respo(sData[i]);
            }
            
        }
		private void UpdateControls( bool connected ) 
		{
			buttonConnect.Enabled = !connected;
			buttonDisconnect.Enabled = connected;
			string connectStatus = connected? "Connected" : "Not Connected";
			textBoxConnectStatus.Text = connectStatus;
		}
		void ButtonDisconnectClick(object sender, System.EventArgs e)
		{
			if ( m_clientSocket != null )
			{
				m_clientSocket.Close();
				m_clientSocket = null;
				UpdateControls(false);
			}
		}
	   //----------------------------------------------------	
	   // This is a helper function used (for convenience) to 
	   // get the IP address of the local machine
   	   //----------------------------------------------------
   	   String GetIP()
	   {	   
	   		String strHostName = Dns.GetHostName();
		
		   	// Find host by name
		   	IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
		
		   	// Grab the first IP addresses
		   	String IPStr = "";
		   	foreach(IPAddress ipaddress in iphostentry.AddressList){
		        IPStr = ipaddress.ToString();
		   		return IPStr;
		   	}
		   	return IPStr;
	   }

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			richTextRxMessage.Clear();
		}		
	}
}
