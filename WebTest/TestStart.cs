using Domain;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace WebTest
{
	public class TestStart
	{
		WebDriverSpecific driver;

		public TestStart() => driver = new WebDriverSpecific();

		[Test]
		public void Test()
		{
			driver.ChromeDriver.Url = "http://www.demoqa.com";
			driver.ChromeDriver.Close();
		}

	}
}
