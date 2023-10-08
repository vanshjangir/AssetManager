#if TOOLS
using Godot;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

[Tool]
public partial class plugin : EditorPlugin
{
	Button button;
	Container container;
	Control AssetWindow;
	PackedScene pkdScene = ResourceLoader.Load<PackedScene>("res://addons/AssetManager/main.tscn");
	Panel homePanelIns;
	Assets assets = new Assets();
	
	public void ahhh()
	{
		//not doing python integration
	}
	
	private void onButtonPressed(){
		
		
		if (homePanelIns != null){
			GD.Print("lol");
			homePanelIns.Visible = ! homePanelIns.Visible;
			return;
		}

		var editorRoot = GetTree().Root;
		homePanelIns = (Panel)pkdScene.Instantiate();
		editorRoot.AddChild(homePanelIns);
		
	}
	
	public override void _MakeVisible(bool visible)
	{
		if (homePanelIns != null)
		{
			homePanelIns.Visible = visible;
		}
	}
	 
	public override void _EnterTree()
	{
		button = new Button();
		container = new Container();
		var editorRoot = GetTree().Root;
		
		var onButtonPressedCallable = new Callable(this, nameof(onButtonPressed));
		var ahhhCallable = new Callable(this, nameof(ahhh));
		
		button.Text = "OniChan_Yamete_kudasai";
		button.Connect("pressed", onButtonPressedCallable);
		button.Connect("pressed", ahhhCallable);
		container.SetPosition(new Vector2(1080,10));
		container.AddChild(button);
		editorRoot.AddChild(container);
		
	}

	public override void _ExitTree()
	{
		if(button != null){
			button.QueueFree();
		}
		if(container != null){
			container.QueueFree();
		}
		if(homePanelIns != null){
			homePanelIns.QueueFree();
		}
		
	}
}

class Assets
{
	private ChromeDriver driver;
	private string driverPath = @"C:\Users\devan\Desktop\DesktopFiles\godot\addons\AssetManager\chromedriver-win64\";

	public Assets()
	{
		ChromeOptions coptions = new ChromeOptions();
		coptions.AddArgument("--headless");
		driver = new ChromeDriver(driverPath, coptions);
	}

	public List<Dictionary<string, string>> Load()
	{
		List<Dictionary<string, string>> assetList = new List<Dictionary<string, string>>();

		try
		{
			driver.Navigate().GoToUrl("https://itch.io/game-assets/free");

			int count = 0;
			IReadOnlyCollection<IWebElement> parentElements = driver.FindElements(By.CssSelector(".game_cell.has_cover"));
			Console.WriteLine(parentElements.Count);

			foreach (IWebElement element in parentElements)
			{
				count++;
				Dictionary<string, string> assetData = new Dictionary<string, string>
				{
					{"image", element.FindElement(By.ClassName("game_thumb"))
									.FindElement(By.TagName("a"))
									.FindElement(By.TagName("img"))
									.GetAttribute("src")},
					{"text", element.FindElement(By.ClassName("game_cell_data"))
								   .FindElement(By.TagName("a"))
								   .Text},
					{"link", element.FindElement(By.ClassName("game_cell_data"))
								   .FindElement(By.TagName("a"))
								   .GetAttribute("href")}
				};
				assetList.Add(assetData);
				driver.ExecuteScript("window.scrollTo(0, 500)");

				if (count >= 8)
				{
					break;
				}
			}
			driver.ExecuteScript("window.scrollTo(0, 500)");
		}
		catch (Exception e)
		{
			Console.WriteLine("Error: " + e.Message);
		}

		return assetList;
	}

	public void download(string url){
		try
		{
			driver.Navigate().GoToUrl(url);

			//Thread.Sleep(5000);

			IWebElement firstDownloadButton = driver.FindElement(By.CssSelector(".button.buy_btn"));
			firstDownloadButton.Click();
			Console.WriteLine("1");

			// driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
			//Thread.Sleep(2000);
			IWebElement directDownloadButton = driver.FindElement(By.CssSelector(".direct_download_btn"));
			directDownloadButton.Click();
			Console.WriteLine("1");

			// driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
			//Thread.Sleep(2000);
			Console.WriteLine("1");
			IReadOnlyCollection<IWebElement> finalDownloadButtons = driver.FindElements(By.CssSelector(".button.download_btn"));
			foreach (IWebElement element in finalDownloadButtons)
			{
				element.Click();
			}
			//Thread.Sleep(2000);

		}
		catch (Exception e)
		{
			Console.WriteLine("Error: " + e.Message);
		}
	}
}



#endif
