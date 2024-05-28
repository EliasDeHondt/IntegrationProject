using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using QRCoder;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class QrController : Controller
{
    [HttpPost]
    [Authorize]
    public IActionResult Post(QrCodeModel model)
    {
        try
        {
            var payload = new PayloadGenerator.Url(model.Text);
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(payload);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            var base64 = qrCode.GetGraphic(20);
            return Ok(base64);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}