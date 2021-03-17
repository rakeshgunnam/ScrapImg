using System;
using System.Net;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ScrapImg
{
    class Program
    {
        static void Main(string[] args)
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();
            firefoxOptions.AcceptInsecureCertificates = true;
            IWebDriver driver = new FirefoxDriver(firefoxOptions);
                        
            Console.WriteLine("\nEnter PlatesMania.com 's Targert URL:");
            driver.Navigate().GoToUrl(Console.ReadLine());

            Console.WriteLine("\nEnter SavePath:\n");
            string savePath = Console.ReadLine();

            Console.WriteLine("\nBrand Name: \n");
            string brandName = Console.ReadLine();

            int count = 0;
            int clks = 0;

            // 'count < value' change value according to the requirement, it represents img count.
            while(count < 450 && clks < 100)
            {
                var collections = driver.FindElements(By.XPath("//div[@class='panel-body']/div/a/img"));

                foreach(var collection in collections)
                {

                    try
                    {
                        var ImgURL = collection.GetAttribute("SRC");
                        try
                        {
                            var oImgURL = ImgURL.Replace("/m/", "/o/");

                            WebClient client = new WebClient();
                            client.DownloadFile(oImgURL, savePath + @"\" + brandName + count + ".jpg");

                            Console.WriteLine(oImgURL);
                            ++count;
                        }
                        catch(NullReferenceException e)
                        {
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                    //catch (NullReferenceException e) {
                    //    continue;
                    //}
                                        
                }

                try
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('pagination')[0].getElementsByTagName('A')[11].click()");
                    Thread.Sleep(1000);
                    ++clks;
                }
                catch (Exception e) {
                    ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('pagination')[0].getElementsByTagName('A')[11].click()");
                    Thread.Sleep(1000);
                    ++clks;
                }
                
            }

            Console.WriteLine("\n\nSuccessfully Downloaded!...");

            driver.Close();
            //Console.WriteLine(((IJavaScriptExecutor)driver).ExecuteScript("return document.getElementsByClassName('panel - body')[0].getElementsByTagName('DIV')[0].getElementsByTagName('A')[0].getElementsByTagName('IMG')[0].getAttribute('SRC')").ToString());
        }
    }
}
