
using Newtonsoft.Json;
using IbayQuery;
using System.Net.Http.Headers;

class Program
{
    // Initialize the HttpClient
    static HttpClient client = new HttpClient();

    static string token = string.Empty;

    static string newProduct = string.Empty;

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Get All Products
        Products_GetAll();

        //// Get Product by Id
        //Products_GetById();

        Console.WriteLine("\n\nTo Start, Enter your API Token :");
        token = Console.ReadLine();

        // Add a product
        //await Products_Insert();

        // Update a product
        //await Products_Update();

        // Delete a product
        //await Products_Delete();

        // Get All Users
        Users_GetAll();

        // Register an User
        //await Users_Register();

        // Login
        //await Users_Login();

        // Update an User
        //await Users_Update();

        // Delete an User
        await Users_Delete();



        Console.Read();
    }


    public static void Products_GetAll()
    {


        // Get all users
        var response = client.GetAsync("https://localhost:7140/Products").Result;
        var products = JsonConvert.DeserializeObject<List<Products>>(response.Content.ReadAsStringAsync().Result);

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
        var product = JsonConvert.DeserializeObject<Products>(response.Content.ReadAsStringAsync().Result);



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
    // CHECK VALIDATION READLINE A FAIRE
    public static async Task<bool> Products_Insert()
    {
        var form = new MultipartFormDataContent();
        Console.WriteLine("--------- Add a Product ---------");

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
            HttpResponseMessage response = client.PostAsync("https://localhost:7140/Products/Add", form).Result;

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"{name} - {price} € - Created Successfully 👍");
            else
                Console.WriteLine("{0}-({1})", (int)response.StatusCode, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    public static async Task<bool> Products_Update()
    {
        var form = new MultipartFormDataContent();
        Console.WriteLine("--------- Update a Product ---------");

        Console.WriteLine("\n\nInsert Product Id :");
        int id = int.Parse(Console.ReadLine());

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
            HttpResponseMessage response = client.PutAsync($"https://localhost:7140/Products/Update/?id={id}", form).Result;

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"{name} - Updated Successfully 👍");
            else
                Console.WriteLine("{0}-({1})", (int)response.StatusCode, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }


    public static async Task<bool> Products_Delete()
    {
        Console.WriteLine("--------- Delete a Product ---------");

        Console.WriteLine("\n\nInsert Product Id :");
        int id = int.Parse(Console.ReadLine());


        // Set JWT as bearer token in Authorization header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        try
        {
            HttpResponseMessage response = client.DeleteAsync($"https://localhost:7140/Products/Delete/?id={id}").Result;

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"{id} - Deleted Successfully 👍");
            else
                Console.WriteLine("{0}-({1})", (int)response.StatusCode, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }


    //////////////////////////////////// USERS
    ///

    public static void Users_GetAll()
    {
        // Set JWT as bearer token in Authorization header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {        
            // Get all users
            var response = client.GetAsync("https://localhost:7140/Users").Result;
            var users = JsonConvert.DeserializeObject<List<Users>>(response.Content.ReadAsStringAsync().Result);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\n----------- All Users ----------- \n");
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Pseudo} - {user.Email} - (Id :{user.Id})");
                }
                Console.WriteLine("\n--------------------------------");
            }
            else
                Console.WriteLine("{0}-({1})", (int)response.StatusCode, response.ReasonPhrase);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public static async Task<bool> Users_Register()
    {
        var form = new MultipartFormDataContent();
        Console.WriteLine("--------- Register ---------");

        Console.WriteLine("\n\nPseudo :");
        string pseudo = Console.ReadLine();

        Console.WriteLine("\n\nEmail :");
        string email1 = Console.ReadLine();

        Console.WriteLine("\n\nConfirm Email :");
        string email2 = Console.ReadLine();

        Console.WriteLine("\n\nPassword :");
        string password1 = Console.ReadLine();

        Console.WriteLine("\n\nConfirm Password :");
        string password2 = Console.ReadLine();

        Console.WriteLine("\n\nRole (0 / 1 / 2 ) :");
        int role = int.Parse(Console.ReadLine());

        form.Add(new StringContent(pseudo), "Pseudo");
        form.Add(new StringContent(email1), "Email");
        form.Add(new StringContent(email2), "ConfirmEmail");
        form.Add(new StringContent(password1), "Password");
        form.Add(new StringContent(password2), "ConfirmPassword");
        form.Add(new StringContent(role.ToString()), "Role");

        try
        {
            HttpResponseMessage response = client.PostAsync("https://localhost:7140/Users/register", form).Result;

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"{pseudo} - {email1} - {role} - Register Successfully 👍");
            else
                Console.WriteLine("{0}-({1})", (int)response.StatusCode, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    public static async Task<bool> Users_Login()
    {
        var form = new MultipartFormDataContent();
        Console.WriteLine("--------- Login ---------");


        Console.WriteLine("\n\nEmail :");
        string email = Console.ReadLine();

        Console.WriteLine("\n\nPassword :");
        string password = Console.ReadLine();


        form.Add(new StringContent(email), "Email");
        form.Add(new StringContent(password), "Password");

        try
        {
            HttpResponseMessage response = client.PostAsync("https://localhost:7140/Users/login", form).Result;

            if (response.IsSuccessStatusCode)
            {
                token = response.Content.ReadAsStringAsync().Result.Trim();

                Console.WriteLine($"{email} - Connected Successfully 👍");
                Console.WriteLine($"\n\nToken : \n\n{token}");

 
            }
            else
                Console.WriteLine("{0}-({1})", (int)response.StatusCode, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    public static async Task<bool> Users_Update()
    {
        var form = new MultipartFormDataContent();
        Console.WriteLine("--------- Update User ---------");

        Console.WriteLine("\n\nInsert User Id :");
        int id = int.Parse(Console.ReadLine());

        Console.WriteLine("\n\nPseudo :");
        string pseudo = Console.ReadLine();

        Console.WriteLine("\n\nEmail :");
        string email = Console.ReadLine();

        Console.WriteLine("\n\nPassword :");
        string password = Console.ReadLine();

        Console.WriteLine("\n\nRole (0 / 1 / 2 ) :");
        int role = int.Parse(Console.ReadLine());

        form.Add(new StringContent(pseudo), "Pseudo");
        form.Add(new StringContent(email), "Email");
        form.Add(new StringContent(password), "Password");
        form.Add(new StringContent(role.ToString()), "Role");


        try
        {
            HttpResponseMessage response = client.PutAsync($"https://localhost:7140/Users/Update/?id={id}", form).Result;

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"{pseudo} - {email} - Updated Successfully 👍");
            else
                Console.WriteLine("{0}-({1})", (int)response.StatusCode, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    public static async Task<bool> Users_Delete()
    {
        Console.WriteLine("--------- Delete a Product ---------");

        Console.WriteLine("\n\nInsert User Id :");
        int id = int.Parse(Console.ReadLine());


        // Set JWT as bearer token in Authorization header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        try
        {
            HttpResponseMessage response = client.DeleteAsync($"https://localhost:7140/Users/Delete/?id={id}").Result;

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"{id} - Deleted Successfully 👍");
            else
                Console.WriteLine("{0}-({1})", (int)response.StatusCode, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

}