using CreationalPattern.Builder;
using CreationalPattern.Singleton;
using System.Text;

Logger logger =  Logger.GetLogger();
Logger logger2 = Logger.GetLogger();

//Using our created builder
ItemBuilder itemBuilder = new ItemBuilder();
itemBuilder.SetValue3("Password");
itemBuilder.SetValue1("UserName");

Item item = itemBuilder.GetItem();

//Using built in builder
StringBuilder stringBuilder = new StringBuilder();
stringBuilder.AppendLine("Password");
stringBuilder.AppendLine("UserName");

string text = stringBuilder.ToString();