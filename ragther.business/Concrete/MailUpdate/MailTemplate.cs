using System;

namespace ragther.business.Concrete.MailUpdate
{
    public class MailTemplate
    {
        //this class only for development

        static string repass2 = "<!DOCTYPE html> <html lang=\"en\"> <head> <meta charset=\"UTF-8\"> <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css\" integrity=\"sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh\" crossorigin=\"anonymous\"> <link rel=\"icon\" type=\"image/x-icon\" href=\"favicon.ico\">  <link rel=\"stylesheet\" href=\"https://pro.fontawesome.com/releases/v5.2.0/css/all.css\"> <style> .title-background{{ background: -webkit-linear-gradient(215deg, rgba(0,255,243,1) 0%, rgba(255,0,85,1) 100%); }} .footer-background{{ background: linear-gradient(90deg, rgb(11, 8, 72) 0%, rgba(9,9,121,1) 35%, rgba(0,212,255,1) 100%); }} .btn-update{{ background: rgb(211,0,0); background: linear-gradient(90deg, rgb(81, 0, 211) 0%, rgba(131,0,225,1) 100%); }} .icon-background{{ background: -webkit-linear-gradient(135deg, rgba(0,255,243,1) 0%, rgba(255,0,85,1) 100%); -webkit-background-clip: text; -webkit-text-fill-color: transparent; padding: 5px; font-size: 26px; }} .radius-0 {{ border-radius: 0; }} </style> </head> <body> <div class=\"container pt-2\"> <div class=\"row justify-content-center\"> <div class=\"col-md-8\"> <div class=\"title-background p-2 mb-3\"> <h2 style=\"color:white; padding:10px 20px;\" class=\"text-center text-light font-weight-lighter\">Şifre Yenileme</h2> </div> <div class=\"container border border-top-0 border-bottom-0\"> <div class=\"p-2\"> <h5>Merhabalar {0}</h5> <p class=\"lead\">Şifreni unuttuğuna dair bir duyum aldık ve senin için yeni bir şifre ürettik. Artık aşağıda bulunan şifre ile sisteme giriş yapabilirsin. Dilersen daha sonra şifreni sistem içerisinden güncelleyebilirsin.</p> <hr> <div class=\"text-center pb-1\"> <h5 class=\"font-weight-light pb-2\">Yeni Şifren</h5> <p style=\"color:white; padding:5px 10px; width:80px;\" class=\"d-inline px-4 py-2 btn-update border-0 text-light w-25 mt-2 radius-0\">{1}</p> </div> <hr> <h6 class=\"mb-0\">Giriş yapma bağlantısı</h6> <span class=\"small\"><a href=\"http://localhost:4200/login\">http://localhost:4200/login</a></span> <hr> </div> </div> <div style=\"color:white; padding:10px 20px;\" class=\"text-center footer-background p-3 text-light mt-2\">Ragther Project By Ramazan Can GÖLGEN</div> </div> </div> </div> </body> </html>";

        static string mail2 = "<html lang=\"en\"> <head> <meta charset=\"UTF-8\"> <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css\" integrity=\"sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh\" crossorigin=\"anonymous\"> <link rel=\"icon\" type=\"image/x-icon\" href=\"favicon.ico\">  <link rel=\"stylesheet\" href=\"https://pro.fontawesome.com/releases/v5.2.0/css/all.css\"> <style> .title-background{{ background: -webkit-linear-gradient(215deg, rgba(0,255,243,1) 0%, rgba(255,0,85,1) 100%); }} .footer-background{{ background: linear-gradient(90deg, rgb(11, 8, 72) 0%, rgba(9,9,121,1) 35%, rgba(0,212,255,1) 100%); }} .btn-update{{ background: rgb(211,0,0); background: linear-gradient(90deg, rgb(81, 0, 211) 0%, rgba(131,0,225,1) 100%); }} .icon-background{{ background: -webkit-linear-gradient(135deg, rgba(0,255,243,1) 0%, rgba(255,0,85,1) 100%); -webkit-background-clip: text; -webkit-text-fill-color: transparent; padding: 5px; font-size: 26px; }} </style> </head> <body> <div class=\"container pt-2\"> <div class=\"row justify-content-center\"> <div class=\"col-md-8\"> <div class=\"title-background p-2 mb-3\"> <h2 style=\"color:white; padding:10px 20px;\">Mail Yenileme Talebi</h2> </div> <div class=\"container border border-top-0 border-bottom-0\"> <div class=\"p-2\"> <h5>Merhabalar {0}</h5> <p class=\"lead\">Mail güncelleme talebinde bulundunuz. Mail adresininiz onaylamanız haline aşağıda belirliten mail adresine güncellenecektir.</p> <hr> <div class=\"text-center pb-3\"> <h5 class=\"font-weight-light\">Butona Tıklayarak Mail Adresinizi Güncelleyebilirsiniz</h5> <a href=\" {1} \" class=\"btn-update\" style=\"padding:5px; color:white;\">Güncelle</a> </div> <h6>Güncellenecek Email Adresi: <span class=\"text-primary\"><a href=\"mailto: {2} \">{3}</a></span></h6> <hr> <div class=\"mt-2 small text-muted font-weight-lighter font-italic\">Eğer butona tıklandığı zaman bağlantı açılmıyor ise aşağıdaki bağlantı linkine tıklayınız yada kopyalayıp taraycınızın arama çubuğuna yapıştırın.</div> <h6 class=\"mb-0 pt-2\">Güncelleme Bağlantısı:</h6> <span class=\"small\"><a href=\" {4} \"> {5} </a></span> <hr> </div> </div> <div style=\"color:white; padding:10px 20px;\" class=\"text-center footer-background p-3 text-light mt-2\">Ragther Project By Ramazan Can GÖLGEN</div> </div> </div> </div> </body> </html>";

        public static string GetRepassMailBody(string userName, string newPass)
        {
            string formatted = String.Format(repass2, userName, newPass);
            return formatted.Replace("{{", "{").Replace("}}","}");
        }
        public static string GetEmailUpdateBody(string userName, string newEmail, string link)
        {
            return String.Format(mail2, userName, link, newEmail, newEmail, link, link);
        }

    }
}