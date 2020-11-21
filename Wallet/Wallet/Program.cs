using PL;
using BLL;
using DAL;
using DAL.Provider;
namespace Wallet
{
    class Program
    {
        static void Main(string[] args)
        {
            VerifyInputService verifyInputService = new VerifyInputService();
            ReadUserInputService readUserInputService = new ReadUserInputService(); 
            GetInputService getInputService = new GetInputService(readUserInputService, verifyInputService);

            XmlProvider<Bill> xmlProvider = new XmlProvider<Bill>();
            BillContext billContext = new BillContext(xmlProvider, "data.txt");
            ReadWriteService readWriteService = new ReadWriteService(billContext);
            BillService billService = new BillService(readWriteService);
            BillBusinessHandler billBusinessHandler = new BillBusinessHandler(getInputService, billService);

            CategoryService categoryService = new CategoryService();
            CategoryBusinessHandler categoryBusinessHandler = new CategoryBusinessHandler(getInputService, billService, categoryService);

            MoneyEventService moneyEventService = new MoneyEventService();
            MoneyEventHandler moneyEventHandler = new MoneyEventHandler(getInputService, billService, categoryService, moneyEventService);

            Menu menu = new Menu(getInputService, billBusinessHandler, categoryBusinessHandler, moneyEventHandler);
            menu.Print();
        }
    }
}
