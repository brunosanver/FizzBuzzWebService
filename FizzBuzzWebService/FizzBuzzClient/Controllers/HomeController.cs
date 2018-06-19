using FizzBuzzClient.Models;
using FizzBuzzClient.WCFFizzBuzzServiceReference;
using System;
using System.ServiceModel;
using System.Web.Mvc;

namespace FizzBuzzClient.Controllers
{
    public class HomeController : Controller
    {
        WCFFizzBuzzServiceClient client = new WCFFizzBuzzServiceClient();
        public ActionResult Index()
        {
            ViewBag.Welcome = "Welcome,";
            ViewBag.Message = "let's play the fizz-buzz game!";
            ViewBag.Instruction = "Remember, number must be an integer less than or equal to 100 ...";
            ViewBag.Warning = TempData["Warning"];

            return View();
        }

        public ActionResult ShowNumbers()
        {
            try
            {
                string input = Request.Form["StartNumber"];

                if (Int32.TryParse(input, out int num))
                {
                    string[] sequence = client.GetData(num);
                    ViewBag.Message = sequence;
                    client.Close();

                    return View();
                }
                else if (string.IsNullOrWhiteSpace(input))
                {
                    throw new ArgumentNullOrWhiteSpaceException(
                        "Input value is null, empty or white spaces.");
                }
                else
                {
                    throw new ArgumentNotAnIntegerException();
                }
            }            
            catch (FaultException e)
            {
                client.Abort();

                TempData["Warning"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (CommunicationException e)
            {
                client.Abort();

                TempData["Warning"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (TimeoutException e) 
            {
                client.Abort();

                TempData["Warning"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                client.Abort();

                TempData["Warning"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}