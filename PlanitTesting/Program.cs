using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlanitTesting
{
	class Program
	{
		static void Main(string[] args)
		{
			// Creating driver Object to launch chrome browser
			IWebDriver driver = new ChromeDriver(@"C:\Selenium_Webdriver\Visual");
			

			//Open Chrome, and Naviagted to the given URL
			driver.Navigate().GoToUrl("http://demowebshop.tricentis.com/");
			driver.Manage().Window.Maximize();

			//Click on Login button
			driver.FindElement(By.XPath("//a[@href ='/login']")).Click();
			try
			{
				//Validate "Welcome, Please Sign" Message
				driver.FindElement(By.ClassName("page-title")).GetAttribute("Welcome, Please Sign In!");
				Console.WriteLine("Successfully validated Welcome, Please Sign In message");
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to load the page");
			}
			//Log In with Valid Credentials
			driver.FindElement(By.XPath("//*[@id='Email']")).SendKeys("testdemowebshop@gmail.com");
			driver.FindElement(By.XPath("//*[@id='Password']")).SendKeys("Test123");
			IWebElement webElement1 = driver.FindElement(By.XPath("//*[@class='button-1 login-button']"));
			webElement1.Click();
            try 
			{	
				//Validate the user account ID on top right
				IWebElement webElement2 = driver.FindElement(By.XPath("//*[@class='account']"));
				webElement2.GetAttribute("testdemowebshop@gmail.com");
				Console.WriteLine("User ID Validated");
			}
			catch(Exception f)
            {
				Console.WriteLine("Failed to Validate User ID");
			}
			//Clear the shopping cart
			//driver.FindElement(By.XPath("//*[@id='topcartlink']")).Click();
			//driver.FindElement(By.XPath("//input[@name='itemquantity1954655']")).SendKeys("0");
			//driver.FindElement(By.XPath("//input[@name='updatecart']")).Click();

			//Select the "Books" from Categories
			driver.FindElement(By.XPath("//a[@href ='/books']")).Click();

			//Select a book from the list displayed
			IWebElement addtocart= driver.FindElement(By.XPath("//input[@class ='button-2 product-box-add-to-cart-button']"));

			//Get the Price from the list displayed
			//Console.WriteLine(driver.FindElement(By.XPath("//span[contains(@class,'price actual-price')]  [contains(text(),'10.00')]")));
            try 
			{
				//Click on "Add to Cart"
				driver.FindElement(By.XPath("//*[@class='button-2 product-box-add-to-cart-button']")).Click();
				//Validate "The product has been add to your shopping cart" message
				IWebElement ele= driver.FindElement(By.XPath("//*[@id='bar - notification']/p/text()"));
				String addtocartmsg= ele.Text;
				Console.WriteLine(addtocartmsg.Equals("The product has been added to your "));
			}
			catch(Exception e)
            {
				Console.WriteLine("Failed to add the item to cart!");
            }
			
			//Click on shopping Cart
			driver.FindElement(By.XPath("/html/body/div[4]/div[1]/div[4]/div[2]/div[2]/div[2]/div[3]/div[1]/div/div[2]/div[3]/div[2]/input")).Click();
			//Enter the quantity more than one
			//driver.FindElement(By.XPath("//input[@name='itemquantity1954989']")).SendKeys("7");
			//driver.FindElement(By.XPath("//input[@name='updatecart']")).Click();
			// validate the "Sub-Total" for selected book
			driver.FindElement(By.XPath("//span[contains(@class,'price actual-price')]  [contains(text(),'70.00')]"));
			//Click on "Check-out"
			driver.FindElement(By.XPath("//*[@id='termsofservice']")).Click();
			driver.FindElement(By.XPath("//*[@id='checkout']")).Click();
			//Select "NewAddress" From "Billing Address" drop down
			IWebElement newaddress = driver.FindElement(By.XPath("//*[@id='billing - address - select']"));
			SelectElement selectelement = new SelectElement(newaddress);
			selectelement.SelectByText("NewAddress");

			//Fill all mandatory fields in "Billing Address" and click "Continue"
			driver.FindElement(By.XPath("//*[@id='BillingNewAddress_FirstName']")).SendKeys("Demo");
			driver.FindElement(By.XPath("//*[@id='BillingNewAddress_LastName']")).SendKeys("test");

			SelectElement selectcountry = new SelectElement(driver.FindElement(By.XPath("//*[@id='BillingNewAddress_CountryId']")));
			selectcountry.SelectByValue("41");
			driver.FindElement(By.XPath("//*[@id='BillingNewAddress_City']")).SendKeys("Hyderabad");
			driver.FindElement(By.XPath("//*[@id='BillingNewAddress_Address1'])")).SendKeys("7 Inorbit Mall Rd, Vittalrao Nagar, HiTech City");
			driver.FindElement(By.XPath("//*[@id='BillingNewAddress_ZipPostalCode']")).SendKeys("500081");
			driver.FindElement(By.XPath("//*[@name='BillingNewAddress.PhoneNumber']")).SendKeys("9876543210");
			driver.FindElement(By.XPath("//*[@id='billing - buttons - container']/input")).Click();

			//Select the "Shipping Address" as Same as "Billing Address" from "Shipping Address" dropdown
			IWebElement shippingaddress = driver.FindElement(By.XPath("//*[@id='shipping - address - select']"));
			SelectElement selectelementadress = new SelectElement(shippingaddress);
			selectelementadress.SelectByValue("2122846");

			driver.FindElement(By.XPath("//*[@id='shipping - buttons - container']/input")).Click();

			//Select the shipping method as "Next Day Air" and Click on Continue
			driver.FindElement(By.XPath("//*[@id='shippingoption_1']")).Click();
			driver.FindElement(By.XPath("//*[@id='shipping-method-buttons-container']/input")).Click();


			//Choose the payment method as COD(Cash on Delivey) and click on "Continue"
			driver.FindElement(By.XPath("//*[@id='paymentmethod_0']")).Click();
			driver.FindElement(By.XPath("//*[@id='payment-method-buttons-container']/input")).Click();


			//Validate the message "You Will pay by COD" and click on "Continue"
			string actual_Result = driver.FindElements(By.XPath("//*[@id='checkout - payment - info - load']/div/div/div[1]")).ToString();
			string expected_Result = "You will pay by COD";

			if(actual_Result==expected_Result)
            {
				Console.WriteLine("Message Validate the message You Will pay by COD Successfully!");
            }
            else
            {
				Console.WriteLine("Failed to Validate the message You Will pay by COD Message");
			}
			driver.FindElement(By.XPath("//*[@id='payment - info - buttons - container']/input")).Click();

			//Click on "Confirm Order"
			driver.FindElement(By.XPath("//*[@id='confirm - order - buttons - container']/input")).Click();


			//Validate the message "Your order has successfully processed!" and Print Order number
			IWebElement successfullorder = driver.FindElement(By.XPath("//*[@class='title']/div"));
			String text= successfullorder.Text;
			if(text== "Your order has been successfully processed!")
            {
				Console.WriteLine("Your order has been successfully processed!");
            }
            else
            {
				Console.WriteLine("Failed to Order, Please try Again!");
			}

			IWebElement orderdeatils = driver.FindElement(By.XPath("//*[@class='details']/ul"));
			String ordernumber = orderdeatils.Text;
			Console.WriteLine(ordernumber);

			//Click on "Continue" and log out from the application
			driver.FindElement(By.XPath("//*[@type='button']")).Click();
			driver.FindElement(By.XPath("//*[@class='ico-logout']")).Click();
		}
	}
}	
	


        
        

