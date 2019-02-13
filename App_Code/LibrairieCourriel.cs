using System;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Description résumée de LibrairieCourriel
/// </summary>
public static class LibrairieCourriel
{
    private static SmtpClient smtpClient =
        new SmtpClient("smtp.gmail.com", 587) {
            EnableSsl = true,
            Credentials = new NetworkCredential("ppuces@gmail.com", "Secret!123")
        };

    public static bool envoyerCourriel(MailMessage message)
    {
        bool binOK = true;

        try
        {
            smtpClient.Send(message);
        }
        catch (Exception ex)
        {
            binOK = false;
        }

        return binOK;
    }

    public static bool envoyerCourriel(string expediteur, string destinataires, string objet, string corps)
    {
        bool binOK = true;

        try
        {
            smtpClient.Send(expediteur, destinataires, objet, corps);
        }
        catch (Exception)
        {
            binOK = false;
        }

        return binOK;
    }
}