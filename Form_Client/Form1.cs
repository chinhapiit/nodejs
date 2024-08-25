using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Form_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txt_Message_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event if needed
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string word = this.txt_Message.Text;
            string response = SendWordToServer(word);
            this.txt_receiver.Text = response;
        }

        private string SendWordToServer(string word)
        {
            try
            {
                using (TcpClient client = new TcpClient("localhost", 5150))
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(word);
                    stream.Write(data, 0, data.Length);

                    byte[] responseData = new byte[1024];
                    int bytesRead = stream.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytesRead);
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
