using Limilabs.Client.POP3;
using Limilabs.Mail;
using static System.Net.Mime.MediaTypeNames;

using (Pop3 pop3 = new Pop3())
{
    pop3.Connect("pop.yandex.ru");
    pop3.StartTLS();
    pop3.UseBestLogin("login", "password");
    foreach (string uid in pop3.GetAll())
    {
        IMail email = new MailBuilder()
        .CreateFromEml(pop3.GetMessageByUID(uid));
        Console.WriteLine(email.Subject);
        email.Attachments.Where(a => a.FileName.EndsWith(".zip") || a.FileName.EndsWith(".rar")).ToList().ForEach(mime => mime.Save("emails" + mime.SafeFileName));
    }
    pop3.Close();
}