using System;
using System.IO;
using System.Threading;
using Xunit;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using PetStore_Testing.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace PetStore_Testing.Tests
{
    public class PetStoreTest
    {
        private ITestOutputHelper output;
        private RemoteWebDriver driver;
        private Actions action;
        private IWebElement element;
        private IJavaScriptExecutor jse;

        public PetStoreTest(ITestOutputHelper helper)
        {
            this.output = helper;

        }


        [Fact]
        public void CreatePet_PostMethod()
        {
            new ServiceWorkflow(output).ValidatePetIsAdded(CustomConfigurationProvider.GetSection($"AddNewPET"));
        }

        [Fact]
        public void Validate_getPetByID()
        {
            var result = new ServiceWorkflow(output).validate_getPetResponse(CustomConfigurationProvider.GetSection($"AddNewPET.id"), CustomConfigurationProvider.GetSection($"AddNewPET"));
            if (!result)
            {
                output.WriteLine("Pet has not been created: " + result);
            }
            else
                output.WriteLine("Pet has been created");
        }


        [Fact]
        public void getAvailablePets()
        {
            var result = new ServiceWorkflow(output).getPetsByStatus("available");
            if (!result)
                output.WriteLine("0 results returned");
            else
                output.WriteLine("Pets are available");
        }

        [Fact]
        public void confirmToggleFunctionality()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://petstore.swagger.io/");
            action = new Actions(driver);

            element = driver.FindElementById("operations-pet-uploadFile");
            toggleEndpoint(element, "post");
            element = driver.FindElementById("operations-pet-addPet");
            toggleEndpoint(element, "post");
            element = driver.FindElementById("operations-pet-updatePet");
            toggleEndpoint(element, "put");
            element = driver.FindElementById("operations-pet-findPetsByStatus");
            toggleEndpoint(element, "get");
            element = driver.FindElementById("operations-pet-getPetById");
            toggleEndpoint(element, "get");
            element = driver.FindElementById("operations-pet-updatePetWithForm");
            toggleEndpoint(element, "post");
            element = driver.FindElementById("operations-pet-deletePet");
            toggleEndpoint(element, "delete");

            driver.Quit();
        }

        [Fact]
        public void uploadPetImage()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://petstore.swagger.io/");
            jse = (IJavaScriptExecutor)driver;
            action = new Actions(driver);

            // Expose parameters for file upload
            element = driver.FindElementById("operations-pet-uploadFile");
            action.MoveToElement(element);
            element.Click();

            // Enable form editor
            driver.FindElementByXPath("//div[@class='try-out']/button").Click();

            // Enter id
            element = driver.FindElementByXPath("//table[@class='parameters']/tbody/tr[1]/td[2]/input");
            element.Clear();
            element.SendKeys("200");

            // Select the file input element
            element = driver.FindElementByXPath("//table[@class='parameters']/tbody/tr[3]/td[2]/input");
            // Enter in image file name
            String defaultDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory().ToString()).ToString()).ToString()).ToString();
            element.SendKeys(defaultDirectory +"\\Data\\Test1\\doggie.jpeg");



            driver.FindElementByXPath("//div[@class='execute-wrapper']/button").Click();


            driver.Quit();
        }

        private void toggleEndpoint(IWebElement e, string operation)
        {
            action.MoveToElement(e);
            e.Click();
            // Confirm that div has updated to include the is-open attribute
            Assert.True("opblock opblock-"+operation+" is-open" == e.GetAttribute("class").ToString());
            e.Click();
            // Confirm that div has removed the is-open attribute
            Assert.True("opblock opblock-" + operation == e.GetAttribute("class").ToString());

        }

    }
}
