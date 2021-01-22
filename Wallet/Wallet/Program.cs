using BLL;
using DAL;
using DAL.Provider;
using PL;
namespace Wallet
{
    class Program
    {
        static void Main(string[] args)
        {
            VerifyInputService verifyInputService = new VerifyInputService();
            ReadUserInputService readUserInputService = new ReadUserInputService();
            GetInputService getInputService = new GetInputService(readUserInputService, verifyInputService);

            XmlProvider<Bill> billProvider = new XmlProvider<Bill>();
            DataContext<Bill> billContext = new DataContext<Bill>(billProvider, "bills.xml");
            ReadWriteService<Bill> billReadWrite = new ReadWriteService<Bill>(billContext);
            BillService billService = new BillService(billReadWrite);

            XmlProvider<string> stringProvider = new XmlProvider<string>();
            DataContext<string> stringContext = new DataContext<string>(stringProvider, "categories.xml");
            ReadWriteService<string> stringReadWrite = new ReadWriteService<string>(stringContext); 
            CategoryService categoryService = new CategoryService(stringReadWrite);

            MoneyEventService moneyEventService = new MoneyEventService(billService);

            Menu menu = new Menu(getInputService, billService, categoryService, moneyEventService);
            menu.Print();
        }
    }
}
