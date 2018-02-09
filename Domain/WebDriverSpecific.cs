using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Domain
{
	public class WebDriverSpecific
	{
		static RemoteWebDriver _fireFoxDriver;
		public RemoteWebDriver FireFoxDriver
		{
			get
			{
				if (_fireFoxDriver == null)
					_fireFoxDriver = new FirefoxDriver(@"C:\Users\Tonja\source\repos\AutomaticWebTest\packages");
				return _fireFoxDriver;
			}
		}

		static RemoteWebDriver _chromeFoxDriver;
		public RemoteWebDriver ChromeDriver
		{
			get
			{
				if (_chromeFoxDriver == null)
					_chromeFoxDriver = new ChromeDriver(@"C:\Users\Tonja\source\repos\AutomaticWebTest\packages");
				return _chromeFoxDriver;
			}
		}

		static RemoteWebDriver _internetExplorerDriver; 
		public RemoteWebDriver InternetExplorer
		{
			get
			{
				if (_internetExplorerDriver == null)
					_internetExplorerDriver = new InternetExplorerDriver(@"C:\Users\Tonja\source\repos\AutomaticWebTest\packages");
				return _internetExplorerDriver;
			}
		}
	}
}
