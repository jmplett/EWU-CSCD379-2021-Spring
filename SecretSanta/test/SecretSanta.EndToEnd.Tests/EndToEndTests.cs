using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightSharp;

namespace SecretSanta.EndToEndTests
{
    [TestClass]
    public class EndToEndTests
    {
        private static WebHostServerFixture<Web.Startup, Api.Startup> Server;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext) {
            Server = new();
        }

        [TestMethod]
        public async Task LandOnHomepage()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Firefox.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            var headerContent = await page.GetTextContentAsync("//html/body/header/div/a");
            Assert.AreEqual("Secret Santa", headerContent);
        }

        [TestMethod]
        public async Task NavigateToUsers()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Firefox.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await Task.WhenAll(
                Task.Run(
                    async () => { response = await page.WaitForNavigationAsync(); }), 
                page.ClickAsync("text=Users"));

            Assert.IsTrue(response.Ok);
        }

        [TestMethod]
        public async Task NavigateToGroups()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Firefox.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await Task.WhenAll(
                Task.Run(
                    async () => { response = await page.WaitForNavigationAsync(); }), 
                page.ClickAsync("text=Groups"));

            Assert.IsTrue(response.Ok);
        }

        [TestMethod]
        public async Task NavigateToGifts()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Firefox.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost);

            Assert.IsTrue(response.Ok);

            await Task.WhenAll(
                Task.Run(
                    async () => { response = await page.WaitForNavigationAsync(); }), 
                page.ClickAsync("text=Gifts"));

            Assert.IsTrue(response.Ok);
        }
    }
}