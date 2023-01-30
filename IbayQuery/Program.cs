
using Newtonsoft.Json;
using System;
using System.Net.Http;
using IbayApi;
using Dal.Entities;
using IbayQuery;
using System.Text;
using System.Security.Policy;
using System.Net.Http.Headers;

class Program
{
    // Initialize the HttpClient
    static HttpClient client = new HttpClient();
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Get All Products
        Products_GetAll();

        // Get Product by Id
        Products_GetById();

        // Add a product
        Products_Insert();

        Console.Read();
    }


    public static void Products_GetAll()
    {


        // Get all users
        var response = client.GetAsync("https://localhost:7140/Products").Result;
        var products = JsonConvert.DeserializeObject<List<ProductsEntities>>(response.Content.ReadAsStringAsync().Result);

        try
        {
            Console.WriteLine("--------- All Products --------- \n");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} - {product.Price} € - (Id :{product.Id})");
            }
            Console.WriteLine("\n--------------------------------");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
    public static void Products_GetById()
    {
        Console.WriteLine("\n\nPut the Id of the product you want to see :");
        int id = int.Parse(Console.ReadLine());

        // Get all users
        var response = client.GetAsync($"https://localhost:7140/Products/{id}").Result;
        var product = JsonConvert.DeserializeObject<ProductsEntities>(response.Content.ReadAsStringAsync().Result);



        try
        {
            Console.WriteLine("--------- Get Product by Id --------- \n");
            Console.WriteLine($"{product.Name} - {product.Price} € - (Id :{product.Id})");
 

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public static async Task Products_Insert()
    {



        var form = new MultipartFormDataContent();
        Console.WriteLine("--------- Add a Product --------- \n");

        Console.WriteLine("\n\nTo Start, Enter your API Token :");
        string token = Console.ReadLine();

        Console.WriteLine("\n\nName :");
        string name = Console.ReadLine();

        Console.WriteLine("\n\nImage File Path :");
        string image = Console.ReadLine();

        Console.WriteLine("\n\nPrice :");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.WriteLine("\n\nAvailable :");
        bool available = bool.Parse(Console.ReadLine());

        // Set JWT as bearer token in Authorization header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        form.Add(new StringContent(name), "Name");
        form.Add(new StreamContent(File.OpenRead(image)), "Image", Path.GetFileName(image));
        form.Add(new StringContent(price.ToString()), "Price");
        form.Add(new StringContent(available.ToString()), "Available");


        try
        {
            var response = await client.PostAsync("https://localhost:7140/Products/", form);

            Console.WriteLine($"{name} - {price} - Created Successfully 👍");
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}