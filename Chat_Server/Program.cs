using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DictionaryServer
{
    class Program
    {
        private static Dictionary<string, string> dictionary = new Dictionary<string, string>()
        {
            {"hello", "xin chào"},
            {"world", "thế giới"},
            {"goodbye", "tạm biệt"},
            {"thank you", "cảm ơn"}
        };

        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5150);
            server.Start();
            Console.WriteLine("Server started on port 5150...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedWord = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim(); // Sử dụng UTF-8 để đọc dữ liệu

                Console.WriteLine("Received: " + receivedWord);

                string response = dictionary.ContainsKey(receivedWord.ToLower()) ?
                                  $"{receivedWord} có nghĩa là {dictionary[receivedWord.ToLower()]}" :
                                  $"Không tìm thấy nghĩa của từ {receivedWord}";

                byte[] responseBytes = Encoding.UTF8.GetBytes(response); // Sử dụng UTF-8 để ghi dữ liệu
                stream.Write(responseBytes, 0, responseBytes.Length);

                client.Close();
            }

        }
    }
}
