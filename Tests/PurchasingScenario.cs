﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

public class PiguLtLoginTest
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
    }

    [Test, Order (1)]
    public void LoginToPiguLtWithValidCredentials()
    {
        driver.Navigate().GoToUrl("https://pigu.lt/lt/");
        System.Threading.Thread.Sleep(1000);
        IWebElement closeCookies = driver.FindElement(By.XPath("//*[@id='cookie_block']/div/div/div[2]/div[2]/button[3]"));
        closeCookies.Click();
        System.Threading.Thread.Sleep(1000);
        IWebElement loginButton = driver.FindElement(By.XPath("//*[@id='headeMenu']/li[1]/a/i"));
        Actions actions = new Actions(driver);
        actions.MoveToElement(loginButton);
        actions.Perform();
        IWebElement prisijungtiButton = driver.FindElement(By.XPath("//*[@id='headeMenu']/li[1]/div/div/div[2]/a[1]"));
        prisijungtiButton.Click();
        IWebElement usernameInput = driver.FindElement(By.XPath("//*[@id='loginModal']/div[1]/div[1]/form/div[3]/input"));
        IWebElement passwordInput = driver.FindElement(By.XPath("//*[@id='passwordCont']/input"));

        usernameInput.SendKeys("testastest40@gmail.com");
        passwordInput.SendKeys("testas");

        IWebElement loginSubmitButton = driver.FindElement(By.XPath("//*[@id='loginModal']/div[1]/div[1]/form/div[6]/input"));
        loginSubmitButton.Click();

        IWebElement loginButton2 = driver.FindElement(By.XPath("//*[@id='headeMenu']/li[1]/a/i"));
        actions.MoveToElement(loginButton2);
        actions.Perform();
        string expectedLogInSuccessed = "Sveiki, testastest40@gmail.com!";
        IWebElement logInResult = driver.FindElement(By.XPath("//*[@id='headeMenu']/li[1]/div/div/div/div[2]/p"));
        Assert.That(expectedLogInSuccessed, Is.EqualTo(logInResult.Text));
    }

    [Test, Order (2)]
    public void HamburgerMenuTesting()
    {
        driver.Navigate().GoToUrl("https://pigu.lt/lt/");
        System.Threading.Thread.Sleep(1000);
        IWebElement closeCookies = driver.FindElement(By.XPath("//*[@id='cookie_block']/div/div/div[2]/div[2]/button[3]"));
        closeCookies.Click();
        System.Threading.Thread.Sleep(2000);
        IWebElement hamburgerMenuIcon = driver.FindElement(By.XPath("//*[@id='menuBurger']"));
        Actions actions = new Actions(driver);
        actions.MoveToElement(hamburgerMenuIcon);
        actions.Perform();

        System.Threading.Thread.Sleep(1000);
        IWebElement kvepalaiKosmetikaCategory = driver.FindElement(By.XPath("//*[@id='department-82']/a/span"));
        kvepalaiKosmetikaCategory.Click();

        System.Threading.Thread.Sleep(5000);
        string expectedSearchResult = "Kvepalai";
        IWebElement searchResult = driver.FindElement(By.XPath("//*[@id='productsCategoryBranch']/div[2]/div/div/div[1]/a/p"));
        Assert.That(expectedSearchResult, Is.EqualTo(searchResult.Text));
    }

    [Test, Order (3)]
    public void AddKvepalaiMoterimsToCartPerSearch()
    {
        driver.Navigate().GoToUrl("https://pigu.lt/lt/");
        System.Threading.Thread.Sleep(1000);
        IWebElement closeCookies = driver.FindElement(By.XPath("//*[@id='cookie_block']/div/div/div[2]/div[2]/button[3]"));
        closeCookies.Click();
        System.Threading.Thread.Sleep(2000);
        IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
        searchInput.SendKeys("kvepalai moterims");
        searchInput.SendKeys(Keys.Enter);

        IWebElement firstResult = driver.FindElement(By.XPath("//*[@id='_0productBlock1039724']/div/div/div[3]/a/img"));
        firstResult.Click();

        System.Threading.Thread.Sleep(2000);
        IWebElement addToCartButton = driver.FindElement(By.XPath("//*[@id='product-sidebar']/div[1]/div[6]/div/div[1]"));
        addToCartButton.Click();

        System.Threading.Thread.Sleep(5000);
        string expectedAddedToCart = "Prekė įtraukta į krepšelį";
        IWebElement addedToCart = driver.FindElement(By.XPath("//*[@id='add-to-cart-modal-header']/div"));
        Assert.That(expectedAddedToCart, Is.EqualTo(addedToCart.Text));
    }

    [Test, Order (4)]
    public void ShoppingCartBuyButton()
    {
        driver.Navigate().GoToUrl("https://pigu.lt/lt/");
        System.Threading.Thread.Sleep(1000);
        IWebElement closeCookies = driver.FindElement(By.XPath("//*[@id='cookie_block']/div/div/div[2]/div[2]/button[3]"));
        closeCookies.Click();
        System.Threading.Thread.Sleep(2000);
        IWebElement loginButton = driver.FindElement(By.XPath("//*[@id='headeMenu']/li[1]/a/i"));
        Actions actions = new Actions(driver);
        actions.MoveToElement(loginButton);
        actions.Perform();
        IWebElement prisijungtiButton = driver.FindElement(By.XPath("//*[@id='headeMenu']/li[1]/div/div/div[2]/a[1]"));
        prisijungtiButton.Click();
        IWebElement usernameInput = driver.FindElement(By.XPath("//*[@id='loginModal']/div[1]/div[1]/form/div[3]/input"));
        IWebElement passwordInput = driver.FindElement(By.XPath("//*[@id='passwordCont']/input"));

        usernameInput.SendKeys("testastest40@gmail.com");
        passwordInput.SendKeys("testas");
        IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
        searchInput.SendKeys("kvepalai moterims");
        searchInput.SendKeys(Keys.Enter);

        IWebElement firstResult = driver.FindElement(By.XPath("//*[@id='_0productBlock1039724']/div/div/div[3]/a/img"));
        firstResult.Click();

        System.Threading.Thread.Sleep(2000);
        IWebElement addToCartButton = driver.FindElement(By.XPath("//*[@id='product-sidebar']/div[1]/div[6]/div/div[1]"));
        addToCartButton.Click();
        System.Threading.Thread.Sleep(2000);
        driver.FindElement(By.XPath("//*[@id='buy']")).Click(); //press buy button 
        System.Threading.Thread.Sleep(2000);
        string expectedResult = "VIENETO KAINA";
        IWebElement actualResult = driver.FindElement(By.ClassName("white-space_nowrap"));
        Assert.That(expectedResult, Is.EqualTo(actualResult.Text));
    }

    [Test, Order (5)]
    public void DeleteItemsFromShoppingCart()
    {
        driver.Navigate().GoToUrl("https://pigu.lt/lt/");
        System.Threading.Thread.Sleep(1000);
        IWebElement closeCookies = driver.FindElement(By.XPath("//*[@id='cookie_block']/div/div/div[2]/div[2]/button[3]"));
        closeCookies.Click();
        System.Threading.Thread.Sleep(2000);
        IWebElement loginButton = driver.FindElement(By.XPath("//*[@id='headeMenu']/li[1]/a/i"));
        Actions actions = new Actions(driver);
        actions.MoveToElement(loginButton);
        actions.Perform();
        IWebElement prisijungtiButton = driver.FindElement(By.XPath("//*[@id='headeMenu']/li[1]/div/div/div[2]/a[1]"));
        prisijungtiButton.Click();
        IWebElement usernameInput = driver.FindElement(By.XPath("//*[@id='loginModal']/div[1]/div[1]/form/div[3]/input"));
        IWebElement passwordInput = driver.FindElement(By.XPath("//*[@id='passwordCont']/input"));

        usernameInput.SendKeys("testastest40@gmail.com");
        passwordInput.SendKeys("testas");
        IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
        searchInput.SendKeys("kvepalai moterims");
        searchInput.SendKeys(Keys.Enter);

        IWebElement firstResult = driver.FindElement(By.XPath("//*[@id='_0productBlock1039724']/div/div/div[3]/a/img"));
        firstResult.Click();

        System.Threading.Thread.Sleep(2000);
        IWebElement addToCartButton = driver.FindElement(By.XPath("//*[@id='product-sidebar']/div[1]/div[6]/div/div[1]"));
        addToCartButton.Click();
        System.Threading.Thread.Sleep(2000);
        driver.FindElement(By.XPath("//*[@id='buy']")).Click(); //press buy button 
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='cartWidget']/a")));
        IWebElement deleteItemFromShoppingCart = driver.FindElement(By.ClassName("remove_item"));
        string expectedItemDeleted = "Peržiūrėti pasiūlymus";
        IWebElement deletedItem = driver.FindElement(By.ClassName("c-btn--secondary"));
        Assert.That(expectedItemDeleted, Is.EqualTo(deletedItem.Text));
    }


    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
