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
	PackedScene homePanel = ResourceLoader.Load<PackedScene>("res://addons/AssetManager/main.tscn");
	Panel homePanelInstance;
	
	public override void _EnterTree()
	{
		homePanelInstance = (Panel)homePanel.Instantiate();
		// Add the main panel to the editor's main viewport.
		GetEditorInterface().GetEditorMainScreen().AddChild(homePanelInstance);
		// Hide the main panel. Very much required.
		
		_MakeVisible(false);
	}

	public override void _ExitTree()
	{
		if (homePanelInstance != null)
		{
			homePanelInstance.QueueFree();
		}
	}

	public override bool _HasMainScreen()
	{
		return true;
	}

	public override void _MakeVisible(bool visible)
	{
		if (homePanelInstance != null)
		{
			homePanelInstance.Visible = visible;
		}
		
	}

	public override string _GetPluginName()
	{
		return "Asset Manager";
	}

	public override Texture2D _GetPluginIcon()
	{
		// Must return some kind of Texture for the icon.
		return GetEditorInterface().GetBaseControl().GetThemeIcon("Node", "EditorIcons");
	}
}


class Assets
{
	private ChromeDriver driver;
	private string driverPath = @"D:\Godot\Godot-python\addons\AssetManager\chromedriver-win64";

	public Assets()
	{
		ChromeOptions coptions = new ChromeOptions();
		coptions.AddArgument("--headless");
		driver = new ChromeDriver(driverPath);
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
