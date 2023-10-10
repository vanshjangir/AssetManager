using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
	static void Main()
	{
		// Set the path to the ChromeDriver executable
		string driverPath = @"/usr/bin/"; // Update with your ChromeDriver path

		// Initialize the ChromeDriver
		var driver = new ChromeDriver(driverPath);

		try
		{
			// Navigate to the website you want to scrape
			driver.Navigate().GoToUrl("https://google.com");

			// Perform scraping operations here
			IWebElement element = driver.FindElement(By.CssSelector("h1"));
			string text = element.Text;
			Console.WriteLine("Scraped Text: " + text);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}
		finally
		{
			// Close the WebDriver
			driver.Quit();
		}
	}
}

