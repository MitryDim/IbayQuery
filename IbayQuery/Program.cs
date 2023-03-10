
using Newtonsoft.Json;
using IbayQuery;
using System.Net.Http.Headers;
using System.Configuration;
using System.Collections.Specialized;

class Program
{

    
    // Initialize the HttpClient
    static HttpClient client = new HttpClient();

    static string host = string.Empty;  
    static string port = string.Empty;
    static string token = string.Empty;

    static string newProduct = string.Empty;

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;


         host = ConfigurationManager.AppSettings.Get("Hostname");
         port = ConfigurationManager.AppSettings.Get("Port");
        
        Console.WriteLine(host+ ":" + port);

        // Register an User
        await Users_Register();

        // Login
        await Users_Login();


        while (String.IsNullOrEmpty(token))
        {
            Console.WriteLine("\n\nToken cannot be null please enter one token :");
            token = Console.ReadLine();

        }
        // Set JWT as bearer token in Authorization header
       client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Get All Users
        //await Users_GetAll();

        // Update an User
        //await Users_Update();

        // Delete an User
        //await Users_Delete();


        // Register an User
        //await Users_Register();

        // Login
        //await Users_Login();

        //while (String.IsNullOrEmpty(token))
        //{
        //    Console.WriteLine("\n\nToken cannot be null please enter one token :");
        //    token = Console.ReadLine();

        //}

        // Get All Users
        await Users_GetAll();


        // Add a product
        await Products_Insert();

        // Get All Products
        await Products_GetAll();

        // Update a product
        await Products_Update();

        //// Get Product by Id
        await Products_GetById();


        // Add product on a Cart
        await Carts_Insert();

        // Get all Carts
        await Carts_GetAll();


        // Get All Products
        await Products_GetAll();


        // Add an order
        await Orders_Insert();

        // Delete a product on a Cart
        await Carts_Delete();


        // Get All Products
        await Products_GetAll();

        // Delete a product
        await Products_Delete();


        Console.Read();
    }


    public static async Task<bool> Products_GetAll()
    {


        // Get all users
        var response = client.GetAsync("https://"+ host + ":"+port+"/Products").Result;
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
            return false;
        }
        return true;

    }
    public static async Task<bool> Products_GetById()
    {
        Console.WriteLine("\n\nPut the Id of the product you want to see :");
        int id = int.Parse(Console.ReadLine());

        // Get all users
        var response = client.GetAsync($"https://"+ host + ":"+port+"/Products/{id}").Result;
        var product = JsonConvert.DeserializeObject<Products>(response.Content.ReadAsStringAsync().Result);



        try
        {
            Console.WriteLine("--------- Get Product by Id --------- \n");
            Console.WriteLine($"{product.Name} - {product.Price} € - (Id :{product.Id})\n");


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;

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



        form.Add(new StringContent(name), "Name");
        form.Add(new StreamContent(File.OpenRead(image)), "Image", Path.GetFileName(image));
        form.Add(new StringContent(price.ToString()), "Price");
        form.Add(new StringContent(available.ToString()), "Available");


        try
        {
            HttpResponseMessage response = client.PostAsync("https://"+ host + ":"+port+"/Products/Add", form).Result;

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
            HttpResponseMessage response = client.PutAsync($"https://"+ host + ":"+port+"/Products/Update/?id={id}", form).Result;

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
            HttpResponseMessage response = client.DeleteAsync($"https://"+ host + ":"+port+"/Products/Delete/?id={id}").Result;

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

    public static async Task<bool> Users_GetAll()
    {
        // Set JWT as bearer token in Authorization header
       // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {        
            // Get all users
            var response = client.GetAsync("https://"+ host + ":"+port+"/Users").Result;
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
            return false;
        }
        return true;
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
            HttpResponseMessage response = client.PostAsync("http://localhost:5000/Users/Register", form).Result;

            if (response.IsSuccessStatusCode)
            {

                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Users>(result);
            Console.WriteLine($"{pseudo} - {email1} - {role} - ID : {json.Id} - Register Successfully 👍");
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
            HttpResponseMessage response = client.PostAsync("https://"+ host + ":"+port+"/Users/Login", form).Result;

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
            HttpResponseMessage response = client.PutAsync($"https://"+ host + ":"+port+"/Users/Update/?id={id}", form).Result;

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
        Console.WriteLine("--------- Delete users ---------");

        Console.WriteLine("\n\nInsert User Id :");
        int id = int.Parse(Console.ReadLine());


        // Set JWT as bearer token in Authorization header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        try
        {
            HttpResponseMessage response = client.DeleteAsync($"https://"+ host + ":"+port+"/Users/Delete/?id={id}").Result;

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


    //////////////////////////////////// CARTS
    ///


    public static async Task<bool> Carts_GetAll()
    {
        // Set JWT as bearer token in Authorization header
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            // Get all users
            var response = client.GetAsync("https://"+ host + ":"+port+"/Carts").Result;
        

            if (response.IsSuccessStatusCode)
            {
                var cartsData = JsonConvert.DeserializeObject<Carts>(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("\n----------- Cart Information ----------- \n");

                Console.WriteLine("Total Amount : " + cartsData.TotalAmount + " €");
                Console.WriteLine("Id : " + cartsData.Id);
                Console.WriteLine("UserId : " + cartsData.UserId);
                Console.WriteLine("Status : " + cartsData.Status);

                foreach (var item in cartsData.CartItems)
                {
                    Console.WriteLine("\n-----------  Product in cart ----------- \n");
                    Console.WriteLine("Id : " + item.Id);
                    Console.WriteLine("CartId : " + item.CartId);
                    Console.WriteLine("ProductId : " + item.ProductId);
                    Console.WriteLine("Quantity : " + item.Quantity);
                    Console.WriteLine("Status : " + item.Status);
                    Console.WriteLine("\n--------------------------------\n");
                }
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

    public static async Task<bool> Carts_Insert()
    {
        var form = new MultipartFormDataContent();
        Console.WriteLine("--------- Add a product on a Cart ---------");

        Console.WriteLine("\n\nProduct Id :");
        string productId = Console.ReadLine();

        Console.WriteLine("\n\nQuantity :");
        string quantity = Console.ReadLine();

        form.Add(new StringContent(productId), "productId");
        form.Add(new StringContent(quantity), "quantity");

        // Set JWT as bearer token in Authorization header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            HttpResponseMessage response = client.PostAsync($"https://"+ host + ":"+port+"/Carts/Add", form).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{productId} x {quantity} - Added Successfully 👍");
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

    public static async Task<bool> Carts_Delete()
    {
        Console.WriteLine("--------- Delete a product in a Cart ---------");

        Console.WriteLine("\n\nInsert Product Id :");
        int productId = int.Parse(Console.ReadLine());


        // Set JWT as bearer token in Authorization header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            HttpResponseMessage response = client.DeleteAsync($"https://"+ host + ":"+port+"/Carts/RemoveFromCart/?productId={productId}").Result;

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"Product Id : {productId} - Deleted Successfully 👍");
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


    //////////////////////////////////// Orders
    ///

    public static async Task<bool> Orders_Insert()
    {
        // Set JWT as bearer token in Authorization header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var form = new MultipartFormDataContent();
        Console.WriteLine("--------- Add an Order ---------");


        Console.WriteLine("\n\nStatus :");
        string status = Console.ReadLine();

        Console.WriteLine("\n\nTotal Price :");
        decimal totalPrice = decimal.Parse(Console.ReadLine());

        Console.WriteLine("\n\nPayment Type :");
        string paymentType = Console.ReadLine();


        form.Add(new StringContent(status), "status");
        form.Add(new StringContent(totalPrice.ToString()), "TotalPrice");
        form.Add(new StringContent(paymentType), "paymentType");

        try
        {
            HttpResponseMessage response = client.PostAsync($"https://"+ host + ":"+port+"/Orders/Add/?status={status}&TotalPrice={totalPrice}", form).Result;

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"{status} - {totalPrice} € - {paymentType} - Connected Successfully 👍");
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