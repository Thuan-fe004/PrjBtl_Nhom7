using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class frm_server : Form
    {
        private string chatLogFilePath = @"E:\LtMang\demobtl\WindowsFormsApp1\chatlog.txt";

        private EndPoint IP;
        private Socket serverSocket;
        private List<EndPoint> clientList;

        public frm_server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
        }

        private void btMassage_Click(object sender, EventArgs e)
        {
            string serverMessage = $"Server: {tbMassage.Text}";
            foreach (EndPoint clientEndPoint in clientList)
            {
                SendTo(serverMessage, clientEndPoint);
                SaveChatLog(serverMessage);
            }
            AddMassage(serverMessage);
            tbMassage.Clear();
        }

        private void SaveChatLog(string serverMessage)
        {
            try
            {
                // Ghi dữ liệu vào file chatlog.txt
                File.AppendAllText(chatLogFilePath, serverMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving chat log: {ex.Message}");
            }
        }

        private void Connect()
        {
            clientList = new List<EndPoint>();
            IP = new IPEndPoint(IPAddress.Any, 9999);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverSocket.Bind(IP);

            Thread listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        EndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                        byte[] data = new byte[1024];
                        int bytesReceived = serverSocket.ReceiveFrom(data, ref clientEndPoint);

                        if (!clientList.Contains(clientEndPoint))
                        {
                            clientList.Add(clientEndPoint);
                        }

                        string message = (string)Deserialize(data.Take(bytesReceived).ToArray());
                        foreach (EndPoint item in clientList)
                        {
                            if (!item.Equals(clientEndPoint))
                            {
                                SendTo(message, item);
                            }
                        }

                        AddMassage(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            listen.IsBackground = true;
            listen.Start();
        }

        private void Close()
        {
            string disconnectMessage = "Server:DISCONNECT";
            foreach (EndPoint clientEndPoint in clientList)
            {
                SendTo(disconnectMessage, clientEndPoint);
            }
            serverSocket.Close();
        }

        private void SendTo(string message, EndPoint clientEndPoint)
        {
            if (clientEndPoint != null && !string.IsNullOrEmpty(message))
            {
                byte[] data = Serialize(message);
                serverSocket.SendTo(data, clientEndPoint);
            }
        }

        private void AddMassage(string s)
        {
            lvMassage.Items.Add(new ListViewItem() { Text = s });
        }

        private byte[] Serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        private object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }

        private void lvMassage_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tbMassage_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}