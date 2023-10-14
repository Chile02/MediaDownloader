using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter the URL of the media file: ");
        string mediaUrl = Console.ReadLine();

        try
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(mediaUrl);

                if (response.IsSuccessStatusCode)
                {
                    string fileExtension = Path.GetExtension(mediaUrl);

                  
                    string fileName = Guid.NewGuid().ToString() + fileExtension;

                    string saveDirectory = @"C:\Downloads"; 

                    string fullPath = Path.Combine(saveDirectory, fileName);

                    Directory.CreateDirectory(saveDirectory);

                    using (var fileStream = File.Create(fullPath))
                    {
                        await response.Content.CopyToAsync(fileStream);
                    }

                    Console.WriteLine($"File downloaded and saved to {fullPath}");
                    Console.WriteLine("FriendlyBot Out :)");
                }
                else
                {
                    Console.WriteLine($"Failed to download the file. Status code: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
