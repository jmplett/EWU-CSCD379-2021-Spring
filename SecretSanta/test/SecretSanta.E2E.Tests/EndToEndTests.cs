using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaywrightSharp;

namespace SecretSanta.E2E.Tests
{
    [TestClass]
    public class EndToEndTests
    {
        private static WebHostServerFixture<SecretSanta.Web.Startup, SecretSanta.Api.Startup> Server;
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            Server = new();
        }

        [TestMethod]
        public async Task LandHomepage()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
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
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
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
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
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
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
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

        [TestMethod]
        public async Task CreateGift(){
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost + "Gifts");

            Assert.IsTrue(response.Ok);

            await page.WaitForSelectorAsync("//html/body/section/section/section/a/section/div");

            var giftsBefore = await page.QuerySelectorAllAsync("//html/body/section/section/section/a/section/div");
            
            await page.ClickAsync("text=Create");

            await page.TypeAsync("input#Title", "The Best Most Awesome Gift Ever");
            await page.TypeAsync("input#Description", "You will definitely love this and everyone will be jealous");
            await page.TypeAsync("input#Priority", "2");
            await page.SelectOptionAsync("select#UserId", new string[]{ "2" });

            await page.ClickAsync("text=Create");

            var giftsAfter = await page.QuerySelectorAllAsync("//html/body/section/section/section/a/section/div");

            Assert.IsTrue(giftsBefore.Count() + 1 == giftsAfter.Count());
        }

        [TestMethod]
        public async Task ModifyGift()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var _newTitle = "Junk Title kjbn234nj234kjkj3ufwer";

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost + "Gifts");

            Assert.IsTrue(response.Ok);

            await page.WaitForSelectorAsync("//html/body/section/section/section/a/section/div");
            
            await page.ClickAsync("//html/body/section/section/section[last()]/a/section/div");

            IElementHandle elementHandle = await page.QuerySelectorAsync("input#Title");
            await elementHandle.FillAsync(_newTitle);

            await page.ClickAsync("text=Update");

            string titleAfterUpdate = await page.GetTextContentAsync("//html/body/section/section/section[last()]/a/section/div");

            Assert.AreEqual(_newTitle, titleAfterUpdate);
        }

        [TestMethod]
        public async Task DeleteGift()
        {
            var localhost = Server.WebRootUri.AbsoluteUri.Replace("127.0.0.1", "localhost");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            var response = await page.GoToAsync(localhost + "Gifts");

            Assert.IsTrue(response.Ok);

            await page.WaitForSelectorAsync("//html/body/section/section/section/a/section/div");
            
            var giftsBefore = await page.QuerySelectorAllAsync("//html/body/section/section/section/a/section/div");

            Assert.IsTrue(giftsBefore.Count() > 0, "No existing Gifts to delete");
            
            page.Dialog += (_, args) => args.Dialog.AcceptAsync();
            await page.ClickAsync("//html/body/section/section/section[last()]/a/section/form/button");

            var giftsAfter = await page.QuerySelectorAllAsync("//html/body/section/section/section/a/section/div");

            Assert.IsTrue(giftsBefore.Count() - 1 == giftsAfter.Count());
        }
    }
}