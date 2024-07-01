using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class frm_Client : Form
    {
        private string clientName = "Client";

        public frm_Client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
        }


        IPEndPoint serverEndPoint;
        IPEndPoint localEndPoint;
        UdpClient client;

        
        /// Kết nối tới server 
        
        void Connect()
        {
            serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            client = new UdpClient(localEndPoint);

            Thread listen = new Thread(Receive);
            listen.IsBackground = true;
            listen.Start();
        }

        private void SaveMessageToFile(string message)
        {
            // Lưu tin nhắn vào file TXT
            string folderPath = @"E:\LtMang\demobtl\WindowsFormsApp1"; // Thư mục chứa file txt
            string fileName = "chatlog.txt"; // Tên file txt
            string filePath = Path.Combine(folderPath, fileName);

            using (StreamWriter writer = new StreamWriter(filePath, true)) // Mở file để ghi (true: ghi thêm vào cuối file)
            {
                writer.WriteLine(message); // Ghi tin nhắn vào file
            }
        }
        //Gửi tin nhắn tới server

        void Send()
        {
            if (tbMassage.Text != string.Empty)
            {
                string message = $"{clientName}: {tbMassage.Text}"; 
                byte[] data = Serialize(message);
                client.Send(data, data.Length, serverEndPoint);
                AddMassage(message); 
                tbMassage.Clear(); 
                SaveMessageToFile(message);
            }

        }

       
        /// Nhận tin nhắn từ server
       
        void Receive()
        {
            try
            {
                while (true)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = client.Receive(ref remoteEndPoint);
                    string message = (string)Deserialize(data);
                    if (message == "Server:DISCONNECT")
                    {
                        MessageBox.Show("Server has been disconnected. Client will now close.");
                        this.Close(); // Tắt client khi server ngắt kết nối
                        break;
                    }
                    AddMassage(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close(); // Đảm bảo tắt client khi có lỗi
            }
        }

        /// <summary>
        /// Thêm tin nhắn vào khung chat
        /// </summary>
        /// <param name="s"></param>
        void AddMassage(string s)
        {
            lvMassage.Items.Add(new ListViewItem() { Text = s });
        }

        /// <summary>
        /// Phân mảnh
        /// </summary>
        ///<param name="obj"></param>
        /// <returns></returns>
        byte[] Serialize(object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Gom mảnh
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        object Deserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream);
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void tbMassage_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btMassage_Click(object sender, EventArgs e)
        {
            Send();

        }
    }
}
