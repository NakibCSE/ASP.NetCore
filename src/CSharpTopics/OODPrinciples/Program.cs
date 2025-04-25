
using OODPrinciples.Composition;
using OODPrinciples.DIP;
using OODPrinciples.Interface;
using OODPrinciples.LSP;

Square square = new Square();
square.Width = 6;
square.Height = 9;

IEmailSender emailSender = new HtmlEmailSender();

Membership membership = new Membership(emailSender);

Electronics electronics = new Electronics();
electronics.Price = 2000;
electronics.Name = "Scanner";
electronics.GetPriceAfterDiscount(20);


void Start(IPowerSwtich item)
{

}

void Connect(IUSB item)
{
    // many codes

    item.Connect();
}